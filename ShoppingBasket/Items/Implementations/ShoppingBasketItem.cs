using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class ShoppingBasketItem : IShoppingBasketItem
    {
        private int quantity;
        public int Quantity 
        { 
            get => quantity; 
            set
            {
                quantity = value;
                Updated?.Invoke(this, new ShoppingUpdatedEventArgs(this));
            } 
        }

        public long Id { get; }
        public string Name { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }

        public decimal SubTotal { get => UnitPrice * Quantity; }
        public decimal Tax { get; }
        public decimal Total { get => SubTotal + Tax; }
        public decimal UnitPrice { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasketItem(long id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            Quantity = 0;
            UnitPrice = unitPrice;
        }
    }
}

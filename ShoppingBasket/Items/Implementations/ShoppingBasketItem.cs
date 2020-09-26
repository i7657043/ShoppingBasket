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
        public decimal Tax { get => CalculateTax(); }
        public decimal Total { get => SubTotal + Tax; }
        public decimal UnitPrice { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasketItem(long id, string name, decimal unitPrice, IEnumerable<ITaxRule> taxRules)
        {
            Id = id;
            Name = name;
            Quantity = 0;
            UnitPrice = unitPrice;
            TaxRules = taxRules;
        }

        private decimal CalculateTax()
        {
            decimal tax = 0;

            foreach (ITaxRule taxRule in TaxRules)
                tax += taxRule.CalculateTax(null, this); //null doesn't seem ideal here

            return tax;
        }
    }
}

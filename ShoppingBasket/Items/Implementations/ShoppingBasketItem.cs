using ShoppingBasketChallenge.Discounts;
using ShoppingBasketChallenge.Totals;
using ShoppingBasketChallenge.Updated;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasketChallenge.Items
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
        public IEnumerable<IDiscountRule> DiscountRules { get; }
        public decimal SubTotal { get => UnitPrice * Quantity; }
        public decimal Tax { get { return TaxRules == null ? 0 : TaxRules.Sum(x => x.CalculateTax(null, this)); } }
        public decimal Total { get => (SubTotal + Tax) - Discount; }
        public decimal Discount { get { return DiscountRules == null ? 0 : DiscountRules.Sum(x => x.CalculateDiscount(this)); } }
        public decimal UnitPrice { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasketItem(long id, string name, decimal unitPrice, IEnumerable<ITaxRule> taxRules, IEnumerable<IDiscountRule> discountRules, int quantity = 1)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            TaxRules = taxRules;
            DiscountRules = discountRules;
            Quantity = quantity;
        }
    }
}

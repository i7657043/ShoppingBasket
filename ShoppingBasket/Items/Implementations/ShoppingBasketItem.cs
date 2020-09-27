using ShoppingBasketChallenge.Discounts;
using ShoppingBasketChallenge.Totals;
using ShoppingBasketChallenge.Updated;
using System;
using System.Collections.Generic;

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
        public decimal Tax { get => CalculateTax(); }
        public decimal Total { get => (SubTotal + Tax) - Discount; }
        public decimal Discount { get => CalculateDiscount(); }
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

        private decimal CalculateTax()
        {
            decimal tax = 0;

            if (TaxRules != null)
                foreach (ITaxRule taxRule in TaxRules)
                    tax += taxRule.CalculateTax(null, this); //null doesn't seem ideal here

            return tax;
        }

        private decimal CalculateDiscount()
        {
            decimal discount = 0;

            if (DiscountRules != null)
                foreach (IDiscountRule discountRule in DiscountRules)
                    discount += discountRule.CalculateDiscount(this);

            return discount;
        }
    }
}

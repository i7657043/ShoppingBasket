using ShoppingBasketChallenge.Discounts;
using ShoppingBasketChallenge.Totals;
using System;
using System.Collections.Generic;

namespace ShoppingBasketChallenge.Items
{
    public class ShoppingItem : IShoppingItem
    {
        public long Id { get; }
        public string Name { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }
        public IEnumerable<IDiscountRule> DiscountRules { get; }
        public decimal UnitPrice { get; set; }

        public ShoppingItem(long id, string name, decimal unitPrice, IEnumerable<ITaxRule> taxRules = null, IEnumerable<IDiscountRule> discountRules = null)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            TaxRules = taxRules;
            DiscountRules = discountRules;
        }
    }
}

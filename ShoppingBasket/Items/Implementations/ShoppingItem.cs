using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class ShoppingItem : IShoppingItem
    {
        public long Id { get; }
        public string Name { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }
        public decimal UnitPrice { get; set; }

        public ShoppingItem(long id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}

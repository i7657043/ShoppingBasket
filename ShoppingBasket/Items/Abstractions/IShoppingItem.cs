using System.Collections.Generic;

namespace ShoppingBasket
{
    public interface IShoppingItem
    {
        long Id { get; }
        string Name { get; }
        IEnumerable<ITaxRule> TaxRules { get; }
        IEnumerable<IDiscountRule> DiscountRules { get; }
        public decimal UnitPrice { get; }
    }
}

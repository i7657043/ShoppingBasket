using ShoppingBasketChallenge.Discounts;
using ShoppingBasketChallenge.Totals;
using System.Collections.Generic;

namespace ShoppingBasketChallenge.Items
{
    public interface IShoppingItem
    {
        long Id { get; }
        string Name { get; }
        IEnumerable<ITaxRule> TaxRules { get; }
        IEnumerable<IDiscountRule> DiscountRules { get; }
        decimal UnitPrice { get; }
    }
}

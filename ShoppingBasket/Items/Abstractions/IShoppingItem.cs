using System.Collections.Generic;

namespace ShoppingBasket
{
    public interface IShoppingItem
    {
        long Id { get; }
        string Name { get; }
        IEnumerable<ITaxRule> TaxRules { get; }
    }
}

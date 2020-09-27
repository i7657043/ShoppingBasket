using ShoppingBasketChallenge.Totals;
using ShoppingBasketChallenge.Updated;

namespace ShoppingBasketChallenge.Items
{
    public interface IShoppingBasketItem : IShoppingItem, ITotals, IUpdated
    {
        int Quantity { get; set; }
    }
}

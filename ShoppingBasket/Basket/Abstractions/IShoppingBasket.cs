using ShoppingBasketChallenge.Items;
using ShoppingBasketChallenge.Totals;
using ShoppingBasketChallenge.Updated;
using System.Collections.Generic;

namespace ShoppingBasketChallenge.Basket
{
    public interface IShoppingBasket : ITotals, IUpdated
    {
        IShoppingBasketItem AddItem(IShoppingItem item);
        IShoppingBasketItem AddItem(IShoppingItem item, int quantity);
        IShoppingBasketItem RemoveItem(IShoppingBasketItem item);
        IEnumerable<IShoppingBasketItem> Items { get; }
    }
}

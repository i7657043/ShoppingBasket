using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket
{
    public static class ShoppingBasketExtensions
    {
        public static IShoppingBasketItem AddItemToBasket(this List<IShoppingBasketItem> items, IShoppingItem item)
        {
            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);
            if (basketItem == null)
            {
                basketItem = new ShoppingBasketItem(item.Id, item.Name);
                items.Add(basketItem);
            }

            return basketItem;
        }

        public static IShoppingBasketItem RemoveItemFromBasket(this List<IShoppingBasketItem> items, IShoppingItem item)
        {
            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);

            if (basketItem == null)
                return null;

            if (--basketItem.Quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(basketItem.Quantity), "The quantity of any Shopping-Basket Item cannot be 0");

            return basketItem;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket
{
    public static class ShoppingBasketExtensions
    {
        public static IShoppingBasketItem AddItemToBasket(this List<IShoppingBasketItem> items, IShoppingItem item, int quantity = 1)
        {
            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);

            if (basketItem == null)
                items.Add(new ShoppingBasketItem(item.Id, item.Name, quantity));
            else
                basketItem.Quantity += quantity;

            return basketItem ?? items.FirstOrDefault(x => x.Id == item.Id);
        }

        public static IShoppingBasketItem RemoveItemFromBasket(this List<IShoppingBasketItem> items, IShoppingItem item)
        {
            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);

            if (basketItem == null)
                return null;

            //This stops the quantity actually hitting 0
            if (basketItem.Quantity == 1/*--basketItem.Quantity < 1*/)
                throw new ArgumentOutOfRangeException(nameof(basketItem.Quantity), "The quantity of any Shopping-Basket Item cannot be 0");

            return basketItem;
        }
    }
}

using ShoppingBasketChallenge.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasketChallenge.Extensions
{
    public static class ShoppingBasketExtensions
    {
        public static IShoppingBasketItem AddItemTobasket(this List<IShoppingBasketItem> items, IShoppingItem item, int quantity = 1)
        {
            IShoppingBasketItem basketItem = new ShoppingBasketItem(item.Id, item.Name, item.UnitPrice, item.TaxRules, item.DiscountRules, quantity);

            items.Add(basketItem);

            return basketItem;
        }

        public static IShoppingBasketItem RemoveItemFromBasket(this List<IShoppingBasketItem> items, IShoppingItem item)
        {
            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);

            if (basketItem == null)
                return null;

            if ((basketItem.Quantity - 1) < 0)
                throw new ArgumentOutOfRangeException(nameof(basketItem.Quantity), "The quantity of any Shopping-Basket Item cannot be less than 0");

            basketItem.Quantity--;

            return basketItem;
        }
    }
}

using ShoppingBasketChallenge.Items;
using System;

namespace ShoppingBasketChallenge.Updated
{
    public class ShoppingUpdatedEventArgs : EventArgs
    {
        public IShoppingBasketItem BasketItem { get; set; }
        public ShoppingUpdatedEventType EventType { get; set; }

        public ShoppingUpdatedEventArgs(IShoppingBasketItem basketItem)
        {
            BasketItem = basketItem;
        }
    }
}

using System;

namespace ShoppingBasket
{
    public class ShoppingUpdatedEventArgs : EventArgs
    {
        public IShoppingBasketItem BasketItem { get; set; }
        public ShoppingUpdatedEventType EventType { get; set; }
    }

    public enum ShoppingUpdatedEventType
    {
        Add = 1, Remove = 2
    }
}

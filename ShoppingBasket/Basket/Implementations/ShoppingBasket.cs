using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class ShoppingBasket : IShoppingBasket
    {
        private readonly IAlertService _alertService;

        private List<IShoppingBasketItem> items { get; set; } = new List<IShoppingBasketItem>();

        public IEnumerable<IShoppingBasketItem> Items { get => items; }
        public decimal SubTotal { get; }
        public decimal Tax { get; }
        public decimal Total { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasket(IAlertService alertService)
        {
            _alertService = alertService;

            Updated += _alertService.OnBasketUpdated;
        }

        void TriggerBasketUpdate(IShoppingBasketItem item, ShoppingUpdatedEventType eventType)
        {
            OnBasketUpdated(new ShoppingUpdatedEventArgs(item) { EventType = eventType });
        }

        protected virtual void OnBasketUpdated(ShoppingUpdatedEventArgs e)
        {
            Updated?.Invoke(this, e);
        }

        public IShoppingBasketItem AddItem(IShoppingItem item)
        {
            return AddItem(item, 1);
        }        

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity = 1)
        {
            IShoppingBasketItem basketItem = items.AddItemToBasket(item);

            basketItem.Updated += _alertService.OnItemUpdated;

            basketItem.Quantity += quantity;

            TriggerBasketUpdate(basketItem, ShoppingUpdatedEventType.Add);

            return basketItem;
        }

        public IShoppingBasketItem RemoveItem(IShoppingBasketItem item)
        {
            IShoppingBasketItem basketItem = items.RemoveItemFromBasket(item);

            TriggerBasketUpdate(basketItem, ShoppingUpdatedEventType.Remove);

            return basketItem;
        }
    }
}

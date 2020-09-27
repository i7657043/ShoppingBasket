using ShoppingBasketChallenge.Alerts;
using ShoppingBasketChallenge.Items;
using ShoppingBasketChallenge.Updated;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasketChallenge.Basket
{
    public class ShoppingBasket : IShoppingBasket
    {
        private readonly IAlertService _alertService;

        private List<IShoppingBasketItem> items { get; set; } = new List<IShoppingBasketItem>();
        public IEnumerable<IShoppingBasketItem> Items { get => items; }
        public decimal SubTotal { get => items.Sum(x => x.SubTotal); }
        public decimal Tax { get => items.Sum(x => x.Tax); }
        public decimal Total { get => (SubTotal + Tax) - Discount; }
        public decimal Discount { get => items.Sum(x => x.Discount); }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasket(IAlertService alertService)
        {
            _alertService = alertService;

            Updated += _alertService.OnBasketUpdated;
        }

        public IShoppingBasketItem AddItem(IShoppingItem item)
        {
            return AddItem(item, 1);
        }        

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity = 1)
        {
            ValidateAddQuantity(quantity);

            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);
            if (basketItem == null)
            {
                basketItem = new ShoppingBasketItem(item.Id, item.Name, item.UnitPrice, item.TaxRules, item.DiscountRules, quantity);

                basketItem.Updated += _alertService.OnItemUpdated;

                items.Add(basketItem);
            }

            basketItem.Quantity += quantity;

            OnBasketUpdated(new ShoppingUpdatedEventArgs(basketItem) { EventType = ShoppingUpdatedEventType.Add });

            return basketItem;
        }

        public IShoppingBasketItem RemoveItem(IShoppingBasketItem item)
        {
            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);

            ValidateItemFound(basketItem);
            ValidateRemoveQuantity(basketItem.Quantity);

            basketItem.Quantity--;

            OnBasketUpdated(new ShoppingUpdatedEventArgs(basketItem) { EventType = ShoppingUpdatedEventType.Remove });

            return basketItem;
        }

        protected virtual void OnBasketUpdated(ShoppingUpdatedEventArgs e)
        {
            Updated?.Invoke(this, e);
        }

        private static void ValidateAddQuantity(int quantity)
        {
            if (quantity == 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Cannot add an Item with a count of 0");
        }

        private static void ValidateRemoveQuantity(int quantity)
        {
            if ((quantity - 1) < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "The quantity of any Shopping-Basket Item cannot be 0 or less");
        }
        private static void ValidateItemFound(IShoppingBasketItem basketItem)
        {
            if (basketItem == null)
                throw new KeyNotFoundException("The Item could not be found in the Basket");
        }
    }
}

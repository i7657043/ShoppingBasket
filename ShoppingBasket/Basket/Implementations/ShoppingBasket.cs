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

        public decimal SubTotal { get => items.Sum(x => x.SubTotal); }
        public decimal Tax { get => items.Sum(x => x.Tax); /* CalculateTax();*/ }
        public decimal Total { get => SubTotal + Tax; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        private decimal CalculateTax()
        {
            decimal tax = 0;

            foreach (IShoppingBasketItem item in Items)
                foreach (ITaxRule taxRule in item.TaxRules)
                    tax += taxRule.CalculateTax(this, item);

            return tax;
        }

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
            ValidateQuantity(quantity);

            IShoppingBasketItem basketItem = items.FirstOrDefault(x => x.Id == item.Id);
            if (basketItem == null)
            {
                basketItem = items.AddItemTobasket(item, quantity);

                basketItem.Updated += _alertService.OnItemUpdated;
            }
            else
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

        private static void ValidateQuantity(int quantity)
        {
            if (quantity == 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "The quantity of any Shopping-Basket Item cannot be less than 0");
        }
    }
}

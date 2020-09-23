using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class ShoppingBasketItem : IShoppingBasketItem
    {
        public int Quantity { get; set; }

        public long Id { get; }
        public string Name { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }

        public decimal SubTotal { get; }
        public decimal Tax { get; }
        public decimal Total { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasketItem(long id, string name, int quantity = 1)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        void TriggerShoppingBasketItemUpdate(IShoppingItem item)
        {
            //Map item to Event Args

            //Do something before event

            OnShoppingBasketItemUpdated(new ShoppingUpdatedEventArgs());

            //Do something after event
        }

        protected virtual void OnShoppingBasketItemUpdated(ShoppingUpdatedEventArgs e)
        {
            Updated?.Invoke(this, e);
        }
    }


}

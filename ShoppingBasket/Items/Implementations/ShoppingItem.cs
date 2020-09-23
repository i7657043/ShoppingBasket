using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class ShoppingItem : IShoppingItem
    {
        public long Id { get; }
        public string Name { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingItem(long id, string name)
        {
            Id = id;
            Name = name;
        }

        void TriggerShoppingItemUpdate(IShoppingItem item)
        {
            //Map item to Event Args

            //Do something before event

            OnShoppingItemUpdated(new ShoppingUpdatedEventArgs());

            //Do something after event
        }

        protected virtual void OnShoppingItemUpdated(ShoppingUpdatedEventArgs e)
        {
            Updated?.Invoke(this, e);
        }
    }
}

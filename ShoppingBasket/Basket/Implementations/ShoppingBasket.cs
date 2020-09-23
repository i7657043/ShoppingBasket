using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class ShoppingBasket : IShoppingBasket
    {
        public IEnumerable<IShoppingBasketItem> Items { get => _items; }
        private List<IShoppingBasketItem> _items { get; set; } = new List<IShoppingBasketItem>();

        public decimal SubTotal { get; }
        public decimal Tax { get; }
        public decimal Total { get; }

        //Other classes just need to subscribe to this event
        //and implement what they want to happen when they get the event
        //as all this class is doing is triggering it
        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public ShoppingBasket()
        {

        }

        public void OnItemUpdate(object sender, ShoppingUpdatedEventArgs e)
        {
            Console.WriteLine($"Item level Alert");
        }

        void TriggerBasketUpdate(IShoppingItem item)
        {
            //Map item to Event Args

            //Do something before event

            //IShoppingBasketItem bi = item as IShoppingBasketItem;
            //bi.Updated += OnItemUpdate;

            OnBasketUpdated(new ShoppingUpdatedEventArgs());

            //Do something after event
        }

        protected virtual void OnBasketUpdated(ShoppingUpdatedEventArgs e)
        {
            Updated?.Invoke(this, e);
        }

        public IShoppingBasketItem AddItem(IShoppingItem item)
        {
            //this.TriggerBasketUpdate(item);

            return _items.AddItemToBasket(item);
        }        

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity = 1)
        {
            return _items.AddItemToBasket(item, quantity);
        }

        public IShoppingBasketItem RemoveItem(IShoppingBasketItem item)
        {
            return _items.RemoveItemFromBasket(item);
        }
    }
}

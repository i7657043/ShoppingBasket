using System;

namespace ShoppingBasket
{
    public class AlertService : IAlertService
    {
        public AlertService()
        {

        }

        public void OnBasketUpdated(object sender, ShoppingUpdatedEventArgs e)
        {
            //Could have used:
            //Console.WriteLine($"Item: {e.ItemId}-[{e.ItemName}] " +
            //$"{(e.EventType == ShoppingUpdatedEventType.Add ? "Added to" : "Removed from")} " +
            //$"Basket");
            //but if more types are added obviously the ternary operator wont be suitable

            switch (e.EventType)
            {
                case ShoppingUpdatedEventType.Add:
                    Console.WriteLine($"Item: {e.BasketItem.Id}-[{e.BasketItem.Name}] Added to Basket");
                    break;

                case ShoppingUpdatedEventType.Remove:
                    Console.WriteLine($"Item: {e.BasketItem.Id}-[{e.BasketItem.Name}] Removed from Basket");
                    break;
            }
        }

        public void OnItemUpdated(object sender, ShoppingUpdatedEventArgs e)
        {
            Console.WriteLine($"Item: {e.BasketItem.Id}-[{e.BasketItem.Name}] Quantity changed to: {e.BasketItem.Quantity}");
        }
    }
}

using Newtonsoft.Json;
using System;

namespace ShoppingBasket
{
    public class AlertService : IAlertService
    {
        public void OnBasketUpdated(object sender, ShoppingUpdatedEventArgs e)
        {
            //Could have used:
            //Console.WriteLine($"Item: {e.ItemId}-[{e.ItemName}] " +
            //$"{(e.EventType == ShoppingUpdatedEventType.Add ? "Added to" : "Removed from")} " +
            //$"Basket");
            //but if more options are added obviously the ternary operator wont be suitable

            switch (e.EventType)
            {
                case ShoppingUpdatedEventType.Add:
                    Console.WriteLine($"Item Added to Basket: {JsonConvert.SerializeObject(e.BasketItem, Formatting.Indented)}\n");
                    break;

                case ShoppingUpdatedEventType.Remove:
                    Console.WriteLine($"Item Removed from Basket: {JsonConvert.SerializeObject(e.BasketItem, Formatting.Indented)}\n");
                    break;
            }
        }

        public void OnItemUpdated(object sender, ShoppingUpdatedEventArgs e)
        {
            Console.WriteLine($"{e.BasketItem.Quantity} of the following Item: {e.BasketItem.Id}-[{e.BasketItem.Name}] is being updated\n");
        }
    }
}

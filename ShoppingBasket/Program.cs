using System;

namespace ShoppingBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started Shopping Basket Challenge...\n");
            
            IShoppingBasket basket = new ShoppingBasket();

            IShoppingBasketItem item = basket.AddItem(new ShoppingItem(1001, "Ham"));
            item = basket.AddItem(new ShoppingItem(1001, "Ham"));

            item = basket.RemoveItem(item);
            item = basket.RemoveItem(item);

            Console.WriteLine("Finished Shopping Basket Challenge");
        }
    }
}

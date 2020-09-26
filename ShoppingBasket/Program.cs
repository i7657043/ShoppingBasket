using System;

namespace ShoppingBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started Shopping Basket Challenge...\n");

            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            IShoppingBasketItem hamItem = basket.AddItem(new ShoppingItem(1001, "Ham"));
            hamItem = basket.AddItem(new ShoppingItem(1001, "Ham"));
            IShoppingBasketItem fishItem = basket.AddItem(new ShoppingItem(1002, "Fish"));

            hamItem = basket.RemoveItem(hamItem);
            fishItem = basket.RemoveItem(fishItem);

            Console.WriteLine("Finished Shopping Basket Challenge");
        }
    }
}

using System;

namespace ShoppingBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started Shopping Basket Challenge...\n");

            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            IShoppingItem hamItem = new ShoppingItem(1001, "Ham");
            IShoppingItem fishItem = new ShoppingItem(1002, "Fish");

            IShoppingBasketItem hamBasketItem = basket.AddItem(hamItem);
            hamItem = basket.AddItem(hamItem);
            IShoppingBasketItem fishBasketItem = basket.AddItem(fishItem);

            hamItem = basket.RemoveItem(hamBasketItem);
            fishItem = basket.RemoveItem(fishBasketItem);

            Console.WriteLine("Finished Shopping Basket Challenge");
        }
    }
}

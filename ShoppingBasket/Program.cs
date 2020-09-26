using System;

namespace ShoppingBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started Shopping Basket Challenge...\n");

            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            IShoppingItem hamItem = new ShoppingItem(1001, "Ham", 1);
            IShoppingItem fishItem = new ShoppingItem(1002, "Fish", 2);

            IShoppingBasketItem hamBasketItem = basket.AddItem(hamItem);
            basket.AddItem(hamItem, 2);

            IShoppingBasketItem fishBasketItem = basket.AddItem(fishItem);

            basket.RemoveItem(hamBasketItem);
            basket.RemoveItem(fishBasketItem);

            Console.WriteLine("Finished Shopping Basket Challenge");
        }
    }
}

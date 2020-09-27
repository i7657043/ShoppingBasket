using ShoppingBasketChallenge.Basket;
using ShoppingBasketChallenge.Items;

namespace ShoppingBasketChallenge.Totals
{
    public interface ITaxRule
    {
        decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item);
    }
}

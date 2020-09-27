using ShoppingBasketChallenge.Basket;
using ShoppingBasketChallenge.Items;

namespace ShoppingBasketChallenge.Totals
{
    public class VatAs20PercentTaxRule : ITaxRule
    {
        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            return item.SubTotal * 0.20M; //Add 20%
        }
    }
}
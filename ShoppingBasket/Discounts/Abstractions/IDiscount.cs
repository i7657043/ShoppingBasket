using ShoppingBasketChallenge.Items;

namespace ShoppingBasketChallenge.Discounts
{
    public interface IDiscountRule
    {
        decimal CalculateDiscount(IShoppingBasketItem basketItem);
    }
}

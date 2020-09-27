namespace ShoppingBasket
{
    public interface IDiscountRule
    {
        decimal CalculateDiscount(IShoppingBasketItem basketItem);
    }
}

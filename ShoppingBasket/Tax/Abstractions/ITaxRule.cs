namespace ShoppingBasket
{
    public interface ITaxRule
    {
        decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item);
    }
}

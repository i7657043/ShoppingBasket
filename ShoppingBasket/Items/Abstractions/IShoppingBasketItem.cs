namespace ShoppingBasket
{
    public interface IShoppingBasketItem : IShoppingItem, ITotals, IUpdated
    {
        int Quantity { get; set; }
    }
}

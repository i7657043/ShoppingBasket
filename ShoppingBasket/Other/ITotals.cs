namespace ShoppingBasket
{
    public interface ITotals
    {
        decimal SubTotal { get; }
        decimal Tax { get; }
        decimal Total { get; }
        decimal Discount { get; }
    }
}

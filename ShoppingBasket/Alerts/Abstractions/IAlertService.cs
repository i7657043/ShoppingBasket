namespace ShoppingBasket
{
    public interface IAlertService
    {
        void OnBasketUpdated(object sender, ShoppingUpdatedEventArgs e);
        void OnItemUpdated(object sender, ShoppingUpdatedEventArgs e);
    }
}

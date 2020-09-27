using ShoppingBasketChallenge.Updated;

namespace ShoppingBasketChallenge.Alerts
{
    public interface IAlertService
    {
        void OnBasketUpdated(object sender, ShoppingUpdatedEventArgs e);
        void OnItemUpdated(object sender, ShoppingUpdatedEventArgs e);
    }
}

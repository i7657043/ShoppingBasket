using System;

namespace ShoppingBasket
{
    public interface IUpdated
    {
        event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
}

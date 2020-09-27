using ShoppingBasketChallenge.Basket;
using System;

namespace ShoppingBasketChallenge.Updated
{
    public interface IUpdated
    {
        event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
}

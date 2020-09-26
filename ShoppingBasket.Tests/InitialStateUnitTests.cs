using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class InitialStateUnitTests
    {
        [Fact]
        public void A_Newly_Constrcuted_Basket_Should_Be_Created_As_Empty()
        {
            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            List<IShoppingBasketItem> basketItems = (List<IShoppingBasketItem>) basket.Items;

            basketItems.Count.Should().Be(0);
            //basketItems.Should().Contain(x => x.Total == 0);
        }
    }
}
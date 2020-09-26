using FluentAssertions;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class InitialStateUnitTests : IClassFixture<IocFixture>
    {
        private readonly IShoppingBasket _basket;

        public InitialStateUnitTests(IocFixture fixture)
        {
            _basket = fixture.ServiceProvider.GetService<IShoppingBasket>();
        }

        [Fact]
        public void A_Newly_Constrcuted_Basket_Should_Be_Created_As_Empty()
        {
            //Arrange
            List<IShoppingBasketItem> basketItems = (List<IShoppingBasketItem>) _basket.Items;

            //Act and Assert
            basketItems.Count.Should().Be(0);
            _basket.SubTotal.Should().Be(0);
            _basket.Total.Should().Be(0);
        }
    }
}
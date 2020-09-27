using FluentAssertions;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ShoppingBasketChallenge.Items;
using ShoppingBasketChallenge.Basket;

namespace ShoppingBasketChallenge.Tests
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

            //Act
            //In this case the "Act" of constructing a Basket has been done in the constructor
            
            //Assert
            basketItems.Count.Should().Be(0);
            _basket.SubTotal.Should().Be(0);
            _basket.Total.Should().Be(0);
        }
    }
}
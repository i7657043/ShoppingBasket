using FluentAssertions;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class TotalsUnitTests : IClassFixture<IocFixture>
    {
        private readonly IShoppingBasket _basket;

        public TotalsUnitTests(IocFixture fixture)
        {
            _basket = fixture.ServiceProvider.GetService<IShoppingBasket>();
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_A_Single_Item_To_An_Empty_Basket_Both_The_Subtotal_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            //basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_A_Single_Item_With_A_NoTax_rule_To_An_Empty_Basket_Both_The_Tax_Of_The_Item_And_The_Basket_Should_Equal_0_And_Both_The_Subtotal_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            //basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_Two_Items_With_Different_Quantities_Both_With_A_NoTax_Rule_To_An_Empty_Basket_Both_The_Tax_Of_The_Item_And_The_Basket_Should_Equal_0_And_Both_The_Subtotal_And_The_Total_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            //basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);
        }
    }
}
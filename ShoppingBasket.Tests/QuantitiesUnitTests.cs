using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasketChallenge.Basket;
using ShoppingBasketChallenge.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasketChallenge.Tests
{
    public class QuantitiesUnitTests : IClassFixture<IocFixture>
    {
        private readonly IShoppingBasket _basket;

        public QuantitiesUnitTests(IocFixture fixture)
        {
            _basket = fixture.ServiceProvider.GetService<IShoppingBasket>();
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void Adding_An_Item_Without_An_Explicit_Quantity_Results_In_A_Quantity_Of_1_For_The_Item(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);            
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_A_Single_Item_To_An_Empty_Basket_Both_The_Basket_And_Item_Quantity_Are_1(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);
            basketItems.Sum(x => x.Quantity).Should().Be(1); //As Basket doesn't have a Quantity property we count the Quantity of individual items
        }

        [Theory]
        [InlineData(1001, "Ham", 10, 2, 1002, "Fish", 15, 3)]
        [InlineData(1001, "Ham", 30, 5, 1002, "Fish", 100, 10)]
        public void After_Adding_Two_Items_With_Different_Quantities_To_An_Empty_Basket_Both_The_Basket_And_Item_Quantities_Are_Correct(
            int hamItemId, string hamItemName, decimal hamItemPrice, int hamItemQuantity, 
            int fishItemId, string fishItemName, decimal fishItem2Price, int fishItemQuantity)
        {
            //Arrange
            IShoppingItem item1 = new ShoppingItem(hamItemId, hamItemName, hamItemPrice);
            IShoppingItem item2 = new ShoppingItem(fishItemId, fishItemName, fishItem2Price);

            //Act
            _basket.AddItem(item1, hamItemQuantity);
            _basket.AddItem(item2, fishItemQuantity);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == hamItemId).Quantity.Should().Be(hamItemQuantity);
            basketItems.FirstOrDefault(x => x.Id == fishItemId).Quantity.Should().Be(fishItemQuantity);
            basketItems.Sum(x => x.Quantity).Should().Be(hamItemQuantity + fishItemQuantity); //Basket doesn't have "Quantity" Property so will need to work it out like this
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Updating_The_Quantity_On_An_Item_Already_In_A_Basket_Both_The_Basket_And_Item_Quantities_Are_Correct(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            IShoppingBasketItem basketItem = _basket.AddItem(item);
            _basket.AddItem(item, 3); //As there is no "Edit" method defined on the interface updating the quantity is done by adding another item

            _basket.RemoveItem(basketItem); //As we added 4 Items and removed 1, 3 Items should now remain in the Basket

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(3);
            basketItems.Sum(x => x.Quantity).Should().Be(3);
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void Adding_An_Item_With_A_Quantity_Of_0_Or_Less_Will_Result_In_An_ArgumentOutOfRangeException_Being_Thrown(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act And Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _basket.AddItem(item, 0));
        }

        [Theory]
        [InlineData(1001, "Ham", 10)] //Should this say "Updating_An_Item_To_A_Quantity_Of_Less_Than_0" as should be able to remove all "Ham" from _basket to 0, and add "Fish" for example
        public void Updating_An_Item_To_A_Quantity_Of_0_Or_Less_Will_Result_In_An_ArgumentOutOfRangeException_Being_Thrown(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act And Assert
            IShoppingBasketItem basketItem = _basket.AddItem(item);

            _basket.RemoveItem(basketItem);

            Assert.Throws<ArgumentOutOfRangeException>(() => _basket.RemoveItem(basketItem));
        }
                
        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_A_Single_Item_To_The_Basket_Adding_The_Same_Item_Again_Will_Update_The_Quantity_Of_The_Previously_Added_Item(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act
            _basket.AddItem(item);
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(2);
            basketItems.Sum(x => x.Quantity).Should().Be(2);
        }

        #region  Extended tests using new tax rule and discount rule below

        [Theory]
        [InlineData(1001, "Ham", 10)] 
        public void Removing_An_Item_That_Doesnt_Exist_In_The_Basket_Will_Result_In_An_KeyNotFoundException_Being_Thrown(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice);

            //Act And Assert
            IShoppingBasketItem basketItem = _basket.AddItem(item);

            IShoppingBasketItem itemThatDoesntExistInBasket = new ShoppingBasketItem(itemId + 1, itemName + "1", itemPrice + 1, null, null);

            //Assert
            Assert.Throws<KeyNotFoundException>(() => _basket.RemoveItem(itemThatDoesntExistInBasket));
        }
        
        #endregion
    }
}
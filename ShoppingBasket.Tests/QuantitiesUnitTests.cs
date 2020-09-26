using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class QuantitiesUnitTests
    {
        [Theory]
        [InlineData(1001, "Ham")]
        public void Adding_An_Item_Without_An_Explicit_Quantity_Results_In_A_Quantity_Of_1_For_The_Item(int itemId, string itemName)
        {
            //Arrange
            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            IShoppingItem item = new ShoppingItem(itemId, itemName);

            //Act
            basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);            
        }

        [Theory]
        [InlineData(1001, "Ham")]
        public void After_Adding_A_Single_Item_To_An_Empty_Basket_Both_The_Basket_And_Item_Quantity_Are_1(int itemId, string itemName)
        {
            //Arrange
            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            IShoppingItem item = new ShoppingItem(itemId, itemName);

            //Act
            basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == itemId).Quantity.Should().Be(1);
            basketItems.Sum(x => x.Quantity).Should().Be(1); //As Basket doesn't have a Quantity property we count the Quantity of individual items
        }

        [Theory]
        [InlineData(1001, "Ham", 1002, "Fish")]
        public void After_Adding_Two_Items_With_Different_Quantities_To_An_Empty_Basket_Both_The_Basket_And_Item_Quantities_Are_Correct(
            int item1Id, string item1Name, int item2Id, string item2Name)
        {
            //Arrange
            IShoppingBasket basket = new ShoppingBasket(new AlertService());

            IShoppingItem item1 = new ShoppingItem(item1Id, item1Name);
            IShoppingItem item2 = new ShoppingItem(item2Id, item2Name);

            //Act
            basket.AddItem(item1, 2);
            basket.AddItem(item2, 3);

            //Assert
            List<IShoppingBasketItem> basketItems = basket.Items as List<IShoppingBasketItem>;

            basketItems.FirstOrDefault(x => x.Id == item1Id).Quantity.Should().Be(2);
            basketItems.FirstOrDefault(x => x.Id == item2Id).Quantity.Should().Be(3);
            basketItems.Sum(x => x.Quantity).Should().Be(5);
        }

        [Fact]
        public void After_Updating_The_Quantity_On_AnItem_Already_In_A_Basket_Both_The_Basket_And_Item_Quantities_Are_Correct()
        {

        }

        [Fact]
        public void Adding_An_Item_With_Or_Updating_An_Item_To_A_Quantity_Of_0_Or_Less_Will_Result_In_An_ArgumentOutOfRangeException_Being_Thrown()
{

        }

        [Fact]
        public void After_Adding_A_Single_Item_To_The_Basket_Adding_The_Same_Item_Again_Will_Update_The_Quantity_Of_The_Previously_Added_Item()
{

        }
    }
}
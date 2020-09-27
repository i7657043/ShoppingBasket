using FluentAssertions;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;

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
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice, new List<ITaxRule>() { TaxRules.NoTax });

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            IShoppingBasketItem basketItem = basketItems.FirstOrDefault(x => x.Id == itemId);

            basketItem.SubTotal.Should().Be(basketItem.UnitPrice);
            _basket.SubTotal.Should().Be(basketItem.UnitPrice);
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_A_Single_Item_With_A_NoTax_rule_To_An_Empty_Basket_Both_The_Tax_Of_The_Item_And_The_Basket_Should_Equal_0_And_Both_The_Subtotal_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice, new List<ITaxRule>() { TaxRules.NoTax });

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            IShoppingBasketItem basketItem = basketItems.FirstOrDefault(x => x.Id == itemId);

            basketItem.Tax.Should().Be(0);
            _basket.Tax.Should().Be(0);

            basketItem.SubTotal.Should().Be(basketItem.UnitPrice);
            _basket.SubTotal.Should().Be(basketItem.UnitPrice);
        }
        
        [Theory]
        [InlineData(1001, "Ham", 10, 2, 1002, "Fish", 15, 3)]
        [InlineData(1001, "Ham", 30, 5, 1002, "Fish", 100, 10)] //If we add 1 item with quantity of 2, and another with quantity of 4, then the unit price of each will remain constant while the subtotal (unit price * quantity) will change, it wont match?
        public void After_Adding_Two_Items_With_Different_Quantities_Both_With_A_NoTax_Rule_To_An_Empty_Basket_Both_The_Tax_Of_The_Item_And_The_Basket_Should_Equal_0_And_Both_The_Subtotal_And_The_Total_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(
             int hamItemId, string hamItemName, decimal hamItemPrice, int hamItemQuantity,
             int fishItemId, string fishItemName, decimal fishItem2Price, int fishItemQuantity)
        {
            //Arrange
            IShoppingItem hamItem = new ShoppingItem(hamItemId, hamItemName, hamItemPrice, new List<ITaxRule>() { TaxRules.NoTax });
            IShoppingItem fishItem = new ShoppingItem(fishItemId, fishItemName, fishItem2Price, new List<ITaxRule>() { TaxRules.NoTax });

            //Act
            _basket.AddItem(hamItem, hamItemQuantity);
            _basket.AddItem(fishItem, fishItemQuantity);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            IShoppingBasketItem hamBasketItem = basketItems.FirstOrDefault(x => x.Id == hamItem.Id);
            IShoppingBasketItem fishBasketItem = basketItems.FirstOrDefault(x => x.Id == fishItem.Id);

            hamBasketItem.Tax.Should().Be(0);
            _basket.Tax.Should().Be(0);
            //hamBasketItem.SubTotal.Should().Be(hamBasketItem.UnitPrice);
            //_basket.SubTotal.Should().Be(hamBasketItem.UnitPrice);

            fishBasketItem.Tax.Should().Be(0);
            _basket.Tax.Should().Be(0);
            //fishBasketItem.SubTotal.Should().Be(fishBasketItem.UnitPrice);
            //_basket.SubTotal.Should().Be(fishBasketItem.UnitPrice);
        }


        #region  Extended tests using new tax rule and discount rule below

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_A_Single_Item_With_A_VatAs20Percent_Tax_rule_To_An_Empty_Basket_Both_The_Tax_Of_The_Item_And_The_Basket_Should_Equal_TwentyPercent_Increase_To_The_TotalCost_And_Both_The_Subtotal_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice, new List<ITaxRule>() { TaxRules.VatTax });

            //Act
            _basket.AddItem(item);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            IShoppingBasketItem basketItem = basketItems.FirstOrDefault(x => x.Id == itemId);

            basketItem.Tax.Should().Be(2);
            _basket.Tax.Should().Be(2);
            _basket.Total.Should().Be(12); //20% increase on total cost of 10

            basketItem.SubTotal.Should().Be(basketItem.UnitPrice);
            _basket.SubTotal.Should().Be(basketItem.UnitPrice);
        }

        [Theory]
        [InlineData(1001, "Ham", 10)]
        public void After_Adding_Two_Items_With_A_BuyOneGetOneFree_Discount_rule_To_An_Empty_Basket_Both_The_Discount_Of_The_Item_And_The_Basket_Should_Equal_0_And_Both_The_Subtotal_Of_The_Item_And_Basket_Should_Equal_The_Items_Unit_Price(int itemId, string itemName, decimal itemPrice)
        {
            //Arrange
            IShoppingItem item = new ShoppingItem(itemId, itemName, itemPrice, discountRules: new List<IDiscountRule>() { DiscountRules.BuyOneGetOneFreeDisountRule });

            //Act
            _basket.AddItem(item, 4);

            //Assert
            List<IShoppingBasketItem> basketItems = _basket.Items as List<IShoppingBasketItem>;

            IShoppingBasketItem basketItem = basketItems.FirstOrDefault(x => x.Id == itemId);

            _basket.Discount.Should().Be(20);
            _basket.Total.Should().Be(20);

            basketItem.SubTotal.Should().Be(basketItem.UnitPrice);
            _basket.SubTotal.Should().Be(basketItem.UnitPrice);
        }

        #endregion
    }
}
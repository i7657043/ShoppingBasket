# Mid Level Software Engineer Technical Challenge
​
## Overview
​
Build a shopping basket solution that provides the following facilities:
​
* Add item to cart  - done
    * With / without quantity (without - assume quantity to be 1)  - done
* Alter item quantity in cart - done
* Remove item from cart - done
* Calculate the sub total of each item - done
* Calculate the tax total of each item - done
* Calculate the grand total inc. tax of each item - done
* Calculate sub total of the whole basket - done
* Calculate tax total of the whole basket - done
* Calculate grand total inc. tax of the whole basket - done
* Allow each item to specify their own tax calculation rules - done
​
Operations performed against items (update quantity, add, remove, etc.) should cause the basket to update its totals automatically without further operations or methods being called. e.g.
​
```csharp
var basket = new ShoppingBasket();
basket.Add(1, Items.Cheese);
Console.WriteLine(basket.SubTotal);
```
​
```csharp
var basket = new ShoppingBasket();
basket.Add(1, Items.Cheese);
Console.WriteLine(basket.SubTotal);
basket.Items.First().Quantity = 100;
Console.WriteLine(basket.SubTotal);
```
​
The basket should implement a mechanism to notify interested parties that it has updated, and each individual item in the basket should do similarly. 
​
## Boiler Plate
​
The following defines the interfaces to the basket that will be used by other systems. The basket must implement these interfaces.
​
```csharp
​
    public interface IShoppingBasket : ITotals, IUpdated
    {
        IShoppingBasketItem AddItem(IShoppingItem item);
        IShoppingBasketItem AddItem(IShoppingItem item, int quantity);
        IShoppingBasketItem RemoveItem(IShoppingBasketItem item);
​
        IEnumerable<IShoppingBasketItem> Items { get; }
    }
​
    public interface IShoppingBasketItem : IShoppingItem, ITotals, IUpdated
    {
        int Quantity { get; set; }
    }
​
    public interface IShoppingItem
    {
        long Id { get; }
        string Name { get; }
        IEnumerable<ITaxRule> TaxRules { get; }
    }
​
    public interface ITaxRule
    {
        decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item);
    }
​
    public interface ITotals
    {
        decimal SubTotal { get; }
        decimal Tax { get; }
        decimal Total { get; }
    }
​
    public interface IUpdated
    {
        event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
​
    public class ShoppingUpdatedEventArgs : EventArgs
    {
​
    }
​
    public static class TaxRules
    {
        public static ITaxRule NoTax = new ItemSubTotalPercentageTaxRule(0m);
    }
​
    public class ItemSubTotalPercentageTaxRule : ITaxRule
    {
        // TODO:Please provide the implementation of this type to calculate the tax as a percentage of the sub total for the item
    }
​
```
​
## Testing
​
Implementation should following a red-green-refactor model using a TDD approach:
​
1. Using the defined interfaces above you can write a suite of tests to cover each of the defined criteria
2. Provide sufficient implementation of the types to compile
3. Run your tests - they all fail
4. Get a set of your tests to pass
5. Examine whether your design requires refactoring (use your test suite as reassurance)
6. Have all your tests gone green? If not go to 4.
7. FIN!
​
*ALL TESTS* must be machine executable - no manual tests!
​
#### Required test scenarios
​
At a _minimum_ you must ensure the following scenarios are covered by your test suite:
​
1. Initial state
    * A newly constructed basket has 0 items in and totals that are all 0 - done
​
2. Quantities
    * Adding an item without an explicit quantity results in a quantity of 1 for the item - done
    * After adding a single item to an empty basket, both the basket and item quantity are 1 - done
    * After adding two items with different quantities to an empty basket, both the basket and item quantities are correct - done
    * After updating the quantity on an item already in a basket, both the basket and item quantities are correct - done
    * Adding an item with, or updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown - done
    * After adding a single item to the basket, adding the same item again will update the quantity of the previously added item - done
​
3. Totals
    * After adding a single item to an empty basket, both the subtotal of the item and basket should equal the items unit price - done
    * After adding a single item (with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0 and both the subtotal of the item and basket should equal the items unit price - done
    * After adding two items with different quantities (both with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0, both the subtotal and the total of the item and basket should equal the items unit price - done
​
The set of tests described above are a bare _minimum_ - in reality there are many more scenarios that should be tested. Please consider what you think to be the most important cases and provide additional tests to improve coverage.
​
## Scretch Goals
​
If you're feeling like you want more, here are some scretch goals to consider implementing / testing around discounts:
​
​
* Design an IDiscountRule interface similar to ITaxRule - done
* Allow items to define their discounts - done
* Provide a property to expose the value of the discount on ITotals - done
* Possible discount types :
    * Buy one get one free - done
    * Buy item 1 and item 2 together for a discount
* And of course - lots of test scenarios for the new functionality - done
​
## Implementation
​
Implementation of the basket must be in C#. You can decide whether you wish to make use of .NET Core / Framework, but it is suggested to make use of .NET Core.
​
Tests can be implemented as you wish, either as traditional unit tests or as formal SpecFlow specifications.
​
Submissions should be made using an online source code repository (for example, GitHub).
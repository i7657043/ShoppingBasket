namespace ShoppingBasket
{
    public class ItemSubTotalPercentageTaxRule : ITaxRule
    {
        // TODO:Please provide the implementation of this type to calculate the tax as a percentage of the sub total for the item
        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            //return item.SubTotal + (item.SubTotal * decimal.Parse("0.20")); //Add 20%
            return 0; //No tax rule from Rules.md
        }
    }
}
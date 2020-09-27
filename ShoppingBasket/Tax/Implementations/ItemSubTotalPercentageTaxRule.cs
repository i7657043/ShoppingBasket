namespace ShoppingBasket
{
    public class ItemSubTotalPercentageTaxRule : ITaxRule
    {
        //TODO:Please provide the implementation of this type to calculate the tax as a percentage of the sub total for the item
        //Unsure as to why need reference to basket and item. With just basket can iterate over all items, or with just item can calculate individually when required
        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            return item.SubTotal * 0M; //Add 0% - NoTax Rule from Rules.md
        }

        //public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        //{
        //    return item.SubTotal * 0.20M; //Add 20%
        //}
    }
}
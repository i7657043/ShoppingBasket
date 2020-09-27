namespace ShoppingBasket
{
    public interface ITaxRule
    {
        decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item);
    }

    public interface IDiscount
    {
        decimal CalculateDiscount(IShoppingBasketItem basketItem);
    }

    public class BuyOneGetOneFreeDiscountRule : IDiscount
    {
        //TODO:Please provide the implementation of this type to calculate the tax as a percentage of the sub total for the item
        //Discounts use Total and take Tax into consideration, should they work on Sub-total instead?
        public decimal CalculateDiscount(IShoppingBasketItem basketItem)
        {
            if (basketItem.Quantity <= 1)
                return 0;

            if (basketItem.Quantity % 2 == 1)
            {
                //Get the discount of the even number of items by removing the odd item and dividing by 2, then add the price of 1 item back on

                decimal costOf1 = basketItem.Total / basketItem.Quantity; //
                decimal costWithout1 = basketItem.Total - costOf1; //
                decimal bogofDiscountWithout1 = costWithout1 / 2;
                decimal totalDiscount = bogofDiscountWithout1 + costOf1;

                return ((basketItem.Total - (basketItem.Total / basketItem.Quantity)) / 2) + (basketItem.Total / basketItem.Quantity);
            }
            else
            {
                return basketItem.Total / 2;
            }
        }
    }
}

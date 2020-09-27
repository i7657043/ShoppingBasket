using ShoppingBasketChallenge.Items;

namespace ShoppingBasketChallenge.Discounts
{
    public class BuyOneGetOneFreeDiscountRule : IDiscountRule
    {
        //Discounts DO NOT use Total and DO NOT take Tax into consideration
        public decimal CalculateDiscount(IShoppingBasketItem basketItem)
        {
            if (basketItem.Quantity <= 1)
                return 0;

            if (basketItem.Quantity % 2 == 1)
            {
                //Get the discount of the even number of items by removing the odd item and dividing by 2, then add the price of 1 item back on

                decimal costOf1 = basketItem.Total / basketItem.Quantity;
                decimal costWithout1 = basketItem.Total - costOf1;
                decimal bogofDiscountWithout1 = costWithout1 / 2;
                decimal totalDiscount = bogofDiscountWithout1 + costOf1;

                return ((basketItem.SubTotal - (basketItem.SubTotal / basketItem.Quantity)) / 2) + (basketItem.SubTotal / basketItem.Quantity);
            }
            else
            {
                return basketItem.SubTotal / 2;
            }
        }
    }
}

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

            return GetBogofDiscount(basketItem);
        }

        private static decimal GetBogofDiscount(IShoppingBasketItem basketItem)
        {
            //Get the discount for odd quantity by getting the even number of items by removing the odd item and dividing by 2
            //then adding the price of 1 item back on to get the total
            return basketItem.Quantity % 2 == 1
                ? ((basketItem.SubTotal - (basketItem.SubTotal / basketItem.Quantity)) / 2) + (basketItem.SubTotal / basketItem.Quantity)
                : basketItem.SubTotal / 2;
        }
    }
}

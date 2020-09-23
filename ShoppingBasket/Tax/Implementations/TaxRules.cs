namespace ShoppingBasket
{
    public static class TaxRules
    {
        public static ITaxRule NoTax = new ItemSubTotalPercentageTaxRule();
    }
}

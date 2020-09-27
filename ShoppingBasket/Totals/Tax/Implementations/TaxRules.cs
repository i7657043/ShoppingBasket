namespace ShoppingBasketChallenge.Totals
{
    public static class TaxRules
    {
        public static ITaxRule NoTax = new ItemSubTotalPercentageTaxRule();
        public static ITaxRule VatTax = new VatAs20PercentTaxRule();
    }
}

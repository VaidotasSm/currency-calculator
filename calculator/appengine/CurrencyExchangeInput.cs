namespace calculator.appengine
{
    class CurrencyExchangeInput
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal FromAmount { get; set; }
    }
}
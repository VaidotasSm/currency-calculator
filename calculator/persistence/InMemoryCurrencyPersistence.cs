using System.Collections.Generic;
using calculator.models;

namespace calculator.persistence
{
    public class InMemoryCurrencyPersistence : ICurrencyPersistence
    {
        private static readonly string DefaultCUrrency = "DKK";
        private static readonly List<CurrencyPrice> DkkToBuy100 = new List<CurrencyPrice>
        {
            new CurrencyPrice {Currency = "EUR", Price = 743.94m},
            new CurrencyPrice {Currency = "USD", Price = 663.11m},
            new CurrencyPrice {Currency = "GBP", Price = 852.85m},
            new CurrencyPrice {Currency = "SEK", Price = 76.10m},
            new CurrencyPrice {Currency = "NOK", Price = 78.40m},
            new CurrencyPrice {Currency = "CHF", Price = 683.58m},
            new CurrencyPrice {Currency = "JPY", Price = 5.9740m}
        };

        public decimal GetDkkAmountToBuy100(string currency)
        {
            var currencyPrice = DkkToBuy100.Find(c => c.Currency == currency);
            if (currencyPrice == null)
            {
                throw new CurrencyDoesNotExistException();
            }

            return currencyPrice.Price;
        }

        public bool IsCurrencySupported(string currency)
        {
            return currency == DefaultCUrrency || DkkToBuy100.Find(c => c.Currency == currency) != null;
        }
    }
}
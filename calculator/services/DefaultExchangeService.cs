using calculator.persistence;

namespace calculator.services
{
    public class DefaultExchangeService : IExchangeService
    {
        private const string Dkk = "DKK";
        private readonly ICurrencyPersistence _currencyPersistence;

        public DefaultExchangeService()
        {
            _currencyPersistence = new InMemoryCurrencyPersistence();
        }

        public DefaultExchangeService(ICurrencyPersistence persistence)
        {
            _currencyPersistence = persistence;
        }

        public decimal GetAmount(string currencyFrom, string currencyTo, decimal fromAmount)
        {
            ValidateCurrencies(currencyFrom, currencyTo);
            if (currencyFrom == currencyTo)
            {
                return fromAmount;
            }

            decimal amountDkk = fromAmount * CostOfOneInDkk(currencyFrom);
            return amountDkk / CostOfOneInDkk(currencyTo);
        }

        private decimal CostOfOneInDkk(string currency)
        {
            if (currency == Dkk)
            {
                return 1;
            }
            
            decimal dkkToBuy100 = _currencyPersistence.GetDkkAmountToBuy100(currency);
            return dkkToBuy100 / 100;
        }
        
        private void ValidateCurrencies(string currencyFrom, string currencyTo)
        {
            if (!_currencyPersistence.IsCurrencySupported(currencyFrom))
            {
                throw new InvalidCurrencyException(currencyFrom);
            }

            if (!_currencyPersistence.IsCurrencySupported(currencyTo))
            {
                throw new InvalidCurrencyException(currencyTo);
            }
        }
    }
}
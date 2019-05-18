namespace calculator.persistence
{
    public interface ICurrencyPersistence
    {
        /// <summary>
        /// Get DKK amount to buy 100 of currency
        /// </summary>
        /// <param name="currency">Currency to buy with DKK</param>
        /// <returns>Amount of DKK</returns>
        decimal GetDkkAmountToBuy100(string currency);

        bool IsCurrencySupported(string currency);
    }
}
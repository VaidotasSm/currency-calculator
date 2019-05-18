namespace calculator.services
{
    public interface IExchangeService
    {
        /// <summary>
        /// How much currencyTo can buy with fromAmout of currencyFrom
        /// </summary>
        /// <param name="currencyFrom">Currency to buy with</param>
        /// <param name="currencyTo">Currency to buy</param>
        /// <param name="fromAmount">Amount of currency to buy with</param>
        /// <returns>Amount of currency to buy</returns>
        decimal GetAmount(string currencyFrom, string currencyTo, decimal fromAmount);
    }
}
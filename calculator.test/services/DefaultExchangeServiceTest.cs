using calculator.services;
using Xunit;

namespace calculator.test.services
{
    public class DefaultExchangeServiceTest
    {
        [Fact]
        public void ShouldErrorWhenUnknownCurrency()
        {
            var service = new DefaultExchangeService();

            var result = Assert.Throws<InvalidCurrencyException>(
                () => service.GetAmount("AAA", "USD", 1)
            );
            Assert.Equal("AAA", result.InvalidValue);

            result = Assert.Throws<InvalidCurrencyException>(
                () => service.GetAmount("USD", "BBB", 1)
            );
            Assert.Equal("BBB", result.InvalidValue);
        }

        [Theory]
        [InlineData("EUR", "EUR", 1)]
        [InlineData("USD", "USD", 1)]
        [InlineData("GBP", "GBP", 1)]
        [InlineData("SEK", "SEK", 1)]
        public void ShouldReturnSameAmountWhenCurrenciesMatch(string from, string to, decimal fromAmount)
        {
            var service = new DefaultExchangeService();
            Assert.Equal(fromAmount, service.GetAmount(from, to, fromAmount));
        }

        [Theory]
        [InlineData("EUR", "USD", 1, 1.12)]
        [InlineData("EUR", "USD", 100, 112)]
        [InlineData("USD", "EUR", 1, 0.90)]
        [InlineData("EUR", "GBP", 1, 0.88)]
        [InlineData("SEK", "GBP", 1, 0.089)]
        [InlineData("NOK", "USD", 1, 0.118)]
        [InlineData("CHF", "USD", 1, 0.99)]
        [InlineData("JPY", "USD", 1, 0.0091)]
        public void ShouldCalculateAmountsWhenNoDkk(string from, string to, decimal fromAmount, decimal expected)
        {
            var service = new DefaultExchangeService();

            decimal delta = expected * 0.05m;
            Assert.InRange(
                service.GetAmount(from, to, fromAmount),
                expected - delta, expected + delta
            );
        }

        [Theory]
        [InlineData("EUR", "DKK", 1, 7.4394)]
        public void ShouldCalculateWHenDkkUsed(string from, string to, decimal fromAmount, decimal expected)
        {
            var service = new DefaultExchangeService();

            decimal delta = expected * 0.05m;
            Assert.InRange(
                service.GetAmount(from, to, fromAmount),
                expected - delta, expected + delta
            );
        }
    }
}
using System;
using calculator.appengine;
using calculator.appengine.commands;
using calculator.appengine.ui;
using calculator.services;
using Moq;
using Xunit;

namespace calculator.test.appengine
{
    public class CalculatorAppTest
    {
        [Fact]
        public void ShouldHandleInvalidCommands()
        {
            var output = ExecuteProcessCommandWithMocks(null);

            Assert.Equal(
                "Incorrect command, please try again or type HELP",
                output
            );
        }

        [Fact]
        public void ShouldHandleHelpCommand()
        {
            var output = ExecuteProcessCommandWithMocks(new HelpCommand("help"));

            Assert.Equal(
                "Usage: Exchange <currency>/<currency> <amount to exchange>",
                output
            );
        }

        [Fact]
        public void ShouldDisplayInfoWhenIncorrectExchangeArguments()
        {
            var output = ExecuteProcessCommandWithMocks(new ExchangeCommand("exchange USD/E 1"));

            Assert.Equal(
                "Usage: Exchange <currency>/<currency> <amount to exchange>",
                output
            );
        }

        [Fact]
        public void ShouldRunExchangeCommand()
        {
            var exchangeServiceMock = new Mock<IExchangeService>();
            exchangeServiceMock
                .Setup(m => m.GetAmount("EUR", "USD", 1))
                .Returns(1.12m);

            var output = ExecuteProcessCommandWithMocks(
                new ExchangeCommand("exchange EUR/USD 1"), exchangeServiceMock
            );

            Assert.Equal(
                "1.1200",
                output
            );
        }

        [Fact]
        public void ShouldHandleInvalidCurrencyException()
        {
            var exchangeServiceMock = new Mock<IExchangeService>();
            exchangeServiceMock
                .Setup(m => m.GetAmount("AAA", "USD", 1))
                .Throws(new InvalidCurrencyException("AAA"));

            var output = ExecuteProcessCommandWithMocks(
                new ExchangeCommand("exchange aaa/USD 1"), exchangeServiceMock
            );

            Assert.Equal(
                "Currency 'AAA' is not supported.",
                output
            );
        }

        [Fact]
        public void ShouldHandleAnyException()
        {
            var exchangeServiceMock = new Mock<IExchangeService>();
            exchangeServiceMock
                .Setup(m => m.GetAmount("EUR", "USD", 1))
                .Throws(new Exception());

            var output = ExecuteProcessCommandWithMocks(
                new ExchangeCommand("exchange EUR/USD 1"), exchangeServiceMock
            );

            Assert.Equal(
                "Something went wrong, please try again.",
                output
            );
        }

        private static string ExecuteProcessCommandWithMocks(
            Command command,
            Mock<IExchangeService> exchangeMock = null,
            Mock<IUserInteraction> userMock = null)
        {
            var exchangeServiceMock = exchangeMock ?? new Mock<IExchangeService>();
            var userInteractionMock = userMock ?? new Mock<IUserInteraction>();
            userInteractionMock.Setup(m => m.WaitForUserAction()).Returns(command);

            string output = "";
            CaptureUserOutput(userInteractionMock, (value) => output = value);

            var calculatorApp = new CalculatorApp(exchangeServiceMock.Object, userInteractionMock.Object);
            calculatorApp.ProcessCommand();

            return output;
        }

        private static void CaptureUserOutput(Mock<IUserInteraction> userInteractionMock, Action<string> cb)
        {
            userInteractionMock
                .Setup(m => m.DisplayMessage(It.IsAny<string>()))
                .Callback(cb);
        }
    }
}
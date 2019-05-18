using System;
using calculator.appengine.commands;
using calculator.appengine.ui;
using Moq;
using Xunit;

namespace calculator.test.appengine.ui
{
    public class UserInteractionTest
    {
        [Fact]
        public void ShouldPassMessageToIoStream()
        {
            var mockIoStream = new Mock<IUserIoStream>();
            var sut = new UserInteraction(mockIoStream.Object);
            
            sut.DisplayMessage("Message 1");
            mockIoStream.Verify(m => m.WriteLine("Message 1"), Times.Once());
        }

        [Fact]
        public void ShouldReturnNullForInvalidCommand()
        {
            var mockIoStream = new Mock<IUserIoStream>();
            mockIoStream.Setup(m => m.ReadLine()).Returns("Invalid Input");
            var sut = new UserInteraction(mockIoStream.Object);
            
            var result = sut.WaitForUserAction();
            Assert.Null(result);
        }
        
        [Theory]
        [InlineData("EXIT", typeof(ExitCommand), true)]
        [InlineData("exit", typeof(ExitCommand), true)]
        [InlineData("HELP", typeof(HelpCommand), true)]
        [InlineData("help", typeof(HelpCommand), true)]
        [InlineData("EXCHANGE", typeof(ExchangeCommand), false)]
        [InlineData("exchange", typeof(ExchangeCommand), false)]
        public void ShouldIdentifyCommands(string input, Type expectedCommand, bool expectedCorrectArgs)
        {
            var mockIoStream = new Mock<IUserIoStream>();
            mockIoStream.Setup(m => m.ReadLine()).Returns(input);
            var sut = new UserInteraction(mockIoStream.Object);
            
            var result = sut.WaitForUserAction();
            Assert.IsType(expectedCommand, result);
            Assert.Equal(expectedCorrectArgs, result.HasCorrectArguments);
        }

        [Theory]
        [InlineData("Exchange EUR/USD 1", true)]
        [InlineData("Exchange EUR 1", false)]
        [InlineData("Exchange EUR/USD", false)]
        [InlineData("Exchange EUR/US", false)]
        [InlineData("Exchange EU/US", false)]
        [InlineData("Exchange / 1", false)]
        [InlineData("Exchange 1 1", false)]
        public void ShouldIdentifyValidExchangeArguments(string input, bool expectedCorrectArgs)
        {
            var mockIoStream = new Mock<IUserIoStream>();
            mockIoStream.Setup(m => m.ReadLine()).Returns(input);
            var sut = new UserInteraction(mockIoStream.Object);
            
            var result = sut.WaitForUserAction();
            Assert.Equal(expectedCorrectArgs, result.HasCorrectArguments);
        }

        [Fact]
        public void ShouldParseExchangeCommandArguments()
        {
            var mockIoStream = new Mock<IUserIoStream>();
            mockIoStream.Setup(m => m.ReadLine()).Returns("Exchange EUR/USD 1");
            var sut = new UserInteraction(mockIoStream.Object);
            
            var result = sut.WaitForUserAction();
            var exchangeCommand = (ExchangeCommand)result;

            Assert.Equal("EUR", exchangeCommand.CurrencyFrom);
            Assert.Equal("USD", exchangeCommand.CurrencyTo);
            Assert.Equal(1, exchangeCommand.FromAmount);
        }
        
        [Fact]
        public void ShouldParseCaseInsensitiveCurrencyArguments()
        {
            var mockIoStream = new Mock<IUserIoStream>();
            mockIoStream.Setup(m => m.ReadLine()).Returns("Exchange euR/Usd 1");
            var sut = new UserInteraction(mockIoStream.Object);
            
            var result = sut.WaitForUserAction();
            var exchangeCommand = (ExchangeCommand)result;

            Assert.Equal("EUR", exchangeCommand.CurrencyFrom);
            Assert.Equal("USD", exchangeCommand.CurrencyTo);
        }

    }
}
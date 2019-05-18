using System;

namespace calculator.appengine.commands
{
    public class ExchangeCommand : Command
    {
        public string CurrencyFrom { get; protected set; }
        public string CurrencyTo { get; protected set; }
        public decimal FromAmount { get; protected set; }
        
        public ExchangeCommand(string inputLine) : base(inputLine)
        {
            var arguments = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (arguments.Length != 3)
            {
                HasCorrectArguments = false;
                return;
            }

            if (arguments[1].Length != 7 || !arguments[1].Contains("/"))
            {
                HasCorrectArguments = false;
                return;
            }
            
            var currencies = arguments[1].Split("/");
            if (currencies.Length != 2 || currencies[0].Length != 3 || currencies[1].Length != 3)
            {
                HasCorrectArguments = false;
                return;
            }
            if (!decimal.TryParse(arguments[2], out var amount))
            {
                HasCorrectArguments = false;
                return;
            }

            CurrencyFrom = currencies[0].ToUpper();
            CurrencyTo = currencies[1].ToUpper();
            FromAmount = amount;
        }

        public override string GetArgumentHelpMessage()
        {
            return "Usage: Exchange <currency>/<currency> <amount to exchange>";
        }
    }
}
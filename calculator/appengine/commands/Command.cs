using System;

namespace calculator.appengine.commands
{
    public abstract class Command
    {
        public bool HasCorrectArguments { get; protected set; }

        public abstract string GetArgumentHelpMessage();

        protected Command(string inputLine)
        {
            HasCorrectArguments = true;
        }

        public static Command From(string inputLine)
        {
            if (String.IsNullOrWhiteSpace(inputLine))
            {
                return null;
            }

            if (inputLine.ToLower().StartsWith("exchange"))
            {
                return new ExchangeCommand(inputLine);
            }

            if (inputLine.ToLower().StartsWith("help"))
            {
                return new HelpCommand(inputLine);
            }

            if (inputLine.ToLower().StartsWith("exit"))
            {
                return new ExitCommand(inputLine);
            }

            return null;
        }
    }
}
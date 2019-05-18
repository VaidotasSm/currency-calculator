using System;
using calculator.appengine.commands;
using calculator.appengine.ui;
using calculator.services;

namespace calculator.appengine
{
    public class CalculatorApp
    {
        public static readonly string HELP_MESSAGE = "Usage: Exchange <currency>/<currency> <amount to exchange>";
        private readonly IExchangeService _exchangeService;
        private readonly IUserInteraction _userInteraction;

        public CalculatorApp()
        {
            _exchangeService = new DefaultExchangeService();
            _userInteraction = new UserInteraction();
        }

        public CalculatorApp(IExchangeService exchangeService, IUserInteraction userInteraction)
        {
            _exchangeService = exchangeService;
            _userInteraction = userInteraction;
        }

        public Command ProcessCommand()
        {
            try
            {
                Command command = _userInteraction.WaitForUserAction();
                if (command == null)
                {
                    _userInteraction.DisplayMessage("Incorrect command, please try again or type HELP");
                    return null;
                }

                if (command is HelpCommand)
                {

                    _userInteraction.DisplayMessage(HELP_MESSAGE);
                    return command;
                }

                if (command is ExchangeCommand)
                {
                    if (!command.HasCorrectArguments)
                    {
                        _userInteraction.DisplayMessage(command.GetArgumentHelpMessage());
                        return null;
                    }

                    var exchangeCommand = (ExchangeCommand) command;
                    var result = _exchangeService.GetAmount(
                        exchangeCommand.CurrencyFrom,
                        exchangeCommand.CurrencyTo,
                        exchangeCommand.FromAmount
                    );
                    _userInteraction.DisplayMessage($"{result:N4}");
                }

                return command;
            }
            catch (InvalidCurrencyException ex)
            {
                _userInteraction.DisplayMessage($"Currency '{ex.InvalidValue}' is not supported.");
                return null;
            }
            catch (Exception)
            {
                _userInteraction.DisplayMessage("Something went wrong, please try again.");
                return null;
            }
        }
    }
}
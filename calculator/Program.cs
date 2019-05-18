using System;
using calculator.appengine;
using calculator.appengine.commands;
using calculator.appengine.ui;
using calculator.services;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInteraction = new UserInteraction();
            var calculatorApp = new CalculatorApp(new DefaultExchangeService(), userInteraction);

            userInteraction.DisplayMessage(CalculatorApp.HELP_MESSAGE);
            userInteraction.DisplayMessage("Other Commands: HELP, EXIT");
            Command lastExecutedCommand = null;
            while (!(lastExecutedCommand is ExitCommand))
            {
                lastExecutedCommand = calculatorApp.ProcessCommand();
            }
        }
    }
}
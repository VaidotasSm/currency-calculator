using System;
using System.Collections.Generic;
using calculator.appengine.commands;

namespace calculator.appengine.ui
{
    public class UserInteraction : IUserInteraction
    {
        private readonly IUserIoStream _userIoStream;

        public UserInteraction()
        {
            _userIoStream = new ConsoleUserIoStream();
        }

        public UserInteraction(IUserIoStream userIoStream)
        {
            _userIoStream = userIoStream;
        }

        public Command WaitForUserAction()
        {
            var input = _userIoStream.ReadLine();
            return Command.From(input);
        }

        public void DisplayMessage(string message)
        {
            _userIoStream.WriteLine(message);
        }
    }
}
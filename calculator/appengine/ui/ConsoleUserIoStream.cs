using System;

namespace calculator.appengine.ui
{
    public class ConsoleUserIoStream : IUserIoStream
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
namespace calculator.appengine.commands
{
    public class ExitCommand : Command
    {
        public ExitCommand(string inputLine) : base(inputLine)
        {
        }

        public override string GetArgumentHelpMessage()
        {
            return "";
        }
    }
}
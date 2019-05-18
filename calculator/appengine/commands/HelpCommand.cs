namespace calculator.appengine.commands
{
    public class HelpCommand : Command
    {
        public HelpCommand(string inputLine) : base(inputLine)
        {
        }

        public override string GetArgumentHelpMessage()
        {
            return "";
        }
    }
}
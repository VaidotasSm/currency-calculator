using calculator.appengine.commands;

namespace calculator.appengine.ui
{
    public interface IUserInteraction
    {
        Command WaitForUserAction();
        void DisplayMessage(string message);
    }
}
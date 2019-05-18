namespace calculator.appengine.ui
{
    public interface IUserIoStream
    {
        void WriteLine(string message);

        string ReadLine();
    }
}
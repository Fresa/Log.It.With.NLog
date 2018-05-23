namespace Log.It.With.NLog
{
    public interface IOutput
    {
        void Write(string message);
    }
}
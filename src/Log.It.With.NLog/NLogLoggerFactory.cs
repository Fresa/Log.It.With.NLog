namespace Log.It.With.NLog
{
    public class NLogLoggerFactory
    {
        public ILogFactory Create()
        {
            return new NLogFactory(new LogicalThreadContext());
        }
    }
}
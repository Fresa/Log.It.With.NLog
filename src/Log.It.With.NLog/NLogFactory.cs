using System;
using System.Diagnostics;

namespace Log.It.With.NLog
{
    public class NLogFactory : ILogFactory
    {
        private readonly ILogContext _logContext;

        public NLogFactory(ILogContext logContext)
        {
            _logContext = logContext;
        }

        public ILogger Create(string logger)
        {
            return new NLogLogger(logger, _logContext);
        }

        public ILogger Create<T>()
        {
            return new NLogLogger(typeof(T).GetPrettyName(), _logContext);
        }

        [Obsolete("Will be removed in next major version.")]
        public ILogger Create()
        {
            var stackTrace = new StackFrame(1, false);
            return new NLogLogger(stackTrace.GetMethod().DeclaringType.GetPrettyName(), _logContext);
        }
    }
}
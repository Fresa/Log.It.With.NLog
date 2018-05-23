using System.Text;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;

namespace Log.It.With.NLog
{
    [LayoutRenderer("lc")]
    public class NLogLogContextLayoutRenderer : LayoutRenderer
    {
        private ILogContext LogContext => _logger.LogicalThread;
        private readonly ILogger _logger = LogFactory.Create<NLogLogContextLayoutRenderer>();

        [RequiredParameter]
        [DefaultParameter]
        public string Key { get; set; }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (LogContext.Contains(Key))
            {
                builder.AppendFormat(logEvent.FormatProvider, LogContext.Get<object>(Key).ToString());
            }
        }
    }
}
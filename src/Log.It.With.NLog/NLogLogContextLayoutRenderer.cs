using System.Text;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.MessageTemplates;

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

        /// <summary>
        /// Set to @ for structured logging
        /// </summary>
        public string Format { get; set; }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (LogContext.Contains(Key) == false)
            {
                return;
            }

            var value = LogContext.Get<object>(Key);

            if (value is string && string.IsNullOrEmpty(Format))
            {
                builder.Append(value);
                return;
            }

            var formatProvider = logEvent.FormatProvider;

            if (Format == "@")
            {
                ConfigurationItemFactory.Default.ValueFormatter.FormatValue(value, null, CaptureType.Serialize, formatProvider, builder);
                return;
            }

            if (value == null)
            {
                return;
            }

            ConfigurationItemFactory.Default.ValueFormatter.FormatValue(value, Format, CaptureType.Normal, formatProvider, builder);
        }
    }
}

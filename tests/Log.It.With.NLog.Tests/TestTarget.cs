using System.Collections.Generic;
using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace Log.It.With.NLog.Tests
{
    public sealed class TestTarget : TargetWithLayout
    {
        public IReadOnlyList<string> MessagesWritten => _messages;
        private readonly List<string> _messages = new List<string>();

        public override Layout Layout { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);

            _messages.Add(logMessage);
        }
    }
}
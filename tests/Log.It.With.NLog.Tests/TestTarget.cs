using System.Collections.Generic;
using NLog;
using NLog.Targets;

namespace Log.It.With.NLog.Tests
{
    [Target("Test")]
    public sealed class TestTarget : TargetWithLayout
    {
        public static IReadOnlyList<string> MessagesWritten => Messages;
        private static readonly List<string> Messages = new List<string>();

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);

            Messages.Add(logMessage);
        }
    }
}
using System;

namespace Log.It.With.NLog
{
    public static class NLogCapturer
    {
        private static readonly ILogger Logger = LogFactory.Create(typeof(NLogCapturer).FullName);

        private static readonly string ContextKey = typeof(NLogCapturer).FullName;

        public static IDisposable Capture(IOutput output)
        {
            var id = Guid.NewGuid();

            void Output(string message)
            {
                if (ShouldCapture(id))
                {
                    output.Write(message);
                }
            }

            NLogCapturingTarget.Subscribe += Output;

            Logger.LogicalThread.Set(ContextKey, id);

            return new DisposableAction(() =>
            {
                NLogCapturingTarget.Subscribe -= Output;
                Logger.LogicalThread.Remove(ContextKey);
            });
        }

        private static bool ShouldCapture(Guid id)
        {
            return Logger.LogicalThread.Contains(ContextKey) &&
                   Logger.LogicalThread.Get<Guid>(ContextKey).Equals(id);
        }

        private class DisposableAction : IDisposable
        {
            private readonly Action _action;

            public DisposableAction(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action();
            }
        }
    }
}
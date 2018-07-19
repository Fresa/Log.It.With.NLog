using System;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Targets;

namespace Log.It.With.NLog.Tests
{
    internal class LoggingFixture : IDisposable
    {
        private LoggingRule _loggingRule;
        private readonly string _name = Guid.NewGuid().ToString();

        public LoggingFixture WithLayoutRenderer<TLayoutRenderer>(string name) 
            where TLayoutRenderer : LayoutRenderer
        {
            LayoutRenderer.Register<TLayoutRenderer>(name);
            return this;
        }

        public TTarget CreateTarget<TTarget>(string layout) where TTarget : TargetWithLayout, new()
        {
            var target = new TTarget
            {
                Name = _name,
                Layout = layout
            };
            _loggingRule = new LoggingRule(_name, LogLevel.Info, LogLevel.Info, target);

            LogManager.Configuration.LoggingRules.Add(_loggingRule);
            LogManager.ReconfigExistingLoggers();

            return target;
        }


        public NLogLogger CreateLogger()
        {
            return new NLogLogger(_name, new LogicalThreadContext());
        }

        public void Dispose()
        {
            LogManager.Configuration.LoggingRules.Remove(_loggingRule);
            LogManager.Configuration.RemoveTarget(_name);
        }
    }
}
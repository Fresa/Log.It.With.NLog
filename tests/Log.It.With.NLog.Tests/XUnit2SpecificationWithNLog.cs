using NLog;
using NLog.Config;
using Test.It.With.XUnit;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
    public class XUnit2SpecificationWithNLog : XUnit2Specification
    {
        static XUnit2SpecificationWithNLog()
        {
            LogManager.Configuration = new LoggingConfiguration();
            var nlogTarget = new NLogCapturingTarget
            {
                Layout = "${logger}: ${message}"
            };
            LogManager.Configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, nlogTarget);
        }

        public XUnit2SpecificationWithNLog(ITestOutputHelper testOutputHelper) : base(testOutputHelper, false)
        {
            NLogCapturingTarget.Subscribe += TestOutputHelper.WriteLine;
            Setup();
        }

        protected override void Dispose(bool disposing)
        {
            NLogCapturingTarget.Subscribe -= TestOutputHelper.WriteLine;
            base.Dispose(disposing);
        }
    }
}
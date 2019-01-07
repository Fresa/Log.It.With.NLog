using System;
using System.Collections.Generic;
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
            LogFactory.Initialize(new NLogLoggerFactory().Create());
            LogManager.Configuration = new LoggingConfiguration();
            var nlogTarget = new NLogCapturingTarget
            {
                Layout = "${logger}: ${message}"
            };
            LogManager.Configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, nlogTarget);
            LogManager.ReconfigExistingLoggers();
        }

        protected List<string> MessagesWrittenToOutput = new List<string>();

        private void WriteToOutput(string output)
        {
            MessagesWrittenToOutput.Add(output);
            TestOutputHelper.WriteLine(output);
        }

        public XUnit2SpecificationWithNLog(ITestOutputHelper testOutputHelper) : base(testOutputHelper, false)
        {
            NLogCapturingTarget.Subscribe += WriteToOutput;
            Setup();
        }

        protected override void Dispose(bool disposing)
        {
            NLogCapturingTarget.Subscribe -= WriteToOutput;
            base.Dispose(disposing);
        }
    }
}
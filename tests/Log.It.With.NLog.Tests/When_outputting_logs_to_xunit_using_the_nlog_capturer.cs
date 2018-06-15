using System;
using Should.Fluent;
using Test.It.With.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
    public class When_outputting_logs_to_xunit_using_the_nlog_capturer : XUnit2Specification
    {
        private readonly ILogger _logger = LogFactory.Create();
        private OutputRecorder _outputRecorder;
        private IDisposable _subscription;

        public When_outputting_logs_to_xunit_using_the_nlog_capturer(ITestOutputHelper output) : base(output)
        {
        }

        protected override void Given()
        {
            _outputRecorder = new OutputRecorder(TestOutputHelper);
            _subscription = NLogCapturer.Capture(_outputRecorder);
        }

        protected override void When()
        {
            _logger.Info("Logging");
        }

        [Fact]
        public void It_should_have_recorded_logging()
        {
            _outputRecorder.Logs.Should().Count.Exactly(1);
        }

        [Fact]
        public void It_should_have_recorded_the_logging()
        {
            _outputRecorder.Logs.Should().Contain.Item("Logging");
        }

        protected override void Dispose(bool disposing)
        {
            _subscription.Dispose();
            base.Dispose(disposing);
        }
    }
}
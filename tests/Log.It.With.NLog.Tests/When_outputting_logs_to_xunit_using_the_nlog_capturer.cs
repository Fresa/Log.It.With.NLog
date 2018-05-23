using System.Collections.Generic;
using Should.Fluent;
using Test.It.With.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
    public class When_outputting_logs_to_xunit_using_the_nlog_capturer : XUnit2Specification
    {
        private readonly ILogger _logger = LogFactory.Create<When_outputting_logs_to_xunit_using_the_nlog_capturer>();
        private OutputRecorder _outputRecorder;

        public When_outputting_logs_to_xunit_using_the_nlog_capturer(ITestOutputHelper output) : base(output)
        {
        }

        protected override void Given()
        {
            _outputRecorder = new OutputRecorder(TestOutputHelper);
            var subscription = NLogCapturer.Capture(_outputRecorder);

            OnDisposing += (sender, args) => subscription.Dispose();
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
    }

    internal class OutputRecorder : IOutput
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OutputRecorder(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public List<string> Logs { get; } = new List<string>();

        public void Write(string message)
        {
            Logs.Add(message);
            _testOutputHelper.WriteLine(message);
        }
    }
}
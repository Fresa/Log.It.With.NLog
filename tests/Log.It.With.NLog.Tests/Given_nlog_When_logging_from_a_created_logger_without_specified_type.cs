using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
    public partial class Given_nlog
    {
        public class When_logging_from_a_created_logger_without_specified_type : XUnit2SpecificationWithNLog
        {
            private ILogger _logger;
           
            public When_logging_from_a_created_logger_without_specified_type(ITestOutputHelper output) : base(output)
            {
            }

            protected override void Given()
            {
                _logger = LogFactory.Create();
            }

            protected override void When()
            {
                _logger.Info("Logging");
            }

            [Fact]
            public void It_should_have_writen_the_correct_logger_name()
            {
                MessagesWrittenToOutput.Should().Contain("Log.It.With.NLog.Tests.Given_nlog+When_logging_from_a_created_logger_without_specified_type: Logging");
            }
        }
    }
}
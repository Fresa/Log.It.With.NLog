using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
    public partial class Given_logical_thread_context
    {
        public class When_rendering_as_string : XUnit2SpecificationWithNLog
        {
            private NLogLogger _logger;
            private LoggingFixture _fixture;
            private TestTarget _target;

            public When_rendering_as_string(ITestOutputHelper output) : base(output)
            {
            }

            protected override void Given()
            {
                _fixture = new LoggingFixture()
                    .WithLayoutRenderer<NLogLogContextLayoutRenderer>("lc");
                _target = _fixture.CreateTarget<TestTarget>("item1=${lc:key=item1}, item2=${lc:key=item2}");

                _logger = _fixture.CreateLogger();
                _logger.LogicalThread.Set("item1", new TestContextObject("value1"));
            }

            protected override void When()
            {
                _logger.Info("Logging");
            }

            [Fact]
            public void It_should_have_writen_item1_but_not_item2()
            {
                _target.MessagesWritten.Should().Contain("item1=value1, item2=");
            }

            protected override void Dispose(bool disposing)
            {
                _fixture.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
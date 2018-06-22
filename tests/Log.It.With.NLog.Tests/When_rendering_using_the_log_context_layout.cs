using Should.Fluent;
using Xunit;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
    public class When_rendering_using_the_log_context_layout : XUnit2SpecificationWithNLog
    {
        private NLogLogger _logger;
        
        public When_rendering_using_the_log_context_layout(ITestOutputHelper output) : base(output)
        {
        }

        protected override void Given()
        {
            _logger = new NLogLogger("TestType", new LogicalThreadContext());
            _logger.LogicalThread.Set("item1", new ContextObject("value1"));
        }

        protected override void When()
        {
            _logger.Info("Logging");
        }

        [Fact]
        public void It_should_have_writen_item1_but_not_item2()
        {
            TestTarget.MessagesWritten.Should().Contain.Item("item1=value1, item2=");
        }
    }

    class ContextObject
    {
        public object Value { get; }

        public ContextObject(object value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
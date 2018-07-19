namespace Log.It.With.NLog.Tests
{
    internal class TestContextObject
    {
        public object Value { get; }

        public TestContextObject(object value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Log.It.With.NLog.Tests
{
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
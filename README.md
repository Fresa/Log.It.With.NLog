# Log.It.With.NLog
Log.It.With.NLog is a simple logging framework for NLog based on <a href="https://github.com/Fresa/Log.It" target="_blank">Log.It</a>.

[![Build status](https://ci.appveyor.com/api/projects/status/09dgsheetq6ldg9u?svg=true)](https://ci.appveyor.com/project/Fresa/log-it-with-nlog)

[![Build history](https://buildstats.info/appveyor/chart/Fresa/log-it-with-nlog)](https://ci.appveyor.com/project/Fresa/log-it-with-nlog/history)

## Download
https://www.nuget.org/packages/Log.It.With.NLog/1.0.0

## Getting Started
See <a href="https://github.com/Fresa/Log.It" target="_blank">Log.It</a>. To use the supplied NLog based `ILoggerFactory` use `NLogLoggerFactory` when initializing `LogFactory`.

### ~~Config File Example~~
~~`NLogLoggerFactory`, which implements `ILoggerFactory`, can be specified as following.~~
~~<configuration>
  <configSections>
    <section name="Logging" type="Log.It.LoggingSection, Log.It" />
  </configSections>
  <Logging Factory="Log.It.With.NLog.NLogLoggerFactory, Log.It.With.NLog" />
</configuration>~~

## Usage
See <a href="https://github.com/Fresa/Log.It" target="_blank">Log.It</a>.

## Logging Capturing
If you like to capture logging during a specific context (maybe you are running multiple unit tests concurrently and would like to capture logging per test scenario), you can use `NLogCapturer`. When calling `Capture(IOutput output)` it will start monitoring and outputing log output occuring during the current capture session. It will return an `IDisposable` object which will stop the capture session when called.

## Logical Logging Context 
`NLogLoggerFactory` uses `LogicalThreadContext` which is based on `System.Runtime.Remoting.Messaging.CallContext` to store information throughout the call context. You can also use this call context implementation if you'd like to roll your own logger factory.

## Layout Renderer for the Logical Logging Context
To use what ever information you store through the logical context instance, you can use the `NLogLogContextLayoutRenderer` as a custom layout renderer. In your layout you'll use the `lc` layout name to target the renderer followed by the key you want to render.

Example: See <a href="https://github.com/Fresa/Log.It.With.NLog/blob/master/tests/Log.It.With.NLog.Tests/When_rendering_using_the_log_context_layout.cs" target="_blank">When_rendering_using_the_log_context_layout</a>

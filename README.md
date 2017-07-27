# Log.It.With.NLog
Log.It.With.NLog is a simple logging framework for NLog based on <a href="https://github.com/Fresa/Log.It" target="_blank">Log.It</a>.

## Download
https://www.nuget.org/packages/Log.It.With.NLog/1.0.0

## Getting Started
See <a href="https://github.com/Fresa/Log.It" target="_blank">Log.It</a>. To use the supplied NLog based `ILoggerFactory` use `NLogLoggerFactory`.

### Config File Example
`NLogLoggerFactory`, which implements `ILoggerFactory`, can be specified as following.
```
<configuration>
  <configSections>
    <section name="Logging" type="Log.It.LoggingSection, Log.It" />
  </configSections>
  <Logging Factory="Log.It.With.NLog.NLogLoggerFactory, Log.It.With.NLog" />
</configuration>
```

## Usage
See <a href="https://github.com/Fresa/Log.It" target="_blank">Log.It</a>.

## Logical Logging Context 
`NLogLoggerFactory` uses `LogicalThreadContext` which is based on `System.Runtime.Remoting.Messaging.CallContext` to store information throughout the call context. You can also use this call context implementation if you'd like to roll your own logger factory.

## Layout Renderer for the Logical Logging Context
To use what ever information you store through the logical context instance, you can use the `NLogLogContextLayoutRenderer` as a custom layout renderer. In your layout you'll use the `lc` layout name to target the renderer followed by the key you want to render.

Example: See <a href="https://github.com/Fresa/Log.It.With.NLog/blob/master/tests/Log.It.With.NLog.Tests/When_rendering_using_the_log_context_layout.cs" target="_blank">When_rendering_using_the_log_context_layout</a>

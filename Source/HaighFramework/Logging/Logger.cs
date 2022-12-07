using BearsEngine.Tools;

namespace BearsEngine.Logging;

public class Logger : ILogger
{
    private readonly List<ILoggerOutputStream> _outputStreams = new();
    private readonly IMessageFormatter _messageFormatter = new MessageFormatter();

    public Logger()
    {
    }

    public void AddOutputStream(ILoggerOutputStream output)
    {
        _outputStreams.Add(output);
    }

    public void Debug(object? thingToLog)
    {
        Log(LogLevel.Debug, thingToLog);
    }

    public void Error(object? thingToLog)
    {
        Log(LogLevel.Error, thingToLog);
    }

    public void Fatal(object? thingToLog)
    {
        Log(LogLevel.Fatal, thingToLog);
    }

    public void Information(object? thingToLog)
    {
        Log(LogLevel.Information, thingToLog);
    }

    public void Log(LogLevel logLevel, object? thingToLog)
    {
        if (logLevel == LogLevel.None)
            throw new ArgumentException($"Cannot write log messages with {LogLevel.None}", nameof(logLevel));

        foreach (var stream in _outputStreams)
            if (logLevel >= stream.LogLevel)
                stream.Write(_messageFormatter.FormatToString(thingToLog));
    }

    public void RemoveAllOutputStreams()
    {
        _outputStreams.Clear();
    }

    public void RemoveOutputStream(ILoggerOutputStream output)
    {
        _outputStreams.Remove(output);
    }

    public void Verbose(object? thingToLog)
    {
        Log(LogLevel.Verbose, thingToLog);
    }

    public void Warning(object? thingToLog)
    {
        Log(LogLevel.Warning, thingToLog);
    }
}

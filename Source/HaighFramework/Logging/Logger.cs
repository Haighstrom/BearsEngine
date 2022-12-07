using BearsEngine.Tools;

namespace BearsEngine.Logging;

public class Logger : ILogger
{
    private readonly ILoggingStringConverter _stringConverter = new LoggingStringConverter();
    private readonly List<ILoggerOutputStream> _outputStreams = new();

    public Logger()
    {
    }

    public void AddOutputStream(ILoggerOutputStream output)
    {
        _outputStreams.Add(output);
    }

    public void Log(LogLevel logLevel, object? thingToLog)
    {
        if (logLevel == LogLevel.None)
            throw new ArgumentException($"Cannot write log messages with {LogLevel.None}", nameof(logLevel));

        foreach (var stream in _outputStreams)
            if (logLevel >= stream.LogLevel)
                stream.Write(_stringConverter.ConvertToLoggableString(thingToLog));
    }

    public void RemoveAllOutputStreams()
    {
        _outputStreams.Clear();
    }

    public void RemoveOutputStream(ILoggerOutputStream output)
    {
        _outputStreams.Remove(output);
    }
}

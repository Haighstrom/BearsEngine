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

    public void Log(LogLevel logLevel, object thingToLog)
    {
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

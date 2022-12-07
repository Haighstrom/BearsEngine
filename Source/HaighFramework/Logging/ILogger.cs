namespace BearsEngine.Logging;

public interface ILogger
{
    void AddOutputStream(ILoggerOutputStream output);

    void Log(LogLevel logLevel, object? thingToLog);

    void RemoveAllOutputStreams();

    void RemoveOutputStream(ILoggerOutputStream output);
}
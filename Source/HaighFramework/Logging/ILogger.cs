namespace BearsEngine.Logging;

public interface ILogger
{
    void AddOutputStream(ILoggerOutputStream output);

    void Debug(object? thingToLog);

    void Error(object? thingToLog);

    void Fatal(object? thingToLog);

    void Information(object? thingToLog);

    void Log(LogLevel logLevel, object? thingToLog);

    void RemoveAllOutputStreams();

    void RemoveOutputStream(ILoggerOutputStream output);

    void Verbose(object? thingToLog);

    void Warning(object? thingToLog);
}
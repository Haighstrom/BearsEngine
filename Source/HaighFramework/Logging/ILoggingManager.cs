namespace BearsEngine.Logging;

public interface ILoggingManager
{
    LogLevel ThrowErrorsFromLevel { get; set; }

    void Log(LogLevel logLevel, object? thingToLog);

    void Verbose(object? thingToLog);
    void Debug(object? thingToLog);
    void Information(object? thingToLog);
    void Warning(object? thingToLog);
    void Error(object? thingToLog);
    void Fatal(object? thingToLog);

    void AddConsoleLogging(LogLevel consoleLoggingLevel);

    void AddFileLogging(FileWriteSettings writeSettings);
}
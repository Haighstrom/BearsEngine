namespace BearsEngine.Logging;

public interface ILoggingManager
{
    LogLevel ThrowErrorsFromLevel { get; set; }

    void Log(LogLevel logLevel, object thingToLog);

    void AddLoggingConsole(IConsoleManager console, LogLevel consoleLoggingLevel);

    void AddLoggingFile(string filePath, LogLevel fileLoggingLevel);
}
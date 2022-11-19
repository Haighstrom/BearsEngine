namespace BearsEngine.Logging;

public class LoggingManager : ILoggingManager
{
    public LogLevel ThrowErrorsFromLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void AddLoggingConsole(IConsoleManager console, LogLevel consoleLoggingLevel)
    {
        throw new NotImplementedException();
    }

    public void AddLoggingFile(string filePath, LogLevel fileLoggingLevel)
    {
        throw new NotImplementedException();
    }

    public void Log(LogLevel logLevel, object thingToLog)
    {
        throw new NotImplementedException();
    }
}
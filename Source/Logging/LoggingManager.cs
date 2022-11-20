namespace BearsEngine.Logging;

public class LoggingManager : ILoggingManager
{
    private readonly ILogger _logger = new Logger();

    public LoggingManager()
    {
    }

    public LogLevel ThrowErrorsFromLevel { get; set; } = LogLevel.None;

    public void AddConsoleLogging(LogLevel consoleLoggingLevel)
    {
        _logger.AddOutputStream(new ConsoleOutputStream(consoleLoggingLevel));
    }

    public void AddFileLogging(FileWriteSettings writeSettings)
    {
        if (writeSettings.OverwritePreviousFiles && HaighIO.FileExists(writeSettings.FilePath))
        {
            HaighIO.DeleteFile(writeSettings.FilePath);
        }

        _logger.AddOutputStream(new FileOutputStream(writeSettings.FilePath, writeSettings.LogLevel));
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
        _logger.Log(logLevel, thingToLog);
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
namespace BearsEngine.Logging;

public static class Log
{
    private static readonly ILogger _logger = new Logger();

    public static void AddConsoleLogging(LogLevel consoleLoggingLevel)
    {
        _logger.AddOutputStream(new ConsoleOutputStream(consoleLoggingLevel));
    }

    public static void AddFileLogging(FileWriteSettings writeSettings)
    {
        if (writeSettings.OverwritePreviousFiles && HaighIO.FileExists(writeSettings.FilePath))
        {
            HaighIO.DeleteFile(writeSettings.FilePath);
        }

        _logger.AddOutputStream(new FileOutputStream(writeSettings.FilePath, writeSettings.LogLevel));
    }

    public static void Debug(object? thingToLog)
    {
        Write(LogLevel.Debug, thingToLog);
    }

    public static void Error(object? thingToLog)
    {
        Write(LogLevel.Error, thingToLog);
    }

    public static void Fatal(object? thingToLog)
    {
        Write(LogLevel.Fatal, thingToLog);
    }

    public static void Information(object? thingToLog)
    {
        Write(LogLevel.Information, thingToLog);
    }

    public static void Write(LogLevel logLevel, object? thingToLog)
    {
        _logger.Log(logLevel, thingToLog);
    }

    public static void Verbose(object? thingToLog)
    {
        Write(LogLevel.Verbose, thingToLog);
    }

    public static void Warning(object? thingToLog)
    {
        Write(LogLevel.Warning, thingToLog);
    }
}

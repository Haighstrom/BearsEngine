namespace BearsEngine.Logging;

/// <summary>
/// A simple static entry point for default logging.
/// </summary>
public static class Log
{
    /// <summary>
    /// The default Logger. Will write only to the Console for messages at or above level <see cref="LogLevel.Information"/> by default if not replaced with another Logger.
    /// </summary>
    public static ILogger Logger { get; set; } = new Logger(new ConsoleOutputStream(LogLevel.Information));

    /// <summary>
    /// If messages are written to the default logger (i.e. using <see cref="Log"/>) with priority equal to or higher than this level, an <see cref="LoggingException"/> will be thrown. Off by default.
    /// </summary>
    public static LogLevel LogLevelToThrowErrors { get; set; } = LogLevel.None;

    /// <summary>
    /// Write a Debug level message to the default logger.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public static void Debug(object? thingToLog) => Write(LogLevel.Debug, thingToLog);

    /// <summary>
    /// Write an Error level message to the default logger.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public static void Error(object? thingToLog) => Write(LogLevel.Error, thingToLog);

    /// <summary>
    /// Write a Fatal level message to the default logger.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public static void Fatal(object? thingToLog) => Write(LogLevel.Fatal, thingToLog);

    /// <summary>
    /// Write an Information level message to the default logger.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public static void Information(object? thingToLog) => Write(LogLevel.Information, thingToLog);

    /// <summary>
    /// Write a message to the default logger.
    /// </summary>
    /// <param name="logLevel">The log level of the message.</param>
    /// <param name="thingToLog">The object to be logged.</param>
    /// <exception cref="ArgumentException">Throws an exception if <see cref="LogLevel.None"/> is used</exception>
    public static void Write(LogLevel logLevel, object? thingToLog)
    {
        Logger.Write(logLevel, thingToLog);

        if (logLevel >= LogLevelToThrowErrors)
            throw new LoggingException(logLevel, thingToLog);
    }

    /// <summary>
    /// Write a Verbose level message to the default logger.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public static void Verbose(object? thingToLog) => Write(LogLevel.Verbose, thingToLog);

    /// <summary>
    /// Write a Warning level message to the default logger.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public static void Warning(object? thingToLog) => Write(LogLevel.Warning, thingToLog);
}

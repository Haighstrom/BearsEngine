namespace BearsEngine.Logging;

/// <summary>
/// A type for writing log messages.
/// </summary>
public interface ILogger
{
    LogLevel DefaultLogLevel { get; set; }


    /// <summary>
    /// Write a Verbose level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    void Verbose(object? thingToLog);

    /// <summary>
    /// Write a Debug level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    void Debug(object? thingToLog);

    /// <summary>
    /// Write an Information level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    void Information(object? thingToLog);

    /// <summary>
    /// Write a Warning level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    void Warning(object? thingToLog);

    /// <summary>
    /// Write a Error level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    void Error(object? thingToLog);

    /// <summary>
    /// Write a Fatal level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    void Fatal(object? thingToLog);

    /// <summary>
    /// Write a message to the logger's output stream(s).
    /// </summary>
    /// <param name="logLevel">The log level of the message.</param>
    /// <param name="thingToLog">The object to be logged.</param>
    /// <param name="suppressMetaInfo">Specifies whether addition info (time stamp, log message level) should be suppressed for this message, if relevant.</param>
    /// <exception cref="ArgumentException">Throws an exception if <see cref="LogLevel.None"/> is used</exception>
    void Write(LogLevel logLevel, object? thingToLog, bool suppressMetaInfo = false);

    /// <summary>
    /// Write a message to the logger's output stream(s), using DefaultLevel log level.
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    /// <param name="suppressMetaInfo">Specifies whether addition info (time stamp, log message level) should be suppressed for this message, if relevant.</param>
    /// <exception cref="ArgumentException">Throws an exception if <see cref="LogLevel.None"/> is used</exception>
    void Write(object? thingToLog, bool suppressMetaInfo = false);

    void WriteSectionHeader(LogLevel logLevel, string headerName);

    void WriteSectionHeader(string headerName);

    void WriteNewLine(LogLevel logLevel);

    void WriteNewLine();

    void WriteSectionBreak(LogLevel logLevel);

    void WriteSectionBreak();
}

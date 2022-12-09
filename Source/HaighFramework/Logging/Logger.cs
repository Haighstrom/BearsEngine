using BearsEngine.Tools;

namespace BearsEngine.Logging;

/// <summary>
/// Default class for providing logging functionality.
/// </summary>
public class Logger : ILogger
{
    private readonly ILoggerMessageFormatter _messageFormatter = new LoggerMessageFormatter();
    private readonly IEnumerable<ILoggerOutputStream> _outputStreams;

    public Logger(LogSettings settings)
    {
        List<ILoggerOutputStream> streams = new();

        streams.Add(new ConsoleOutputStream(settings.ConsoleLogLevel));

        foreach (var fileWriteSettings in settings.FileLogging)
        {
            if (fileWriteSettings.OverwritePreviousFiles && HaighIO.FileExists(fileWriteSettings.FilePath))
            {
                HaighIO.DeleteFile(fileWriteSettings.FilePath);
            }

            streams.Add(new FileOutputStream(fileWriteSettings.FilePath, fileWriteSettings.LogLevel));
        }

        _outputStreams = streams;
    }

    public Logger(params ILoggerOutputStream[] outputStreams)
    {
        _outputStreams = outputStreams;
    }

    /// <summary>
    /// Write a Debug level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public void Debug(object? thingToLog) => Write(LogLevel.Debug, thingToLog);

    /// <summary>
    /// Write a Error level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public void Error(object? thingToLog) => Write(LogLevel.Error, thingToLog);

    /// <summary>
    /// Write a Fatal level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public void Fatal(object? thingToLog) => Write(LogLevel.Fatal, thingToLog);

    /// <summary>
    /// Write an Information level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public void Information(object? thingToLog) => Write(LogLevel.Information, thingToLog);

    /// <summary>
    /// Write a message to the logger's output stream(s).
    /// </summary>
    /// <param name="logLevel">The log level of the message.</param>
    /// <param name="thingToLog">The object to be logged.</param>
    /// <exception cref="ArgumentException">Throws an exception if <see cref="LogLevel.None"/> is used</exception>
    public void Write(LogLevel logLevel, object? thingToLog)
    {
        if (logLevel == LogLevel.None)
            throw new ArgumentException($"Cannot write log messages with {LogLevel.None}.", nameof(logLevel));

        foreach (var stream in _outputStreams)
            if (logLevel >= stream.LogLevel)
                stream.Write(_messageFormatter.FormatToLoggingString(thingToLog));
    }

    /// <summary>
    /// Write a Verbose level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public void Verbose(object? thingToLog) => Write(LogLevel.Verbose, thingToLog);

    /// <summary>
    /// Write a Warning level message to the logger's output stream(s).
    /// </summary>
    /// <param name="thingToLog">The object to be logged.</param>
    public void Warning(object? thingToLog) => Write(LogLevel.Warning, thingToLog);
}

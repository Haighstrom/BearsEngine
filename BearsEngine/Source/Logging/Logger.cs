using System.IO;

namespace BearsEngine.Logging;

/// <summary>
/// Default class for providing logging functionality.
/// </summary>
public class Logger : ILogger
{
    private const int SectionBreakLength = 50;

    private readonly ILoggerMessageFormatter _messageFormatter = new LoggerMessageFormatter();
    private readonly IEnumerable<ILoggerOutputStream> _outputStreams;
    private readonly bool _includeLogLevelInMessages, _includeTimeStampInMessages;

    /// <summary>
    /// Create a default instance of an ILogger
    /// </summary>
    /// <param name="settings">The settings to be used.</param>
    public Logger(LogSettings settings)
    {
        _includeLogLevelInMessages = settings.IncludeLogLevelInMessages;
        _includeTimeStampInMessages = settings.IncludeTimeStampInMessages;

        List<ILoggerOutputStream> streams = new()
        {
            new ConsoleOutputStream(settings.ConsoleLogLevel)
        };

        foreach (var fileWriteSettings in settings.FileLogging)
        {
            if (fileWriteSettings.OverwritePreviousFiles && File.Exists(fileWriteSettings.FilePath))
            {
                File.Delete(fileWriteSettings.FilePath);
            }

            streams.Add(new FileOutputStream(fileWriteSettings.FilePath, fileWriteSettings.LogLevel));
        }

        _outputStreams = streams;
    }

    /// <summary>
    /// Create a default instance of an ILogger
    /// </summary>
    /// <param name="includeLogLevelInMessages">Whether the <see cref="LogLevel"/> of the message should be included when writing to the output stream(s).</param>
    /// <param name="includeTimeStampInMessages">Whether the current system time should be included in messages written to the output stream(s).</param>
    /// <param name="outputStreams">The streams that should be written to when logging with this logger.</param>
    public Logger(bool includeLogLevelInMessages, bool includeTimeStampInMessages, params ILoggerOutputStream[] outputStreams)
    {
        _includeLogLevelInMessages = includeLogLevelInMessages;
        _includeTimeStampInMessages = includeTimeStampInMessages;
        _outputStreams = outputStreams;
    }

    /// <summary>
    /// Create a default instance of an ILogger
    /// </summary>
    /// <param name="outputStreams">The streams that should be written to when logging with this logger.</param>
    public Logger(params ILoggerOutputStream[] outputStreams)
        : this(true, false, outputStreams)
    {
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
    /// <param name="suppressMetaInfo">Specifies whether addition info (time stamp, log message level) should be suppressed for this message, if relevant.</param>
    /// <exception cref="ArgumentException">Throws an exception if <see cref="LogLevel.None"/> is used</exception>
    public void Write(LogLevel logLevel, object? thingToLog, bool suppressMetaInfo = false)
    {
        if (logLevel == LogLevel.None)
            throw new ArgumentException($"Cannot write log messages with {LogLevel.None}.", nameof(logLevel));

        foreach (var stream in _outputStreams)
            if (logLevel >= stream.LogLevel)
            {
                string logString = "";

                if (_includeTimeStampInMessages && !suppressMetaInfo)
                {
                    logString += $"{DateTime.Now} ";
                }

                if (_includeLogLevelInMessages && !suppressMetaInfo)
                {
                    logString += logLevel switch
                    {
                        LogLevel.Verbose => "[Verbose] ",
                        LogLevel.Debug => "[Debug] ",
                        LogLevel.Information => "[Information] ",
                        LogLevel.Warning => "[Warning] ",
                        LogLevel.Error => "[Error] ",
                        LogLevel.Fatal => "[Fatal] ",
                        _ => throw new NotImplementedException(),
                    };
                }

                logString += _messageFormatter.FormatToLoggingString(thingToLog);

                stream.Write(logString);
            }
    }

    public void WriteSectionHeader(LogLevel logLevel, string headerName)
    {
        int charsEitherSide = Math.Max(1, (SectionBreakLength - headerName.Length) / 2);
        Write(logLevel, new string('-', charsEitherSide) + headerName + new string('-', charsEitherSide), true);
    }

    public void WriteNewLine(LogLevel logLevel)
    {
        Write(logLevel, Environment.NewLine, true);
    }

    public void WriteSectionBreak(LogLevel logLevel)
    {
        Write(logLevel, new string('-', SectionBreakLength), true);
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

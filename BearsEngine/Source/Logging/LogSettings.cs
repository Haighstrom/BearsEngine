namespace BearsEngine.Logging;

/// <summary>
/// Settings to be applied to static logger <see cref="Log"/>.
/// </summary>
[Serializable]
public class LogSettings
{
    /// <summary>
    /// The default settings for a logger.
    /// </summary>
    public static LogSettings Default => new();

    /// <summary>
    /// The minimum level of log messages that the Console should display.
    /// </summary>
    public LogLevel ConsoleLogLevel { get; set; } = LogLevel.Information;

    /// <summary>
    /// Whether the <see cref="LogLevel"/> of the message should be included when writing to the output stream(s). True by default.
    /// </summary>
    public bool IncludeLogLevelInMessages { get; set; } = true;

    /// <summary>
    /// Whether the current system time should be included in messages written to the output stream(s). False by default.
    /// </summary>
    public bool IncludeTimeStampInMessages { get; set; } = false;

    /// <summary>
    /// Settings for any file writing that should be done (one <see cref="FileWriteSettings"/> should be added for each file to be written to).
    /// </summary>
    public List<FileWriteSettings> FileLogging { get; set; } = new();
}

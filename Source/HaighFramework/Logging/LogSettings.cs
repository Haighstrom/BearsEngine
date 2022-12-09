namespace BearsEngine.Logging;

/// <summary>
/// Settings for writing a file from an <see cref="ILogger"/>
/// </summary>
/// <param name="FilePath">The path of the file to be written</param>
/// <param name="LogLevel">The minimum level of log messages to be written</param>
/// <param name="OverwritePreviousFiles">Whether any previous log file of the same name should be removed before commencing logging</param>
[Serializable]
public record FileWriteSettings(string FilePath, LogLevel LogLevel, bool OverwritePreviousFiles);

/// <summary>
/// Settings to be applied to static logger <see cref="Log"/>.
/// </summary>
[Serializable]
public class LogSettings
{
    /// <summary>
    /// The minimum level of log messages that the Console should display.
    /// </summary>
    public LogLevel ConsoleLogLevel { get; set; } = LogLevel.None;

    /// <summary>
    /// Settings for any file writing that should be done (one <see cref="FileWriteSettings"/> should be added for each file to be written to).
    /// </summary>
    public List<FileWriteSettings> FileLogging { get; set; } = new();
}
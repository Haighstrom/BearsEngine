namespace BearsEngine.Logging;

/// <summary>
/// Settings for writing a file from an <see cref="ILogger"/>
/// </summary>
/// <param name="FilePath">The path of the file to be written</param>
/// <param name="LogLevel">The minimum level of log messages to be written</param>
/// <param name="OverwritePreviousFiles">Whether any previous log file of the same name should be removed before commencing logging</param>
[Serializable]
public record FileWriteSettings(string FilePath, LogLevel LogLevel, bool OverwritePreviousFiles);

namespace BearsEngine.Logging;

[Serializable]
public class LogSettings
{
    public LogLevel ConsoleLogLevel { get; set; } = LogLevel.None;

    public List<FileWriteSettings> FileLogging { get; set; } = new();
}
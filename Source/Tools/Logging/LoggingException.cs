namespace BearsEngine.Logging;

[Serializable]
public class LoggingException : Exception
{
    public LoggingException(LogLevel logLevel, string logMessage)
        : base($"Exception triggered by a log message of level [{logLevel}], with message [{logMessage}].")
    {
        LogLevel = logLevel;
        LogMessage = logMessage;
    }

    public LogLevel LogLevel { get; }

    public string LogMessage { get; }
}

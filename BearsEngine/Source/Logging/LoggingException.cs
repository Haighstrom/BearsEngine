namespace BearsEngine.Logging;

/// <summary>
/// An exception thrown when asking a logger to throw Exceptions due to the level of log message being requested.
/// </summary>
[Serializable]
public class LoggingException : Exception
{
    public LoggingException(LogLevel logLevel, object? thingBeingLogged)
        : base($"Exception triggered by a log message of level [{logLevel}], with message [{thingBeingLogged}].")
    {
        LogLevel = logLevel;
        ThingBeingLogged = thingBeingLogged;
    }

    /// <summary>
    /// The level of the message that triggered the exception.
    /// </summary>
    public LogLevel LogLevel { get; }

    /// <summary>
    /// The object being logged when the exception was thrown.
    /// </summary>
    public object? ThingBeingLogged { get; }
}

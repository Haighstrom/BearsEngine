namespace BearsEngine.Logging;

/// <summary>
/// A stream for writing <see cref="ILogger"/> messages to the console
/// </summary>
public class ConsoleOutputStream : ILoggerOutputStream
{
    public ConsoleOutputStream(LogLevel logLevel)
    {
        LogLevel = logLevel;
    }

    /// <summary>
    /// The minimum level of messages which are being written.
    /// </summary>
    public LogLevel LogLevel { get; }

    /// <summary>
    /// Write a message to the console.
    /// </summary>
    /// <param name="message">The message.</param>
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}
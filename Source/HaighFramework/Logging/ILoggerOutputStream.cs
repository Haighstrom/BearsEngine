namespace BearsEngine.Logging;

/// <summary>
/// An output location for an <see cref="ILogger"/> to write to.
/// </summary>
public interface ILoggerOutputStream
{
    /// <summary>
    /// The minimum level of messages which are being written.
    /// </summary>
    public LogLevel LogLevel { get; }

    /// <summary>
    /// Write a message to this output location.
    /// </summary>
    /// <param name="message">The message.</param>
    public void Write(string message);
}
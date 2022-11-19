namespace BearsEngine.Logging;

public interface ILoggerOutputStream
{
    public LogLevel LogLevel { get; }

    public void Write(string message);
}
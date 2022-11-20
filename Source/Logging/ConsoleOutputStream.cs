namespace BearsEngine.Logging;

public class ConsoleOutputStream : ILoggerOutputStream
{
    public ConsoleOutputStream(LogLevel logLevel)
    {
        LogLevel = logLevel;
    }

    public LogLevel LogLevel { get; set; }

    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}
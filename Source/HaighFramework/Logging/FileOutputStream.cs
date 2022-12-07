namespace BearsEngine.Logging;

public class FileOutputStream : ILoggerOutputStream
{
    public FileOutputStream(string outputFile, LogLevel logLevel)
    {
        LogLevel = logLevel;
        OutputFile = outputFile;
    }

    public string OutputFile { get; }

    public LogLevel LogLevel { get; set; }

    public void Write(string message)
    {
        HaighIO.AppendText(OutputFile, message + Environment.NewLine);
    }
}
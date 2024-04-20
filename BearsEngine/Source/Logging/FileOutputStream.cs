using System.IO;

namespace BearsEngine.Logging;

/// <summary>
/// A stream for writing <see cref="ILogger"/> messages to a file.
/// </summary>
public class FileOutputStream : ILoggerOutputStream
{
    /// <summary>
    /// Create a stream for writing <see cref="ILogger"/> messages to a file.
    /// </summary>
    /// <param name="outputFile">The file path to write to.</param>
    /// <param name="logLevel">The minimum level of messages to be logged.</param>
    public FileOutputStream(string outputFile, LogLevel logLevel)
    {
        LogLevel = logLevel;
        OutputFile = outputFile;
    }

    /// <summary>
    /// The path to the file being written to.
    /// </summary>
    public string OutputFile { get; }

    /// <summary>
    /// The minimum level of messages which are being written.
    /// </summary>
    public LogLevel LogLevel { get; }

    /// <summary>
    /// Write a message to the output file.
    /// </summary>
    /// <param name="message">The message.</param>
    public void Write(string message)
    {
        string directory = new FileInfo(OutputFile).Directory!.FullName;

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        File.AppendAllText(OutputFile, message + Environment.NewLine);
    }
}

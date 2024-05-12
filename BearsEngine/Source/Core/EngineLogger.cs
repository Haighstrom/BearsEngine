using System.Runtime.InteropServices;
using BearsEngine.Displays;

namespace BearsEngine.Source.Core;

internal class EngineLogger
{
    private readonly ILogger _logger;

    public EngineLogger(ILogger logger)
    {
        _logger = logger;
    }

    public void LogIntro()
    {
        _logger.WriteSectionBreak();
        _logger.WriteSectionBreak();
        _logger.WriteSectionHeader("Welcome to BearsEngine!");
        _logger.WriteSectionBreak();
        _logger.WriteSectionBreak();
        _logger.WriteNewLine();
        _logger.WriteSectionHeader("Initialising");
    }

    public void LogSystemInformation()
    {
        _logger.WriteNewLine();
        _logger.WriteSectionHeader("System Information");

        _logger.Write(
            $"Machine: {Environment.MachineName}" +
            $"\nOS: {RuntimeInformation.OSDescription}" +
            $"\nUser: {Environment.UserName}" +
            $"\nProcessors: {Environment.ProcessorCount}" +
            $"\nSystem Architecture: {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}" +
            $"\nProcess Arcitecture: {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}", true);

        _logger.WriteSectionBreak();
        _logger.WriteNewLine();
    }

    public void LogDisplaysInformation(IDisplayManager displays)
    {
        _logger.WriteSectionHeader("Display Devices");

        foreach (var display in displays.AvailableDisplays)
        {
            _logger.Write(display, true);
        }

        _logger.WriteSectionBreak();
        _logger.WriteNewLine();
    }

    public void LogOutro()
    {
        _logger.WriteSectionBreak();
        _logger.WriteSectionHeader($"BearsEngine Initialised");
        _logger.WriteSectionBreak();
        _logger.WriteNewLine();
    }
}

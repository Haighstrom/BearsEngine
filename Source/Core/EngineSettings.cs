using BearsEngine.Window;
using Serilog.Events;

namespace BearsEngine;

public class EngineSettings : WindowSettings
{
    public static EngineSettings Default => new();

    public int TargetFramesPerSecond { get; set; } = 60;
    public int TargetUPS { get; set; } = 60;

    public bool ShowDebugConsole { get; set; } = false;
    public bool LeftAlignDebugConsole { get; set; } = true; //todo: change to Gridposition and Size or something?
    public LogEventLevel DebugLoggingLevel { get; set; } = LogEventLevel.Information;

}
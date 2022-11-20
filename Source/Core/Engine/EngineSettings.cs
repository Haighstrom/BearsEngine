using BearsEngine.Logging;
using BearsEngine.Window;

namespace BearsEngine;

public class GameSettings
{
    public static GameSettings Default => new();

    public ConsoleSettings ConsoleSettings { get; set; } = new();

    public LogSettings LogSettings { get; set; } = new();

    public int TargetFramesPerSecond { get; set; } = 60;

    public int TargetUPS { get; set; } = 60;

    public WindowSettings WindowSettings { get; set; } = new();
}
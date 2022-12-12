using HaighFramework.Window;

namespace BearsEngine;

public class GameSettings
{
    public static GameSettings Default => new();

    public ConsoleSettings ConsoleSettings { get; set; } = new();

    public EngineSettings EngineSettings { get; set; } = new();

    public LogSettings LogSettings { get; set; } = new();

    public WindowSettings WindowSettings { get; set; } = new();
}
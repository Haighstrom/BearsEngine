namespace BearsEngine;

public class GameLoopSettings
{
    public static GameLoopSettings Default => new();

    public int TargetFramesPerSecond { get; set; } = 60;

    public int TargetUPS { get; set; } = 60;
}

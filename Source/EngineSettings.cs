namespace BearsEngine;

public class EngineSettings
{
    public static EngineSettings Default => new();

    public int TargetFPS { get; set; } = 60;
    public int TargetUPS { get; set; } = 60;
}
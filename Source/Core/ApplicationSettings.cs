using BearsEngine.Source.Tools.IO;

namespace BearsEngine.Source.Core;

public class ApplicationSettings
{
    public static ApplicationSettings Default => new();

    public ConsoleSettings ConsoleSettings { get; set; } = ConsoleSettings.Default;
    public EngineSettings EngineSettings { get; set; } = EngineSettings.Default;
    public LogSettings LogSettings { get; set; } = LogSettings.Default;
    public IoSettings IoSettings { get; set; } = IoSettings.Default;
    public WindowSettings WindowSettings { get; set; } = WindowSettings.Default;
}
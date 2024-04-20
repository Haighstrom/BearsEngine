using BearsEngine.Source.Tools.IO;

namespace BearsEngine.Source.Core;

public class ApplicationSettings
{
    public static ApplicationSettings Default => new();

    public ConsoleSettings ConsoleSettings { get; init; } = ConsoleSettings.Default;

    public EngineSettings EngineSettings { get; init; } = EngineSettings.Default;

    public IoSettings IoSettings { get; init; } = IoSettings.Default;

    public LogSettings LogSettings { get; init; } = LogSettings.Default;

    public WindowSettings WindowSettings { get; init; } = WindowSettings.Default;
}
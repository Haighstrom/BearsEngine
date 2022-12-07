namespace BearsEngine.Logging;

public class ConsoleSettings
{
    public bool ShowConsoleWindow { get; set; } = System.Diagnostics.Debugger.IsAttached;

    public int X { get; set; } = -7;

    public int Y { get; set; } = 0;

    public int Width { get; set; } = 450;

    public int Height { get; set; } = ConsoleManager.GetMaxSize().Height;
}
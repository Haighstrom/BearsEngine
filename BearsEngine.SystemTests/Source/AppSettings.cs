using BearsEngine.Window;

namespace BearsEngine.SystemTests.Source;

internal static class AppSettings
{
    public static ApplicationSettings Get() => new()
    {
        LogSettings = new()
        {
            ConsoleLogLevel = LogLevel.Debug,
            FileLogging = new List<FileWriteSettings>()
            {
                new("Log.txt", LogLevel.Debug, true)
            },
        },

        WindowSettings = new()
        {
            Width = 800,
            Height = 600,
            Centre = true,
            Title = "BearsEngine System Tests",
            Border = BorderStyle.NonResizable,
        },
    };
}

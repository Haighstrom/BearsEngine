using BearsEngine.Source.Core;
using BearsEngine.Window;

namespace BearsEngine.SystemTests.Source.Setup;

internal static class Initialiser
{
    public static ApplicationSettings GetApplicationSettings()
    {
        return new()
        {

            LogSettings = new()
            {
                ConsoleLogLevel = LogLevel.Debug,
                FileLogging = new List<FileWriteSettings>()
                {
                    new FileWriteSettings("Log.txt", LogLevel.Debug, true)
                },
            },
            WindowSettings = new()
            {
                Width = 800,
                Height = 600,
                Centre = true,
                Title = "BearsEngine Tester",
                Border = BorderStyle.NonResizable,
            },
        };
    }

    public static IScene CreateFirstScene()
    {
        return new MenuScreen();
    }
}

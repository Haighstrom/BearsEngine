using BearsEngine.Source.Core;
using BearsEngine.Window;

namespace BearsEngine.SystemTests.Source.Setup;

internal class Initialiser : IAppInitialiser
{
    private const string LogSettingsFilePath = "LogSettings.json";

    public ApplicationSettings GetApplicationSettings()
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
                Border = BearsEngine.Window.BorderStyle.NonResizable,
            },
        };
    }

    public void Initialise()
    {
    }

    public IScene CreateFirstScene()
    {
        return new MenuScreen();
    }
}

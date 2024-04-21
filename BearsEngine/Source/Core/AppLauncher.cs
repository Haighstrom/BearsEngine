using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Source.Tools.IO;
using BearsEngine.Window;

namespace BearsEngine.Source.Core;

internal class AppLauncher
{
    public AppLauncher()
    {
    }

    public void Launch(ApplicationSettings appSettings, Func<IScene> getFirstScene)
    {
        var consoleWindow = new ConsoleWindow(appSettings.ConsoleSettings);

        var logger = new Logger(appSettings.LogSettings);

        var ioHelper = new IOHelper(appSettings.IoSettings);

        var displayManager = new DisplayManager();

        var window = new HaighWindow(appSettings.WindowSettings);

        var engine = new GameEngine(appSettings.EngineSettings, getFirstScene);

        engine.Run(window);

        engine.Dispose();
    }

}

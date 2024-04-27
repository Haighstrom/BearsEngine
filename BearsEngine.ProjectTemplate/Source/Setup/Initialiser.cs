using BearsEngine.Input;
using BearsEngine.ProjectTemplate.Source.Globals;
using BearsEngine.Source.Core;

namespace BearsEngine.ProjectTemplate.Source.Setup;

/// <summary>
/// Used for all setup code, e.g. instantiating complex data (or data that requires a render context to exist before it can be created), saving map editor settings, etc, and finally assigning the first Screen to be used
/// </summary>
internal class Initialiser
{
    private const string LogSettingsFilePath = "LogSettings.json";

    public static ApplicationSettings GetApplicationSettings()
    {
        return new()
        {
            LogSettings = Files.ReadJsonFile<LogSettings>(LogSettingsFilePath),

            WindowSettings = new()
            {
                Width = (int)GP.DefaultClientSize.X,
                Height = (int)GP.DefaultClientSize.Y,
                Centre = true,
                Title = typeof(GV).Namespace!.ToString().Split('.')[0],
            },
        };
    }

    public static IScene CreateFirstScene(IMouse mouse)
    {
        return new Screen(mouse);
    }
}
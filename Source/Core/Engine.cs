using HaighFramework.Window;
using HaighFramework.Displays;
using HaighFramework.Input;

namespace BearsEngine;

public static class Engine
{
    private static IGameEngine? _instance;
    private static bool _runCalled;

    private static IGameEngine Instance
    {
        get
        {
            if (!_runCalled)
                throw new InvalidOperationException($"You must call {nameof(Engine)}.{nameof(Run)} before using other static members of {nameof(Engine)}");

            if (_instance is null)
                throw new NullReferenceException("_engine was accessed when null");

            return _instance;
        }

        set => _instance = value;
    }

    /// <summary>
    /// The current Scene. Set this value to change to a new Scene or Screen.
    /// </summary>
    public static IScene Scene
    {
        get => Instance.Scene;
        set => Instance.Scene = value;
    }

    /// <summary>
    /// Closes the application.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws if Engine has not been created.</exception>
    public static void Exit()
    {
        if (Instance is null)
            throw new InvalidOperationException($"You must call {nameof(Run)} before using other members of {nameof(Engine)}");

        Window.Exit();
    }

    /// <summary>
    /// Starts the application.
    /// </summary>
    /// <param name="consoleSettings">Define settings for the Console.</param>
    /// <param name="logSettings">Define settings for logging.</param>
    /// <param name="windowSettings">Define the settings for the Window.</param>
    /// <param name="engineSettings">Define the settings for the engine.</param>
    /// <param name="initialiser">A function to initialise the application and start a Scene.</param>
    /// <exception cref="InvalidOperationException">Throws if Engine has not been created.</exception>
    public static void Run(ConsoleSettings consoleSettings, LogSettings logSettings, WindowSettings windowSettings, EngineSettings engineSettings, Func<IScene> initialiser)
    {
        if (_runCalled)
            throw new InvalidOperationException($"It is not permissible to call {nameof(Engine)}.{nameof(Run)} twice.");

        _runCalled = true;

        Console.Instance = new ConsoleManager(consoleSettings);

        Log.Instance = new Logger(logSettings);

        Displays.Instance = new DisplayManager();

        Window.Instance = new HaighWindow(windowSettings);

        Instance = new GameEngine(engineSettings, initialiser);
        Instance.Run();
        Instance.Dispose();
    }

    /// <summary>
    /// Starts the application with default Console Settings.
    /// </summary>
    /// <param name="logSettings">Define settings for logging.</param>
    /// <param name="windowSettings">Define the settings for the Window.</param>
    /// <param name="engineSettings">Define the settings for the engine.</param>
    /// <param name="initialiser">A function to initialise the application and start a Scene.</param>
    /// <exception cref="InvalidOperationException">Throws if Engine has not been created.</exception>
    public static void Run(LogSettings logSettings, WindowSettings windowSettings, EngineSettings engineSettings, Func<IScene> initialiser) => Run(ConsoleSettings.Default, logSettings, windowSettings, engineSettings, initialiser);

    /// <summary>
    /// Starts the application with default console and logging settings.
    /// </summary>
    /// <param name="windowSettings">Define the settings for the Window.</param>
    /// <param name="engineSettings">Define the settings for the engine.</param>
    /// <param name="initialiser">A function to initialise the application and start a Scene.</param>
    /// <exception cref="InvalidOperationException">Throws if Engine has not been created.</exception>
    public static void Run(WindowSettings windowSettings, EngineSettings engineSettings, Func<IScene> initialiser) => Run(ConsoleSettings.Default, LogSettings.Default, windowSettings, engineSettings, initialiser);

    /// <summary>
    /// Starts the application with default console, logging and engine settings.
    /// </summary>
    /// <param name="windowSettings">Define the settings for the Window.</param>
    /// <param name="initialiser">A function to initialise the application and start a Scene.</param>
    /// <exception cref="InvalidOperationException">Throws if Engine has not been created.</exception>
    public static void Run(WindowSettings windowSettings, Func<IScene> initialiser) => Run(ConsoleSettings.Default, LogSettings.Default, windowSettings, EngineSettings.Default, initialiser);

    /// <summary>
    /// Starts the application with default settings.
    /// </summary>
    /// <param name="initialiser">A function to initialise the application and start a Scene.</param>
    /// <exception cref="InvalidOperationException">Throws if Engine has not been created.</exception>
    public static void Run(Func<IScene> initialiser) => Run(ConsoleSettings.Default, LogSettings.Default, WindowSettings.Default, EngineSettings.Default, initialiser);
}
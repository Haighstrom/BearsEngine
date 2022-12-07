using BearsEngine.DisplayDevices;
using BearsEngine.Input;
using BearsEngine.Logging;
using BearsEngine.Win32API;
using BearsEngine.Window;

namespace BearsEngine;

public static class BE
{
    private static IEngine? _engine;
    private static bool _runCalled;

    private static IEngine Engine
    {
        get
        {
            if (!_runCalled)
                throw new InvalidOperationException($"You must call {nameof(Run)} before using other static members of {nameof(BE)}");

            if (_engine is null)
                throw new NullReferenceException("_engine was accessed when null");

            return _engine;
        }

        set => _engine = value;
    }

    internal static uint LastBoundFrameBuffer { get; set; }

    internal static uint LastBoundShader { get; set; }

    internal static uint LastBoundTexture { get; set; }

    internal static uint LastBoundVertexBuffer { get; set; }

    internal static Matrix4 OrthoMatrix { get; set; }

    internal static Dictionary<string, Texture> TextureDictionary { get; set; } = new();

    public static IConsoleManager Console { get; } = new ConsoleManager();

    public static bool RunWhenUnfocussed { get; set; } = true;

    public static IScene Scene
    {
        get => Engine.Scene;
        set => Engine.Scene = value;
    }

    public static IWindow Window => Engine.Window;

    public static void Exit()
    {
        if (Engine is null)
            throw new InvalidOperationException($"You must call {nameof(Run)} before using other members of {nameof(BE)}");

        Engine.Window.Exit();
    }

    public static void Run(GameSettings settings, Func<IScene> initialiser)
    {
        if (_runCalled)
            throw new InvalidOperationException($"It is not permissible to call {nameof(BE)}.{nameof(Run)} twice.");

        _runCalled = true;

        if (settings.ConsoleSettings.ShowConsoleWindow)
        {
            Console.ShowConsole(settings.ConsoleSettings.X, settings.ConsoleSettings.Y, settings.ConsoleSettings.Width, settings.ConsoleSettings.Height);
        }

        Log.AddConsoleLogging(settings.LogSettings.ConsoleLogLevel);

        foreach (var writeLogSettings in settings.LogSettings.FileLogging)
            Log.AddFileLogging(writeLogSettings);

        IDisplayDeviceManager displayManager = new DisplayDeviceManager();
        IInputDeviceManager inputManager = new InputDeviceManager();
        IWindow window = new HaighWindow(settings.WindowSettings);

        Engine = new Engine(displayManager, inputManager, window, settings.EngineSettings, initialiser);
        Engine.Run();
        Engine.Dispose();
    }
}
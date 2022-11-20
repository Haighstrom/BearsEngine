using BearsEngine.Logging;
using BearsEngine.Win32API;
using BearsEngine.Window;

namespace BearsEngine;

public static class BE
{
    private static bool _runCalled;
    private static IEngine? _engine;

    internal static uint LastBoundFrameBuffer { get; set; }

    internal static uint LastBoundShader { get; set; }

    internal static uint LastBoundTexture { get; set; }

    internal static uint LastBoundVertexBuffer { get; set; }

    internal static Matrix4 OrthoMatrix { get; set; }

    internal static Dictionary<string, Texture> TextureDictionary { get; set; } = new();

    public static bool RunWhenUnfocussed { get; set; } = true;

    public static ILoggingManager Logging { get; } = new LoggingManager();

    public static IConsoleManager Console { get; } = new ConsoleManager();

    public static IScene Scene
    {
        get
        {
            if (_engine is null)
                throw new InvalidOperationException($"You must call {nameof(Run)} before using other static members of {nameof(BE)}");

            return _engine.Scene;
        }
        set
        {
            if (_engine is null)
                throw new InvalidOperationException($"You must call {nameof(Run)} before using other static members of {nameof(BE)}");

            _engine.Scene = value;
        }
    }

    public static IWindow Window
    {
        get
        {
            if (_engine is null)
                throw new InvalidOperationException($"You must call {nameof(Run)} before using other static members of {nameof(BE)}");

            return _engine.Window;
        }
    }

    public static void Run(GameSettings settings, Func<IScene> initialiser)
    {
        if (_runCalled)
            throw new InvalidOperationException($"It is not permissible to call {nameof(BE)}.{nameof(Run)} twice.");

        _runCalled = true;

        if (settings.ShowConsole)
        {
            Console.ShowConsole(settings.ConsoleSettings);
        }

        Logging.AddConsoleLogging(settings.LogSettings.ConsoleLogLevel);

        foreach (var writeLogSettings in settings.LogSettings.FileLogging)
            Logging.AddFileLogging(writeLogSettings);

        _engine = new BEngine(settings, initialiser);
        _engine.Run();
        _engine.Dispose();
    }

    public static void Exit()
    {
        if (_engine is null)
            throw new InvalidOperationException($"You must call {nameof(Run)} before using other members of {nameof(BE)}");

        _engine.Window.Exit();
    }
}
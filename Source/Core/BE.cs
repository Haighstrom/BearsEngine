using BearsEngine.Win32API;
using BearsEngine.Window;
using Serilog;

namespace BearsEngine;

public static class BE
{
    private const float MinFrameTime = 0.5f;

    private static bool _runCalled;
    private static IEngine? _engine;

    internal static uint LastBoundFrameBuffer { get; set; }

    internal static uint LastBoundShader { get; set; }

    internal static uint LastBoundTexture { get; set; }

    internal static uint LastBoundVertexBuffer { get; set; }

    internal static Matrix4 OrthoMatrix { get; set; }

    internal static Dictionary<string, Texture> TextureDictionary { get; set; } = new();

    private static void EnableSerilog(bool writeToConsole, Serilog.Events.LogEventLevel logLevel)
    {
        var loggerconfig = new LoggerConfiguration();

        if (writeToConsole)
            loggerconfig.WriteTo.Console();

        switch (logLevel)
        {
            case Serilog.Events.LogEventLevel.Verbose:
                loggerconfig.MinimumLevel.Verbose();
                break;
            case Serilog.Events.LogEventLevel.Debug:
                loggerconfig.MinimumLevel.Debug();
                break;
            case Serilog.Events.LogEventLevel.Information:
                loggerconfig.MinimumLevel.Information();
                break;
            case Serilog.Events.LogEventLevel.Warning:
                loggerconfig.MinimumLevel.Warning();
                break;
            case Serilog.Events.LogEventLevel.Error:
                loggerconfig.MinimumLevel.Error();
                break;
            case Serilog.Events.LogEventLevel.Fatal:
                loggerconfig.MinimumLevel.Fatal();
                break;
            default:
                throw new System.ComponentModel.InvalidEnumArgumentException();
        }

        Log.Logger = loggerconfig.CreateLogger();

        Log.Information("---{0} Logging Enabled---", "Serilog");
    }

    private static void ShowConsole(bool leftAlign)
    {
        Kernal32.AllocConsole();

        if (leftAlign)
            HConsole.MoveConsoleTo(-7, 0, 450, HConsole.MaxHeight);
    }

    public static bool RunWhenUnfocussed { get; set; } = true;

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

    public static void Run(EngineSettings settings, Func<IScene> initialiser)
    {
        if (_runCalled)
            throw new InvalidOperationException($"It is not permissible to call {nameof(BE)}.{nameof(Run)} twice.");

        _runCalled = true;

        if (settings.ShowDebugConsole)
        {
            ShowConsole(settings.LeftAlignDebugConsole);
        }

        EnableSerilog(settings.ShowDebugConsole, settings.DebugLoggingLevel);

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
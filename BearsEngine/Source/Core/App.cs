using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.Window;

namespace BearsEngine;

public class App : IApp, IDisposable
{
    private bool _isDisposed;
    private readonly ApplicationSettings _appSettings;

    private readonly Mouse _mouse;
    private readonly Keyboard _keyboard;
    private readonly IGameEngine _engine;

    public App(ApplicationSettings appSettings)
    {
        _appSettings = appSettings;

        Logger = new Logger(_appSettings.LogSettings);

        Console = new ConsoleWindow(_appSettings.ConsoleSettings);

        Displays = new DisplayManager();

        Window = new HaighWindow(_appSettings.WindowSettings);

        _mouse = new Mouse(Window);

        _keyboard = new Keyboard();

        _engine = new GameEngine(Window, _mouse, _keyboard, _appSettings.EngineSettings);
    }

    public ILogger Logger { get; } //inject this?

    public IConsoleWindow Console { get; }

    public IWindow Window { get; }

    public IMouse Mouse => _mouse;

    public IKeyboard Keyboard => _keyboard;

    public IDisplayManager Displays { get; }

    public void Run(IScene firstScene)
    {
        Window.Visible = true;

        _engine.Run(firstScene);

        _engine.Dispose();
    }

    public void ChangeScene(IScene scene)
    {
        _engine.Scene = scene;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _isDisposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~App()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

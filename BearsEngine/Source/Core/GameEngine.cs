using System.Diagnostics;
using System.Runtime.InteropServices;
using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.Source.Core;
using BearsEngine.Window;

namespace BearsEngine;

public class GameEngine : IGameEngine
{
    private const double TimeBetweenFrameCountUpdates = 0.1; //how often frames are counted to average
    private const int FrameCountArraySize = 10; //how many captures frame counts are averaged over
    private const double TimeBetweenPeriodicUpdates = 1;

    private bool _disposed = false;
    private readonly int _targetUPS, _targetRPS;
    private readonly ISceneManager _sceneManager;
    private readonly IMouseInternal _mouse;
    private readonly IKeyboardInternal _keyboard;
    private readonly IInputReader _inputReader;

    public GameEngine(ApplicationSettings appSettings)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("Only Windows is currently supported.");
        }

        var componentFactory = new GameEngineComponentFactory();

        Logger = componentFactory.CreateLogger(appSettings.LogSettings);

        Console = componentFactory.CreateConsole(appSettings.ConsoleSettings); //create console first so other setup info can display

        var engineLogger = new EngineLogger(Logger);

        engineLogger.LogIntro();

        engineLogger.LogSystemInformation();

        Displays = componentFactory.CreateDisplayManager(Logger);

        engineLogger.LogDisplaysInformation(Displays);

        Window = componentFactory.CreateWindow(appSettings.WindowSettings);

        _inputReader = componentFactory.CreateInputReader();

        _mouse = componentFactory.CreateMouse(Window);

        _keyboard = componentFactory.CreateKeyboard();

        _sceneManager = componentFactory.CreateSceneManager();

        new GameLoopSettingsValidator().ValidateEngineSettings(appSettings.GameLoopSettings);

        _targetUPS = appSettings.GameLoopSettings.TargetUPS;
        _targetRPS = appSettings.GameLoopSettings.TargetFramesPerSecond;

        OpenGLHelper.Viewport(Window.Viewport);
        OpenGLHelper.OrthoMatrix = Matrix3.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);

        Window.Resized += OnWindowResize;

        engineLogger.LogOutro();
    }

    public bool KeyboardUpdatesWhenWindowUnfocussed { get; set; } = false;

    public bool RunWhenUnfocussed { get; set; } = true;

    public int RenderFramesPerSecond { get; private set; }

    public IScene Scene => _sceneManager.CurrentScene;

    public int UpdateFramesPerSecond { get; private set; }

    public ILogger Logger { get; }

    public IDisplayManager Displays { get; }

    public IConsoleWindow Console { get; }

    public IWindow Window { get; }

    public IMouse Mouse => _mouse;

    public IKeyboard Keyboard => _keyboard;

    private void LogPeriodicInfo()
    {
        Logger.Information($"FPS: {UpdateFramesPerSecond}, RPS: {RenderFramesPerSecond}");
    }

    private void OnWindowResize(object? sender, EventArgs e)
    {
        OpenGLHelper.Viewport(Window.Viewport);
        OpenGLHelper.OrthoMatrix = Matrix3.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);
    }

    private void Render()
    {
        Matrix3 projection = new(OpenGLHelper.OrthoMatrix.Values);

        var identity = Matrix3.Identity;

        Scene.Render(ref projection, ref identity);

        Window.SwapBuffers();
    }

    private void Update(float elapsedTime)
    {
        _mouse.Update(_inputReader.MouseState); //caution about changing this to avoid keys getting stuck down etc

        if (KeyboardUpdatesWhenWindowUnfocussed || Window.Focussed)
        {
            _keyboard.Update(_inputReader.KeyboardState);
        }

        if (RunWhenUnfocussed || Window.Focussed)
        {
            _sceneManager.UpdateScene(elapsedTime);
        }
    }

    public void ChangeScene(IScene scene)
    {
        _sceneManager.ChangeScene(scene);
    }

    public void Run(IScene firstScene)
    {
        Window.Visible = true;

        _sceneManager.ChangeScene(firstScene);

        Window.ProcessEvents(); //get any initial crap out the way before we start timing

        double targetUpdateTime = 1.0 / _targetUPS;
        double targetRenderTime = 1.0 / _targetRPS;

        var timer = new Stopwatch();
        timer.Start();

        double previousTime = timer.Elapsed.TotalSeconds;
        double timeSinceLastUpdate = 0, timeSinceLastRender = 0;

        //for tracking UPS and RPS
        double frameCounterTime = 0;
        int[] updateFramesCounter = new int[FrameCountArraySize + 1];
        int[] renderFramesCounter = new int[FrameCountArraySize + 1];

        double periodicLoggingTimer = 0; //for logging things once per second

        while (Window.IsOpen)
        {
            Window.ProcessEvents();

            double currentTime = timer.Elapsed.TotalSeconds;
            double elapsed = currentTime - previousTime;
            previousTime = currentTime;

            timeSinceLastUpdate += elapsed;
            timeSinceLastRender += elapsed;
            frameCounterTime += elapsed;
            periodicLoggingTimer += elapsed;

            if (timeSinceLastUpdate >= targetUpdateTime)
            {
                //usually we want update to call a frame time of exactly targetUpdateTime to achieve the target frames per second, and keep hold of any remaining time towards the next update
                //but if the game is running slowly it will fall behind and never catch up, so we need to run a long frame, which may as well be all the time built up

                double timeOfFrame;

                if (timeSinceLastUpdate > 2 * targetUpdateTime)
                {
                    timeOfFrame = timeSinceLastUpdate;
                    Logger.Warning($"A frame took {timeSinceLastUpdate / targetUpdateTime: 0.00}x as long as expected.");
                }
                else
                {
                    timeOfFrame = targetUpdateTime;
                }

                if (Window.IsOpen)
                {
                    Update((float)timeOfFrame);
                }

                timeSinceLastUpdate -= timeOfFrame;

                updateFramesCounter[0]++;
            }

            if (timeSinceLastRender >= targetRenderTime)
            {
                if (Window.IsOpen)
                {
                    Render();
                }

                while (timeSinceLastRender >= targetRenderTime)
                    timeSinceLastRender -= targetRenderTime;

                renderFramesCounter[0]++;
            }
        }
        while (frameCounterTime >= TimeBetweenFrameCountUpdates)
        {
            frameCounterTime -= TimeBetweenFrameCountUpdates;

            Array.Copy(updateFramesCounter, 0, updateFramesCounter, 1, FrameCountArraySize);
            updateFramesCounter[0] = 0;
            UpdateFramesPerSecond = (int)(updateFramesCounter.Skip(1).Sum() * FrameCountArraySize * TimeBetweenFrameCountUpdates);

            Array.Copy(renderFramesCounter, 0, renderFramesCounter, 1, FrameCountArraySize);
            renderFramesCounter[0] = 0;
            RenderFramesPerSecond = (int)(renderFramesCounter.Skip(1).Sum() * FrameCountArraySize * TimeBetweenFrameCountUpdates);
        }

        if (periodicLoggingTimer >= TimeBetweenPeriodicUpdates)
        {
            while (periodicLoggingTimer >= TimeBetweenPeriodicUpdates)
            {
                periodicLoggingTimer -= TimeBetweenPeriodicUpdates;
            }

            LogPeriodicInfo();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Scene.Dispose(); //last scene will never have been disposed by SceneManager, since that only disposes when changing scene
                //AppDisplays.Instance.Dispose(); MOVE TO APPLICATION LAYER
                //InputManager.Dispose(); MOVE TO APPLICATION LAYER
                //AppWindow.Instance.Dispose(); MOVE TO APPLICATION LAYER
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~BEngine()
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

﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using BearsEngine.Input;
using BearsEngine.Window;
using HaighFramework.Displays;
using HaighFramework.OpenGL;

namespace BearsEngine;

internal class Engine : IEngine
{
    private const int MinimumUpdateRate = 5;
    private const int MaximumUpdateRate = 120;
    private const int MinimumRenderRate = 5;
    private const int MaximumRenderRate = 120;
    private const double TimeBetweenFrameCountUpdates = 0.1; //how often frames are counted to average
    private const int FrameCountArraySize = 10; //how many captures frame counts are averaged over
    private const double TimeBetweenPeriodicUpdates = 1;

    private static void ValidateEngineSettings(EngineSettings settings)
    {
        if (settings.TargetUPS < MinimumUpdateRate || settings.TargetUPS > MaximumUpdateRate)
        {
            int newRate = Maths.Clamp(settings.TargetUPS, MinimumUpdateRate, MaximumUpdateRate);

            Log.Warning($"Requested an update rate of {settings.TargetUPS}, which is outside the bounds of the allowed values ({MaximumUpdateRate}-{MinimumUpdateRate}). Adjusting to {newRate}.");

            settings.TargetUPS = newRate;
        }

        if (settings.TargetFramesPerSecond < MinimumRenderRate || settings.TargetFramesPerSecond > MaximumRenderRate)
        {
            int newRate = Maths.Clamp(settings.TargetFramesPerSecond, MinimumRenderRate, MaximumRenderRate);

            Log.Warning($"Requested a render rate of {settings.TargetFramesPerSecond}, which is outside the bounds of the allowed values ({MinimumRenderRate}-{MaximumRenderRate}). Adjusting to {newRate}.");

            settings.TargetFramesPerSecond = newRate;
        }
    }

    private bool _disposed = false;
    private readonly int _targetUPS, _targetRPS;
    private readonly ISceneManager _sceneManager;

    public Engine(IDisplayManager displayManager, IInputDeviceManager inputManager, IWindow window, EngineSettings settings, Func<IScene> initialiser)
    {
        Log.Debug($"Initialising {nameof(Engine)}.");

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            throw new InvalidOperationException("Only Windows is currently supported.");

        DisplayManager = displayManager;
        InputManager = inputManager;
        Window = window;

        Log.Debug($"Environment Information:\nMachine: {Environment.MachineName}\nOS: {RuntimeInformation.OSDescription}\nUser: {Environment.UserName}\nProcessors: {Environment.ProcessorCount}\nSystem Architecture: {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}\nProcess Arcitecture: {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}");

        _sceneManager = new SceneManager(initialiser());

        //Window.MouseLeftDoubleClicked += (o, a) => HI.MouseLeftDoubleClicked = true;

        //OpenGL32.DebugMessageCallback(HConsole.HandleOpenGLOutput);
        //OpenGL32.DebugMessageControl(DebugSource.DontCare, DebugType.DontCare, DebugSeverity.DontCare, true);

        ValidateEngineSettings(settings);

        _targetUPS = settings.TargetUPS;
        _targetRPS = settings.TargetFramesPerSecond;

        OpenGL32.glViewport((int)window.Viewport.X, (int)window.Viewport.Y, (int)window.Viewport.W, (int)window.Viewport.H);
        BE.OrthoMatrix = Matrix4.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);

        Window.Resized += OnWindowResize;

        Log.Debug($"{nameof(Engine)} initialised.");
    }

    public IConsoleManager ConsoleManager => throw new NotImplementedException(); //todo: build these after removing logging from ctr

    public IDisplayManager DisplayManager { get; }

    public IInputDeviceManager InputManager { get; }

    public int RenderFramesPerSecond { get; private set; }

    public IScene Scene
    {
        get => _sceneManager.CurrentScene;
        set => _sceneManager.ChangeScene(value);
    }

    public int UpdateFramesPerSecond { get; private set; }

    public IWindow Window { get; }

    private void LogPeriodicInfo()
    {
        Log.Information($"FPS: {UpdateFramesPerSecond}, RPS: {RenderFramesPerSecond}");
    }

    private void OnWindowResize(object? sender, EventArgs e)
    {
        OpenGL32.glViewport((int)Window.Viewport.X, (int)Window.Viewport.Y, (int)Window.Viewport.W, (int)Window.Viewport.H);
        BE.OrthoMatrix = Matrix4.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);
    }

    private void Render()
    {
        OpenGL32.glClearColour(Scene.BackgroundColour);
        OpenGL32.glClear(CLEAR_MASK.GL_COLOR_BUFFER_BIT | CLEAR_MASK.GL_DEPTH_BUFFER_BIT);

        Matrix4 projection = new(BE.OrthoMatrix);
        Matrix4 identity = Matrix4.Identity;
        Scene.Render(ref projection, ref identity);

        Window.SwapBuffers();
    }

    private void Update(float elapsedTime)
    {
        HI.SetStates(InputManager.MouseManager.State, InputManager.KeyboardManager.State);

        if (BE.RunWhenUnfocussed || Window.Focussed)
        {
            _sceneManager.UpdateScene(elapsedTime);
        }
    }

    private double _testTime = 0;
    public void Run()
    {
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
                    Log.Warning($"A frame took {timeSinceLastUpdate / targetUpdateTime: 0.0}x as long as expected. Render took {_testTime / targetUpdateTime:0.0}x");
                }
                else
                {
                    timeOfFrame = targetUpdateTime;
                }

                if (Window.IsOpen)
                    Update((float)timeOfFrame);

                timeSinceLastUpdate -= timeOfFrame;

                updateFramesCounter[0]++;
            }

            if (timeSinceLastRender >= targetRenderTime)
            {
                _testTime = timer.Elapsed.TotalSeconds;
                if (Window.IsOpen)
                    Render();
                _testTime = timer.Elapsed.TotalSeconds - _testTime;

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
                periodicLoggingTimer -= TimeBetweenPeriodicUpdates;
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
                DisplayManager.Dispose();
                InputManager.Dispose();
                Window.Dispose();
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
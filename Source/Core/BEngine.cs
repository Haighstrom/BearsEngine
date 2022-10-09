using System.Diagnostics;
using BearsEngine.DisplayDevices;
using BearsEngine.Input;
using BearsEngine.Win32API;
using BearsEngine.Window;
using Serilog;

namespace BearsEngine;

internal class BEngine : IEngine
{
    private const float MinFrameTime = 0.5f;

    private bool _disposed = false;
    private readonly double _targetUpdateTime, _targetRenderTime;
    private readonly ISceneManager _sceneManager;

    public BEngine(EngineSettings settings, Func<IScene> initialiser)
    {
        Log.Debug("{0} being initialised.", nameof(BEngine));

        DisplayDM = new();
        InputDM = new();
        Window = new HaighWindow(settings);
        _sceneManager = new SceneManager(initialiser());

        _targetUpdateTime = 1.0 / settings.TargetUPS;
        _targetRenderTime = 1.0 / settings.TargetFPS;

        OpenGL32.Viewport(Window.Viewport);
        BE.OrthoMatrix = Matrix4.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);

        Window.Resized += OnWindowResize;

        Log.Debug("{0} initialised.", nameof(BEngine));
    }

    public DisplayDeviceManager DisplayDM { get; }

    public InputDeviceManager InputDM { get; }
    
    public IScene Scene
    {
        get => _sceneManager.CurrentScene;
        set => _sceneManager.ChangeScene(value);
    }

    public IWindow Window { get; }

    private void OnWindowResize(object? sender, EventArgs e)
    {
        OpenGL32.Viewport(Window.Viewport);
        BE.OrthoMatrix = Matrix4.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);
    }

    private void Render()
    {
        OpenGL32.ClearColour(Scene.BackgroundColour);
        OpenGL32.Clear(ClearBufferMask.ColourBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 projection = new(BE.OrthoMatrix);
        Matrix4 identity = Matrix4.Identity;
        Scene.Render(ref projection, ref identity);

        Window.SwapBuffers();
    }

    private void Update(float elapsedTime)
    {
        HI.SetStates(InputDM.MouseManager.State, InputDM.KeyboardManager.State);

        if (BE.RunWhenUnfocussed || Window.Focussed)
        {
            _sceneManager.UpdateScene(elapsedTime);
        }
    }


    public void Run()
    {
        var timer = new Stopwatch();
        timer.Start();

        double previousTime = timer.Elapsed.TotalSeconds;
        double updateTimeElapsed = 0, renderTimeElapsed = 0;

        while (Window.IsOpen)
        {
            Window.ProcessEvents();

            double currentTime = timer.Elapsed.TotalSeconds;
            double elapsed = currentTime - previousTime;
            previousTime = currentTime;

            updateTimeElapsed += elapsed;
            renderTimeElapsed += elapsed;

            if (updateTimeElapsed > _targetUpdateTime) //change to while ?
            {
                if (Window.IsOpen)
                    Update((float)Math.Min(updateTimeElapsed, MinFrameTime)); // running slowly things

                while (updateTimeElapsed > _targetUpdateTime) //run slowly warning?
                    updateTimeElapsed -= _targetUpdateTime;
            }

            if (renderTimeElapsed > _targetRenderTime)
            {
                if (Window.IsOpen)
                    Render();

                while (renderTimeElapsed > _targetRenderTime)
                    renderTimeElapsed -= _targetRenderTime;
            }
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
                DisplayDM.Dispose();
                InputDM.Dispose();
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
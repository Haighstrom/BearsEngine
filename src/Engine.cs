namespace BearsEngine;

using System.Diagnostics;
using HaighFramework.DisplayDevices;
using HaighFramework.Input;
using HaighFramework.Window;
using HaighFramework;
using HaighFramework.OpenGL4;

public sealed class Engine : IDisposable
{
    #region Static
    #region Instance
    private static Engine? _instance;

    internal static Engine Instance
    {
        get
        {
            if (_instance == null)
                throw new NullReferenceException($"Attempted to read {nameof(Engine)} information before {nameof(Engine)}.{nameof(Run)} was called. Call {nameof(Engine)}.{nameof(Run)} first.");

            return _instance;
        }
    }
    #endregion

    #region Run
    public static void Run(WindowSettings windowSettings, IScene scene, int ups = 60, int fps = 60)
    {
        _instance = new Engine(windowSettings, scene);
        _instance.Run(ups, fps);
        _instance.Dispose();
    }
    #endregion
    #endregion

    private const float MinFrameTime = 0.5f;

    #region Fields
    private bool _disposed;
    private IScene _scene, _nextScene;
    #endregion

    #region Constructors
    private Engine(WindowSettings windowSettings, IScene scene)
    {
#if DEBUG
        HConsole.Show();
        HConsole.MoveConsoleTo(-7, 0, 450, HConsole.MaxHeight);
#endif

        _scene = _nextScene = scene;
        DisplayDM = new();
        InputDM = new();
        Window = new HaighWindow2(windowSettings);
    }

    private void Keyboard_CharEntered(object? sender, KeyboardCharEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Properties
    #region Scene
    internal IScene Scene
    {
        get => _scene;
        set => _nextScene = value;
    }
    #endregion

    internal DisplayDeviceManager DisplayDM { get; }
    internal InputDeviceManager InputDM { get; }
    internal IWindow Window { get; }
    #endregion

    #region Run
    private void Run(int ups = 60, int fps = 60)
    {
        Window.Resized += (o, e) => Scene.OnResize();

        HV.OrthoMatrix = Matrix4.CreateOrtho(Window.ClientSize.X, Window.ClientSize.Y);

        _scene.Start();

        var timer = new Stopwatch();
        timer.Start();

        double targetUpdateTime = 1.0 / ups;
        double targetRenderTime = 1.0 / fps;
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

            if (updateTimeElapsed > targetUpdateTime) //change to while ?
            {
                if (Window.IsOpen)
                    Update(Math.Min(updateTimeElapsed, MinFrameTime)); // running slowly things

                while (updateTimeElapsed > targetUpdateTime) //run slowly warning?
                    updateTimeElapsed -= targetUpdateTime;
            }

            if (renderTimeElapsed > targetRenderTime)
            {
                if (Window.IsOpen)
                    Render();

                while (renderTimeElapsed > targetRenderTime)
                    renderTimeElapsed -= targetRenderTime;
            }
        }
        _scene.End();
    }
    #endregion

    #region Update
    private void Update(double elapsed)
    {
        HI.SetStates();

        if (_scene.Active)
            _scene.Update(elapsed);

        if (_nextScene != _scene)
        {
            _scene.End();
            _scene = _nextScene;
            _nextScene.Start();
        }
    }
    #endregion

    #region Render
    private void Render()
    {
        OpenGL.ClearColour(HV.ScreenColour);
        OpenGL.Clear(ClearBufferMask.ColourBufferBit | ClearBufferMask.DepthBufferBit);

        _scene.Render(ref HV.OrthoMatrix, ref Matrix4.Identity);
        Window.SwapBuffers();
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
        if (!_disposed)
        {
            DisplayDM.Dispose();
            InputDM.Dispose();
            Window.Dispose();
            _disposed = true;
        }
        else
            HConsole.Warning($"Dispose called twice on instance of {nameof(Engine)}");
    }
    #endregion
}
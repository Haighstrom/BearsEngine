using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using HaighFramework.DisplayDevices;
using HaighFramework.Input;
using HaighFramework.Window;
using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine
{
    public class HEngine : IEngine
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager();

        public static void Run(Point windowSize, bool resizable, string title, IScene scene, int fps)
        { 
            HV.Engine = new HEngine(windowSize, resizable, title, scene);
            HV.Engine.Run(fps);
        }

        #region Constants
        private const int MIN_RATE = 1;
        private const int MAX_RATE = 120;
        private const int DEFAULT_RATE = 60;
        private const int MAX_UPDATES_PER_FRAME = 10;
        #endregion

        #region Fields
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private IScene _scene;
        private IScene _nextScene;
        #endregion

        #region Constructors
        public HEngine(Point windowSize, bool resizable, string title, IScene scene)
            : this((int)windowSize.X, (int)windowSize.Y, resizable, title, scene)
        {
        }

        public HEngine(int windowWidth, int windowHeight, bool resizable, string title, IScene scene)
        {
            if (!HF.Windows.RunningOnWindows)
                throw new HException("Application is not running on Windows, which is not supported.");

            if (System.Environment.OSVersion.Version.Major < 5)
                throw new HException("BearsEngine requires Windows XP or higher"); //should be vista for dpi?

#if DEBUG
            HConsole.Show();
            HConsole.MoveConsoleTo(-7, 0, 450, HConsole.MaxHeight);
#endif

            HConsole.Log("----------Environment----------\n");
            HConsole.Log("Machine: {0}", System.Environment.MachineName);
            HConsole.Log("Platform: {0}", System.Environment.OSVersion.Platform);
            HConsole.Log("OS: {0}", System.Environment.OSVersion);
            HConsole.Log("User: {0}", System.Environment.UserName);
            HConsole.Log("Processors: {0}", System.Environment.ProcessorCount);
            HConsole.Log("System Architecture: {0}", System.Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit");
            HConsole.Log("Process Arcitecture: {0}", System.Environment.Is64BitProcess ? "64-bit" : "32-bit");
            HConsole.Log("\n-----------------------------\n"); 

            DisplayManager = new DisplayDeviceManager();
            _resourceManager.RegisterResource(DisplayManager);
            InputManager = new InputDeviceManager();
            _resourceManager.RegisterResource(InputManager);

            //Window = new OpenTKWindow(windowWidth, windowHeight, title, resizable, 4, 0);
            Window = new HaighWindow(0, 0, windowWidth, windowHeight, title, resizable, 4, 0);
            _resourceManager.RegisterResource(Window);
            Window.Centre();

            //Subscribe to OpenGL error and warning messages
            OpenGL.DebugMessageCallback(HConsole.HandleOpenGLOutput);
            OpenGL.DebugMessageControl(DebugSource.DontCare, DebugType.DontCare, DebugSeverity.DontCare, true);

            _scene = scene;

            Window.Resize += (object o, SizeEventArgs s) => Scene.OnResize(o, s);
            Window.MouseLeftDoubleClicked += (o, a) => HI.MouseLeftDoubleClicked = true;

            HV.OrthoMatrix = Matrix4.CreateOrtho(windowWidth, windowHeight);
        }
        #endregion

        #region IEngine
        public IScene Scene
        {
            get => _scene;
            set => _nextScene = value ?? throw new HException("Cannot set Scene to null. Use an empty BearsEngine.Worlds.World if required.");
        }

        public IDisplayDeviceManager DisplayManager { get; private set; }

        public IInputDeviceManager InputManager { get; private set; }

        public IWindow Window { get; set; }

        public bool DoPeriodicLogging { get; set; } = false;

        #region Run
        public void Run() => Run(DEFAULT_RATE);

        public void Run(int frameRate) => Run(frameRate, frameRate);

        public void Run(int updateRate, int renderRate)
        {
            if (updateRate < MIN_RATE || updateRate > MAX_RATE)
            {
                var revisedRate = HF.Maths.Clamp(updateRate, MIN_RATE, MAX_RATE);
                HConsole.Warning("Requested Update Rate {0} outside of valid bounds, automatically adjusting to {1}.", updateRate, revisedRate);
                updateRate = revisedRate;
            }

            if (renderRate < MIN_RATE || renderRate > MAX_RATE)
            {
                var revisedRate = HF.Maths.Clamp(renderRate, MIN_RATE, MAX_RATE);
                HConsole.Warning("Requested Update Rate {0} outside of valid bounds, automatically adjusting to {1}.", renderRate, revisedRate);
                renderRate = revisedRate;
            }

            Window.Visible = true;

            Scene.Start();
            StartedRunning(this, EventArgs.Empty);

            var s = new Stopwatch();
            s.Start();

            double targetRenderTime = 1.0 / renderRate;
            double targetUpdateTime = 1.0 / updateRate;
            double currentTime = 0;
            double previousTime = s.Elapsed.TotalSeconds;
            double elapsed = 0; //how much time elapsed since last loop

            double renderTimeBuiltUp = 0; // to compare against targetRenderTime
            double updateTimeBuiltUp = 0; // to compare against targetUpdateTime

            double updateTimeCounter = 0; //for tracking UPS
            int[] updateFramesCounter = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //for tracking UPS

            double renderTimeCounter = 0; //for tracking FPS
            int[] renderFramesCounter = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //for tracking FPS

            double periodicLoggingTimer = 0; //for logging things once per second

            int updatesInOneFrame = 0; //for ensuring slow updates don't queue up to infinity

            while (Window.IsRunning)
            {
                Window.ProcessEvents();

                currentTime = s.Elapsed.TotalSeconds;
                elapsed = currentTime - previousTime;
                previousTime = currentTime;

                updateTimeBuiltUp += elapsed;
                updateTimeCounter += elapsed;
                renderTimeBuiltUp += elapsed;
                renderTimeCounter += elapsed;
                periodicLoggingTimer += elapsed;

                updatesInOneFrame = 0;

                while (updateTimeBuiltUp > targetUpdateTime)
                {
                    if (Window.IsRunning)
                        Update(HV.ElapsedUpdateTime = (float)updateTimeBuiltUp);

                    updateFramesCounter[0]++;

                    updatesInOneFrame++;
                    if (updatesInOneFrame >= MAX_UPDATES_PER_FRAME)
                    {
                        HConsole.Warning("Game is running slowly.");
                        updateTimeBuiltUp = 0;
                        break;
                    }

                    while (updateTimeCounter > 0.1)
                    {
                        updateTimeCounter -= 0.1;
                        Array.Copy(updateFramesCounter, 0, updateFramesCounter, 1, 10);
                        updateFramesCounter[0] = 0;
                        UpdateFPS = updateFramesCounter.Skip(1).Sum();
                    }

                    updateTimeBuiltUp -= targetUpdateTime;
                }

                if (renderTimeBuiltUp > targetRenderTime)
                {
                    if (Window.IsRunning)
                        Render(HV.ElapsedRenderTime = renderTimeBuiltUp);
                        
                    renderFramesCounter[0]++;

                    while (renderTimeCounter > 0.1)
                    {
                        renderTimeCounter -= 0.1;
                        Array.Copy(renderFramesCounter, 0, renderFramesCounter, 1, 10);
                        renderFramesCounter[0] = 0;
                        RenderFPS = renderFramesCounter.Skip(1).Sum();
                    }

                    while (renderTimeBuiltUp > targetRenderTime)
                        renderTimeBuiltUp -= targetRenderTime;
                }

                if (periodicLoggingTimer >= 1.0)
                {
                    while (periodicLoggingTimer >= 1.0)
                        periodicLoggingTimer -= 1.0;

                    if (DoPeriodicLogging)
                        LogPeriodicInfo();
                }
            }

            Scene.End();
            OnClose();
        }
        #endregion

        public int UpdateFPS { get; private set; }

        public int RenderFPS { get; private set; }
        #endregion

        #region IDisposable
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool manual)
        {
            _resourceManager.Dispose();
        }
        ~HEngine()
        {
            Dispose(false);
            HConsole.Warning("BearsEngine exited without calling Dispose.");
        }
        #endregion

        #region Properties

        #endregion

        #region Methods
        #region Update
        protected virtual void Update(double elapsed)
        {
            HI.SetStates();

            if (Scene.Active)
                Scene.Update(elapsed);

            if (_nextScene != null)
            {
                _scene.End();

                _scene = _nextScene;
                _nextScene = null;

                _scene.Start();
            }

            HI.MouseLeftDoubleClicked = false;
        }
        #endregion

        #region Render
        protected virtual void Render(double elapsed)
        {
            Scene.Render(ref HV.OrthoMatrix, ref Matrix4.Identity);

            Window.SwapBuffers();
        }
        #endregion

        #region LogPeriodicInfo
        protected virtual void LogPeriodicInfo()
        {
            HConsole.Log("Updates: {0}ps, Renders: {1}ps.", UpdateFPS, RenderFPS);
        }
        #endregion

        public void Exit() => Window.Close();

        #region OnClose
        protected void OnClose() => Closing(this, EventArgs.Empty);
        #endregion

        #region Events
        public event EventHandler StartedRunning = delegate { };
        public event EventHandler Closing = delegate { };
        #endregion
        #endregion
    }
}

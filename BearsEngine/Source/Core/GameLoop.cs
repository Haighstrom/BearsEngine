using System.Diagnostics;
using BearsEngine.Window;

namespace BearsEngine.Source.Core;

internal class GameLoop : IGameLoop
{
    private const double TimeBetweenFrameCountUpdates = 0.1; //how often frames are counted to average
    private const int FrameCountArraySize = 10; //how many captures frame counts are averaged over
    private const double TimeBetweenLogs = 1;

    private readonly ILogger _logger;
    private readonly IWindow _window;
    private readonly double _targetUpdateTime;
    private readonly double _targetRenderTime;
    private readonly Stopwatch _stopwatch;

    public GameLoop(ILogger logger, IWindow window, int targetUPS, int targetRPS)
    {
        _logger = logger;
        _window = window;

        _targetUpdateTime = 1.0 / targetUPS;
        _targetRenderTime = 1.0 / targetRPS;

        _stopwatch = new Stopwatch();
    }

    public int UpdateFramesPerSecond { get; private set; }

    public int RenderFramesPerSecond { get; private set; }

    public event EventHandler<UpdateEventArgs>? UpdateTriggered;

    public event EventHandler<RenderEventArgs>? RenderTriggered;

    public event EventHandler<LogEventArgs>? LogTriggered;

    public void Run()
    {
        _window.ProcessEvents(); //get any initial crap out the way before we start timing

        _stopwatch.Restart();

        double previousTime = _stopwatch.Elapsed.TotalSeconds;
        double timeSinceLastUpdate = 0;
        double timeSinceLastRender = 0;

        //for tracking UPS and RPS
        double frameCounterTime = 0;
        int[] updateFramesCounter = new int[FrameCountArraySize + 1];
        int[] renderFramesCounter = new int[FrameCountArraySize + 1];

        double periodicLoggingTimer = 0; //for logging things once per second

        while (_window.IsOpen)
        {
            _window.ProcessEvents();

            double currentTime = _stopwatch.Elapsed.TotalSeconds;
            double elapsed = currentTime - previousTime;
            previousTime = currentTime;

            timeSinceLastUpdate += elapsed;
            timeSinceLastRender += elapsed;
            frameCounterTime += elapsed;
            periodicLoggingTimer += elapsed;

            if (timeSinceLastUpdate >= _targetUpdateTime)
            {
                //usually we want update to call a frame time of exactly targetUpdateTime to achieve the target frames per second, and keep hold of any remaining time towards the next update
                //but if the game is running slowly it will fall behind and never catch up, so we need to run a long frame, which should report all the time built up

                double timeOfFrame;

                if (timeSinceLastUpdate > 2 * _targetUpdateTime)
                {
                    timeOfFrame = timeSinceLastUpdate;
                    _logger.Warning($"A frame took {timeSinceLastUpdate / _targetUpdateTime: 0.00}x as long as expected.");
                }
                else
                {
                    timeOfFrame = _targetUpdateTime;
                }

                if (_window.IsOpen)
                {
                    RaiseUpdate((float)timeOfFrame);
                }

                timeSinceLastUpdate -= timeOfFrame;

                updateFramesCounter[0]++;
            }

            if (timeSinceLastRender >= _targetRenderTime)
            {
                //With rendering, we just want to render once if it's time to - no matter how long we've waited

                if (_window.IsOpen)
                {
                    RaiseRender();
                }

                while (timeSinceLastRender >= _targetRenderTime)
                {
                    timeSinceLastRender -= _targetRenderTime;
                }

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

        if (periodicLoggingTimer >= TimeBetweenLogs)
        {
            RaiseLog();

            while (periodicLoggingTimer >= TimeBetweenLogs)
            {
                periodicLoggingTimer -= TimeBetweenLogs;
            }
        }
    }

    private void RaiseUpdate(float timeOfFrame)
    {
        UpdateTriggered?.Invoke(this, new UpdateEventArgs(timeOfFrame));
    }

    private void RaiseRender()
    {
        RenderTriggered?.Invoke(this, new RenderEventArgs());
    }

    private void RaiseLog()
    {
        LogTriggered?.Invoke(this, new LogEventArgs());
    }
}

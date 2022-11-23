namespace BearsEngine.Controllers;

/// <summary>
/// A simple timer which reports progress
/// </summary>
internal class ProgressTimer : IProgressTimer
{
    private readonly Func<float, float> _progressFunction = t => t;

    /// <summary>
    /// A simple timer which reports progress
    /// </summary>
    /// <param name="totalDuration">The time in seconds that the timer should run for</param>
    public ProgressTimer(float totalDuration)
    {
        TotalDuration = totalDuration;
    }

    /// <summary>
    /// A simple timer which reports progress
    /// </summary>
    /// <param name="totalDuration">The time in seconds that the timer should run for</param>
    /// <param name="progressFunction">A function mapping normalised time to reported progress. Requirements: f([0,1])->[0,1], f(0)=0 and f(1)=1. By default f(t) = t, i.e. progress is linear with time</param>
    /// <exception cref="ArgumentException">If the progressFunction is not valid</exception>
    public ProgressTimer(float totalDuration, Func<float, float> progressFunction)
    {
        if (progressFunction(0) != 0 || progressFunction(1) != 1)
            throw new ArgumentException("time to progress mapping must meet following conditions: f([0,1])->[0,1], f(0)=0 and f(1)=1", nameof(progressFunction));

        TotalDuration = totalDuration;
        _progressFunction = progressFunction;
    }

    public bool Active { get; set; } = true;

    /// <summary>
    /// How much time, in seconds has elapsed while the timer has been active
    /// </summary>
    public float TimeElapsed { get; private set; } = 0;

    /// <summary>
    /// How much time, in seconds is left until the timer completes
    /// </summary>
    public float TimeRemaining => TotalDuration - TimeElapsed;

    /// <summary>
    /// The total duration of the timer requested.
    /// </summary>
    public float TotalDuration { get; }

    /// <summary>
    /// Reported progress, [0,1]. Usually normalised time, i.e. moves uniformly from 0 to 1 as TimeElapsed moves from 0 to TotalDuration, but can be modified via the constructor taking a progressFunction if non-linear progress being reported is desired.
    /// </summary>
    public float Progress { get; private set; }

    /// <summary>
    /// <see cref="Progress"/> multiplied by 100.
    /// </summary>
    public float ProgressAsPercentage => Progress * 100;

    public void Update(float elapsedTime)
    {
        if (TimeRemaining == 0)
            return;

        TimeElapsed += elapsedTime;

        if (TimeElapsed > TotalDuration)
        {
            TimeElapsed = TotalDuration;
            Completed?.Invoke(this, EventArgs.Empty);
        }

        Progress = _progressFunction(TimeElapsed / TotalDuration);
        if (Progress < 0 || Progress > 1)
            throw new InvalidOperationException();
    }

    /// <summary>
    /// An event which fires once <see cref="TimeElapsed"/> reaches <see cref="TotalDuration"/>. Note that Progress will be 1 at this point, but may be 1 before this event triggers, if a progressFunction was provided in the constructor.
    /// </summary>
    public event EventHandler? Completed;
}
namespace BearsEngine.Controllers;

internal interface IProgressTimer : IUpdatable
{
    /// <summary>
    /// The time elapsed in the timer, in seconds.
    /// </summary>
    public float TimeElapsed { get; }

    /// <summary>
    /// The time remaining for the timer to complete, in seconds.
    /// </summary>
    public float TimeRemaining { get; }

    /// <summary>
    /// The total time the timer was requested to run for.
    /// </summary>
    public float TotalDuration { get; }

    /// <summary>
    /// The reported progress percentage, from [0,1]. This is calculated as <see cref="TimeToProgressFunction"/>(<see cref="TimeElapsed"/>/<see cref="TotalDuration"/>) Progress is guarntee to be 0 initially, and 1 when complete, but based on <see cref="TimeToProgressFunction"/>, Progress may = 1 at other points too.
    /// </summary>
    public float Progress { get; }

    /// <summary>
    /// An event which triggers when the timer has completed running (TimeElapsed = TotalDuration, and TimeRemaining = 0). Note that Progress will = 1 at this point, but based on <see cref="TimeToProgressFunction"/>, Progress may = 1 at other points too.
    /// </summary>
    public event EventHandler? Completed;
}
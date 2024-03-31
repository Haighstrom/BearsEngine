namespace BearsEngine.Controllers;

/// <summary>
/// Simple alarm, used for timed events etc
/// </summary>
public class Alarm : UpdateableBase
{
    private readonly bool _repeat;
    public Action? _actionOnComplete;

    public Alarm(float duration, bool repeat, Action? actionOnComplete = null)
        : base()
    {
        TotalDuration = duration;
        _repeat = repeat;
        _actionOnComplete = actionOnComplete;
    }

    public float TotalDuration { get; }

    public float TimeElapsed { get; private set; } = 0;

    public float TimeRemaining => TotalDuration - TimeElapsed;

    public event EventHandler? Completed;
    public event EventHandler? SecondChanged;

    private void OnComplete()
    {
        _actionOnComplete?.Invoke();

        Completed?.Invoke(this, EventArgs.Empty);

        if (_repeat)
        {
            TimeElapsed -= TotalDuration;
        }
        else
        {
            Remove();
        }
    }

    public override void Update(float elapsed)
    {
        var prevTime = TimeElapsed;
        TimeElapsed += elapsed;

        if ((int)prevTime != (int)TimeElapsed)
        {
            SecondChanged?.Invoke(this, EventArgs.Empty);
        }

        if (TimeElapsed >= TotalDuration)
        {
            OnComplete();
        }
    }
}
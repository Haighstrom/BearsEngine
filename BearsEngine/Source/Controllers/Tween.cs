namespace BearsEngine.Controllers;

public class Tween : AddableBase, IUpdatable
{
    public Tween(float duration, bool persistType, Action? actionOnCompleted = null, Func<float, float>? easer = null)
    {
        TotalDuration = duration;
        Persistence = persistType;
        ActionOnCompleted = actionOnCompleted;
        TimeAdjustment = easer;
    }
    

    public Action? ActionOnCompleted { get; set; }
    public bool Active { get; set; } = true;
    public Func<float, float>? TimeAdjustment { get; set; }
    public float Elapsed { get; private set; }
    public float PercentComplete => Progress * 100;
    public bool Persistence { get; set; }
    public float Progress { get; private set; }
    public float TotalDuration { get; protected set; }

    public virtual void Update(float elapsed)
    {
        Elapsed += elapsed;
        Progress = Elapsed / TotalDuration;

        if (Elapsed >= TotalDuration)
        {
            Progress = 1;
            OnCompleted();
        }
        else if (TimeAdjustment != null && Progress > 0)
            Progress = TimeAdjustment(Progress);
    }

    protected virtual void OnCompleted()
    {
        if (Persistence)
        {
            Elapsed %= TotalDuration;
            Progress = Elapsed / TotalDuration;
            if (TimeAdjustment != null && Progress > 0 && Progress < 1)
                Progress = TimeAdjustment(Progress);
        }
        else
        {
            Remove();
        }

        ActionOnCompleted?.Invoke();

        Completed?.Invoke(this, EventArgs.Empty);
    }
    

    public virtual void Start()
    {
        Elapsed = 0;
        if (TotalDuration == 0)
            Active = false;
        else
            Active = true;
    }
    
    

    public event EventHandler? Completed;
    
}

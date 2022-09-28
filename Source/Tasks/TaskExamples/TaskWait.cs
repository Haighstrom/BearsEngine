namespace BearsEngine.Tasks;

public class TaskWait : Task
{
    private readonly float _waitTime;
    private float _remainingTime;

    public TaskWait(float waitTime)
    {
        _waitTime = waitTime;
        CompletionConditions.Add(() => _remainingTime <= 0);
    }

    public override void Start()
    {
        base.Start();

        _remainingTime = _waitTime;
    }

    public override void Update(double elapsed)
    {
        base.Update(elapsed);

        _remainingTime -= (float)elapsed;
    }
}

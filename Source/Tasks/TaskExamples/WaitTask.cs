namespace BearsEngine.Tasks;

public class WaitTask : Task
{
    private readonly float _initialWaitTime;
    private float _remainingTime;

    public WaitTask(float waitTime)
    {
        _initialWaitTime = waitTime;
        CompletionConditions.Add(() => _remainingTime <= 0);
    }

    public override void Start()
    {
        base.Start();

        _remainingTime = _initialWaitTime;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _remainingTime -= elapsed;
    }

    public float PercentComplete => Maths.Clamp((int)(10 * (_initialWaitTime - _remainingTime) / _initialWaitTime) / 10f, 0, 1);
}
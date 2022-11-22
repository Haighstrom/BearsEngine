namespace BearsEngine.Controllers;

public class LinearMotion : MotionTween
{
    readonly float _startX;
    readonly float _startY;
    readonly float _rangeX;
    readonly float _rangeY;

    public LinearMotion(float startX, float startY, float endX, float endY, float duration, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Func<float, float>? easer = null)
        : base(duration, persistence, actionOnComplete, easer)
    {
        X = _startX = startX;
        Y = _startY = startY;
        _rangeX = endX - startX;
        _rangeY = endY - startY;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        X = _startX + _rangeX * Progress;
        Y = _startY + _rangeY * Progress;
    }
}
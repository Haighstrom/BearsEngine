namespace BearsEngine.Controllers;

public class EllipticalMotion : MotionTween
{
    readonly float _centreX;
    readonly float _centreY;
    readonly float _radiusX;
    readonly float _radiusY;

    public float Angle { get; private set; }

    readonly float _angleStart;
    readonly float _angleRange;

    public EllipticalMotion(float centreX, float centreY, float radiusX, float radiusY, float startAngle, float duration, bool persistence = true, Action actionOnComplete = null, Func<float, float>? easer = null)
        : base(duration, persistence, actionOnComplete, easer)
    {
        _centreX = centreX;
        _centreY = centreY;
        _radiusX = radiusX;
        _radiusY = radiusY;
        Angle = _angleStart = startAngle * (float)Math.PI / 180;
        _angleRange = 2 * (float)Math.PI;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        Angle = _angleStart + _angleRange * Progress;
        X = _centreX + (float)Math.Cos(Angle) * _radiusX;
        Y = _centreY + (float)Math.Sin(Angle) * _radiusY;
    }
}
public class CircularMotion : EllipticalMotion
{
    public CircularMotion(float centreX, float centreY, float radius, float startAngle, float duration, bool persistence = true, Action? actionOnComplete = null, Func<float, float>? easer = null)
        : base(centreX, centreY, radius, radius, startAngle, duration, persistence, actionOnComplete, easer)
    {

    }
}
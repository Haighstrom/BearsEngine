namespace BearsEngine.Controllers;

public class ShakeTween : MotionTween
{
    private float _elapsed; //nb not using Tween.Elapsed because on looping the value will become disjointed with sin/cos
    private readonly float _centreX;
    private readonly float _centreY;
    private readonly float _radiusX;
    private readonly float _radiusY;
    private readonly float _periodCoefficientX;
    private readonly float _periodCoefficientY;

    public ShakeTween(float centreX, float centreY, float radiusX, float radiusY, float periodX, float periodY, float duration, bool persistence = true, Action actionOnComplete = null, Func<float, float>? easer = null)
        : base(duration, persistence, actionOnComplete, easer)
    {
        _centreX = centreX;
        _centreY = centreY;
        _radiusX = radiusX;
        _radiusY = radiusY;
        _periodCoefficientX = (float)(2 * Math.PI / periodX);
        _periodCoefficientY = (float)(2 * Math.PI / periodY);
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _elapsed += elapsed;

        X = _centreX + _radiusX * (float)Math.Sin(_periodCoefficientX * _elapsed);
        Y = _centreY + _radiusY * (float)Math.Sin(_periodCoefficientY * _elapsed);
    }
}
namespace BearsEngine.Tweens
{
    public class EllipticalMotion : MotionTween
    {
        float _centreX;
        float _centreY;
        float _radiusX;
        float _radiusY;

        public float Angle { get; private set; }
        float _angleStart;
        float _angleRange;

        public EllipticalMotion(float centreX, float centreY, float radiusX, float radiusY, float startAngle, float duration, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Easer easer = null)
            : base(duration, persistence, actionOnComplete, easer)
        {
            _centreX = centreX;
            _centreY = centreY;
            _radiusX = radiusX;
            _radiusY = radiusY;
            Angle = _angleStart = startAngle * (float)Math.PI / 180;
            _angleRange = 2 * (float)Math.PI;
        }

        public override void Update(double elapsed)
        {
            base.Update(elapsed);

            Angle = _angleStart + _angleRange * Progress;
            X = _centreX + (float)Math.Cos(Angle) * _radiusX;
            Y = _centreY + (float)Math.Sin(Angle) * _radiusY;
        }
    }
    public class CircularMotion : EllipticalMotion
    {
        public CircularMotion(float centreX, float centreY, float radius, float startAngle, float duration, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Easer easer = null)
            :base(centreX,centreY,radius,radius,startAngle,duration,persistence,actionOnComplete,easer)
        {

        }
    }
}
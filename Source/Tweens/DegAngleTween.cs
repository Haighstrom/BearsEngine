namespace BearsEngine.Tweens
{
    public enum AngleTweenDirection { Clockwise, CounterClockwise, Nearest }
    public class DegAngleTween : Tween
    {
        public float Angle;
        private readonly float _startAngle;
        private readonly float _range;

        public DegAngleTween(float startAngle, float endAngle, float duration, AngleTweenDirection angleTweenDirection = AngleTweenDirection.Nearest, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Easer easer = null)
            : base(duration, persistence, actionOnComplete, easer)
        {
            if (startAngle < 0 || startAngle > 360 || endAngle < 0 || endAngle > 360) throw new Exception("Angles should only be defined between 0 and 360");

            Angle = _startAngle = startAngle;
            float d = endAngle - startAngle;
            switch (angleTweenDirection)
            {
                case AngleTweenDirection.Clockwise:
                    _range = d + (d > 0 ? 0 : 360);
                    break;
                case AngleTweenDirection.CounterClockwise:
                    _range = d - (d < 0 ? 0 : 360);
                    break;
                case AngleTweenDirection.Nearest:
                    float a = Math.Abs(d);
                    if (a > 180) _range = (360 - a) * (d > 0 ? -1 : 1);
                    else _range = d;
                    break;
                default:
                    throw new Exception("Not all AngleTweenDirections implemented.");
            }
        }

        public override void Update(double elapsed)
        {
            base.Update(elapsed);

            Angle = (360 + _startAngle + _range * Progress) % 360;
        }
    }
}
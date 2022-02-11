namespace BearsEngine.Tweens
{
    public class LinearMotion : MotionTween
    {
        float _startX;
        float _startY;
        float _rangeX;
        float _rangeY;

        public LinearMotion(float startX, float startY, float endX, float endY, float duration, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Easer easer = null)
            : base(duration, persistence, actionOnComplete, easer)
        {
            X = _startX = startX;
            Y = _startY = startY;
            _rangeX = endX - startX;
            _rangeY = endY - startY;
        }

        public override void Update(double elapsed)
        {
            base.Update(elapsed);

            X = _startX + _rangeX * Progress;
            Y = _startY + _rangeY * Progress;
        }
    }
}
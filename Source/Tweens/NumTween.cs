namespace BearsEngine.Tweens
{
    public class NumTween : Tween
    {
        public float Value;
        private readonly float _startValue;
        private readonly float _range;

        public NumTween(float startValue, float endValue, float duration, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Easer easer = null)
            : base(duration, persistence, actionOnComplete, easer)
        {
            Value = _startValue = startValue;
            _range = endValue - startValue;
        }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);
            Value = _startValue + _range * Progress;
        }
    }
}
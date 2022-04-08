namespace BearsEngine.Tasks.TaskExamples
{
    public class T_Wait : Task
    {
        private float _waitTime;
        private float _remainingTime;

        public T_Wait(float waitTime)
        {
            _waitTime = waitTime;
            CompletionConditions.Add(() => _remainingTime <= 0);
        }

        protected override void Start()
        {
            _remainingTime = _waitTime;
            base.Start();
        }

        public override void Update(double elapsed)
        {
            base.Update(elapsed);
            _remainingTime -= (float)elapsed;
        }
    }
}
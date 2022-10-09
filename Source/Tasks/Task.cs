namespace BearsEngine.Tasks
{
    public abstract class Task : ITask
    {
        private bool _isStarted = false;
        protected readonly List<Action> ActionsOnStart = new();
        protected readonly List<Func<bool>> CompletionConditions = new();
        protected readonly List<Action> ActionsOnComplete = new();
        

        public virtual bool Active { get; set; } = true;
        public virtual bool IsComplete => CompletionConditions.All(c => c());
        public ITask? NextTask { get; set; } = null;
        
        public virtual void Complete()
        {
            ActionsOnComplete.ForEach(a => a());

            TaskCompleted?.Invoke(this, EventArgs.Empty);

            _isStarted = false;
        }
        
        public virtual void Reset()
        {
            _isStarted = false;
        }
        
        public virtual void Start()
        {
            ActionsOnStart.ForEach(a => a());

            TaskStarted?.Invoke(this, EventArgs.Empty);

            _isStarted = true; //include this both here and in update in case function is overridden
        }
        
        public virtual void Update(float elapsed)
        {
            if (!_isStarted)
            {
                Start();
                _isStarted = true;
            }
        }
        
        

        public event EventHandler? TaskStarted, TaskCompleted;
    }
}
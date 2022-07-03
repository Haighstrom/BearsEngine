namespace BearsEngine.Tasks
{
    public abstract class Task : ITask
    {
        #region Fields
        private bool _isStarted = false;
        protected readonly List<Action> ActionsOnStart = new();
        protected readonly List<Func<bool>> CompletionConditions = new();
        protected readonly List<Action> ActionsOnComplete = new();
        #endregion

        #region Properties
        public virtual bool Active { get; set; } = true;
        public virtual bool IsComplete => CompletionConditions.All(c => c());
        public ITask? NextTask { get; set; } = null;
        #endregion

        #region Methods
        #region Complete
        public virtual void Complete()
        {
            ActionsOnComplete.ForEach(a => a());

            TaskCompleted?.Invoke(this, EventArgs.Empty);

            _isStarted = false;
        }
        #endregion

        #region Reset
        public virtual void Reset()
        {
            _isStarted = false;
        }
        #endregion

        #region Start
        public virtual void Start()
        {
            ActionsOnStart.ForEach(a => a());

            TaskStarted?.Invoke(this, EventArgs.Empty);

            _isStarted = true; //include this both here and in update in case function is overridden
        }
        #endregion

        #region Update
        public virtual void Update(double elapsed)
        {
            if (!_isStarted)
            {
                Start();
                _isStarted = true;
            }
        }
        #endregion
        #endregion

        public event EventHandler? TaskStarted, TaskCompleted;
    }
}
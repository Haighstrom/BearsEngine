namespace BearsEngine.Tasks
{
    public abstract class Task : ITask
    {
        #region Fields
        private bool _firstUpdate = true;
        protected readonly List<Action> ActionsOnStart = new List<Action>();
        protected readonly List<Func<bool>> CompletionConditions = new List<Func<bool>>();
        protected readonly List<Action> ActionsOnComplete = new List<Action>();
        #endregion

        #region ITask
        #region IUpdateable
        public virtual bool Active { get; set; } = true;

        public virtual void Update(double elapsed)
        {
            if (_firstUpdate)
            {
                Start();
                _firstUpdate = false;
            }
        }
        #endregion

        #region Start
        protected virtual void Start()
        {
            ActionsOnStart.ForEach(a => a());

            TaskStarted?.Invoke(this, EventArgs.Empty);

            IsStarted = true;
        }
        #endregion

        public ITask NextTask { get; set; } = null;

        public bool IsStarted { get; private set; } = false;

        public virtual bool IsComplete => CompletionConditions.All(c => c());

        #region End
        public virtual void End()
        {
            ActionsOnComplete.ForEach(a => a());

            TaskEnded?.Invoke(this, EventArgs.Empty);

            IsStarted = false;
        }
        #endregion
        
        public event EventHandler TaskStarted, TaskEnded;
        #endregion
    }
}
namespace BearsEngine.Tasks
{
    public class TaskGroup : ITask
    {
        #region Fields
        private bool _isStarted = false;
        protected readonly List<Action> ActionsOnStart = new();
        protected readonly List<Func<bool>> CompletionConditions = new();
        protected readonly List<Action> ActionsOnComplete = new();
        #endregion

        #region Constructors
        public TaskGroup(params ITask[] tasks)
        {
            if (tasks.Length == 0)
                return;

            CurrentTask = tasks[0];

            for (int i = 0; i < tasks.Length - 1; ++i)
                tasks[i].NextTask = tasks[i + 1];
        }

        public TaskGroup(ITask firstTask = null)
        {
            CurrentTask = firstTask;
        }
        #endregion

        #region Properties
        public virtual bool Active { get; set; } = true;
        public ITask? CurrentTask { get; set; }
        public bool IsComplete => CurrentTask == null && CompletionConditions.All(c => c());
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

            if (CurrentTask != null && CurrentTask.Active)
            {
                CurrentTask.Update(elapsed);

                if (CurrentTask.IsComplete)
                {
                    CurrentTask.Complete();
                    CurrentTask = CurrentTask.NextTask;
                }
            }
        }
        #endregion
        #endregion

        public event EventHandler TaskStarted, TaskCompleted;
    }
}
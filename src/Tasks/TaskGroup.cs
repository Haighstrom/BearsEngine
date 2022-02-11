namespace BearsEngine.Tasks
{
    public class TaskGroup : ITask
    {
        #region Fields
        private bool _firstUpdate = true;
        protected readonly List<Action> ActionsOnStart = new List<Action>();
        protected readonly List<Func<bool>> CompletionConditions = new List<Func<bool>>();
        protected readonly List<Action> ActionsOnComplete = new List<Action>();
        private ITask _currentTask;
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

        #region ITask
        #region IUpdateable
        public virtual bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            if (_firstUpdate)
            {
                Start();
                _firstUpdate = false;
            }

            if (CurrentTask != null && CurrentTask.Active)
            {
                CurrentTask.Update(elapsed);

                if (CurrentTask.IsComplete)
                {
                    CurrentTask.End();
                    CurrentTask = CurrentTask.NextTask;
                }
            }
        }
        #endregion
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

        public bool IsStarted { get; set; } = false;

        public bool IsComplete => CurrentTask == null && CompletionConditions.All(c => c());

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

        #region CurrentTask
        public ITask CurrentTask
        {
            get => _currentTask;
            set
            {
                //logic for interrupt and whatnot?
                _currentTask = value;
            }
        }
        #endregion
    }
}
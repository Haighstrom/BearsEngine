namespace BearsEngine.Tasks
{
    public class TaskGroup : ITask
    {
        private bool _isStarted = false;
        protected readonly List<Action> ActionsOnStart = new();
        protected readonly List<Func<bool>> CompletionConditions = new();
        protected readonly List<Action> ActionsOnComplete = new();
        

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
        
        public virtual bool Active { get; set; } = true;
        public ITask? CurrentTask { get; set; }
        public bool IsComplete => CurrentTask == null && CompletionConditions.All(c => c());
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
        
        

        public event EventHandler TaskStarted, TaskCompleted;
    }
}
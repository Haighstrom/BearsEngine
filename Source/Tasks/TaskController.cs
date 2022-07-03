namespace BearsEngine.Tasks
{
    public class TaskController : AddableBase, IUpdatable
    {
        #region Constructors
        public TaskController(Func<ITask>? getNextTask = null)
            : this(null, getNextTask)
        {
        }

        public TaskController(ITask? firstTask, Func<ITask>? getNextTask = null)
        {
            CurrentTask = firstTask;
            GetNextTask = getNextTask;
        }
        #endregion

        public virtual bool Active { get; set; } = true;
        public ITask? CurrentTask { get; set; }
        public Func<ITask>? GetNextTask { get; set; }

        #region Update
        public virtual void Update(double elapsed)
        {
            if (Parent == null)
                return;

            if (CurrentTask != null && CurrentTask.Active)
            {
                CurrentTask.Update(elapsed);

                if (CurrentTask.IsComplete)
                {
                    CurrentTask.Complete();
                    CurrentTask = CurrentTask.NextTask;
                }
            }

            if (CurrentTask == null && GetNextTask != null)
                CurrentTask = GetNextTask();
        }
        #endregion
    }
}
namespace BearsEngine.Tasks
{
    public class TaskController : AddableBase, IUpdatable
    {
        public TaskController()
        {
            CurrentTask = null;
            GetNextTask = null;
        }

        public TaskController(ITask[] tasks)
        {
            if (tasks.Length == 0)
                throw new ArgumentException($"TaskController {this} had an empty list of tasks {tasks} passed to it.");

            for (int i = 0; i < tasks.Length - 1; i++)
                tasks[i].NextTask = tasks[i + 1];

            CurrentTask = tasks[0];
        }

        public TaskController(Func<ITask>? getNextTask = null)
            : this(null, getNextTask)
        {
        }

        public TaskController(ITask? firstTask, Func<ITask>? getNextTask = null)
        {
            CurrentTask = firstTask;
            GetNextTask = getNextTask;
        }
        

        public virtual bool Active { get; set; } = true;
        public ITask? CurrentTask { get; set; }
        public Func<ITask>? GetNextTask { get; set; }

        public virtual void Update(float elapsed)
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
        
    }
}
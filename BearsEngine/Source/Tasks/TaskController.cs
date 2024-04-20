namespace BearsEngine.Tasks;

public class TaskController : AddableBase, ITaskController
{
    public TaskController()
    {
        CurrentTask = null;
        GetNextTask = null;
    }

    public TaskController(ITask[] tasks)
    {
        Ensure.ArgumentCollectionNotNullOrEmpty(tasks, nameof(tasks));

        for (int i = 0; i < tasks.Length - 1; i++)
            tasks[i].NextTask = tasks[i + 1];

        CurrentTask = tasks[0];
    }

    public TaskController(Func<ITask?>? getNextTask = null)
        : this(null, getNextTask)
    {
    }

    public TaskController(ITask? firstTask, Func<ITask?>? getNextTask = null)
    {
        CurrentTask = firstTask;
        GetNextTask = getNextTask;
    }


    public virtual bool Active { get; set; } = true;
    public ITask? CurrentTask { get; set; }
    public Func<ITask?>? GetNextTask { get; set; }

    public virtual void Update(float elapsed)
    {
        if (Parent is null)
            return;

        if (CurrentTask != null && CurrentTask.Active)
        {
            CurrentTask.Update(elapsed);

            if (CurrentTask.IsComplete)
            {
                CurrentTask.Complete();
                if (Parent is IAddable ie && ie.Parent is null) //kind of a hack?
                    return;
                CurrentTask = CurrentTask.NextTask;
            }
        }

        if (Parent is IAddable a && a.Parent is null) //kind of a hack?
            return;

        if (CurrentTask == null && GetNextTask != null)
            CurrentTask = GetNextTask();
    }

}
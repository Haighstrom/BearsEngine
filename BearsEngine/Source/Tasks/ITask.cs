namespace BearsEngine.Tasks;

public interface ITask : IUpdatable
{
    bool IsComplete { get; }
    ITask? NextTask { get; set; }

    void Complete();
    void Reset();

    /// <summary>
    /// Use to manually trigger start - not usually required, just add an ITask to a TaskController, TaskGroup, or to ITask.NextTask and when Update is called, Start will be called once.
    /// </summary>
    void Start();

    event EventHandler TaskStarted, TaskCompleted;
}

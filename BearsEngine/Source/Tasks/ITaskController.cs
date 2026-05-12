namespace BearsEngine.Tasks;

public interface ITaskController : IAddable, IUpdatable
{
    ITask? CurrentTask { get; set; }
    void ClearTask();
    Func<ITask?>? GetNextTask { get; set; }
}

namespace BearsEngine.Tasks;

public interface ITaskController : IAddable, IUpdatable
{
    ITask? CurrentTask { get; set; }
    Func<ITask?>? GetNextTask { get; set; }
}
namespace BearsEngine.Tasks
{
    public interface ITask : IUpdatable
    {
        bool IsStarted { get; }
        bool IsComplete { get; }
        ITask NextTask { get; set; }
        void End();
        event EventHandler TaskStarted, TaskEnded;
    }
}
namespace BearsEngine.Tasks
{
    public interface IInterruptableTask : ITask
    {
        void Interrupt();
    }
}
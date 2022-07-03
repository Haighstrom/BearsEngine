namespace BearsEngine.Tasks.TaskExamples
{
    public class TaskAction : Task
    {
        public TaskAction(Action action)
        {
            ActionsOnComplete.Add(action);
        }
    }
}
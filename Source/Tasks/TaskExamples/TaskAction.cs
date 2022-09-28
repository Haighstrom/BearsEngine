namespace BearsEngine.Tasks;

public class TaskAction : Task
{
    public TaskAction(Action action)
    {
        ActionsOnComplete.Add(action);
    }
}

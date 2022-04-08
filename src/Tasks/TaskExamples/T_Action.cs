namespace BearsEngine.Tasks.TaskExamples
{
    public class T_Action : Task
    {
        public T_Action(Action action)
        {
            ActionsOnComplete.Add(action);
        }
    }
}
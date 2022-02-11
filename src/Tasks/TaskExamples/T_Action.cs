namespace BearsEngine.Tasks
{
    public class T_Action : Task
    {
        public T_Action(Action action)
        {
            ActionsOnComplete.Add(action);
        }
    }
}
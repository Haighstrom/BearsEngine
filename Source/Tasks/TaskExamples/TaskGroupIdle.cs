using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskGroupIdle<N> : TaskGroup
    where N : INode
{
    public TaskGroupIdle(IWaypointableAndPathable<N> entity, int maxSteps, float waitTime)
        : base(new TaskRandomRoute<N>(entity, maxSteps), new TaskWait(waitTime))
    {
    }
}

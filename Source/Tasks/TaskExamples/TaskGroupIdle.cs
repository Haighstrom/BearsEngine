using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskGroupIdle<N> : TaskGroup
    where N : INode
{
    public TaskGroupIdle(IPathfinder<N> pathfinder, IWaypointableAndPathable<N> entity, int maxSteps, float waitTime)
        : base(new TaskRandomRoute<N>(pathfinder, entity, maxSteps), new TaskWait(waitTime))
    {
    }
}

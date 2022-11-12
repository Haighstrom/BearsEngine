using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskGroupIdle<N> : TaskGroup where N : IPathfindNode<N>, IPosition
{
    public TaskGroupIdle(IPathfinder<N> pathfinder, IWaypointableAndPathable<N> entity, int maxSteps, bool canBacktrack, float waitTime)
        : base(new TaskRandomRoute<N>(pathfinder, entity, maxSteps, canBacktrack), new TaskWait(waitTime))
    {
    }
}
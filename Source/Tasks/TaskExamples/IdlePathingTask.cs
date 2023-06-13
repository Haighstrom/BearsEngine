using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class IdlePathingTask<N> : TaskGroup where N : IPathfindNode<N>, IPosition
{
    public IdlePathingTask(IPathfinder<N> pathfinder, IWaypointableAndPathable<N> entity, int maxSteps, bool canBacktrack, float waitTime)
        : base(new RandomRouteTask<N>(pathfinder, entity, maxSteps, canBacktrack), new WaitTask(waitTime))
    {
    }
}
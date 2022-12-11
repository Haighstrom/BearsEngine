using BearsEngine.Controllers;

namespace BearsEngine.Pathfinding;

public interface IWaypointableAndPathable<N> : IWaypointable, IPathable<N>
    where N : INode<N>
{
}

using BearsEngine.Pathfinding;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Worlds
{
    public interface IWaypointable : IMoveable
    {
        WaypointController WaypointController { get; }
    }

    public interface IWaypointableAndPathable<N> : IWaypointable, IPathable<N>
        where N : INode
    {
    }
}
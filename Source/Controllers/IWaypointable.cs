using BearsEngine.Pathfinding;
using BearsEngine.Tasks;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Worlds;

public interface IWaypointable : IMoveable
{
    WaypointController WaypointController { get; }
}
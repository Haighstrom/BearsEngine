using BearsEngine.Tasks;
using Serilog;

namespace BearsEngine.Pathfinding;

public class TaskAStarRoute<N> : Task where N : IPathfindNode<N>, IPosition
{
    private readonly IPathfinder<N> _pathfinder;
    private readonly IWaypointableAndPathable<N> _entity;
    private readonly N _destination;

    public TaskAStarRoute(IPathfinder<N> pathfinder, IWaypointableAndPathable<N> entity, N destination)
    {
        _pathfinder = pathfinder;
        _entity = entity;
        _destination = destination;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }
    public override void Start()
    {
        base.Start();

        var route = _pathfinder.FindPath(_entity.CurrentNode, _destination, _entity.CanPathThrough);

        if (route == null)
        {
            Log.Warning("{0} could not find path from {1} to {2}", _entity, _entity.CurrentNode, _destination);
            route = new List<N>();
        }

        _entity.WaypointController.SetWaypoints(route.Select(n => (IPosition)new Point(n.X, n.Y)));
    }
}

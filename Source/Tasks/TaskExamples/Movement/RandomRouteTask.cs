using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class RandomRouteTask<N> : Task where N : IPathfindNode<N>, IPosition
{
    private readonly IPathfinder<N> _pathfinder;
    private readonly int _maxSteps;
    private readonly IWaypointableAndPathable<N> _entity;
    private readonly bool _canBacktrack;

    public RandomRouteTask(IPathfinder<N> pathfinder, IWaypointableAndPathable<N> entity, int maxSteps, bool canBacktrack)
    {
        _pathfinder = pathfinder;
        _entity = entity;
        _maxSteps = maxSteps;
        _canBacktrack = canBacktrack;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public override void Start()
    {
        base.Start();

        var route = _pathfinder.FindRandomPath(_entity.CurrentNode, _entity.CanPathThrough, _maxSteps, _canBacktrack);
        if (route == null)
            throw new Exception();
        _entity.WaypointController.SetWaypoints(route.Select(n => (IPosition)new Point(n.X, n.Y)));
    }
}

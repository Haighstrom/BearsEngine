using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskRandomRoute<N> : Task
    where N : INode
{
    private readonly IPathfinder<N> _pathfinder;
    private readonly int _maxSteps;
    private readonly IWaypointableAndPathable<N> _entity;

    public TaskRandomRoute(IPathfinder<N> pathfinder, IWaypointableAndPathable<N> entity, int maxSteps)
    {
        _pathfinder = pathfinder;
        _entity = entity;
        _maxSteps = maxSteps;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public override void Start()
    {
        base.Start();

        var route = _pathfinder.GetRandomRoute(_entity.CurrentNode, _maxSteps, _entity.CanPathThrough);
        _entity.WaypointController.SetWaypoints(route.Select(n => (IPosition)new Point(n.X, n.Y)));
    }
}

using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskRandomRoute<N> : Task
    where N : INode
{
    private readonly int _maxSteps;
    private readonly IWaypointableAndPathable<N> _entity;

    public TaskRandomRoute(IWaypointableAndPathable<N> entity, int maxSteps)
    {
        _entity = entity;
        _maxSteps = maxSteps;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public override void Start()
    {
        base.Start();

        var route = HF.Pathfinding.ChooseRandomRoute(_entity.CurrentNode, _entity.CanPathThrough, _maxSteps);
        _entity.WaypointController.SetWaypoints(route.Select(n => (IPosition)new Point(n.X, n.Y)));
    }
}

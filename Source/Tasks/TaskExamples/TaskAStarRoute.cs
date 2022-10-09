using BearsEngine.Pathfinding;
using Serilog;

namespace BearsEngine.Tasks;

public class TaskAStarRoute<N> : Task
    where N : INode
{
    private readonly IWaypointableAndPathable<N> _entity;
    private readonly N _destination;

    public TaskAStarRoute(IWaypointableAndPathable<N> entity, N destination)
    {
        _entity = entity;
        _destination = destination;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }
    public override void Start()
    {
        base.Start();

        var route = HF.Pathfinding.GetAStarRoute(_entity.CurrentNode, _destination, _entity.CanPathThrough);

        if (route == null)
        {
            Log.Warning("{0} could not find path from {1} to {2}", _entity, _entity.CurrentNode, _destination);
        }                    

        route = new();

        _entity.WaypointController.SetWaypoints(route.Select(n => (IPosition)new Point(n.X, n.Y)));
    }
}

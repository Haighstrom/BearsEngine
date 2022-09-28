using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskAStarRoute<N> : Task
    where N : INode
{
    private readonly IWaypointableAndPathable<N> _entity;
    private readonly N _destination;
    private readonly ErrorType _errorOnNoPath;

    public TaskAStarRoute(IWaypointableAndPathable<N> entity, N destination, ErrorType errorOnNoPath = ErrorType.Error)
    {
        _entity = entity;
        _destination = destination;
        _errorOnNoPath = errorOnNoPath;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }
    public override void Start()
    {
        base.Start();

        var route = HF.Pathfinding.GetAStarRoute(_entity.CurrentNode, _destination, _entity.CanPathThrough);

        if (route == null)
            if (_errorOnNoPath == ErrorType.Error)
                throw new Exception();
            else
            {
                if (_errorOnNoPath == ErrorType.Warning)
                    HConsole.Log($"{_entity} could not find path from {_entity.CurrentNode} to {_destination}");

                route = new();
            }

        _entity.WaypointController.SetWaypoints(route.Select(n => (IPosition)new Point(n.X, n.Y)));
    }
}

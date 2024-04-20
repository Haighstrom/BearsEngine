using BearsEngine.Controllers;

namespace BearsEngine.Tasks;

public class FollowWaypointsTask : Task
{
    private readonly IWaypointable _entity;
    private readonly IEnumerable<IPosition> _waypoints;

    public FollowWaypointsTask(IWaypointable entity, IEnumerable<IPosition> waypoints)
    {
        _entity = entity;
        _waypoints = waypoints;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public FollowWaypointsTask(IWaypointable entity, params IPosition[] waypoints)
    {
        _entity = entity;
        _waypoints = waypoints;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public override void Start()
    {
        base.Start();
        _entity.WaypointController.SetWaypoints(_waypoints);
    }
}

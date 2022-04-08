namespace BearsEngine.Tasks.TaskExamples;

public class T_FollowWaypoints : Task
{
    private IWaypointable _entity;
    private List<IPosition> _waypoints;

    public T_FollowWaypoints(IWaypointable entity, List<IPosition> waypoints)
    {
        _entity = entity;
        _waypoints = waypoints;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    protected override void Start()
    {
        base.Start();
        _entity.WaypointController.Waypoints = _waypoints;
    }
}

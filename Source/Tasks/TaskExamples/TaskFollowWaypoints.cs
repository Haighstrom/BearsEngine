namespace BearsEngine.Tasks.TaskExamples;

public class TaskFollowWaypoints : Task
{
    private readonly IWaypointable _entity;
    private readonly List<IPosition> _waypoints;

    public TaskFollowWaypoints(IWaypointable entity, List<IPosition> waypoints)
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

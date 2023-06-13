using BearsEngine.Controllers;

namespace BearsEngine.Tasks;

public class FlyToTask : Task
{
    private readonly IWaypointable _entity;
    private readonly IPosition _destination;

    public FlyToTask(IWaypointable entity, IPosition destination)
    {
        _entity = entity;
        _destination = destination;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }
    public override void Start()
    {
        base.Start();
        _entity.WaypointController.SetWaypoints(_destination);
    }
}

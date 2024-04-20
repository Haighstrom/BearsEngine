using BearsEngine.Controllers;

namespace BearsEngine.Tasks;

public class ShortestRouteTask : Task
{
    private readonly IWaypointable _entity;
    private readonly IPosition _destination;

    public ShortestRouteTask(IWaypointable entity, IPosition destination)
    {
        _entity = entity;
        _destination = destination;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public override void Start()
    {
        base.Start();

        _entity.WaypointController.SetWaypoints(GetRoute(_entity.P, new Point(_destination.X, _destination.Y)));
    }

    //todo:move this to bearsengine.pathfinding
    private static List<IPosition> GetRoute(Point start, Point end)
    {
        Point nextPoint = start;

        List<IPosition> route = new() { start };

        while (nextPoint != end)
        {

            if (Math.Abs(end.X - nextPoint.X) > Math.Abs(end.Y - nextPoint.Y))
                nextPoint.X += Math.Sign(end.X - nextPoint.X);
            else
                nextPoint.Y += Math.Sign(end.Y - nextPoint.Y);

            route.Add(nextPoint);
        }

        return route;
    }
}

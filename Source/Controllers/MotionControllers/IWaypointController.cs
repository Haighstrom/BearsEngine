using BearsEngine.Source.Controllers.MotionControllers;

namespace BearsEngine.Controllers;

public interface IWaypointController : IAddable, IUpdatable
{
    IPosition CurrentPosition { get; }
    bool ReachedDestination { get; }
    IList<IPosition> Waypoints { get; }

    event EventHandler<EventArgs>? Arrived;
    event EventHandler<DirectionChangedEventArgs>? DirectionChanged;
    event EventHandler<EventArgs>? ReachedWaypoint;
    event EventHandler<EventArgs>? StartedMoving;

    void AddWaypoints(IEnumerable<IPosition> positions);
    void AddWaypoints(params IPosition[] positions);
    void ClearWaypoints();
    IPosition GetNextWaypoint();
    void SetWaypoints(IEnumerable<IPosition> positions);
    void SetWaypoints(params IPosition[] positions);
}
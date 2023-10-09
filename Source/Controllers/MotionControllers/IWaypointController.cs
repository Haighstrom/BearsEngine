using BearsEngine.Source.Controllers.MotionControllers;

namespace BearsEngine.Controllers;

public interface IWaypointController : IAddable, IUpdatable
{
    IPosition CurrentPosition { get; }
    IPosition NextWaypoint { get; }
    bool ReachedDestination { get; }
    List<IPosition> Waypoints { get; }

    event EventHandler<EventArgs>? Arrived;
    event EventHandler<DirectionChangedEventArgs>? DirectionChanged;

    void AddWaypoints(IEnumerable<IPosition> positions);
    void AddWaypoints(params IPosition[] positions);
    void ClearWaypoints();
    void SetWaypoints(IEnumerable<IPosition> positions);
    void SetWaypoints(params IPosition[] positions);
}
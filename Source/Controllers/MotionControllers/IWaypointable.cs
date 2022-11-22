namespace BearsEngine.Controllers;

public interface IWaypointable : IMoveable
{
    WaypointController WaypointController { get; }
}
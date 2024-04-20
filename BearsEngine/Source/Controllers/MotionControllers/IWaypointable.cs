namespace BearsEngine.Controllers;

public interface IWaypointable : IMoveable
{
    IWaypointController WaypointController { get; }
}
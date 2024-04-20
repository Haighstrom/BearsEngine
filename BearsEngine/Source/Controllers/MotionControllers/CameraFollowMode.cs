namespace BearsEngine.Controllers;

[Flags]
public enum CameraFollowMode
{
    /// <summary>
    /// Camera will center on the entity being followed
    /// </summary>
    Default = 0,

    /// <summary>
    /// The camera will stop following if it reaches its bounds
    /// </summary>
    StopAtEdges = 1 << 0,

    /// <summary>
    /// In the case where the camera view is bigger than the map in a given axis, it will centre the view such that the map in that axis is centred, and the "outside" of the map is evenly spread on each side.
    /// </summary>
    CentreIfWindowBiggerThanMap = 1 << 1,
}
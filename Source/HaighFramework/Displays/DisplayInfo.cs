namespace HaighFramework.Displays;

/// <summary>
/// An object representing a single output display such as a monitor
/// </summary>
/// <param name="DisplayName">Platform specific display name.
/// <para>Windows: Name as reported by the system, found programmatically in <see cref="DISPLAY_DEVICE.DeviceID"/></para>
/// </param>
/// <param name="DisplayIndex">The index of the display.</param>
/// <param name="IsPrimary">Whether this display is the primary display of this computer.</param>
/// <param name="X">The top left X-coordinate of the display (may not be 0 if there are multiple displays).</param>
/// <param name="Y">The top left Y-coordinate of the display (may not be 0 if there are multiple displays).</param>
/// <param name="Width">The width of the display in pixels.</param>
/// <param name="Height">The height of the display in pixels.</param>
/// <param name="ColourDepth">The number of bits in the colour buffer.</param>
/// <param name="RefreshRate">Wow many times per second the display is able to draw a new image.</param>
/// <param name="AvailableSettings">All <see cref="DisplaySettings"/> available to this device.</param>
public record DisplayInfo(string DisplayName, int DisplayIndex, bool IsPrimary, int X, int Y, int Width, int Height, int ColourDepth, int RefreshRate, IEnumerable<DisplaySettings> AvailableSettings)
{
    /// <summary>
    /// The centre of the display, in pixels. Bear in mind if there is more than one display the top left of the monitor may not be considered (0,0).
    /// </summary>
    public Point Centre => new(X + Width / 2, Y + Height / 2);

    public override string ToString()
    {
        return $"Device {DisplayIndex}{(IsPrimary ? " (Primary Display)" : "")}:\n  Name: {DisplayName},\n  Position:({X},{Y},{Width},{Height}),\n  ColourDepth:{ColourDepth}\n  DisplayFrequency:{RefreshRate}.\n  Available Settings: {AvailableSettings.Count()}";
    }
}
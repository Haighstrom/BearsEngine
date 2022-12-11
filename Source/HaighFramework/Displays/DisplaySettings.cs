namespace HaighFramework.Displays;

/// <summary>
/// The width, height and refreshrate of a display.
/// </summary>
/// <param name="Width">The width in pixels of the display.</param>
/// <param name="Height">The height in pixels of the display.</param>
/// <param name="RefreshRate">The refresh rate of the display, in Hz.</param>
public record class DisplaySettings(int Width, int Height, int RefreshRate);
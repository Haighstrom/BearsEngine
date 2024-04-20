using HaighFramework.Displays;
using System.Collections.Immutable;

namespace BearsEngine;

/// <summary>
/// Provides a static entrypoint to access to Monitor information and features
/// </summary>
public static class Displays
{
    private static IDisplayManager? _instance;

    internal static IDisplayManager Instance
    {
        get
        {
            if (_instance is null)
                throw new InvalidOperationException($"You must call {nameof(GameEngine)}.{nameof(GameEngine.Run)} before accessing functions within {nameof(Displays)}.");

            return _instance;
        }
        set => _instance = value;
    }

    /// <summary>
    /// The displays currently available to this computer.
    /// </summary>
    public static IImmutableList<DisplayInfo> AvailableDisplays => Instance.AvailableDisplays;

    /// <summary>
    /// The main display.
    /// </summary>
    public static DisplayInfo PrimaryDisplay => Instance.PrimaryDisplay;

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    /// <param name="newRefreshRate">The new refresh rate of the display.</param>
    public static void ChangeSettings(DisplayInfo device, int newWidth, int newHeight, int newRefreshRate) => Instance.ChangeSettings(device, newWidth, newHeight, newRefreshRate);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newSettings">The new settings to be applied.</param>
    public static void ChangeSettings(DisplayInfo display, DisplaySettings newSettings) => ChangeSettings(display, newSettings.Width, newSettings.Height, newSettings.RefreshRate);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    public static void ChangeSettings(DisplayInfo display, int newWidth, int newHeight) => ChangeSettings(display, newWidth, newHeight, display.RefreshRate);

    /// <summary>
    /// Revert all displays back to their original settings.
    /// </summary>
    public static void ResetSettings() => Instance.ResetSettings();
}
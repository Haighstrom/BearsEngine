using System.Collections.Immutable;

namespace HaighFramework.Displays;

/// <summary>
/// A utility for identifying and managing displays
/// </summary>
public interface IDisplayManager : IDisposable
{
    /// <summary>
    /// The displays currently available to this computer
    /// </summary>
    IImmutableList<DisplayInfo> AvailableDisplays { get; }

    /// <summary>
    /// The main display
    /// </summary>
    DisplayInfo PrimaryDisplay { get; }

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    /// <param name="newRefreshRate">The new refresh rate of the display.</param>
    void ChangeSettings(DisplayInfo display, int newWidth, int newHeight, int newRefreshRate);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newSettings">The new settings to be applied.</param>
    void ChangeSettings(DisplayInfo display, DisplaySettings newSettings);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    void ChangeSettings(DisplayInfo display, int newWidth, int newHeight);

    /// <summary>
    /// Revert all displays back to their original settings.
    /// </summary>
    void ResetSettings();
}
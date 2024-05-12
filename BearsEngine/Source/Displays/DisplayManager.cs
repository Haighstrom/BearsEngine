using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace BearsEngine.Displays;

/// <summary>
/// A utility for identifying and managing displays
/// </summary>
public sealed class DisplayManager : IDisplayManager
{
    private bool _disposed = false;
    private readonly IDisplayManager _api;

    /// <summary>
    /// Initialise the default DisplayManager.
    /// </summary>
    /// <exception cref="NotImplementedException">Throws an exception if there is no API available for the current OS.</exception>
    public DisplayManager()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _api = new Windows.WinAPIDisplayManager();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// The displays currently available to this computer.
    /// </summary>
    public IImmutableList<DisplayInfo> AvailableDisplays => _api.AvailableDisplays;

    /// <summary>
    /// The main display.
    /// </summary>
    public DisplayInfo PrimaryDisplay => _api.PrimaryDisplay;

    /// <summary>
    /// Event which is triggered when display settings are changed, for exmaple disconnecting or connecting a monitor, or the resolution of a display is changed.
    /// </summary>
    public event EventHandler DisplaySettingsChanged
    {
        add => _api.DisplaySettingsChanged += value;

        remove => _api.DisplaySettingsChanged -= value;
    }

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    /// <param name="newRefreshRate">The new refresh rate of the display.</param>
    public bool ChangeSettings(DisplayInfo display, int newWidth, int newHeight, int newRefreshRate) => _api.ChangeSettings(display, newWidth, newHeight, newRefreshRate);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newSettings">The new settings to be applied.</param>
    public bool ChangeSettings(DisplayInfo display, DisplaySettings newSettings) => _api.ChangeSettings(display, newSettings);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    public bool ChangeSettings(DisplayInfo display, int newWidth, int newHeight) => _api.ChangeSettings(display, newWidth, newHeight);

    /// <summary>
    /// Revert all displays back to their original settings.
    /// </summary>
    public void ResetSettings() => _api.ResetSettings();

    private void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                _api.Dispose();
            }
            else
            {
                Log.Warning($"Did not dispose {nameof(DisplayManager)} correctly.");
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }
}

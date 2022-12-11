using HaighFramework.Displays.WinAPI;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace HaighFramework.Displays;

/// <summary>
/// A utility for identifying and managing displays
/// </summary>
public sealed class DisplayManager : IDisplayManager
{
    private readonly IDisplayAPI _api;
    private bool _disposed = false;

    /// <summary>
    /// Initialise the default DisplayManager
    /// </summary>
    /// <exception cref="NotImplementedException">Throws an exception if there is no API available for the current OS.</exception>
    public DisplayManager()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            _api = new WindowsDisplayAPI();
        else
            throw new NotImplementedException();

        Log.Information("-------Display Devices-------");

        (AvailableDisplays, PrimaryDisplay) = _api.GetAvailableDevices();

        foreach (DisplayInfo display in AvailableDisplays)
            Log.Information(display);
        Log.Information("-----------------------------\n");

        _api.DisplaySettingsChanged += OnDisplaySettingsChanged;
    }

    /// <summary>
    /// The displays currently available to this computer
    /// </summary>
    public IImmutableList<DisplayInfo> AvailableDisplays { get; private set; }

    /// <summary>
    /// The main display
    /// </summary>
    public DisplayInfo PrimaryDisplay { get; private set; }

    private void OnDisplaySettingsChanged(object? sender, EventArgs e)
    {
        Log.Information("Change in display settings detected. Refreshing display devices.");

        (AvailableDisplays, PrimaryDisplay) = _api.GetAvailableDevices();

        foreach (DisplayInfo display in AvailableDisplays)
            Log.Information(display);
    }

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    /// <param name="newRefreshRate">The new refresh rate of the display.</param>
    public void ChangeSettings(DisplayInfo device, int newWidth, int newHeight, int newRefreshRate)
    {
        _api.ChangeSettings(device.DisplayName, newWidth, newHeight, newRefreshRate);
        OnDisplaySettingsChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newSettings">The new settings to be applied.</param>
    public void ChangeSettings(DisplayInfo device, DisplaySettings newSettings) => ChangeSettings(device, newSettings.Width, newSettings.Height, newSettings.RefreshRate);

    /// <summary>
    /// Change the settings of a display. Valid settings can be identified via the <see cref="AvailableDisplays"/> property.
    /// </summary>
    /// <param name="display">The display to be changed.</param>
    /// <param name="newWidth">The new width of the display.</param>
    /// <param name="newHeight">The new height of the display.</param>
    public void ChangeSettings(DisplayInfo device, int newWidth, int newHeight) => ChangeSettings(device, newWidth, newHeight, device.RefreshRate);

    /// <summary>
    /// Revert all displays back to their original settings.
    /// </summary>
    public void ResetSettings()
    {
        _api.ResetSettings();
    }

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

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }
}

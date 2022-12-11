using System.Collections.Immutable;

namespace HaighFramework.Displays;

internal interface IDisplayAPI : IDisposable
{
    bool ChangeSettings(string deviceName, int newWidth, int newHeight, int newRefreshRate);

    (IImmutableList<DisplayInfo> Displays, DisplayInfo Primary) GetAvailableDevices();

    void ResetSettings();

    event EventHandler DisplaySettingsChanged;
}
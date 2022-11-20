using BearsEngine.DisplayDevices;
using System.Collections.ObjectModel;

namespace BearsEngine.Display;

public interface IDisplay
{
    IList<DisplayDeviceSettings> AvailableSettings { get; }

    Point Centre { get; }

    int ColourDepth { get; }

    Rect Position { get; }

    float RefreshRate { get; }

    DisplayDeviceSettings Settings { get; }

    void ChangeSettings(DisplayDeviceSettings targetSettings);

    void RestoreSettings();
}
using System.Collections.ObjectModel;

namespace BearsEngine.DisplayDevices;

public interface IDisplayDevice
{
    void ChangeSettings(DisplayDeviceSettings targetSettings);
    void ChangeSettings(int width, int height, int colourDepth, float refreshRate);
    void ChangeResolution(int width, int height);
    void RestoreSettings();

    string DeviceID { get; }
    DisplayDeviceIndex DeviceIndex { get; }
    bool IsPrimary { get; }
    DisplayDeviceSettings Settings { get; }
    DisplayDeviceSettings OriginalSettings { get; }
    ReadOnlyCollection<DisplayDeviceSettings> AvailableSettings { get; }
    ReadOnlyCollection<DisplayDeviceSettings> AvailableResolutions { get; }
    Rect BoundingRect { get; }
    int X { get; }
    int Y { get; }
    int Width { get; }
    int Height { get; }
    Point Centre { get; }
    int CentreX { get; }
    int CentreY { get; }
    int ColourDepth { get; }
    float RefreshRate { get; }
}

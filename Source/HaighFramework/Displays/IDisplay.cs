using System.Collections.ObjectModel;

namespace BearsEngine.Displays;

public interface IDisplay
{
    void ChangeSettings(DisplaySettings targetSettings);
    void ChangeSettings(int width, int height, float refreshRate);
    void ChangeResolution(int width, int height);
    void RestoreSettings();

    string DeviceID { get; }
    int DeviceIndex { get; }
    bool IsPrimary { get; }
    DisplaySettings Settings { get; }
    DisplaySettings OriginalSettings { get; }
    ReadOnlyCollection<DisplaySettings> AvailableSettings { get; }
    ReadOnlyCollection<DisplaySettings> AvailableResolutions { get; }
    Rect BoundingRect { get; }
    int X { get; }
    int Y { get; }
    int Width { get; }
    int Height { get; }
    Point Centre { get; }
    int CentreX { get; }
    int CentreY { get; }
    int ColourDepth { get; }
    float DisplayFrequency { get; }
}
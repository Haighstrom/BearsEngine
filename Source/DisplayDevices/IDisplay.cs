using System.Collections.Immutable;

namespace BearsEngine.DisplayDevices;

public interface IDisplay
{
    IImmutableList<DisplayDeviceSettings> AvailableSettings { get; }

    Point Centre { get; }

    int ColourDepth { get; }

    Rect Position { get; }

    float RefreshRate { get; }

    DisplayDeviceSettings Settings { get; }

    void ChangeSettings(DisplayDeviceSettings targetSettings);

    void RestoreSettings();
}
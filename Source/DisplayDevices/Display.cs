using System.Collections.Immutable;

namespace BearsEngine.DisplayDevices;

public class Display : IDisplay
{
    public Display(IEnumerable<DisplayDeviceSettings> availableSettings)
    {
        AvailableSettings = availableSettings.ToImmutableList();
    }

    public IImmutableList<DisplayDeviceSettings> AvailableSettings { get; }

    public Point Centre => throw new NotImplementedException();

    public int ColourDepth => throw new NotImplementedException();

    public Rect Position => throw new NotImplementedException();

    public float RefreshRate => throw new NotImplementedException();

    public DisplayDeviceSettings Settings => throw new NotImplementedException();

    public void ChangeSettings(DisplayDeviceSettings targetSettings)
    {
        throw new NotImplementedException();
    }

    public void RestoreSettings()
    {
        throw new NotImplementedException();
    }
}
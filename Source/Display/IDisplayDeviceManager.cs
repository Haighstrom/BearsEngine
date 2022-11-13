namespace BearsEngine.DisplayDevices;

public interface IDisplayDeviceManager : IDisposable
{
    List<IDisplayDevice> AvailableDevices { get; }
    IDisplayDevice PrimaryDevice { get; }
    IDisplayDevice GetDevice(DisplayDeviceIndex index);

    void ChangeSettings(IDisplayDevice device, DisplayDeviceSettings settings);
    void ChangeSettings(IDisplayDevice device, int width, int height, int colourDepth, float refreshRate);

    void ChangeSettings(DisplayDeviceIndex index, DisplayDeviceSettings settings);
    void ChangeSettings(DisplayDeviceIndex index, int width, int height, int colourDepth, float refreshRate);

    void ChangeResolution(IDisplayDevice device, int width, int height);
    void ChangeResolution(DisplayDeviceIndex index, int width, int height);

    void RestoreSettings();
    void RestoreSettings(IDisplayDevice device);
    void RestoreSettings(DisplayDeviceIndex index);
}

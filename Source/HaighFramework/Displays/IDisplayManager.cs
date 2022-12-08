namespace BearsEngine.Displays;

public interface IDisplayManager : IDisposable
{
    IList<IDisplay> AvailableDevices { get; }
    IDisplay PrimaryDevice { get; }

    void ChangeSettings(IDisplay device, DisplaySettings settings);
    void ChangeSettings(IDisplay device, int width, int height, float refreshRate);

    void ChangeResolution(IDisplay device, int width, int height);

    void RestoreSettings();
    void RestoreSettings(IDisplay device);
}
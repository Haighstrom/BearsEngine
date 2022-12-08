using BearsEngine.Win32API;
using Microsoft.Win32;

namespace BearsEngine.Displays;

public sealed class DisplayManager : IDisplayManager
{    
    public DisplayManager() 
    {
        RefreshDevices();
        SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
    }
    
    private void OnDisplaySettingsChanged(object? sender, EventArgs e)
    {
        Log.Information("Change in display settings detected. Refreshing display devices.");
        RefreshDevices();
    }

    private void RefreshDevices()
    {
        Log.Information("-------Display Devices-------");

        //save old device list so we can copy device "original settings" if it existed before
        //this is to cover the case that user has changed resolution from this program (which will later want to be restored), and then changed windows settings, triggering this function
        IDisplay[] previousDevices = AvailableDevices.ToArray();

        AvailableDevices.Clear();

        IDisplay device;
        DisplaySettings curSettings = null;
        List<DisplaySettings> availableSettings = new();
        bool isPrimary = false;
        int deviceCount = 0, settingsCount = 0;
        DISPLAY_DEVICE win32DisplayDevice = new();

        while (User32.EnumDisplayDevices(IntPtr.Zero, deviceCount++, win32DisplayDevice, 0))
        {
            if ((win32DisplayDevice.StateFlags & DisplayDeviceStateFlags.AttachedToDesktop) == 0) continue;

            DEVMODE dm = new();

            if (User32.EnumDisplaySettingsEx(win32DisplayDevice.DeviceName, DisplayModeSettingsEnum.CurrentSettings, dm, 0) || User32.EnumDisplaySettingsEx(win32DisplayDevice.DeviceName, DisplayModeSettingsEnum.RegistrySettings, dm, 0))
            {
                //todo: DPI (GetSCale())
                curSettings = new DisplaySettings(dm.Position.X, dm.Position.Y, dm.PelsWidth, dm.PelsHeight, dm.DisplayFrequency);

                isPrimary = (win32DisplayDevice.StateFlags & DisplayDeviceStateFlags.PrimaryDevice) != 0;
            }

            availableSettings.Clear();
            settingsCount = 0;
            while (User32.EnumDisplaySettingsEx(win32DisplayDevice.DeviceName, settingsCount++, dm, 0))
            {
                //todo: DPI
                DisplaySettings settings = new(dm.Position.X, dm.Position.Y, dm.PelsWidth, dm.PelsHeight, dm.DisplayFrequency);

                availableSettings.Add(settings);
            }

            device = new Display(win32DisplayDevice.DeviceName, deviceCount - 1, isPrimary, dm.BitsPerPel, curSettings, availableSettings);

            //set device.
            foreach (IDisplay prevDevice in previousDevices)
            {
                if (device.DeviceID == prevDevice.DeviceID)
                {
                    ((Display)device).OriginalSettings = prevDevice.OriginalSettings;
                }
            }

            AvailableDevices.Add(device);
            if (isPrimary) PrimaryDevice = device;

            Log.Information(device.ToString());
            Log.Information("");
        }

        Log.Information("-----------------------------\n");
    }
    
    public IDisplay PrimaryDevice { get; private set; }

    public IList<IDisplay> AvailableDevices { get; } = new List<IDisplay>();
    
    public void ChangeSettings(IDisplay device, DisplaySettings settings)
    {
        device.ChangeSettings(settings);
    }

    public void ChangeSettings(IDisplay device, int width, int height, float refreshRate)
    {
        device.ChangeSettings(width, height, refreshRate);
    }
    
    public void ChangeResolution(IDisplay device, int width, int height)
    {
        device.ChangeResolution(width, height);
    }
    
    public void RestoreSettings()
    {
        foreach (IDisplay device in AvailableDevices)
            device.RestoreSettings();
    }

    public void RestoreSettings(IDisplay device)
    {
        device.RestoreSettings();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool calledFromDispose)
    {
        //todo: get this working again
        SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;
        RestoreSettings();
    }

    ~DisplayManager()
    {
        Dispose(false);
    }
}

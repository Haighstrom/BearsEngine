using BearsEngine.Win32API;

namespace BearsEngine.DisplayDevices;

public sealed class DisplayDeviceManager : IDisplayDeviceManager
{
    #region Fields
    private readonly List<IDisplayDevice> _availableDevices = new();
    private IDisplayDevice _primaryDevice;
    #endregion

    #region Constructors
    public DisplayDeviceManager() 
    {
        RefreshDevices();
        //todo: get this working again
        //SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
    }
    #endregion

    #region Private Methods
    private void OnDisplaySettingsChanged(object sender, EventArgs e)
    {
        Console.WriteLine("Change in display settings detected. Refreshing display devices. \n");
        RefreshDevices();
    }

    #region RefreshDevices
    private void RefreshDevices()
    {
        Console.WriteLine("-------Display Devices-------\n");

        //save old device list so we can copy device "original settings" if it existed before
        //this is to cover the case that user has changed resolution from this program (which will later want to be restored), and then changed windows settings, triggering this function
        IDisplayDevice[] previousDevices = _availableDevices.ToArray();

        _availableDevices.Clear();

        IDisplayDevice device;
        DisplayDeviceSettings curSettings = null;
        List<DisplayDeviceSettings> availableSettings = new();
        bool isPrimary = false;
        int deviceCount = 0, settingsCount = 0;
        DISPLAY_DEVICE win32DisplayDevice = new();

        while (User32.EnumDisplayDevices(null, deviceCount++, win32DisplayDevice, 0))
        {
            if ((win32DisplayDevice.StateFlags & DisplayDeviceStateFlags.AttachedToDesktop) == 0) continue;

            DeviceMode dm = new();

            if (User32.EnumDisplaySettingsEx(win32DisplayDevice.DeviceName, DisplayModeSettingsEnum.CurrentSettings, dm, 0) || User32.EnumDisplaySettingsEx(win32DisplayDevice.DeviceName, DisplayModeSettingsEnum.RegistrySettings, dm, 0))
            {
                //todo: DPI (GetSCale())
                curSettings = new DisplayDeviceSettings(dm.Position.X, dm.Position.Y, dm.PelsWidth, dm.PelsHeight, dm.BitsPerPel, dm.DisplayFrequency);

                isPrimary = (win32DisplayDevice.StateFlags & DisplayDeviceStateFlags.PrimaryDevice) != 0;
            }

            availableSettings.Clear();
            settingsCount = 0;
            while (User32.EnumDisplaySettingsEx(win32DisplayDevice.DeviceName, settingsCount++, dm, 0))
            {
                //todo: DPI
                DisplayDeviceSettings settings = new(dm.Position.X, dm.Position.Y, dm.PelsWidth, dm.PelsHeight, dm.BitsPerPel, dm.DisplayFrequency);

                availableSettings.Add(settings);
            }

            device = new DisplayDevice(win32DisplayDevice.DeviceName, (DisplayDeviceIndex)(deviceCount - 1), isPrimary, curSettings, availableSettings);

            //set device.
            foreach (IDisplayDevice prevDevice in previousDevices)
            {
                if (device.DeviceID == prevDevice.DeviceID)
                {
                    ((DisplayDevice)device).OriginalSettings = prevDevice.OriginalSettings;
                }
            }

            _availableDevices.Add(device);
            if (isPrimary) _primaryDevice = device;

            Console.WriteLine(device.ToString());
            Console.WriteLine();
        }

        Console.WriteLine("-----------------------------\n");
    }
    #endregion
    #endregion

    #region IDisplayDeviceManager
    #region PrimaryDevice
    public IDisplayDevice PrimaryDevice
    {
        get { return _primaryDevice; }
    }
    #endregion

    #region AvailableDevices
    public List<IDisplayDevice> AvailableDevices
    {
        get { return _availableDevices; }
    }
    #endregion

    #region GetDevice
    public IDisplayDevice GetDevice(DisplayDeviceIndex index)
    {
        if (index == DisplayDeviceIndex.Primary) return _primaryDevice;
        else if ((int)index < _availableDevices.Count)
            return _availableDevices[(int)index];
        else 
            return null;
    }
    #endregion

    #region ChangeSettings
    public void ChangeSettings(IDisplayDevice device, DisplayDeviceSettings settings)
    {
        device.ChangeSettings(settings);
    }
    public void ChangeSettings(IDisplayDevice device, int width, int height, int colourDepth, float refreshRate)
    {
        device.ChangeSettings(width, height, colourDepth, refreshRate);
    }
    public void ChangeSettings(DisplayDeviceIndex index, DisplayDeviceSettings settings)
    {
        GetDevice(index).ChangeSettings(settings);
    }
    public void ChangeSettings(DisplayDeviceIndex index, int width, int height, int colourDepth, float refreshRate)
    {
        GetDevice(index).ChangeSettings(width, height, colourDepth, refreshRate);
    }
    #endregion
    
    #region ChangeResolution
    public void ChangeResolution(IDisplayDevice device, int width, int height)
    {
        device.ChangeResolution(width, height);
    }
    public void ChangeResolution(DisplayDeviceIndex index, int width, int height)
    {
        GetDevice(index).ChangeResolution(width, height);
    }
    #endregion

    #region RestoreSettings
    public void RestoreSettings()
    {
        foreach (IDisplayDevice device in _availableDevices)
            device.RestoreSettings();
    }
    public void RestoreSettings(IDisplayDevice device)
    {
        device.RestoreSettings();
    }

    public void RestoreSettings(DisplayDeviceIndex index)
    {
        GetDevice(index).RestoreSettings();
    }
    #endregion
    #endregion

    #region Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool calledFromDispose)
    {
        //todo: why was this called twice?
        //todo: get this working again
        //SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;
        //SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;
        RestoreSettings();
    }
    ~DisplayDeviceManager()
    {
        Dispose(false);
    }
    #endregion
}

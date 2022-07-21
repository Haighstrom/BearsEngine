using System.Collections.ObjectModel;
using BearsEngine.Win32API;

namespace BearsEngine.DisplayDevices;

public class DisplayDevice : IDisplayDevice
{
    private const float MAX_REFRESH_RATE_DIFFERENCE = 3.0f;

    #region Fields
    private List<DisplayDeviceSettings> _availableSettings;
    #endregion

    #region Constructors
    internal DisplayDevice(string deviceID, DisplayDeviceIndex index, bool primary, DisplayDeviceSettings currentSettings, IEnumerable<DisplayDeviceSettings> availableSettings)
    {
        DeviceID = deviceID;
        DeviceIndex = index;
        IsPrimary = primary;
        Settings = currentSettings;
        _availableSettings = new List<DisplayDeviceSettings>(availableSettings);
    }
    #endregion

    #region Properties
    public string DeviceID { get; }
    public DisplayDeviceIndex DeviceIndex { get; }
    public bool IsPrimary { get; }
    public DisplayDeviceSettings Settings { get; private set; }
    public Rect BoundingRect => Settings.BoundingRect;

    #region X
    public int X { get { return Settings.X; } }
    #endregion

    #region Y
    public int Y { get { return Settings.Y; } }
    #endregion

    #region Width
    public int Width
    {
        get { return Settings.Width; }
    }
    #endregion

    #region Height
    public int Height
    {
        get { return Settings.Height; }
    }
    #endregion

    #region ColourDepth
    public int ColourDepth
    {
        get { return Settings.ColourDepth; }
    }
    #endregion

    #region RefreshRate
    public float RefreshRate
    {
        get { return Settings.RefreshRate; }
    }
    #endregion

    #region OriginalSettings
    public DisplayDeviceSettings OriginalSettings { get; internal set; }
    #endregion
    #endregion

    #region Methods
    public DisplayDeviceSettings GetSettings(int width, int height)
    {
        return AvailableResolutions.Single(x => x.Width == width && x.Height == height);
    }
    public DisplayDeviceSettings GetSettings(int width, int height, int colourDepth, float refreshRate)
    {
        return _availableSettings.Find(x =>
               x.Width == width &&
               x.Height == height &&
               x.ColourDepth == colourDepth &&
               x.RefreshRate == refreshRate);
    }
    #endregion

    #region Private Methods
    private bool TryChangeSettings(DisplayDeviceSettings targetSettings)
    {
        DeviceMode mode = null;
        if (targetSettings != null)
        {
            mode = new DeviceMode()
            {
                PelsWidth = targetSettings.Width,
                PelsHeight = targetSettings.Height,
                BitsPerPel = targetSettings.ColourDepth,
                //todo: refreshrate should be an int if it only takes ints here...?
                DisplayFrequency = (int)targetSettings.RefreshRate,
                Fields = DeviceModeEnum.DM_BITSPERPEL | DeviceModeEnum.DM_PELSWIDTH | DeviceModeEnum.DM_PELSHEIGHT | DeviceModeEnum.DM_DISPLAYFREQUENCY
            };
        }
        //DISP_CHANGE_SUCCESSFUL = 0;
        return User32.ChangeDisplaySettingsEx(DeviceID, mode, IntPtr.Zero, ChangeDisplaySettingsEnum.Fullscreen, IntPtr.Zero) == 0;
    }
    private bool TryRestoreSettings()
    {
        return TryChangeSettings(null);
    }
    #endregion

    #region IDisplayDevice
    public void ChangeSettings(int width, int height, int colourDepth, float refreshRate)
    {
        ChangeSettings(GetSettings(width, height, colourDepth, refreshRate));
    }

    public void ChangeResolution(int width, int height)
    {
        ChangeSettings(GetSettings(width, height));
    }

    public void ChangeSettings(DisplayDeviceSettings targetSettings)
    {
        if (targetSettings == null) throw new ArgumentNullException("targetSettings");

        if (targetSettings == Settings) return;
        if (TryChangeSettings(targetSettings))
        {
            if (OriginalSettings == null) OriginalSettings = targetSettings;
            Settings = targetSettings;
            return;
        }
        else throw new Exception(string.Format("DisplayDevice: {0} failed to change settings to: {1}.", this, targetSettings));
    }


    public void RestoreSettings()
    {
        if (OriginalSettings == null) return;
        if (TryRestoreSettings())
        {
            Settings = OriginalSettings;
            OriginalSettings = null;
        }
        else throw new Exception(string.Format("DisplayDevice: {0} failed to restore settings.", this));
    }

    public Point Centre
    {
        get { return new Point(CentreX, CentreY); }
    }

    public int CentreX
    {
        get { return X + Width / 2; }
    }

    #region CentreY
    public int CentreY
    {
        get { return Y + Height / 2; }
    }
    #endregion
    #region AvailableSettings
    public ReadOnlyCollection<DisplayDeviceSettings> AvailableSettings
    {
        get { return _availableSettings.AsReadOnly(); }
    }
    #endregion

    #region AvailableResolutions
    public ReadOnlyCollection<DisplayDeviceSettings> AvailableResolutions
    {
        get
        {
            List<DisplayDeviceSettings> ret = new();
            foreach (DisplayDeviceSettings s1 in _availableSettings)
            {
                //bad settings - SHOO
                if (s1.ColourDepth != ColourDepth) continue;
                if (Math.Abs(s1.RefreshRate - RefreshRate) >= MAX_REFRESH_RATE_DIFFERENCE) continue;

                //see if we already have this resolution in the list (list should have at most one entry)
                DisplayDeviceSettings s2 = ret.Find(s => s.Width == s1.Width && s.Height == s1.Height);
                if (s2 != default(DisplayDeviceSettings)) //if we already have this resolution, decide if we replace s2 with s1
                {
                    if (Math.Abs(s1.RefreshRate - RefreshRate) < Math.Abs(s2.RefreshRate - RefreshRate)) //choose the one closest to current
                    {
                        ret.Remove(s2);
                        ret.Add(s1);
                    }
                }
                else ret.Add(s1); //if s1 wasn't already in the list then add it as it passed the requirements

            }
            ret.OrderBy(x => x.Height).ThenBy(y => y.Width);

            return ret.AsReadOnly();
        }
    }
    #endregion
    #endregion

    #region Overrides
    public override string ToString()
    {
        return string.Format("Device {0}{1} \n\nWindows ID: {2} \n{3} \n{4} settings available", DeviceIndex, IsPrimary ? " (Primary Display)" : "", DeviceID, Settings.ToString(), AvailableSettings.Count);
    }
    #endregion

}

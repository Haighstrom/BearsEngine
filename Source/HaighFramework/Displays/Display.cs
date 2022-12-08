using System.Collections.ObjectModel;
using BearsEngine.Win32API;

namespace BearsEngine.Displays;

public class Display : IDisplay
{
    private const float MAX_REFRESH_RATE_DIFFERENCE = 3.0f;

    private List<DisplaySettings> _availableSettings;

    internal Display(string deviceID, int index, bool primary, int colourDepth, DisplaySettings currentSettings, IEnumerable<DisplaySettings> availableSettings)
    {
        DeviceID = deviceID;
        DeviceIndex = index;
        IsPrimary = primary;
        Settings = currentSettings;
        ColourDepth = colourDepth;
        _availableSettings = new List<DisplaySettings>(availableSettings);
    }
    
    public string DeviceID { get; }

    public int DeviceIndex { get; }

    public bool IsPrimary { get; }

    public DisplaySettings Settings { get; private set; }

    public Rect BoundingRect => Settings.BoundingRect;

    public int X => Settings.X;

    public int Y => Settings.Y;

    public int Width => Settings.Width;

    public int Height => Settings.Height;

    public int ColourDepth { get; }

    public float DisplayFrequency => Settings.DisplayFrequency;

    public DisplaySettings OriginalSettings { get; internal set; }

    public DisplaySettings GetSettings(int width, int height)
    {
        return AvailableResolutions.Single(x => x.Width == width && x.Height == height);
    }
    public DisplaySettings GetSettings(int width, int height, float displayFrequency)
    {
        return _availableSettings.Find(x =>
               x.Width == width &&
               x.Height == height &&
               x.DisplayFrequency == displayFrequency);
    }

    private bool TryChangeSettings(DisplaySettings targetSettings)
    {
        DEVMODE mode = null;
        if (targetSettings != null)
        {
            mode = new DEVMODE()
            {
                PelsWidth = targetSettings.Width,
                PelsHeight = targetSettings.Height,
                //todo: refreshrate should be an int if it only takes ints here...?
                DisplayFrequency = targetSettings.DisplayFrequency,
                Fields = DeviceModeEnum.DM_BITSPERPEL | DeviceModeEnum.DM_PELSWIDTH | DeviceModeEnum.DM_PELSHEIGHT | DeviceModeEnum.DM_DISPLAYFREQUENCY
            };
        }
        //DISP_CHANGE_SUCCESSFUL = 0;
        return User32.ChangeDisplaySettingsEx(DeviceID, mode, IntPtr.Zero, CHANGEDISPLAYSETTINGSFLAGS.CDS_FULLSCREEN, IntPtr.Zero) == 0;
    }

    private bool TryRestoreSettings() => TryChangeSettings(null);

    public void ChangeSettings(int width, int height, float refreshRate)
    {
        ChangeSettings(GetSettings(width, height, refreshRate));
    }

    public void ChangeResolution(int width, int height)
    {
        ChangeSettings(GetSettings(width, height));
    }

    public void ChangeSettings(DisplaySettings targetSettings)
    {
        if (targetSettings == null) throw new ArgumentNullException(nameof(targetSettings));

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

    public Point Centre => new(CentreX, CentreY);

    public int CentreX => X + Width / 2;

    public int CentreY => Y + Height / 2;

    public ReadOnlyCollection<DisplaySettings> AvailableSettings => _availableSettings.AsReadOnly();

    public ReadOnlyCollection<DisplaySettings> AvailableResolutions
    {
        get
        {
            List<DisplaySettings> ret = new();
            foreach (DisplaySettings s1 in _availableSettings)
            {
                //bad settings - SHOO
                if (Math.Abs(s1.DisplayFrequency - DisplayFrequency) >= MAX_REFRESH_RATE_DIFFERENCE) continue;

                //see if we already have this resolution in the list (list should have at most one entry)
                DisplaySettings s2 = ret.Find(s => s.Width == s1.Width && s.Height == s1.Height);
                if (s2 != default(DisplaySettings)) //if we already have this resolution, decide if we replace s2 with s1
                {
                    if (Math.Abs(s1.DisplayFrequency - DisplayFrequency) < Math.Abs(s2.DisplayFrequency - DisplayFrequency)) //choose the one closest to current
                    {
                        ret.Remove(s2);
                        ret.Add(s1);
                    }
                }
                else ret.Add(s1); //if s1 wasn't already in the list then add it as it passed the requirements

            }
            _ = ret.OrderBy(x => x.Height).ThenBy(y => y.Width);

            return ret.AsReadOnly();
        }
    }

    public override string ToString()
    {
        return string.Format("Device {0}{1} \n\nWindows ID: {2} \n{3} \n{4} settings available", DeviceIndex, IsPrimary ? " (Primary Display)" : "", DeviceID, Settings.ToString(), AvailableSettings.Count);
    }
}

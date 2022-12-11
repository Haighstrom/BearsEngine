using HaighFramework.Win32API;
using Microsoft.Win32;
using System.Collections.Immutable;

namespace HaighFramework.Displays.WinAPI;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This is part of the Windows API which will only be invoked if on Windows platform.")]
internal sealed class WindowsDisplayAPI : IDisplayAPI
{
    private bool isDisposed;

    public WindowsDisplayAPI()
    {
        SystemEvents.DisplaySettingsChanged += DisplaySettingsChanged;
    }

    public bool ChangeSettings(string deviceName, int newWidth, int newHeight, int newRefreshRate)
    {
        DEVMODE mode = new()
        {
            PelsWidth = newWidth,
            PelsHeight = newHeight,
            DisplayFrequency = newRefreshRate,
            Fields = DeviceModeEnum.DM_PELSWIDTH | DeviceModeEnum.DM_PELSHEIGHT | DeviceModeEnum.DM_DISPLAYFREQUENCY
        };
        
        return User32.ChangeDisplaySettingsEx(deviceName, mode, IntPtr.Zero, CHANGEDISPLAYSETTINGSFLAGS.CDS_FULLSCREEN, IntPtr.Zero) == DISPCHANGERESULT.DISP_CHANGE_SUCCESSFUL;
    }

    public (IImmutableList<DisplayInfo> Displays, DisplayInfo Primary) GetAvailableDevices()
    {
        List<DisplayInfo> displays = new();
        DisplayInfo? primary = null;

        int iDevNum = 0;
        DISPLAY_DEVICE deviceInfo = new();

        while (User32.EnumDisplayDevices(IntPtr.Zero, iDevNum++, deviceInfo, 0))
        {
            if (!deviceInfo.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
                continue;

            DEVMODE dm = new();
            int iModeNum = 0;
            List<DisplaySettings> availableSettings = new();

            while (User32.EnumDisplaySettingsEx(deviceInfo.DeviceName, iModeNum++, dm, 0))
                availableSettings.Add(new(dm.PelsWidth, dm.PelsHeight, dm.DisplayFrequency));

            if (!User32.EnumDisplaySettingsEx(deviceInfo.DeviceName, DisplayModeSettingsEnum.CurrentSettings, dm, 0))
                if (!User32.EnumDisplaySettingsEx(deviceInfo.DeviceName, DisplayModeSettingsEnum.RegistrySettings, dm, 0))
                {
                    Log.Error($"{nameof(User32.EnumDisplayDevices)} returned a display {deviceInfo.DeviceName} but {nameof(User32.EnumDisplaySettingsEx)} threw an error. Skipping display.");
                    continue;
                }

            bool isPrimary = deviceInfo.StateFlags.HasFlag(DisplayDeviceStateFlags.PrimaryDevice);

            DisplayInfo display = new DisplayInfo(deviceInfo.DeviceName, iDevNum - 1, isPrimary, dm.Position.X, dm.Position.Y, dm.PelsWidth, dm.PelsHeight, dm.BitsPerPel, dm.DisplayFrequency, availableSettings);
            displays.Add(display);

            if (isPrimary)
                primary = display;
        }

        if (primary is null)
            throw new InvalidOperationException($"{nameof(IDisplayAPI.GetAvailableDevices)} did not identify any primary display.");

        return (displays.ToImmutableList(), primary);
    }

    public void ResetSettings()
    {
        User32.ChangeDisplaySettings(IntPtr.Zero, 0);
    }

    public event EventHandler? DisplaySettingsChanged;

    private void Dispose(bool disposedCorrectly)
    {
        if (!isDisposed)
        {
            if (!disposedCorrectly)
            {
                Log.Warning($"Did not dispose {nameof(WindowsDisplayAPI)} correctly.");
            }

            SystemEvents.DisplaySettingsChanged -= DisplaySettingsChanged;

            isDisposed = true;
        }
    }

    ~WindowsDisplayAPI()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: false);
    }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }
}
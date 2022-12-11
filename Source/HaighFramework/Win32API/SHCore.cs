using System.Runtime.InteropServices;
using System.Security;

namespace HaighFramework.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class SHCore
{
    private const string Library = "Shcore.dll";

    /// <summary>
    /// It is recommended that you set the process-default DPI awareness via application manifest. See Setting the default DPI awareness for a process for more information. Setting the process-default DPI awareness via API call can lead to unexpected application behavior. Sets the process-default DPI awareness level.This is equivalent to calling SetProcessDpiAwarenessContext with the corresponding DPI_AWARENESS_CONTEXT value.
    /// </summary>
    /// <param name="value">The DPI awareness value to set. Possible values are from the PROCESS_DPI_AWARENESS enumeration.</param>
    /// <returns>This function returns one of the following values. S_OK: The DPI awareness for the app was set successfully. E_INVALIDARG: The value passed in is not valid. E_ACCESSDENIED: The DPI awareness is already set, either by calling this API previously or through the application (.exe) manifest.</returns>
    [DllImport(Library)]
    public static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS value);

    /// <summary>
    /// Queries the dots per inch (dpi) of a display.
    /// </summary>
    /// <param name="hmonitor">Handle of the monitor being queried.</param>
    /// <param name="dpiType">The type of DPI being queried. Possible values are from the MONITOR_DPI_TYPE enumeration.</param>
    /// <param name="dpiX">The value of the DPI along the X axis. This value always refers to the horizontal edge, even when the screen is rotated.</param>
    /// <param name="dpiY">The value of the DPI along the Y axis. This value always refers to the vertical edge, even when the screen is rotated.</param>
    /// <returns>This function returns one of the following values. S_OK: The function successfully returns the X and Y DPI values for the specified monitor. E_INVALIDARG: The handle, DPI type, or pointers passed in are not valid.</returns>
    [DllImport(Library)]
    public static extern uint GetDpiForMonitor(IntPtr hmonitor, MONITOR_DPI_TYPE dpiType, out uint dpiX, out uint dpiY);
}
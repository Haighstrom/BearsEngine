using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.WinAPI;

#region Enums
/// <summary>
/// Identifies the dots per inch (dpi) setting for a monitor.
/// </summary>
internal enum MONITOR_DPI_TYPE
{
    /// <summary>
    /// The effective DPI. This value should be used when determining the correct scale factor for scaling UI elements. This incorporates the scale factor set by the user for this specific display.
    /// </summary>
    MDT_EFFECTIVE_DPI = 0,

    /// <summary>
    /// The angular DPI. This DPI ensures rendering at a compliant angular resolution on the screen. This does not include the scale factor set by the user for this specific display.
    /// </summary>
    MDT_ANGULAR_DPI = 1,

    /// <summary>
    /// The raw DPI. This value is the linear DPI of the screen as measured on the screen itself. Use this value when you want to read the pixel density and not the recommended scaling setting. This does not include the scale factor set by the user for this specific display and is not guaranteed to be a supported DPI value.
    /// </summary>
    MDT_RAW_DPI = 2,

    /// <summary>
    /// The default DPI setting for a monitor is MDT_EFFECTIVE_DPI.
    /// </summary>
    MDT_DEFAULT = MDT_EFFECTIVE_DPI,
}

/// <summary>
/// Identifies dots per inch (dpi) awareness values. DPI awareness indicates how much scaling work an application performs for DPI versus how much is done by the system.
/// </summary>
internal enum PROCESS_DPI_AWARENESS
{
    /// <summary>
    /// DPI unaware. This app does not scale for DPI changes and is always assumed to have a scale factor of 100% (96 DPI). It will be automatically scaled by the system on any other DPI setting.
    /// </summary>
    PROCESS_DPI_UNAWARE = 0,

    /// <summary>
    /// System DPI aware. This app does not scale for DPI changes. It will query for the DPI once and use that value for the lifetime of the app. If the DPI changes, the app will not adjust to the new DPI value. It will be automatically scaled up or down by the system when the DPI changes from the system value.
    /// </summary>
    PROCESS_SYSTEM_DPI_AWARE = 1,

    /// <summary>
    /// Per monitor DPI aware. This app checks for the DPI when it is created and adjusts the scale factor whenever the DPI changes. These applications are not automatically scaled by the system.
    /// </summary>
    PROCESS_PER_MONITOR_DPI_AWARE = 2
}
#endregion

[SuppressUnmanagedCodeSecurity]
internal static partial class SHCore
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
    [LibraryImport(Library)]
    public static partial uint GetDpiForMonitor(IntPtr hmonitor, MONITOR_DPI_TYPE dpiType, out uint dpiX, out uint dpiY);
}

using System.Runtime.InteropServices;

namespace HaighFramework.Win32API;

/// <summary>
/// Desktop Window Manager https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/
/// </summary>
internal static class DWMAPI
{
    private const string Library = "Dwmapi.dll";

    /// <summary>
    /// Retrieves the current value of a specified Desktop Window Manager (DWM) attribute applied to a window.
    /// </summary>
    /// <param name="hwnd">The handle to the window from which the attribute value is to be retrieved.</param>
    /// <param name="dwAttribute">A flag describing which value to retrieve, specified as a value of the DWMWINDOWATTRIBUTE enumeration. This parameter specifies which attribute to retrieve, and the pvAttribute parameter points to an object into which the attribute value is retrieved.</param>
    /// <param name="pvAttribute">A pointer to a value which, when this function returns successfully, receives the current value of the attribute. The type of the retrieved value depends on the value of the dwAttribute parameter. The DWMWINDOWATTRIBUTE enumeration topic indicates, in the row for each flag, what type of value you should pass a pointer to in the pvAttribute parameter.</param>
    /// <param name="cbAttribute">The size, in bytes, of the attribute value being received via the pvAttribute parameter. The type of the retrieved value, and therefore its size in bytes, depends on the value of the dwAttribute parameter.</param>
    /// <returns>If the function succeeds, it returns S_OK (0). Otherwise, it returns an HRESULT error code.</returns>
    [DllImport(Library)]
    public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out bool pvAttribute, int cbAttribute);
    
    /// <summary>
    /// Retrieves the current value of a specified Desktop Window Manager (DWM) attribute applied to a window.
    /// </summary>
    /// <param name="hwnd">The handle to the window from which the attribute value is to be retrieved.</param>
    /// <param name="dwAttribute">A flag describing which value to retrieve, specified as a value of the DWMWINDOWATTRIBUTE enumeration. This parameter specifies which attribute to retrieve, and the pvAttribute parameter points to an object into which the attribute value is retrieved.</param>
    /// <param name="pvAttribute">A pointer to a value which, when this function returns successfully, receives the current value of the attribute. The type of the retrieved value depends on the value of the dwAttribute parameter. The DWMWINDOWATTRIBUTE enumeration topic indicates, in the row for each flag, what type of value you should pass a pointer to in the pvAttribute parameter.</param>
    /// <param name="cbAttribute">The size, in bytes, of the attribute value being received via the pvAttribute parameter. The type of the retrieved value, and therefore its size in bytes, depends on the value of the dwAttribute parameter.</param>
    /// <returns>If the function succeeds, it returns S_OK (0). Otherwise, it returns an HRESULT error code.</returns>
    [DllImport(Library)]
    public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out RECT pvAttribute, int cbAttribute);

    /// <summary>
    /// Sets the value of Desktop Window Manager (DWM) non-client rendering attributes for a window.
    /// </summary>
    /// <param name="hwnd">The handle to the window for which the attribute value is to be set.</param>
    /// <param name="dwAttribute">A flag describing which value to set, specified as a value of the DWMWINDOWATTRIBUTE enumeration. This parameter specifies which attribute to set, and the pvAttribute parameter points to an object containing the attribute value.</param>
    /// <param name="pvAttribute">A pointer to an object containing the attribute value to set. The type of the value set depends on the value of the dwAttribute parameter. The DWMWINDOWATTRIBUTE enumeration topic indicates, in the row for each flag, what type of value you should pass a pointer to in the pvAttribute parameter.</param>
    /// <param name="cbAttribute">The size, in bytes, of the attribute value being set via the pvAttribute parameter. The type of the value set, and therefore its size in bytes, depends on the value of the dwAttribute parameter.</param>
    /// <returns>If the function succeeds, it returns S_OK (0). Otherwise, it returns an HRESULT error code.</returns>
    [DllImport(Library)]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, RECT pvAttribute, int cbAttribute);

    /// <summary>
    /// Sets the value of Desktop Window Manager (DWM) non-client rendering attributes for a window.
    /// </summary>
    /// <param name="hwnd">The handle to the window for which the attribute value is to be set.</param>
    /// <param name="dwAttribute">A flag describing which value to set, specified as a value of the DWMWINDOWATTRIBUTE enumeration. This parameter specifies which attribute to set, and the pvAttribute parameter points to an object containing the attribute value.</param>
    /// <param name="pvAttribute">A pointer to an object containing the attribute value to set. The type of the value set depends on the value of the dwAttribute parameter. The DWMWINDOWATTRIBUTE enumeration topic indicates, in the row for each flag, what type of value you should pass a pointer to in the pvAttribute parameter.</param>
    /// <param name="cbAttribute">The size, in bytes, of the attribute value being set via the pvAttribute parameter. The type of the value set, and therefore its size in bytes, depends on the value of the dwAttribute parameter.</param>
    /// <returns>If the function succeeds, it returns S_OK (0). Otherwise, it returns an HRESULT error code.</returns>
    [DllImport(Library)]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, bool pvAttribute, int cbAttribute);
}
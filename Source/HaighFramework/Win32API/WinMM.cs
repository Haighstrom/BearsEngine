using System.Runtime.InteropServices;
using System.Security;

namespace HaighFramework.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class WinMM
{
    private const string Library = "Winmm.dll";

    /// <summary>
    /// The joyGetDevCaps function queries a joystick to determine its capabilities.
    /// </summary>
    /// <param name="uJoyID">Identifier of the joystick to be queried. Valid values for uJoyID range from -1 to 15. A value of -1 enables retrieval of the szRegKey member of the JOYCAPS structure whether a device is present or not.</param>
    /// <param name="pjc">Pointer to a JOYCAPS structure to contain the capabilities of the joystick.</param>
    /// <param name="cbjc">Size, in bytes, of the JOYCAPS structure.</param>
    /// <returns>Returns JOYERR_NOERROR if successful, or an error value if not.</returns>
    [DllImport(Library)]
    public static extern MMResult joyGetDevCaps(int uJoyID, out JOYCAPS pjc, int cbjc);

    /// <summary>
    /// The joyGetPos function queries a joystick for its position and button status.
    /// </summary>
    /// <param name="uJoyID">Identifier of the joystick to be queried. Valid values for uJoyID range from zero (JOYSTICKID1) to 15.</param>
    /// <param name="pji">Pointer to a JOYINFO structure that contains the position and button status of the joystick.</param>
    /// <returns>Returns JOYERR_NOERROR if successful, or an error value if not.</returns>
    [DllImport(Library)]
    public static extern MMResult joyGetPos(int uJoyID, ref JOYINFO pji);

    /// <summary>
    /// The joyGetPosEx function queries a joystick for its position and button status.
    /// </summary>
    /// <param name="uJoyID">Identifier of the joystick to be queried. Valid values for uJoyID range from zero (JOYSTICKID1) to 15, except for Windows NT 4.0. For Windows NT 4.0, valid values are limited to JOYSTICKID1 and JOYSTICKID2.</param>
    /// <param name="pji">Pointer to a JOYINFOEX structure that contains extended position information and button status of the joystick. You must set the dwSize and dwFlags members or joyGetPosEx will fail. The information returned from joyGetPosEx depends on the flags you specify in dwFlags.</param>
    /// <returns>Returns JOYERR_NOERROR if successful, or an error value if not.</returns>
    [DllImport(Library)]
    public static extern MMResult joyGetPosEx(int uJoyID, ref JOYINFOEX pji);

    /// <summary>
    /// The joyGetNumDevs function queries the joystick driver for the number of joysticks it supports.
    /// </summary>
    /// <returns>The joyGetNumDevs function returns the number of joysticks supported by the current driver or zero if no driver is installed.</returns>
    [DllImport(Library)]
    public static extern int joyGetNumDevs();

    /// <summary>
    /// The joyConfigChanged function informs the joystick driver that the configuration has changed and should be reloaded from the registry.
    /// </summary>
    /// <param name="flags">Reserved for future use. Must equal zero.</param>
    /// <returns>Returns JOYERR_NOERROR if successful. Returns JOYERR_PARMS if the parameter is non-zero.</returns>
    [DllImport(Library)]
    public static extern MMResult joyConfigChanged(int flags);
}
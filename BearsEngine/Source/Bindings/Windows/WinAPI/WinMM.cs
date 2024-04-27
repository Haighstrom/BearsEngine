using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.WinAPI;

#region Enums
/// <summary>
/// Results of MultiMedia calls
/// </summary>
internal enum MMResult : uint
{
    /// <summary>
    /// No error.
    /// </summary>
    JOYERR_NOERROR = 0,

    /// <summary>
    /// The joystick driver is not present, or the specified joystick identifier is invalid. The specified joystick identifier is invalid.
    /// </summary>
    MMSYSERR_NODRIVER = 6,

    /// <summary>
    /// 	An invalid parameter was passed.
    /// </summary>
    MMSYSERR_INVALPARAM = 11,

    /// <summary>
    /// Windows 95/98/Me: The specified joystick identifier is invalid.
    /// </summary>
    MMSYSERR_BADDEVICEID = 2,

    /// <summary>
    /// The specified joystick is not connected to the system.
    /// </summary>
    JOYERR_UNPLUGGED = 167,

    /// <summary>
    /// Windows NT/2000/XP: The specified joystick identifier is invalid.
    /// </summary>
    JOYERR_PARMS = 165,
}
#endregion

#region Structs
/// <summary>
/// The JOYCAPS structure contains information about the joystick capabilities.
/// </summary>
internal struct JOYCAPS
{
    /// <summary>
    /// Joystick capabilities
    /// </summary>
    [Flags]
    public enum FLAGS
    {
        /// <summary>
        /// Joystick has z-coordinate information.
        /// </summary>
        JOYCAPS_HASZ = 0x1,

        /// <summary>
        /// Joystick has rudder (fourth axis) information.
        /// </summary>
        JOYCAPS_HASR = 0x2,

        /// <summary>
        /// Joystick has u-coordinate (fifth axis) information.
        /// </summary>
        JOYCAPS_HASU = 0x4,

        /// <summary>
        /// 	Joystick has v-coordinate (sixth axis) information.
        /// </summary>
        JOYCAPS_HASV = 0x8,

        /// <summary>
        /// Joystick has point-of-view information.
        /// </summary>
        JOYCAPS_HASPOV = 0x16,

        /// <summary>
        /// Joystick point-of-view supports discrete values (centered, forward, backward, left, and right).
        /// </summary>
        JOYCAPS_POV4DIR = 0x32,

        /// <summary>
        /// Joystick point-of-view supports continuous degree bearings.
        /// </summary>
        JOYCAPS_POVCTS = 0x64
    }

    /// <summary>
    /// Manufacturer identifier. Manufacturer identifiers are defined in Manufacturer and Product Identifiers.
    /// </summary>
    public ushort wMid;

    /// <summary>
    /// Product identifier. Product identifiers are defined in Manufacturer and Product Identifiers.
    /// </summary>
    public ushort wPid;

    /// <summary>
    /// Null-terminated string containing the joystick product name.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szPname;

    /// <summary>
    /// Minimum X-coordinate.
    /// </summary>
    public int wXmin;

    /// <summary>
    /// Maximum X-coordinate.
    /// </summary>
    public int wXmax;

    /// <summary>
    /// Minimum Y-coordinate.
    /// </summary>
    public int wYmin;

    /// <summary>
    /// Maximum Y-coordinate.
    /// </summary>
    public int wYmax;

    /// <summary>
    /// Minimum Z-coordinate.
    /// </summary>
    public int wZmin;

    /// <summary>
    /// Maximum Z-coordinate.
    /// </summary>
    public int wZmax;

    /// <summary>
    /// Number of joystick buttons.
    /// </summary>
    public int wNumButtons;

    /// <summary>
    /// Smallest polling frequency supported when captured by the joySetCapture function.
    /// </summary>
    public int wPeriodMin;

    /// <summary>
    /// Largest polling frequency supported when captured by joySetCapture.
    /// </summary>
    public int wPeriodMax;

    /// <summary>
    /// Minimum rudder value. The rudder is a fourth axis of movement.
    /// </summary>
    public int wRmin;

    /// <summary>
    /// Maximum rudder value. The rudder is a fourth axis of movement.
    /// </summary>
    public int wRmax;

    /// <summary>
    /// Minimum u-coordinate (fifth axis) values.
    /// </summary>
    public int wUmin;

    /// <summary>
    /// Maximum u-coordinate (fifth axis) values.
    /// </summary>
    public int wUmax;

    /// <summary>
    /// Minimum v-coordinate (sixth axis) values.
    /// </summary>
    public int wVmin;

    /// <summary>
    /// Maximum v-coordinate (sixth axis) values.
    /// </summary>
    public int wVmax;

    /// <summary>
    /// Joystick capabilities
    /// </summary>
    public FLAGS wCaps;

    /// <summary>
    /// Maximum number of axes supported by the joystick.
    /// </summary>
    public int wMaxAxes;

    /// <summary>
    /// Number of axes currently in use by the joystick.
    /// </summary>
    public int wNumAxes;

    /// <summary>
    /// Maximum number of buttons supported by the joystick.
    /// </summary>
    public int wMaxButtons;

    /// <summary>
    /// Null-terminated string containing the registry key for the joystick.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szRegKey;

    /// <summary>
    /// Null-terminated string identifying the joystick driver OEM.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string szOEMVxD;
}

/// <summary>
/// The JOYINFO structure contains information about the joystick position and button state.
/// </summary>
internal struct JOYINFO
{
    /// <summary>
    /// Joystick buttons
    /// </summary>
    [Flags]
    internal enum BUTTON
    {
        /// <summary>
        /// First joystick button
        /// </summary>
        JOY_BUTTON1 = 1,

        /// <summary>
        /// Second joystick button
        /// </summary>
        JOY_BUTTON2 = 2,

        /// <summary>
        /// Third joystick button
        /// </summary>
        JOY_BUTTON3 = 4,

        /// <summary>
        /// Fourth jooystick button
        /// </summary>
        JOY_BUTTON4 = 8,
    }

    /// <summary>
    /// Current X-coordinate.
    /// </summary>
    public int wXpos;

    /// <summary>
    /// Current Y-coordinate.
    /// </summary>
    public int wYpos;

    /// <summary>
    /// Current Z-coordinate.
    /// </summary>
    public int wZpos;

    /// <summary>
    /// Current state of joystick buttons.
    /// </summary>
    public BUTTON wButtons;
}

/// <summary>
/// The JOYINFOEX structure contains extended information about the joystick position, point-of-view position, and button state.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct JOYINFOEX
{
    /// <summary>
    /// Flags indicating the valid information returned in JOYINFOEX. https://learn.microsoft.com/en-us/previous-versions/ms709358(v=vs.85)
    /// </summary>
    [Flags]
    internal enum FLAGS
    {
        /// <summary>
        /// Equivalent to setting all of the JOY_RETURN bits except JOY_RETURNRAWDATA.
        /// </summary>
        JOY_RETURNALL = JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ | JOY_RETURNR | JOY_RETURNU | JOY_RETURNV | JOY_RETURNPOV | JOY_RETURNBUTTONS,

        /// <summary>
        /// The dwButtons member contains valid information about the state of each joystick button.
        /// </summary>
        JOY_RETURNBUTTONS = 128,

        /// <summary>
        /// Centers the joystick neutral position to the middle value of each axis of movement.
        /// </summary>
        JOY_RETURNCENTERED = 1024,

        /// <summary>
        /// The dwPOV member contains valid information about the point-of-view control, expressed in discrete units.
        /// </summary>
        JOY_RETURNPOV = 64,

        /// <summary>
        /// The dwPOV member contains valid information about the point-of-view control expressed in continuous, one-hundredth degree units.
        /// </summary>
        JOY_RETURNPOVCTS = 512,

        /// <summary>
        /// The dwRpos member contains valid rudder pedal data. This information represents another (fourth) axis.
        /// </summary>
        JOY_RETURNR = 8,

        /// <summary>
        /// Data stored in this structure is uncalibrated joystick readings.
        /// </summary>
        JOY_RETURNRAWDATA = 256,

        /// <summary>
        /// The dwUpos member contains valid data for a fifth axis of the joystick, if such an axis is available, or returns zero otherwise.
        /// </summary>
        JOY_RETURNU = 16,

        /// <summary>
        /// The dwVpos member contains valid data for a sixth axis of the joystick, if such an axis is available, or returns zero otherwise.
        /// </summary>
        JOY_RETURNV = 32,

        /// <summary>
        /// The dwXpos member contains valid data for the x-coordinate of the joystick.
        /// </summary>
        JOY_RETURNX = 1,

        /// <summary>
        /// The dwYpos member contains valid data for the y-coordinate of the joystick.
        /// </summary>
        JOY_RETURNY = 2,

        /// <summary>
        /// The dwZpos member contains valid data for the z-coordinate of the joystick.
        /// </summary>
        JOY_RETURNZ = 4,

        /// <summary>
        /// Expands the range for the neutral position of the joystick and calls this range the dead zone. The joystick driver returns a constant value for all positions in the dead zone.
        /// </summary>
        JOY_USEDEADZONE = 2048,

        /// <summary>
        /// Read the x-, y-, and z-coordinates and store the raw values in dwXpos, dwYpos, and dwZpos.
        /// </summary>
        JOY_CAL_READ3 = 0x40000,

        /// <summary>
        /// Read the rudder information and the x-, y-, and z-coordinates and store the raw values in dwXpos, dwYpos, dwZpos, and dwRpos.
        /// </summary>
        JOY_CAL_READ4 = 0x80000,

        /// <summary>
        /// Read the rudder information and the x-, y-, z-, and u-coordinates and store the raw values in dwXpos, dwYpos, dwZpos, dwRpos, and dwUpos.
        /// </summary>
        JOY_CAL_READ5 = 0x400000,

        /// <summary>
        /// Read the raw v-axis data if a joystick mini driver is present that will provide the data. Returns zero otherwise.
        /// </summary>
        JOY_CAL_READ6 = 0x800000,

        /// <summary>
        /// Read the joystick port even if the driver does not detect a device.
        /// </summary>
        JOY_CAL_READALWAYS = 0x10000,

        /// <summary>
        /// Read the rudder information if a joystick mini-driver is present that will provide the data and store the raw value in dwRpos. Return zero otherwise.
        /// </summary>
        JOY_CAL_READRONLY = 0x2000000,

        /// <summary>
        /// Read the x-coordinate and store the raw (uncalibrated) value in dwXpos.
        /// </summary>
        JOY_CAL_READXONLY = 0x100000,

        /// <summary>
        /// Reads the x- and y-coordinates and place the raw values in dwXpos and dwYpos.
        /// </summary>
        JOY_CAL_READXYONLY = 0x20000,

        /// <summary>
        /// Reads the y-coordinate and store the raw value in dwYpos.
        /// </summary>
        JOY_CAL_READYONLY = 0x200000,

        /// <summary>
        /// Read the z-coordinate and store the raw value in dwZpos.
        /// </summary>
        JOY_CAL_READZONLY = 0x1000000,

        /// <summary>
        /// Read the u-coordinate if a joystick mini-driver is present that will provide the data and store the raw value in dwUpos. Return zero otherwise.
        /// </summary>
        JOY_CAL_READUONLY = 0x4000000,

        /// <summary>
        /// Read the v-coordinate if a joystick mini-driver is present that will provide the data and store the raw value in dwVpos. Return zero otherwise.
        /// </summary>
        JOY_CAL_READVONLY = 0x8000000,
    }

    /// <summary>
    /// Size, in bytes, of this structure.
    /// </summary>
    public int dwSize;

    /// <summary>
    /// Flags indicating the valid information returned in this structure. Members that do not contain valid information are set to zero.
    /// </summary>
    [MarshalAs(UnmanagedType.I4)]
    public FLAGS dwFlags;

    /// <summary>
    /// Current X-coordinate.
    /// </summary>
    public int dwXpos;

    /// <summary>
    /// Current Y-coordinate.
    /// </summary>
    public int dwYpos;

    /// <summary>
    /// Current Z-coordinate.
    /// </summary>
    public int dwZpos;

    /// <summary>
    /// Current position of the rudder or fourth joystick axis.
    /// </summary>
    public int dwRpos;

    /// <summary>
    /// Current fifth axis position.
    /// </summary>
    public int dwUpos;

    /// <summary>
    /// Current sixth axis position.
    /// </summary>
    public int dwVpos;

    /// <summary>
    /// Current state of the 32 joystick buttons. The value of this member can be set to any combination of JOY_BUTTONn flags, where n is a value in the range of 1 through 32 corresponding to the button that is pressed.
    /// </summary>
    public uint dwButtons;

    /// <summary>
    /// Current button number that is pressed.
    /// </summary>
    public uint dwButtonNumber;

    /// <summary>
    /// Current position of the point-of-view control. Values for this member are in the range 0 through 35,900. These values represent the angle, in degrees, of each view multiplied by 100.
    /// </summary>
    public int dwPOV;

    /// <summary>
    /// Reserved; do not use.
    /// </summary>
    private readonly uint dwReserved1;

    /// <summary>
    /// Reserved; do not use.
    /// </summary>
    private readonly uint dwReserved2;

    public static readonly int SizeInBytes;

    static JOYINFOEX()
    {
        SizeInBytes = Marshal.SizeOf(default(JOYINFOEX));
    }

    public int GetAxis(int i)
    {
        switch (i)
        {
            case 0: return dwXpos;
            case 1: return dwYpos;
            case 2: return dwZpos;
            case 3: return dwRpos;
            case 4: return dwUpos;
            case 5: return dwVpos;
            default: return 0;
        }
    }
}
#endregion

[SuppressUnmanagedCodeSecurity]
internal static partial class WinMM
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
    [LibraryImport(Library)]
    public static partial MMResult joyGetPos(int uJoyID, ref JOYINFO pji);

    /// <summary>
    /// The joyGetPosEx function queries a joystick for its position and button status.
    /// </summary>
    /// <param name="uJoyID">Identifier of the joystick to be queried. Valid values for uJoyID range from zero (JOYSTICKID1) to 15, except for Windows NT 4.0. For Windows NT 4.0, valid values are limited to JOYSTICKID1 and JOYSTICKID2.</param>
    /// <param name="pji">Pointer to a JOYINFOEX structure that contains extended position information and button status of the joystick. You must set the dwSize and dwFlags members or joyGetPosEx will fail. The information returned from joyGetPosEx depends on the flags you specify in dwFlags.</param>
    /// <returns>Returns JOYERR_NOERROR if successful, or an error value if not.</returns>
    [LibraryImport(Library)]
    public static partial MMResult joyGetPosEx(int uJoyID, ref JOYINFOEX pji);

    /// <summary>
    /// The joyGetNumDevs function queries the joystick driver for the number of joysticks it supports.
    /// </summary>
    /// <returns>The joyGetNumDevs function returns the number of joysticks supported by the current driver or zero if no driver is installed.</returns>
    [LibraryImport(Library)]
    public static partial int joyGetNumDevs();

    /// <summary>
    /// The joyConfigChanged function informs the joystick driver that the configuration has changed and should be reloaded from the registry.
    /// </summary>
    /// <param name="flags">Reserved for future use. Must equal zero.</param>
    /// <returns>Returns JOYERR_NOERROR if successful. Returns JOYERR_PARMS if the parameter is non-zero.</returns>
    [LibraryImport(Library)]
    public static partial MMResult joyConfigChanged(int flags);
}

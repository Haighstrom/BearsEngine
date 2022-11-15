namespace BearsEngine.Win32API;

/// <summary>
/// Appbar message value to send. https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shappbarmessage
/// </summary>
internal enum AppBarMessage : uint
{
    /// <summary>
    /// Registers a new appbar and specifies the message identifier that the system should use to send notification messages to the appbar.
    /// </summary>
    ABM_NEW = 0x00000000,

    /// <summary>
    /// Unregisters an appbar, removing the bar from the system's internal list.
    /// </summary>
    ABM_REMOVE = 0x00000001,

    /// <summary>
    /// Requests a size and screen position for an appbar.
    /// </summary>
    ABM_QUERYPOS = 0x00000002,

    /// <summary>
    /// Sets the size and screen position of an appbar.
    /// </summary>
    ABM_SETPOS = 0x00000003,

    /// <summary>
    /// Retrieves the autohide and always-on-top states of the Windows taskbar.
    /// </summary>
    ABM_GETSTATE = 0x00000004,

    /// <summary>
    /// Retrieves the bounding rectangle of the Windows taskbar. Note that this applies only to the system taskbar. Other objects, particularly toolbars supplied with third-party software, also can be present. As a result, some of the screen area not covered by the Windows taskbar might not be visible to the user. To retrieve the area of the screen not covered by both the taskbar and other app bars—the working area available to your application—, use the GetMonitorInfo function.
    /// </summary>
    ABM_GETTASKBARPOS = 0x00000005,

    /// <summary>
    /// Notifies the system to activate or deactivate an appbar. The lParam member of the APPBARDATA pointed to by pData is set to TRUE to activate or FALSE to deactivate.
    /// </summary>
    ABM_ACTIVATE = 0x00000006,

    /// <summary>
    /// Retrieves the handle to the autohide appbar associated with a particular edge of the screen.
    /// </summary>
    ABM_GETAUTOHIDEBAR = 0x00000007,

    /// <summary>
    /// Registers or unregisters an autohide appbar for an edge of the screen.
    /// </summary>
    ABM_SETAUTOHIDEBAR = 0x00000008,

    /// <summary>
    /// Notifies the system when an appbar's position has changed.
    /// </summary>
    ABM_WINDOWPOSCHANGED = 0x00000009,

    /// <summary>
    /// Windows XP and later: Sets the state of the appbar's autohide and always-on-top attributes.
    /// </summary>
    ABM_SETSTATE = 0x0000000A,

    /// <summary>
    /// Windows XP and later: Retrieves the handle to the autohide appbar associated with a particular edge of a particular monitor.
    /// </summary>
    ABM_GETAUTOHIDEBAREX = 0x0000000B,

    /// <summary>
    /// Windows XP and later: Registers or unregisters an autohide appbar for an edge of a particular monitor.
    /// </summary>
    ABM_SETAUTOHIDEBAREX = 0x0000000C,
}

/// <summary>
/// A value that specifies an edge of the screen. https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-appbardata
/// </summary>
internal enum APPBARDATA_uEdge : uint
{
    /// <summary>
    /// Bottom edge.
    /// </summary>
    ABE_BOTTOM = 3,
    
    /// <summary>
    /// Left edge.
    /// </summary>
    ABE_LEFT = 0,

    /// <summary>
    /// Right edge.
    /// </summary>
    ABE_RIGHT = 2,

    /// <summary>
    /// Top edge.
    /// </summary>
    ABE_TOP = 1,
}

/// <summary>
/// The item to be returned in GetDeviceCaps. https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps
/// </summary>
internal enum GetDeviceCapsIndex : int
{
    /// <summary>
    /// The device driver version.
    /// </summary>
    DRIVERVERSION = 0,

    /// <summary>
    /// Device technology. If the hdc parameter is a handle to the DC of an enhanced metafile, the device technology is that of the referenced device as specified to the CreateEnhMetaFile function. To determine whether it is an enhanced metafile DC, use the GetObjectType function.
    /// </summary>
    TECHNOLOGY = 2,

    /// <summary>
    /// Width, in millimeters, of the physical screen.
    /// </summary>
    HORZSIZE = 4,

    /// <summary>
    /// Height, in millimeters, of the physical screen.
    /// </summary>
    VERTSIZE = 6,

    /// <summary>
    /// Width, in pixels, of the screen; or for printers, the width, in pixels, of the printable area of the page.
    /// </summary>
    HORZRES = 8,

    /// <summary>
    /// Height, in raster lines, of the screen; or for printers, the height, in pixels, of the printable area of the page.
    /// </summary>
    VERTRES = 10,

    /// <summary>
    /// Number of pixels per logical inch along the screen width. In a system with multiple display monitors, this value is the same for all monitors.
    /// </summary>
    LOGPIXELSX = 88,

    /// <summary>
    /// Number of pixels per logical inch along the screen height. In a system with multiple display monitors, this value is the same for all monitors.
    /// </summary>
    LOGPIXELSY = 90,

    /// <summary>
    /// Number of adjacent color bits for each pixel.
    /// </summary>
    BITSPIXEL = 12,

    /// <summary>
    /// Number of color planes.
    /// </summary>
    PLANES = 14,

    /// <summary>
    /// Number of device-specific brushes.
    /// </summary>
    NUMBRUSHES = 16,

    /// <summary>
    /// Number of device-specific pens.
    /// </summary>
    NUMPENS = 18,

    /// <summary>
    /// Number of device-specific fonts.
    /// </summary>
    NUMFONTS = 22,

    /// <summary>
    /// Number of entries in the device's color table, if the device has a color depth of no more than 8 bits per pixel. For devices with greater color depths, 1 is returned.
    /// </summary>
    NUMCOLORS = 24,

    /// <summary>
    /// Relative width of a device pixel used for line drawing.
    /// </summary>
    ASPECTX = 40,

    /// <summary>
    /// Relative height of a device pixel used for line drawing.
    /// </summary>
    ASPECTY = 42,

    /// <summary>
    /// Diagonal width of the device pixel used for line drawing.
    /// </summary>
    ASPECTXY = 44,

    /// <summary>
    /// Reserved.
    /// </summary>
    PDEVICESIZE = 26,

    /// <summary>
    /// Flag that indicates the clipping capabilities of the device. If the device can clip to a rectangle, it is 1. Otherwise, it is 0.
    /// </summary>
    CLIPCAPS = 36,

    /// <summary>
    /// Number of entries in the system palette. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
    /// </summary>
    SIZEPALETTE = 104,

    /// <summary>
    /// Number of reserved entries in the system palette. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
    /// </summary>
    NUMRESERVED = 106,

    /// <summary>
    /// Actual color resolution of the device, in bits per pixel. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
    /// </summary>
    COLORRES = 108,

    /// <summary>
    /// For printing devices: the width of the physical page, in device units. For example, a printer set to print at 600 dpi on 8.5-x11-inch paper has a physical width value of 5100 device units. Note that the physical page is almost always greater than the printable area of the page, and never smaller.
    /// </summary>
    PHYSICALWIDTH = 110,

    /// <summary>
    /// For printing devices: the height of the physical page, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper has a physical height value of 6600 device units. Note that the physical page is almost always greater than the printable area of the page, and never smaller.
    /// </summary>
    PHYSICALHEIGHT = 111,

    /// <summary>
    /// For printing devices: the distance from the left edge of the physical page to the left edge of the printable area, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the leftmost 0.25-inch of paper, has a horizontal physical offset of 150 device units.
    /// </summary>
    PHYSICALOFFSETX = 112,

    /// <summary>
    /// For printing devices: the distance from the top edge of the physical page to the top edge of the printable area, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the topmost 0.5-inch of paper, has a vertical physical offset of 300 device units.
    /// </summary>
    PHYSICALOFFSETY = 113,

    /// <summary>
    /// For display devices: the current vertical refresh rate of the device, in cycles per second (Hz). A vertical refresh rate value of 0 or 1 represents the display hardware's default refresh rate. This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.
    /// </summary>
    VREFRESH = 116,

    /// <summary>
    /// Scaling factor for the x-axis of the printer.
    /// </summary>
    SCALINGFACTORX = 114,

    /// <summary>
    /// Scaling factor for the y-axis of the printer.
    /// </summary>
    SCALINGFACTORY = 115,

    /// <summary>
    /// Preferred horizontal drawing alignment, expressed as a multiple of pixels. For best drawing performance, windows should be horizontally aligned to a multiple of this value. A value of zero indicates that the device is accelerated, and any alignment may be used.
    /// </summary>
    BLTALIGNMENT = 119,

    /// <summary>
    /// Value that indicates the shading and blending capabilities of the device. See Remarks for further comments.
    /// </summary>
    SHADEBLENDCAPS = 45,

    /// <summary>
    /// Value that indicates the raster capabilities of the device
    /// </summary>
    RASTERCAPS = 38,

    /// <summary>
    /// Value that indicates the curve capabilities of the device
    /// </summary>
    CURVECAPS = 28,

    /// <summary>
    /// Value that indicates the line capabilities of the device
    /// </summary>
    LINECAPS = 30,

    /// <summary>
    /// Value that indicates the polygon capabilities of the device
    /// </summary>
    POLYGONALCAPS = 32,

    /// <summary>
    /// Value that indicates the text capabilities of the device
    /// </summary>
    TEXTCAPS = 34,

    /// <summary>
    /// Value that indicates the color management capabilities of the device
    /// </summary>
    COLORMGMTCAPS = 121,
}

/// <summary>
/// Joystick capabilities
/// </summary>
[Flags]
internal enum JOYCAPS_Caps
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
/// Joystick buttons
/// </summary>
[Flags]
internal enum JOYINFO_wButtons
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
/// The standard device. https://learn.microsoft.com/en-us/windows/console/getstdhandle
/// </summary>
internal enum StdHandle
{
    /// <summary>
    /// The standard input device. Initially, this is the console input buffer, CONIN$.
    /// </summary>
    STD_INPUT_HANDLE = -10,

    /// <summary>
    /// The standard output device. Initially, this is the active console screen buffer, CONOUT$.
    /// </summary>
    STD_OUTPUT_HANDLE = -11,

    /// <summary>
    /// The standard error device. Initially, this is the active console screen buffer, CONOUT$.
    /// </summary>
    STD_ERROR_HANDLE = -12
}
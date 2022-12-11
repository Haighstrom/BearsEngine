namespace HaighFramework.Win32API;

/// <summary>
/// Appbar message value to send. https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shappbarmessage
/// </summary>
internal enum APPBARMESSAGE : uint
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
/// Flags indicating how the graphics mode should be changed in <see cref="User32.ChangeDisplaySettings"/> and <see cref="User32.ChangeDisplaySettingsEx"/>
/// </summary>
[Flags]
internal enum CHANGEDISPLAYSETTINGSFLAGS
{
    /// <summary>
    /// The graphics mode for the current screen will be changed dynamically.
    /// </summary>
    CDS_NONE = 0,

    /// <summary>
    /// The mode is temporary in nature. If you change to and from another desktop, this mode will not be reset.
    /// </summary>
    CDS_FULLSCREEN = 0x00000004,

    /// <summary>
    /// The settings will be saved in the global settings area so that they will affect all users on the machine. Otherwise, only the settings for the user are modified. This flag is only valid when specified with the CDS_UPDATEREGISTRY flag.
    /// </summary>
    CDS_GLOBAL = 0x00000008,

    /// <summary>
    /// The settings will be saved in the registry, but will not take effect. This flag is only valid when specified with the CDS_UPDATEREGISTRY flag.
    /// </summary>
    CDS_NORESET = 0x10000000,

    /// <summary>
    /// The settings should be changed, even if the requested settings are the same as the current settings.
    /// </summary>
    CDS_RESET = 0x40000000,

    /// <summary>
    /// This device will become the primary device.
    /// </summary>
    CDS_SET_PRIMARY = 0x00000010,

    /// <summary>
    /// The system tests if the requested graphics mode could be set.
    /// </summary>
    CDS_TEST = 0x00000002,

    /// <summary>
    /// The graphics mode for the current screen will be changed dynamically and the graphics mode will be updated in the registry. The mode information is stored in the USER profile.
    /// </summary>
    CDS_UPDATEREGISTRY = 0x00000001,

    /// <summary>
    /// When set, the lParam parameter is a pointer to a VIDEOPARAMETERS structure.
    /// </summary>
    CDS_VIDEOPARAMETERS = 0x00000020,

    /// <summary>
    /// Enables settings changes to unsafe graphics modes.
    /// </summary>
    CDS_ENABLE_UNSAFE_MODES = 0x00000100,

    /// <summary>
    /// Disables settings changes to unsafe graphics modes.
    /// </summary>
    CDS_DISABLE_UNSAFE_MODES = 0x00000200,
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerdevicenotificationa
/// For use with User32 RegisterDeviceNotification
/// </summary>
internal enum DEVICENOTIFYFLAGS
{
    /// <summary>
    /// The hRecipient parameter is a window handle.
    /// </summary>
    DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000,

    /// <summary>
    /// The hRecipient parameter is a service status handle.
    /// </summary>
    DEVICE_NOTIFY_SERVICE_HANDLE = 0x00000001,

    /// <summary>
    /// Notifies the recipient of device interface events for all device interface classes. (The dbcc_classguid member is ignored.)
    /// This value can be used only if the dbch_devicetype member is DBT_DEVTYP_DEVICEINTERFACE.
    /// </summary>
    DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 0x00000004,
}

/// <summary>
/// The result returned by <see cref="User32.ChangeDisplaySettings"/> and <see cref="User32.ChangeDisplaySettingsEx"/> functions
/// </summary>
public enum DISPCHANGERESULT : int
{
    /// <summary>
    /// The settings change was successful.
    /// </summary>
    DISP_CHANGE_SUCCESSFUL = 0,
    /// <summary>
    /// The settings change was unsuccessful because the system is DualView capable.
    /// </summary>
    DISP_CHANGE_BADDUALVIEW = -6,
    /// <summary>
    /// An invalid set of flags was passed in.
    /// </summary>
    DISP_CHANGE_BADFLAGS = -4,
    /// <summary>
    /// The graphics mode is not supported.
    /// </summary>
    DISP_CHANGE_BADMODE = -2,
    /// <summary>
    /// An invalid parameter was passed in. This can include an invalid flag or combination of flags.
    /// </summary>
    DISP_CHANGE_BADPARAM = -5,
    /// <summary>
    /// The display driver failed the specified graphics mode.
    /// </summary>
    DISP_CHANGE_FAILED = -1,
    /// <summary>
    /// Unable to write settings to the registry.
    /// </summary>
    DISP_CHANGE_NOTUPDATED = -3,
    /// <summary>
    /// The computer must be restarted for the graphics mode to work.
    /// </summary>
    DISP_CHANGE_RESTART = 1,
}

/// <summary>
/// Identifies the awareness context for a window. https://learn.microsoft.com/en-us/windows/win32/hidpi/dpi-awareness-context
/// </summary>
internal enum DPI_AWARENESS_CONTEXT
{
    /// <summary>
    /// DPI unaware. This window does not scale for DPI changes and is always assumed to have a scale factor of 100% (96 DPI). It will be automatically scaled by the system on any other DPI setting.
    /// </summary>
    DPI_AWARENESS_CONTEXT_UNAWARE = -1,
    /// <summary>
    /// System DPI aware. This window does not scale for DPI changes. It will query for the DPI once and use that value for the lifetime of the process. If the DPI changes, the process will not adjust to the new DPI value. It will be automatically scaled up or down by the system when the DPI changes from the system value.
    /// </summary>
    DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2,
    /// <summary>
    /// Per monitor DPI aware. This window checks for the DPI when it is created and adjusts the scale factor whenever the DPI changes. These processes are not automatically scaled by the system.
    /// </summary>
    DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3,
    /// <summary>
    /// Also known as Per Monitor v2. An advancement over the original per-monitor DPI awareness mode, which enables applications to access new DPI-related scaling behaviors on a per top-level window basis. Per Monitor v2 was made available in the Creators Update of Windows 10, and is not available on earlier versions of the operating system.
    /// </summary>
    DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4,
    /// <summary>
    /// DPI unaware with improved quality of GDI-based content. This mode behaves similarly to DPI_AWARENESS_CONTEXT_UNAWARE, but also enables the system to automatically improve the rendering quality of text and other GDI-based primitives when the window is displayed on a high-DPI monitor. For more details, see Improving the high-DPI experience in GDI-based Desktop apps. DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED was introduced in the October 2018 update of Windows 10 (also known as version 1809).
    /// </summary>
    DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED = -5,
}

/// <summary>
/// Flag used in <see cref="User32.EnumDisplayDevices"/>
/// </summary>
internal enum ENUMDISPLAYDEVICEFLAG : uint
{
    /// <summary>
    /// Standard behaviour
    /// </summary>
    EDD_NONE = 0,
    /// <summary>
    /// Retrieve (into the DeviceID member of the DISPLAY_DEVICE structure the device interface name for GUID_DEVINTERFACE_MONITOR, which is registered by the operating system on a per monitor basis.
    /// </summary>
    EDD_GET_DEVICE_INTERFACE_NAME = 0x00000001,
}

/// <summary>
/// For use with <see cref="User32.EnumDisplaySettingsEx"/>
/// </summary>
internal enum ENUMDISPLAYSETTINGSFLAG : uint
{
    /// <summary>
    /// Default behaviour.
    /// </summary>
    EDS_DEFAULT = 0,

    /// <summary>
    /// If set, the function will return all graphics modes reported by the adapter driver, regardless of monitor capabilities. Otherwise, it will only return modes that are compatible with current monitors.
    /// </summary>
    EDS_RAWMODE = 0x00000002,

    /// <summary>
    /// If set, the function will return graphics modes in all orientations. Otherwise, it will only return modes that have the same orientation as the one currently set for the requested display.
    /// </summary>
    EDS_ROTATEDMODE = 0x00000004,
}

/// <summary>
/// The item to be returned in GetDeviceCaps. https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps
/// </summary>
internal enum GETDEVICECAPS_INDEX : int
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
/// The resolution desired in <see cref="User32.GetMouseMovePointsEx"/>
/// </summary>
internal enum GMMP_RESOLUTION : uint
{
    /// <summary>
    /// Retrieves the points using the display resolution.
    /// </summary>
    GMMP_USE_DISPLAY_POINTS = 1,

    /// <summary>
    /// Retrieves high resolution points. Points can range from zero to 65,535 (0xFFFF) in both x- and y-coordinates. This is the resolution provided by absolute coordinate pointing devices such as drawing tablets.
    /// </summary>
    GMMP_USE_HIGH_RESOLUTION_POINTS = 2,
}

/// <summary>
/// The zero-based offset to the value to be set in SetWindowLong or get in GetWindowLong. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer. To set any other value, specify one of the following values.
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
/// </summary>
internal enum GWL : int
{
    /// <summary>
    /// Extended window style.
    /// https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
    /// </summary>
    GWL_EXSTYLE = -20,

    /// <summary>
    /// Application instance handle.
    /// </summary>
    GWL_HINSTANCE = -6,

    /// <summary>
    /// Identifier of the child window. The window cannot be a top-level window.
    /// </summary>
    GWL_ID = -12,

    /// <summary>
    /// Window style.
    /// https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
    /// </summary>
    GWL_STYLE = -16,

    /// <summary>
    /// The user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
    /// </summary>
    GWL_USERDATA = -21,

    /// <summary>
    /// The address for the window procedure. You cannot change this attribute if the window does not belong to the same process as the calling thread. You must use the CallWindowProc function to call the window procedure.
    /// </summary>
    GWL_WNDPROC = -4,
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
/// Determines <see cref="User32.MonitorFromWindow"/>'s return value if the window does not intersect any display monitor.
/// </summary>
internal enum MONITORFROMWINDOWFLAGS
{
    /// <summary>
    /// Returns a handle to the display monitor that is nearest to the window.
    /// </summary>
    MONITOR_DEFAULTTONEAREST = 2,

    /// <summary>
    /// Returns NULL.
    /// </summary>
    MONITOR_DEFAULTTONULL = 0,

    /// <summary>
    /// Returns a handle to the primary display monitor.
    /// </summary>
    MONITOR_DEFAULTTOPRIMARY = 1,
}

/// <summary>
/// Specifies how messages are to be handled, for use with <see cref="User32.PeekMessage"/>
/// </summary>
[Flags]
internal enum PEEKMESSAGEFLAGS : uint
{
    /// <summary>
    /// Messages are not removed from the queue after processing by PeekMessage.
    /// </summary>
    PM_NOREMOVE = 0x0000,

    /// <summary>
    /// Messages are removed from the queue after processing by PeekMessage.
    /// </summary>
    PM_REMOVE = 0x0001,

    /// <summary>
    /// Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle).
    /// Combine this value with either PM_NOREMOVE or PM_REMOVE.
    /// </summary>
    PM_NOYIELD = 0x0002,

    /// <summary>
    /// Process mouse and keyboard messages.
    /// </summary>
    PM_QS_INPUT = QUEUESTATUSFLAGS.QS_INPUT << 16,

    /// <summary>
    /// Process paint messages.
    /// </summary>
    PM_QS_PAINT = QUEUESTATUSFLAGS.QS_PAINT << 16,

    /// <summary>
    /// Process all posted messages, including timers and hotkeys.
    /// </summary>
    PM_QS_POSTMESSAGE = (QUEUESTATUSFLAGS.QS_POSTMESSAGE | QUEUESTATUSFLAGS.QS_HOTKEY | QUEUESTATUSFLAGS.QS_TIMER) << 16,

    /// <summary>
    /// Process all sent messages.
    /// </summary>
    PM_QS_SENDMESSAGE = QUEUESTATUSFLAGS.QS_SENDMESSAGE << 16,
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getqueuestatus
/// For use with User32 GetQueueStatus
/// </summary>
[Flags]
internal enum QUEUESTATUSFLAGS : uint
{
    /// <summary>
    /// An input, WM_TIMER, WM_PAINT, WM_HOTKEY, or posted message is in the queue.
    /// </summary>
    QS_ALLEVENTS = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY,

    /// <summary>
    /// Any message is in the queue.
    /// </summary>
    QS_ALLINPUT = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY | QS_SENDMESSAGE,

    /// <summary>
    /// A posted message (other than those listed here) is in the queue.
    /// </summary>
    QS_ALLPOSTMESSAGE = 0x0100,

    /// <summary>
    /// A WM_HOTKEY message is in the queue.
    /// </summary>
    QS_HOTKEY = 0x0080,

    /// <summary>
    /// An input message is in the queue.
    /// </summary>
    QS_INPUT = QS_MOUSE | QS_KEY | QS_RAWINPUT,

    /// <summary>
    /// A WM_KEYUP, WM_KEYDOWN, WM_SYSKEYUP, or WM_SYSKEYDOWN message is in the queue.
    /// </summary>
    QS_KEY = 0x0001,

    /// <summary>
    /// A WM_MOUSEMOVE message or mouse-button message (WM_LBUTTONUP, WM_RBUTTONDOWN, and so on).
    /// </summary>
    QS_MOUSE = QS_MOUSEMOVE | QS_MOUSEBUTTON,

    /// <summary>
    /// A mouse-button message (WM_LBUTTONUP, WM_RBUTTONDOWN, and so on).
    /// </summary>
    QS_MOUSEBUTTON = 0x0004,

    /// <summary>
    /// A WM_MOUSEMOVE message is in the queue.
    /// </summary>
    QS_MOUSEMOVE = 0x0002,

    /// <summary>
    /// A WM_PAINT message is in the queue.
    /// </summary>
    QS_PAINT = 0x0020,

    /// <summary>
    /// A posted message (other than those listed here) is in the queue.
    /// </summary>
    QS_POSTMESSAGE = 0x0008,

    /// <summary>
    /// A raw input message is in the queue. For more information, see Raw Input.
    /// Windows 2000:  This flag is not supported.
    /// </summary>
    QS_RAWINPUT = 0x0400,

    /// <summary>
    /// A message sent by another thread or application is in the queue.
    /// </summary>
    QS_SENDMESSAGE = 0x0040,

    /// <summary>
    /// A WM_TIMER message is in the queue.
    /// </summary>
    QS_TIMER = 0x0010,
}

/// <summary>
/// The command flag used with <see cref="User32.GetRawInputData"/>
/// </summary>
internal enum RAWINPUTDATAFLAG
{
    /// <summary>
    /// Get the header information from the RAWINPUT structure.
    /// </summary>
    RID_HEADER = 0x10000005,

    /// <summary>
    /// Get the raw data from the RAWINPUT structure.
    /// </summary>
    RID_INPUT = 0x10000003,
}

/// <summary>
/// Specifies what data will be returned in pData in <see cref="User32.GetRawInputDeviceInfo"/>
/// </summary>
internal enum RAWINPUTDEVICEINFOFLAG
{
    /// <summary>
    /// pData is a PHIDP_PREPARSED_DATA pointer to a buffer for a top-level collection's preparsed data.
    /// </summary>
    RIDI_PREPARSEDDATA = 0x20000005,

    /// <summary>
    /// pData points to a string that contains the device interface name. If this device is opened with Shared Access Mode then you can call CreateFile with this name to open a HID collection and use returned handle for calling ReadFile to read input reports and WriteFile to send output reports. For this uiCommand only, the value in pcbSize is the character count (not the byte count).
    /// </summary>
    RIDI_DEVICENAME = 0x20000007,

    /// <summary>
    /// pData points to an RID_DEVICE_INFO structure.
    /// </summary>
    RIDI_DEVICEINFO = 0x2000000b
}

/// <summary>
/// The standard device. https://learn.microsoft.com/en-us/windows/console/getstdhandle
/// </summary>
internal enum STDHANDLE
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

/// <summary>
/// Controls how the window is to be shown in <see cref="User32.ShowWindow"/>
/// </summary>
internal enum SHOWWINDOWCOMMAND
{
    /// <summary>
    /// Hides the window and activates another window.
    /// </summary>
    SW_HIDE = 0,

    /// <summary>
    /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
    /// </summary>
    SW_SHOWNORMAL = 1,

    /// <summary>
    /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
    /// </summary>
    SW_NORMAL = 1,

    /// <summary>
    /// Activates the window and displays it as a minimized window.
    /// </summary>
    SW_SHOWMINIMIZED = 2,

    /// <summary>
    /// Activates the window and displays it as a maximized window.
    /// </summary>
    SW_SHOWMAXIMIZED = 3,

    /// <summary>
    /// Activates the window and displays it as a maximized window.
    /// </summary>
    SW_MAXIMIZE = 3,

    /// <summary>
    /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
    /// </summary>
    SW_SHOWNOACTIVATE = 4,

    /// <summary>
    /// Activates the window and displays it in its current size and position.
    /// </summary>
    SW_SHOW = 5,

    /// <summary>
    /// Minimizes the specified window and activates the next top-level window in the Z order.
    /// </summary>
    SW_MINIMIZE = 6,

    /// <summary>
    /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
    /// </summary>
    SW_SHOWMINNOACTIVE = 7,

    /// <summary>
    /// Displays the window in its current size and position. This value is similar to SW_SHOW, except the window is not activated.
    /// </summary>
    SW_SHOWNA = 8,

    /// <summary>
    /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
    /// </summary>
    SW_RESTORE = 9,

    /// <summary>
    /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
    /// </summary>
    SW_SHOWDEFAULT = 10,

    /// <summary>
    /// Windows 2000/XP: Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
    /// </summary>
    SW_FORCEMINIMIZE = 11,
}

/// <summary>
/// For use with MapVirtualKey - The translation to be performed.
/// </summary>
internal enum VIRTUALKEYMAPTYPE
{
    /// <summary>The uCode parameter is a virtual-key code and is translated into a scan code. If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned. If there is no translation, the function returns 0.</summary>
    MAPVK_VK_TO_VSC = 0,

    /// <summary>The uCode parameter is a scan code and is translated into a virtual-key code that does not distinguish between left- and right-hand keys. If there is no translation, the function returns 0.</summary>
    MAPVK_VSC_TO_VK = 1,

    /// <summary>The uCode parameter is a virtual-key code and is translated into an unshifted character value in the low order word of the return value. Dead keys (diacritics) are indicated by setting the top bit of the return value. If there is no translation, the function returns 0.</summary>
    MAPVK_VK_TO_CHAR = 2,

    /// <summary>The uCode parameter is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand keys. If there is no translation, the function returns 0.</summary>
    MAPVK_VSC_TO_VK_EX = 3,

    /// <summary>
    /// Windows Vista and later: The uCode parameter is a virtual-key code and is translated into a scan code. If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned. If the scan code is an extended scan code, the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan code. If there is no translation, the function returns 0.
    /// </summary>
    MAPVK_VK_TO_VSC_EX = 4,
}

/// <summary>
/// https://docs.microsoft.com/en-gb/windows/win32/winmsg/about-messages-and-message-queues?redirectedfrom=MSDN
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/window-notifications
/// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633573(v=vs.85)
/// A window receives this message through its WindowProc function.
/// The system passes input to a window procedure in the form of a message. Messages are generated by both the system and applications. The system generates a message at each input event—for example, when the user types, moves the mouse, or clicks a control such as a scroll bar. The system also generates messages in response to changes in the system brought about by an application, such as when an application changes the pool of system font resources or resizes one of its windows. An application can generate messages to direct its own windows to perform tasks or to communicate with windows in other applications.
/// The system sends a message to a window procedure with a set of four parameters: a window handle, a message identifier, and two values called message parameters.The window handle identifies the window for which the message is intended.The system uses it to determine which window procedure should receive the message.
/// A message identifier is a named constant that identifies the purpose of a message. When a window procedure receives a message, it uses a message identifier to determine how to process the message. For example, the message identifier WM_PAINT tells the window procedure that the window's client area has changed and must be repainted.
/// Message parameters specify data or the location of data used by a window procedure when processing a message. The meaning and value of the message parameters depend on the message. A message parameter can contain an integer, packed bit flags, a pointer to a structure containing additional data, and so on.When a message does not use message parameters, they are typically set to NULL. A window procedure must check the message identifier to determine how to interpret the message parameters.
/// </summary>
internal enum WINDOWMESSAGE : int
{
    /// <summary>
    /// Performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
    /// </summary>
    WM_NULL = 0x00000000,

    /// <summary>
    /// Sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
    /// </summary>
    WM_CREATE = 0x00000001,

    /// <summary>
    /// Sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen.
    /// This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
    /// </summary>
    WM_DESTROY = 0x00000002,

    /// <summary>
    /// Sent after a window has been moved.
    /// </summary>
    WM_MOVE = 0x00000003,

    /// <summary>
    /// Sent to a window after its size has changed.
    /// </summary>
    WM_SIZE = 0x00000005,

    WM_ACTIVATE = 0x00000006,
    WM_SETFOCUS = 0x00000007,
    WM_KILLFOCUS = 0x00000008,
    WM_ENABLE = 0x0000000A,
    WM_SETREDRAW = 0x0000000B,
    WM_SETTEXT = 0x0000000C,
    WM_GETTEXT = 0x0000000D,
    WM_GETTEXTLENGTH = 0x0000000E,
    WM_PAINT = 0x0000000F,
    WM_CLOSE = 0x00000010,
    WM_QUERYENDSESSION = 0x00000011,
    WM_QUIT = 0x00000012,
    WM_QUERYOPEN = 0x00000013,
    WM_ERASEBKGND = 0x00000014,
    WM_SYSCOLORCHANGE = 0x00000015,
    WM_ENDSESSION = 0x00000016,
    WM_SHOWWINDOW = 0x00000018,
    WM_CTLCOLOR = 0x00000019,
    WM_WININICHANGE = 0x0000001A,
    WM_DEVMODECHANGE = 0x0000001B,
    WM_ACTIVATEAPP = 0x0000001C,
    WM_FONTCHANGE = 0x0000001D,
    WM_TIMECHANGE = 0x0000001E,
    WM_CANCELMODE = 0x0000001F,
    WM_SETCURSOR = 0x00000020,
    WM_MOUSEACTIVATE = 0x00000021,
    WM_CHILDACTIVATE = 0x00000022,
    WM_QUEUESYNC = 0x00000023,
    WM_GETMINMAXINFO = 0x00000024,
    WM_PAINTICON = 0x00000026,
    WM_ICONERASEBKGND = 0x00000027,
    WM_NEXTDLGCTL = 0x00000028,
    WM_SPOOLERSTATUS = 0x0000002A,
    WM_DRAWITEM = 0x0000002B,
    WM_MEASUREITEM = 0x0000002C,
    WM_DELETEITEM = 0x0000002D,
    WM_VKEYTOITEM = 0x0000002E,
    WM_CHARTOITEM = 0x0000002F,
    WM_SETFONT = 0x00000030,
    WM_GETFONT = 0x00000031,
    WM_SETHOTKEY = 0x00000032,
    WM_GETHOTKEY = 0x00000033,
    WM_QUERYDRAGICON = 0x00000037,
    WM_COMPAREITEM = 0x00000039,
    WM_GETOBJECT = 0x0000003D,
    WM_COMPACTING = 0x00000041,
    WM_COMMNOTIFY = 0x00000044,
    WM_WINDOWPOSCHANGING = 0x00000046,
    WM_WINDOWPOSCHANGED = 0x00000047,
    WM_POWER = 0x00000048,
    WM_COPYGLOBALDATA = 0x00000049,
    WM_COPYDATA = 0x0000004A,
    WM_CANCELJOURNAL = 0x0000004B,
    WM_NOTIFY = 0x0000004E,
    WM_INPUTLANGCHANGEREQUEST = 0x00000050,
    WM_INPUTLANGCHANGE = 0x00000051,
    WM_TCARD = 0x00000052,
    WM_HELP = 0x00000053,
    WM_USERCHANGED = 0x00000054,
    WM_NOTIFYFORMAT = 0x00000055,
    WM_CONTEXTMENU = 0x0000007B,
    WM_STYLECHANGING = 0x0000007C,
    WM_STYLECHANGED = 0x0000007D,
    WM_DISPLAYCHANGE = 0x0000007E,
    WM_GETICON = 0x0000007F,
    WM_SETICON = 0x00000080,
    WM_NCCREATE = 0x00000081,
    WM_NCDESTROY = 0x00000082,
    WM_NCCALCSIZE = 0x00000083,
    WM_NCHITTEST = 0x00000084,
    WM_NCPAINT = 0x00000085,
    WM_NCACTIVATE = 0x00000086,
    WM_GETDLGCODE = 0x00000087,
    WM_SYNCPAINT = 0x00000088,
    WM_NCMOUSEMOVE = 0x000000A0,
    WM_NCLBUTTONDOWN = 0x000000A1,
    WM_NCLBUTTONUP = 0x000000A2,
    WM_NCLBUTTONDBLCLK = 0x000000A3,
    WM_NCRBUTTONDOWN = 0x000000A4,
    WM_NCRBUTTONUP = 0x000000A5,
    WM_NCRBUTTONDBLCLK = 0x000000A6,
    WM_NCMBUTTONDOWN = 0x000000A7,
    WM_NCMBUTTONUP = 0x000000A8,
    WM_NCMBUTTONDBLCLK = 0x000000A9,
    WM_NCXBUTTONDOWN = 0x000000AB,
    WM_NCXBUTTONUP = 0x000000AC,
    WM_NCXBUTTONDBLCLK = 0x000000AD,
    WM_DPICHANGED = 0x02E0,
    EM_GETSEL = 0x000000B0,
    EM_SETSEL = 0x000000B1,
    EM_GETRECT = 0x000000B2,
    EM_SETRECT = 0x000000B3,
    EM_SETRECTNP = 0x000000B4,
    EM_SCROLL = 0x000000B5,
    EM_LINESCROLL = 0x000000B6,
    EM_SCROLLCARET = 0x000000B7,
    EM_GETMODIFY = 0x000000B9,
    EM_SETMODIFY = 0x000000BB,
    EM_GETLINECOUNT = 0x000000BC,
    EM_LINEINDEX = 0x000000BD,
    EM_SETHANDLE = 0x000000BE,
    EM_GETHANDLE = 0x000000BF,
    EM_GETTHUMB = 0x000000C0,
    EM_LINELENGTH = 0x000000C1,
    EM_REPLACESEL = 0x000000C2,
    EM_SETFONT = 0x000000C3,
    EM_GETLINE = 0x000000C4,
    EM_LIMITTEXT = 0x000000C5,
    EM_SETLIMITTEXT = 0x000000C5,
    EM_CANUNDO = 0x000000C6,
    EM_UNDO = 0x000000C7,
    EM_FMTLINES = 0x000000C8,
    EM_LINEFROMCHAR = 0x000000C9,
    EM_SETWORDBREAK = 0x000000CA,
    EM_SETTABSTOPS = 0x000000CB,
    EM_SETPASSWORDCHAR = 0x000000CC,
    EM_EMPTYUNDOBUFFER = 0x000000CD,
    EM_GETFIRSTVISIBLELINE = 0x000000CE,
    EM_SETREADONLY = 0x000000CF,
    EM_SETWORDBREAKPROC = 0x000000D1,
    EM_GETWORDBREAKPROC = 0x000000D1,
    EM_GETPASSWORDCHAR = 0x000000D2,
    EM_SETMARGINS = 0x000000D3,
    EM_GETMARGINS = 0x000000D4,
    EM_GETLIMITTEXT = 0x000000D5,
    EM_POSFROMCHAR = 0x000000D6,
    EM_CHARFROMPOS = 0x000000D7,
    EM_SETIMESTATUS = 0x000000D8,
    EM_GETIMESTATUS = 0x000000D9,
    SBM_SETPOS = 0x000000E0,
    SBM_GETPOS = 0x000000E1,
    SBM_SETRANGE = 0x000000E2,
    SBM_GETRANGE = 0x000000E3,
    SBM_ENABLE_ARROWS = 0x000000E4,
    SBM_SETRANGEREDRAW = 0x000000E6,
    SBM_SETSCROLLINFO = 0x000000E9,
    SBM_GETSCROLLINFO = 0x000000EA,
    SBM_GETSCROLLBARINFO = 0x000000EB,
    BM_GETCHECK = 0x000000F0,
    BM_SETCHECK = 0x000000F1,
    BM_GETSTATE = 0x000000F2,
    BM_SETSTATE = 0x000000F3,
    BM_SETSTYLE = 0x000000F4,
    BM_CLICK = 0x000000F5,
    BM_GETIMAGE = 0x000000F6,
    BM_SETIMAGE = 0x000000F7,
    BM_SETDONTCLICK = 0x000000F8,
    WM_INPUT = 0x000000FF,
    WM_KEYDOWN = 0x00000100,
    WM_KEYFIRST = 0x00000100,
    WM_KEYUP = 0x00000101,
    WM_CHAR = 0x00000102,
    WM_DEADCHAR = 0x00000103,
    WM_SYSKEYDOWN = 0x00000104,
    WM_SYSKEYUP = 0x00000105,
    WM_SYSCHAR = 0x00000106,
    WM_SYSDEADCHAR = 0x00000107,
    WM_KEYLAST = 0x00000108,
    WM_UNICHAR = 0x00000109,
    WM_WNT_CONVERTREQUESTEX = 0x00000109,
    WM_CONVERTREQUEST = 0x0000010A,
    WM_CONVERTRESULT = 0x0000010B,
    WM_INTERIM = 0x0000010C,
    WM_IME_STARTCOMPOSITION = 0x0000010D,
    WM_IME_ENDCOMPOSITION = 0x0000010E,
    WM_IME_COMPOSITION = 0x0000010F,
    WM_IME_KEYLAST = 0x0000010F,
    WM_INITDIALOG = 0x00000110,
    WM_COMMAND = 0x00000111,
    WM_SYSCOMMAND = 0x00000112,
    WM_TIMER = 0x00000113,
    WM_HSCROLL = 0x00000114,
    WM_VSCROLL = 0x00000115,
    WM_INITMENU = 0x00000116,
    WM_INITMENUPOPUP = 0x00000117,
    WM_SYSTIMER = 0x00000118,
    WM_MENUSELECT = 0x0000011F,
    WM_MENUCHAR = 0x00000120,
    WM_ENTERIDLE = 0x00000121,
    WM_MENURBUTTONUP = 0x00000122,
    WM_MENUDRAG = 0x00000123,
    WM_MENUGETOBJECT = 0x00000124,
    WM_UNINITMENUPOPUP = 0x00000125,
    WM_MENUCOMMAND = 0x00000126,
    WM_CHANGEUISTATE = 0x00000127,
    WM_UPDATEUISTATE = 0x00000128,
    WM_QUERYUISTATE = 0x00000129,
    WM_CTLCOLORMSGBOX = 0x00000132,
    WM_CTLCOLOREDIT = 0x00000133,
    WM_CTLCOLORLISTBOX = 0x00000134,
    WM_CTLCOLORBTN = 0x00000135,
    WM_CTLCOLORDLG = 0x00000136,
    WM_CTLCOLORSCROLLBAR = 0x00000137,
    WM_CTLCOLORSTATIC = 0x00000138,
    WM_MOUSEFIRST = 0x00000200,
    WM_MOUSEMOVE = 0x00000200,
    WM_LBUTTONDOWN = 0x00000201,
    WM_LBUTTONUP = 0x00000202,
    WM_LBUTTONDBLCLK = 0x00000203,
    WM_RBUTTONDOWN = 0x00000204,
    WM_RBUTTONUP = 0x00000205,
    WM_RBUTTONDBLCLK = 0x00000206,
    WM_MBUTTONDOWN = 0x00000207,
    WM_MBUTTONUP = 0x00000208,
    WM_MBUTTONDBLCLK = 0x00000209,
    WM_MOUSELAST = 0x00000209,
    WM_MOUSEWHEEL = 0x0000020A,
    WM_XBUTTONDOWN = 0x0000020B,
    WM_XBUTTONUP = 0x0000020C,
    WM_XBUTTONDBLCLK = 0x0000020D,
    WM_MOUSEHWHEEL = 0x0000020E,
    WM_PARENTNOTIFY = 0x00000210,
    WM_ENTERMENULOOP = 0x00000211,
    WM_EXITMENULOOP = 0x00000212,
    WM_NEXTMENU = 0x00000213,
    WM_SIZING = 0x00000214,
    WM_CAPTURECHANGED = 0x00000215,
    WM_MOVING = 0x00000216,
    WM_POWERBROADCAST = 0x00000218,
    WM_DEVICECHANGE = 0x00000219,
    WM_MDICREATE = 0x00000220,
    WM_MDIDESTROY = 0x00000221,
    WM_MDIACTIVATE = 0x00000222,
    WM_MDIRESTORE = 0x00000223,
    WM_MDINEXT = 0x00000224,
    WM_MDIMAXIMIZE = 0x00000225,
    WM_MDITILE = 0x00000226,
    WM_MDICASCADE = 0x00000227,
    WM_MDIICONARRANGE = 0x00000228,
    WM_MDIGETACTIVE = 0x00000229,
    WM_MDISETMENU = 0x00000230,
    WM_ENTERSIZEMOVE = 0x00000231,
    WM_EXITSIZEMOVE = 0x00000232,
    WM_DROPFILES = 0x00000233,
    WM_MDIREFRESHMENU = 0x00000234,
    WM_IME_REPORT = 0x00000280,
    WM_IME_SETCONTEXT = 0x00000281,
    WM_IME_NOTIFY = 0x00000282,
    WM_IME_CONTROL = 0x00000283,
    WM_IME_COMPOSITIONFULL = 0x00000284,
    WM_IME_SELECT = 0x00000285,
    WM_IME_CHAR = 0x00000286,
    WM_IME_REQUEST = 0x00000288,
    WM_IMEKEYDOWN = 0x00000290,
    WM_IME_KEYDOWN = 0x00000290,
    WM_IMEKEYUP = 0x00000291,
    WM_IME_KEYUP = 0x00000291,
    WM_NCMOUSEHOVER = 0x000002A0,
    WM_MOUSEHOVER = 0x000002A1,
    WM_NCMOUSELEAVE = 0x000002A2,
    WM_MOUSELEAVE = 0x000002A3,
    WM_CUT = 0x00000300,
    WM_COPY = 0x00000301,
    WM_PASTE = 0x00000302,
    WM_CLEAR = 0x00000303,
    WM_UNDO = 0x00000304,
    WM_RENDERFORMAT = 0x00000305,
    WM_RENDERALLFORMATS = 0x00000306,
    WM_DESTROYCLIPBOARD = 0x00000307,
    WM_DRAWCLIPBOARD = 0x00000308,
    WM_PAINTCLIPBOARD = 0x00000309,
    WM_VSCROLLCLIPBOARD = 0x0000030A,
    WM_SIZECLIPBOARD = 0x0000030B,
    WM_ASKCBFORMATNAME = 0x0000030C,
    WM_CHANGECBCHAIN = 0x0000030D,
    WM_HSCROLLCLIPBOARD = 0x0000030E,
    WM_QUERYNEWPALETTE = 0x0000030F,
    WM_PALETTEISCHANGING = 0x00000310,
    WM_PALETTECHANGED = 0x00000311,
    WM_HOTKEY = 0x00000312,
    WM_PRINT = 0x00000317,
    WM_PRINTCLIENT = 0x00000318,
    WM_APPCOMMAND = 0x00000319,
    WM_HANDHELDFIRST = 0x00000358,
    WM_HANDHELDLAST = 0x0000035F,
    WM_AFXFIRST = 0x00000360,
    WM_AFXLAST = 0x0000037F,
    WM_PENWINFIRST = 0x00000380,
    WM_RCRESULT = 0x00000381,
    WM_HOOKRCRESULT = 0x00000382,
    WM_GLOBALRCCHANGE = 0x00000383,
    WM_PENMISCINFO = 0x00000383,
    WM_SKB = 0x00000384,
    WM_HEDITCTL = 0x00000385,
    WM_PENCTL = 0x00000385,
    WM_PENMISC = 0x00000386,
    WM_CTLINIT = 0x00000387,
    WM_PENEVENT = 0x00000388,
    WM_PENWINLAST = 0x0000038F,
    DDM_SETFMT = 0x00000400,
    DM_GETDEFID = 0x00000400,
    NIN_SELECT = 0x00000400,
    TBM_GETPOS = 0x00000400,
    WM_PSD_PAGESETUPDLG = 0x00000400,
    WM_USER = 0x00000400,
    CBEM_INSERTITEMA = 0x00000401,
    DDM_DRAW = 0x00000401,
    DM_SETDEFID = 0x00000401,
    HKM_SETHOTKEY = 0x00000401,
    PBM_SETRANGE = 0x00000401,
    RB_INSERTBANDA = 0x00000401,
    SB_SETTEXTA = 0x00000401,
    TB_ENABLEBUTTON = 0x00000401,
    TBM_GETRANGEMIN = 0x00000401,
    TTM_ACTIVATE = 0x00000401,
    WM_CHOOSEFONT_GETLOGFONT = 0x00000401,
    WM_PSD_FULLPAGERECT = 0x00000401,
    CBEM_SETIMAGELIST = 0x00000402,
    DDM_CLOSE = 0x00000402,
    DM_REPOSITION = 0x00000402,
    HKM_GETHOTKEY = 0x00000402,
    PBM_SETPOS = 0x00000402,
    RB_DELETEBAND = 0x00000402,
    SB_GETTEXTA = 0x00000402,
    TB_CHECKBUTTON = 0x00000402,
    TBM_GETRANGEMAX = 0x00000402,
    WM_PSD_MINMARGINRECT = 0x00000402,
    CBEM_GETIMAGELIST = 0x00000403,
    DDM_BEGIN = 0x00000403,
    HKM_SETRULES = 0x00000403,
    PBM_DELTAPOS = 0x00000403,
    RB_GETBARINFO = 0x00000403,
    SB_GETTEXTLENGTHA = 0x00000403,
    TBM_GETTIC = 0x00000403,
    TB_PRESSBUTTON = 0x00000403,
    TTM_SETDELAYTIME = 0x00000403,
    WM_PSD_MARGINRECT = 0x00000403,
    CBEM_GETITEMA = 0x00000404,
    DDM_END = 0x00000404,
    PBM_SETSTEP = 0x00000404,
    RB_SETBARINFO = 0x00000404,
    SB_SETPARTS = 0x00000404,
    TB_HIDEBUTTON = 0x00000404,
    TBM_SETTIC = 0x00000404,
    TTM_ADDTOOLA = 0x00000404,
    WM_PSD_GREEKTEXTRECT = 0x00000404,
    CBEM_SETITEMA = 0x00000405,
    PBM_STEPIT = 0x00000405,
    TB_INDETERMINATE = 0x00000405,
    TBM_SETPOS = 0x00000405,
    TTM_DELTOOLA = 0x00000405,
    WM_PSD_ENVSTAMPRECT = 0x00000405,
    CBEM_GETCOMBOCONTROL = 0x00000406,
    PBM_SETRANGE32 = 0x00000406,
    RB_SETBANDINFOA = 0x00000406,
    SB_GETPARTS = 0x00000406,
    TB_MARKBUTTON = 0x00000406,
    TBM_SETRANGE = 0x00000406,
    TTM_NEWTOOLRECTA = 0x00000406,
    WM_PSD_YAFULLPAGERECT = 0x00000406,
    CBEM_GETEDITCONTROL = 0x00000407,
    PBM_GETRANGE = 0x00000407,
    RB_SETPARENT = 0x00000407,
    SB_GETBORDERS = 0x00000407,
    TBM_SETRANGEMIN = 0x00000407,
    TTM_RELAYEVENT = 0x00000407,
    CBEM_SETEXSTYLE = 0x00000408,
    PBM_GETPOS = 0x00000408,
    RB_HITTEST = 0x00000408,
    SB_SETMINHEIGHT = 0x00000408,
    TBM_SETRANGEMAX = 0x00000408,
    TTM_GETTOOLINFOA = 0x00000408,
    CBEM_GETEXSTYLE = 0x00000409,
    CBEM_GETEXTENDEDSTYLE = 0x00000409,
    PBM_SETBARCOLOR = 0x00000409,
    RB_GETRECT = 0x00000409,
    SB_SIMPLE = 0x00000409,
    TB_ISBUTTONENABLED = 0x00000409,
    TBM_CLEARTICS = 0x00000409,
    TTM_SETTOOLINFOA = 0x00000409,
    CBEM_HASEDITCHANGED = 0x0000040A,
    RB_INSERTBANDW = 0x0000040A,
    SB_GETRECT = 0x0000040A,
    TB_ISBUTTONCHECKED = 0x0000040A,
    TBM_SETSEL = 0x0000040A,
    TTM_HITTESTA = 0x0000040A,
    WIZ_QUERYNUMPAGES = 0x0000040A,
    CBEM_INSERTITEMW = 0x0000040B,
    RB_SETBANDINFOW = 0x0000040B,
    SB_SETTEXTW = 0x0000040B,
    TB_ISBUTTONPRESSED = 0x0000040B,
    TBM_SETSELSTART = 0x0000040B,
    TTM_GETTEXTA = 0x0000040B,
    WIZ_NEXT = 0x0000040B,
    CBEM_SETITEMW = 0x0000040C,
    RB_GETBANDCOUNT = 0x0000040C,
    SB_GETTEXTLENGTHW = 0x0000040C,
    TB_ISBUTTONHIDDEN = 0x0000040C,
    TBM_SETSELEND = 0x0000040C,
    TTM_UPDATETIPTEXTA = 0x0000040C,
    WIZ_PREV = 0x0000040C,
    CBEM_GETITEMW = 0x0000040D,
    RB_GETROWCOUNT = 0x0000040D,
    SB_GETTEXTW = 0x0000040D,
    TB_ISBUTTONINDETERMINATE = 0x0000040D,
    TTM_GETTOOLCOUNT = 0x0000040D,
    CBEM_SETEXTENDEDSTYLE = 0x0000040E,
    RB_GETROWHEIGHT = 0x0000040E,
    SB_ISSIMPLE = 0x0000040E,
    TB_ISBUTTONHIGHLIGHTED = 0x0000040E,
    TBM_GETPTICS = 0x0000040E,
    TTM_ENUMTOOLSA = 0x0000040E,
    SB_SETICON = 0x0000040F,
    TBM_GETTICPOS = 0x0000040F,
    TTM_GETCURRENTTOOLA = 0x0000040F,
    RB_IDTOINDEX = 0x00000410,
    SB_SETTIPTEXTA = 0x00000410,
    TBM_GETNUMTICS = 0x00000410,
    TTM_WINDOWFROMPOINT = 0x00000410,
    RB_GETTOOLTIPS = 0x00000411,
    SB_SETTIPTEXTW = 0x00000411,
    TBM_GETSELSTART = 0x00000411,
    TB_SETSTATE = 0x00000411,
    TTM_TRACKACTIVATE = 0x00000411,
    RB_SETTOOLTIPS = 0x00000412,
    SB_GETTIPTEXTA = 0x00000412,
    TB_GETSTATE = 0x00000412,
    TBM_GETSELEND = 0x00000412,
    TTM_TRACKPOSITION = 0x00000412,
    RB_SETBKCOLOR = 0x00000413,
    SB_GETTIPTEXTW = 0x00000413,
    TB_ADDBITMAP = 0x00000413,
    TBM_CLEARSEL = 0x00000413,
    TTM_SETTIPBKCOLOR = 0x00000413,
    RB_GETBKCOLOR = 0x00000414,
    SB_GETICON = 0x00000414,
    TB_ADDBUTTONSA = 0x00000414,
    TBM_SETTICFREQ = 0x00000414,
    TTM_SETTIPTEXTCOLOR = 0x00000414,
    RB_SETTEXTCOLOR = 0x00000415,
    TB_INSERTBUTTONA = 0x00000415,
    TBM_SETPAGESIZE = 0x00000415,
    TTM_GETDELAYTIME = 0x00000415,
    RB_GETTEXTCOLOR = 0x00000416,
    TB_DELETEBUTTON = 0x00000416,
    TBM_GETPAGESIZE = 0x00000416,
    TTM_GETTIPBKCOLOR = 0x00000416,
    RB_SIZETORECT = 0x00000417,
    TB_GETBUTTON = 0x00000417,
    TBM_SETLINESIZE = 0x00000417,
    TTM_GETTIPTEXTCOLOR = 0x00000417,
    RB_BEGINDRAG = 0x00000418,
    TB_BUTTONCOUNT = 0x00000418,
    TBM_GETLINESIZE = 0x00000418,
    TTM_SETMAXTIPWIDTH = 0x00000418,
    RB_ENDDRAG = 0x00000419,
    TB_COMMANDTOINDEX = 0x00000419,
    TBM_GETTHUMBRECT = 0x00000419,
    TTM_GETMAXTIPWIDTH = 0x00000419,
    RB_DRAGMOVE = 0x0000041A,
    TBM_GETCHANNELRECT = 0x0000041A,
    TB_SAVERESTOREA = 0x0000041A,
    TTM_SETMARGIN = 0x0000041A,
    RB_GETBARHEIGHT = 0x0000041B,
    TB_CUSTOMIZE = 0x0000041B,
    TBM_SETTHUMBLENGTH = 0x0000041B,
    TTM_GETMARGIN = 0x0000041B,
    RB_GETBANDINFOW = 0x0000041C,
    TB_ADDSTRINGA = 0x0000041C,
    TBM_GETTHUMBLENGTH = 0x0000041C,
    TTM_POP = 0x0000041C,
    RB_GETBANDINFOA = 0x0000041D,
    TB_GETITEMRECT = 0x0000041D,
    TBM_SETTOOLTIPS = 0x0000041D,
    TTM_UPDATE = 0x0000041D,
    RB_MINIMIZEBAND = 0x0000041E,
    TB_BUTTONSTRUCTSIZE = 0x0000041E,
    TBM_GETTOOLTIPS = 0x0000041E,
    TTM_GETBUBBLESIZE = 0x0000041E,
    RB_MAXIMIZEBAND = 0x0000041F,
    TBM_SETTIPSIDE = 0x0000041F,
    TB_SETBUTTONSIZE = 0x0000041F,
    TTM_ADJUSTRECT = 0x0000041F,
    TBM_SETBUDDY = 0x00000420,
    TB_SETBITMAPSIZE = 0x00000420,
    TTM_SETTITLEA = 0x00000420,
    MSG_FTS_JUMP_VA = 0x00000421,
    TB_AUTOSIZE = 0x00000421,
    TBM_GETBUDDY = 0x00000421,
    TTM_SETTITLEW = 0x00000421,
    RB_GETBANDBORDERS = 0x00000422,
    MSG_FTS_JUMP_QWORD = 0x00000423,
    RB_SHOWBAND = 0x00000423,
    TB_GETTOOLTIPS = 0x00000423,
    MSG_REINDEX_REQUEST = 0x00000424,
    TB_SETTOOLTIPS = 0x00000424,
    MSG_FTS_WHERE_IS_IT = 0x00000425,
    RB_SETPALETTE = 0x00000425,
    TB_SETPARENT = 0x00000425,
    RB_GETPALETTE = 0x00000426,
    RB_MOVEBAND = 0x00000427,
    TB_SETROWS = 0x00000427,
    TB_GETROWS = 0x00000428,
    TB_GETBITMAPFLAGS = 0x00000429,
    TB_SETCMDID = 0x0000042A,
    RB_PUSHCHEVRON = 0x0000042B,
    TB_CHANGEBITMAP = 0x0000042B,
    TB_GETBITMAP = 0x0000042C,
    MSG_GET_DEFFONT = 0x0000042D,
    TB_GETBUTTONTEXTA = 0x0000042D,
    TB_REPLACEBITMAP = 0x0000042E,
    TB_SETINDENT = 0x0000042F,
    TB_SETIMAGELIST = 0x00000430,
    TB_GETIMAGELIST = 0x00000431,
    TB_LOADIMAGES = 0x00000432,
    EM_CANPASTE = 0x00000432,
    TTM_ADDTOOLW = 0x00000432,
    EM_DISPLAYBAND = 0x00000433,
    TB_GETRECT = 0x00000433,
    TTM_DELTOOLW = 0x00000433,
    EM_EXGETSEL = 0x00000434,
    TB_SETHOTIMAGELIST = 0x00000434,
    TTM_NEWTOOLRECTW = 0x00000434,
    EM_EXLIMITTEXT = 0x00000435,
    TB_GETHOTIMAGELIST = 0x00000435,
    TTM_GETTOOLINFOW = 0x00000435,
    EM_EXLINEFROMCHAR = 0x00000436,
    TB_SETDISABLEDIMAGELIST = 0x00000436,
    TTM_SETTOOLINFOW = 0x00000436,
    EM_EXSETSEL = 0x00000437,
    TB_GETDISABLEDIMAGELIST = 0x00000437,
    TTM_HITTESTW = 0x00000437,
    EM_FINDTEXT = 0x00000438,
    TB_SETSTYLE = 0x00000438,
    TTM_GETTEXTW = 0x00000438,
    EM_FORMATRANGE = 0x00000439,
    TB_GETSTYLE = 0x00000439,
    TTM_UPDATETIPTEXTW = 0x00000439,
    EM_GETCHARFORMAT = 0x0000043A,
    TB_GETBUTTONSIZE = 0x0000043A,
    TTM_ENUMTOOLSW = 0x0000043A,
    EM_GETEVENTMASK = 0x0000043B,
    TB_SETBUTTONWIDTH = 0x0000043B,
    TTM_GETCURRENTTOOLW = 0x0000043B,
    EM_GETOLEINTERFACE = 0x0000043C,
    TB_SETMAXTEXTROWS = 0x0000043C,
    EM_GETPARAFORMAT = 0x0000043D,
    TB_GETTEXTROWS = 0x0000043D,
    EM_GETSELTEXT = 0x0000043E,
    TB_GETOBJECT = 0x0000043E,
    EM_HIDESELECTION = 0x0000043F,
    TB_GETBUTTONINFOW = 0x0000043F,
    EM_PASTESPECIAL = 0x00000440,
    TB_SETBUTTONINFOW = 0x00000440,
    EM_REQUESTRESIZE = 0x00000441,
    TB_GETBUTTONINFOA = 0x00000441,
    EM_SELECTIONTYPE = 0x00000442,
    TB_SETBUTTONINFOA = 0x00000442,
    EM_SETBKGNDCOLOR = 0x00000443,
    TB_INSERTBUTTONW = 0x00000443,
    EM_SETCHARFORMAT = 0x00000444,
    TB_ADDBUTTONSW = 0x00000444,
    EM_SETEVENTMASK = 0x00000445,
    TB_HITTEST = 0x00000445,
    EM_SETOLECALLBACK = 0x00000446,
    TB_SETDRAWTEXTFLAGS = 0x00000446,
    EM_SETPARAFORMAT = 0x00000447,
    TB_GETHOTITEM = 0x00000447,
    EM_SETTARGETDEVICE = 0x00000448,
    TB_SETHOTITEM = 0x00000448,
    EM_STREAMIN = 0x00000449,
    TB_SETANCHORHIGHLIGHT = 0x00000449,
    EM_STREAMOUT = 0x0000044A,
    TB_GETANCHORHIGHLIGHT = 0x0000044A,
    EM_GETTEXTRANGE = 0x0000044B,
    TB_GETBUTTONTEXTW = 0x0000044B,
    EM_FINDWORDBREAK = 0x0000044C,
    TB_SAVERESTOREW = 0x0000044C,
    EM_SETOPTIONS = 0x0000044D,
    TB_ADDSTRINGW = 0x0000044D,
    EM_GETOPTIONS = 0x0000044E,
    TB_MAPACCELERATORA = 0x0000044E,
    EM_FINDTEXTEX = 0x0000044F,
    TB_GETINSERTMARK = 0x0000044F,
    EM_GETWORDBREAKPROCEX = 0x00000450,
    TB_SETINSERTMARK = 0x00000450,
    EM_SETWORDBREAKPROCEX = 0x00000451,
    TB_INSERTMARKHITTEST = 0x00000451,
    EM_SETUNDOLIMIT = 0x00000452,
    TB_MOVEBUTTON = 0x00000452,
    TB_GETMAXSIZE = 0x00000453,
    EM_REDO = 0x00000454,
    TB_SETEXTENDEDSTYLE = 0x00000454,
    EM_CANREDO = 0x00000455,
    TB_GETEXTENDEDSTYLE = 0x00000455,
    EM_GETUNDONAME = 0x00000456,
    TB_GETPADDING = 0x00000456,
    EM_GETREDONAME = 0x00000457,
    TB_SETPADDING = 0x00000457,
    EM_STOPGROUPTYPING = 0x00000458,
    TB_SETINSERTMARKCOLOR = 0x00000458,
    EM_SETTEXTMODE = 0x00000459,
    TB_GETINSERTMARKCOLOR = 0x00000459,
    EM_GETTEXTMODE = 0x0000045A,
    TB_MAPACCELERATORW = 0x0000045A,
    EM_AUTOURLDETECT = 0x0000045B,
    TB_GETSTRINGW = 0x0000045B,
    EM_GETAUTOURLDETECT = 0x0000045C,
    TB_GETSTRINGA = 0x0000045C,
    EM_SETPALETTE = 0x0000045D,
    EM_GETTEXTEX = 0x0000045E,
    EM_GETTEXTLENGTHEX = 0x0000045F,
    EM_SHOWSCROLLBAR = 0x00000460,
    EM_SETTEXTEX = 0x00000461,
    TAPI_REPLY = 0x00000463,
    ACM_OPENA = 0x00000464,
    BFFM_SETSTATUSTEXTA = 0x00000464,
    CDM_FIRST = 0x00000464,
    CDM_GETSPEC = 0x00000464,
    EM_SETPUNCTUATION = 0x00000464,
    IPM_CLEARADDRESS = 0x00000464,
    WM_CAP_UNICODE_START = 0x00000464,
    ACM_PLAY = 0x00000465,
    BFFM_ENABLEOK = 0x00000465,
    CDM_GETFILEPATH = 0x00000465,
    EM_GETPUNCTUATION = 0x00000465,
    IPM_SETADDRESS = 0x00000465,
    PSM_SETCURSEL = 0x00000465,
    UDM_SETRANGE = 0x00000465,
    WM_CHOOSEFONT_SETLOGFONT = 0x00000465,
    ACM_STOP = 0x00000466,
    BFFM_SETSELECTIONA = 0x00000466,
    CDM_GETFOLDERPATH = 0x00000466,
    EM_SETWORDWRAPMODE = 0x00000466,
    IPM_GETADDRESS = 0x00000466,
    PSM_REMOVEPAGE = 0x00000466,
    UDM_GETRANGE = 0x00000466,
    WM_CAP_SET_CALLBACK_ERRORW = 0x00000466,
    WM_CHOOSEFONT_SETFLAGS = 0x00000466,
    ACM_OPENW = 0x00000467,
    BFFM_SETSELECTIONW = 0x00000467,
    CDM_GETFOLDERIDLIST = 0x00000467,
    EM_GETWORDWRAPMODE = 0x00000467,
    IPM_SETRANGE = 0x00000467,
    PSM_ADDPAGE = 0x00000467,
    UDM_SETPOS = 0x00000467,
    WM_CAP_SET_CALLBACK_STATUSW = 0x00000467,
    BFFM_SETSTATUSTEXTW = 0x00000468,
    CDM_SETCONTROLTEXT = 0x00000468,
    EM_SETIMECOLOR = 0x00000468,
    IPM_SETFOCUS = 0x00000468,
    PSM_CHANGED = 0x00000468,
    UDM_GETPOS = 0x00000468,
    CDM_HIDECONTROL = 0x00000469,
    EM_GETIMECOLOR = 0x00000469,
    IPM_ISBLANK = 0x00000469,
    PSM_RESTARTWINDOWS = 0x00000469,
    UDM_SETBUDDY = 0x00000469,
    CDM_SETDEFEXT = 0x0000046A,
    EM_SETIMEOPTIONS = 0x0000046A,
    PSM_REBOOTSYSTEM = 0x0000046A,
    UDM_GETBUDDY = 0x0000046A,
    EM_GETIMEOPTIONS = 0x0000046B,
    PSM_CANCELTOCLOSE = 0x0000046B,
    UDM_SETACCEL = 0x0000046B,
    EM_CONVPOSITION = 0x0000046C,
    PSM_QUERYSIBLINGS = 0x0000046C,
    UDM_GETACCEL = 0x0000046C,
    MCIWNDM_GETZOOM = 0x0000046D,
    PSM_UNCHANGED = 0x0000046D,
    UDM_SETBASE = 0x0000046D,
    PSM_APPLY = 0x0000046E,
    UDM_GETBASE = 0x0000046E,
    PSM_SETTITLEA = 0x0000046F,
    UDM_SETRANGE32 = 0x0000046F,
    PSM_SETWIZBUTTONS = 0x00000470,
    UDM_GETRANGE32 = 0x00000470,
    WM_CAP_DRIVER_GET_NAMEW = 0x00000470,
    PSM_PRESSBUTTON = 0x00000471,
    UDM_SETPOS32 = 0x00000471,
    WM_CAP_DRIVER_GET_VERSIONW = 0x00000471,
    PSM_SETCURSELID = 0x00000472,
    UDM_GETPOS32 = 0x00000472,
    PSM_SETFINISHTEXTA = 0x00000473,
    PSM_GETTABCONTROL = 0x00000474,
    PSM_ISDIALOGMESSAGE = 0x00000475,
    MCIWNDM_REALIZE = 0x00000476,
    PSM_GETCURRENTPAGEHWND = 0x00000476,
    MCIWNDM_SETTIMEFORMATA = 0x00000477,
    PSM_INSERTPAGE = 0x00000477,
    EM_SETLANGOPTIONS = 0x00000478,
    MCIWNDM_GETTIMEFORMATA = 0x00000478,
    PSM_SETTITLEW = 0x00000478,
    WM_CAP_FILE_SET_CAPTURE_FILEW = 0x00000478,
    EM_GETLANGOPTIONS = 0x00000479,
    MCIWNDM_VALIDATEMEDIA = 0x00000479,
    PSM_SETFINISHTEXTW = 0x00000479,
    WM_CAP_FILE_GET_CAPTURE_FILEW = 0x00000479,
    EM_GETIMECOMPMODE = 0x0000047A,
    EM_FINDTEXTW = 0x0000047B,
    MCIWNDM_PLAYTO = 0x0000047B,
    WM_CAP_FILE_SAVEASW = 0x0000047B,
    EM_FINDTEXTEXW = 0x0000047C,
    MCIWNDM_GETFILENAMEA = 0x0000047C,
    EM_RECONVERSION = 0x0000047D,
    MCIWNDM_GETDEVICEA = 0x0000047D,
    PSM_SETHEADERTITLEA = 0x0000047D,
    WM_CAP_FILE_SAVEDIBW = 0x0000047D,
    EM_SETIMEMODEBIAS = 0x0000047E,
    MCIWNDM_GETPALETTE = 0x0000047E,
    PSM_SETHEADERTITLEW = 0x0000047E,
    EM_GETIMEMODEBIAS = 0x0000047F,
    MCIWNDM_SETPALETTE = 0x0000047F,
    PSM_SETHEADERSUBTITLEA = 0x0000047F,
    MCIWNDM_GETERRORA = 0x00000480,
    PSM_SETHEADERSUBTITLEW = 0x00000480,
    PSM_HWNDTOINDEX = 0x00000481,
    PSM_INDEXTOHWND = 0x00000482,
    MCIWNDM_SETINACTIVETIMER = 0x00000483,
    PSM_PAGETOINDEX = 0x00000483,
    PSM_INDEXTOPAGE = 0x00000484,
    DL_BEGINDRAG = 0x00000485,
    MCIWNDM_GETINACTIVETIMER = 0x00000485,
    PSM_IDTOINDEX = 0x00000485,
    DL_DRAGGING = 0x00000486,
    PSM_INDEXTOID = 0x00000486,
    DL_DROPPED = 0x00000487,
    PSM_GETRESULT = 0x00000487,
    DL_CANCELDRAG = 0x00000488,
    PSM_RECALCPAGESIZES = 0x00000488,
    MCIWNDM_GET_SOURCE = 0x0000048C,
    MCIWNDM_PUT_SOURCE = 0x0000048D,
    MCIWNDM_GET_DEST = 0x0000048E,
    MCIWNDM_PUT_DEST = 0x0000048F,
    MCIWNDM_CAN_PLAY = 0x00000490,
    MCIWNDM_CAN_WINDOW = 0x00000491,
    MCIWNDM_CAN_RECORD = 0x00000492,
    MCIWNDM_CAN_SAVE = 0x00000493,
    MCIWNDM_CAN_EJECT = 0x00000494,
    MCIWNDM_CAN_CONFIG = 0x00000495,
    IE_GETINK = 0x00000496,
    IE_MSGFIRST = 0x00000496,
    MCIWNDM_PALETTEKICK = 0x00000496,
    IE_SETINK = 0x00000497,
    IE_GETPENTIP = 0x00000498,
    IE_SETPENTIP = 0x00000499,
    IE_GETERASERTIP = 0x0000049A,
    IE_SETERASERTIP = 0x0000049B,
    IE_GETBKGND = 0x0000049C,
    IE_SETBKGND = 0x0000049D,
    IE_GETGRIDORIGIN = 0x0000049E,
    IE_SETGRIDORIGIN = 0x0000049F,
    IE_GETGRIDPEN = 0x000004A0,
    IE_SETGRIDPEN = 0x000004A1,
    IE_GETGRIDSIZE = 0x000004A2,
    IE_SETGRIDSIZE = 0x000004A3,
    IE_GETMODE = 0x000004A4,
    IE_SETMODE = 0x000004A5,
    IE_GETINKRECT = 0x000004A6,
    WM_CAP_SET_MCI_DEVICEW = 0x000004A6,
    WM_CAP_GET_MCI_DEVICEW = 0x000004A7,
    WM_CAP_PAL_OPENW = 0x000004B4,
    WM_CAP_PAL_SAVEW = 0x000004B5,
    IE_GETAPPDATA = 0x000004B8,
    IE_SETAPPDATA = 0x000004B9,
    IE_GETDRAWOPTS = 0x000004BA,
    IE_SETDRAWOPTS = 0x000004BB,
    IE_GETFORMAT = 0x000004BC,
    IE_SETFORMAT = 0x000004BD,
    IE_GETINKINPUT = 0x000004BE,
    IE_SETINKINPUT = 0x000004BF,
    IE_GETNOTIFY = 0x000004C0,
    IE_SETNOTIFY = 0x000004C1,
    IE_GETRECOG = 0x000004C2,
    IE_SETRECOG = 0x000004C3,
    IE_GETSECURITY = 0x000004C4,
    IE_SETSECURITY = 0x000004C5,
    IE_GETSEL = 0x000004C6,
    IE_SETSEL = 0x000004C7,
    CDM_LAST = 0x000004C8,
    EM_SETBIDIOPTIONS = 0x000004C8,
    IE_DOCOMMAND = 0x000004C8,
    MCIWNDM_NOTIFYMODE = 0x000004C8,
    EM_GETBIDIOPTIONS = 0x000004C9,
    IE_GETCOMMAND = 0x000004C9,
    EM_SETTYPOGRAPHYOPTIONS = 0x000004CA,
    IE_GETCOUNT = 0x000004CA,
    EM_GETTYPOGRAPHYOPTIONS = 0x000004CB,
    IE_GETGESTURE = 0x000004CB,
    MCIWNDM_NOTIFYMEDIA = 0x000004CB,
    EM_SETEDITSTYLE = 0x000004CC,
    IE_GETMENU = 0x000004CC,
    EM_GETEDITSTYLE = 0x000004CD,
    IE_GETPAINTDC = 0x000004CD,
    MCIWNDM_NOTIFYERROR = 0x000004CD,
    IE_GETPDEVENT = 0x000004CE,
    IE_GETSELCOUNT = 0x000004CF,
    IE_GETSELITEMS = 0x000004D0,
    IE_GETSTYLE = 0x000004D1,
    MCIWNDM_SETTIMEFORMATW = 0x000004DB,
    EM_OUTLINE = 0x000004DC,
    MCIWNDM_GETTIMEFORMATW = 0x000004DC,
    EM_GETSCROLLPOS = 0x000004DD,
    EM_SETSCROLLPOS = 0x000004DE,
    EM_SETFONTSIZE = 0x000004DF,
    EM_GETZOOM = 0x000004E0,
    MCIWNDM_GETFILENAMEW = 0x000004E0,
    EM_SETZOOM = 0x000004E1,
    MCIWNDM_GETDEVICEW = 0x000004E1,
    EM_GETVIEWKIND = 0x000004E2,
    EM_SETVIEWKIND = 0x000004E3,
    EM_GETPAGE = 0x000004E4,
    MCIWNDM_GETERRORW = 0x000004E4,
    EM_SETPAGE = 0x000004E5,
    EM_GETHYPHENATEINFO = 0x000004E6,
    EM_SETHYPHENATEINFO = 0x000004E7,
    EM_GETPAGEROTATE = 0x000004EB,
    EM_SETPAGEROTATE = 0x000004EC,
    EM_GETCTFMODEBIAS = 0x000004ED,
    EM_SETCTFMODEBIAS = 0x000004EE,
    EM_GETCTFOPENSTATUS = 0x000004F0,
    EM_SETCTFOPENSTATUS = 0x000004F1,
    EM_GETIMECOMPTEXT = 0x000004F2,
    EM_ISIME = 0x000004F3,
    EM_GETIMEPROPERTY = 0x000004F4,
    EM_GETQUERYRTFOBJ = 0x0000050D,
    EM_SETQUERYRTFOBJ = 0x0000050E,
    FM_GETFOCUS = 0x00000600,
    FM_GETDRIVEINFOA = 0x00000601,
    FM_GETSELCOUNT = 0x00000602,
    FM_GETSELCOUNTLFN = 0x00000603,
    FM_GETFILESELA = 0x00000604,
    FM_GETFILESELLFNA = 0x00000605,
    FM_REFRESH_WINDOWS = 0x00000606,
    FM_RELOAD_EXTENSIONS = 0x00000607,
    FM_GETDRIVEINFOW = 0x00000611,
    FM_GETFILESELW = 0x00000614,
    FM_GETFILESELLFNW = 0x00000615,
    WLX_WM_SAS = 0x00000659,
    SM_GETSELCOUNT = 0x000007E8,
    UM_GETSELCOUNT = 0x000007E8,
    WM_CPL_LAUNCH = 0x000007E8,
    SM_GETSERVERSELA = 0x000007E9,
    UM_GETUSERSELA = 0x000007E9,
    WM_CPL_LAUNCHED = 0x000007E9,
    SM_GETSERVERSELW = 0x000007EA,
    UM_GETUSERSELW = 0x000007EA,
    SM_GETCURFOCUSA = 0x000007EB,
    UM_GETGROUPSELA = 0x000007EB,
    SM_GETCURFOCUSW = 0x000007EC,
    UM_GETGROUPSELW = 0x000007EC,
    SM_GETOPTIONS = 0x000007ED,
    UM_GETCURFOCUSA = 0x000007ED,
    UM_GETCURFOCUSW = 0x000007EE,
    UM_GETOPTIONS = 0x000007EF,
    UM_GETOPTIONS2 = 0x000007F0,
    LVM_FIRST = 0x00001000,
    LVM_GETBKCOLOR = 0x00001000,
    LVM_SETBKCOLOR = 0x00001001,
    LVM_GETIMAGELIST = 0x00001002,
    LVM_SETIMAGELIST = 0x00001003,
    LVM_GETITEMCOUNT = 0x00001004,
    LVM_GETITEMA = 0x00001005,
    LVM_SETITEMA = 0x00001006,
    LVM_INSERTITEMA = 0x00001007,
    LVM_DELETEITEM = 0x00001008,
    LVM_DELETEALLITEMS = 0x00001009,
    LVM_GETCALLBACKMASK = 0x0000100A,
    LVM_SETCALLBACKMASK = 0x0000100B,
    LVM_GETNEXTITEM = 0x0000100C,
    LVM_FINDITEMA = 0x0000100D,
    LVM_GETITEMRECT = 0x0000100E,
    LVM_SETITEMPOSITION = 0x0000100F,
    LVM_GETITEMPOSITION = 0x00001010,
    LVM_GETSTRINGWIDTHA = 0x00001011,
    LVM_HITTEST = 0x00001012,
    LVM_ENSUREVISIBLE = 0x00001013,
    LVM_SCROLL = 0x00001014,
    LVM_REDRAWITEMS = 0x00001015,
    LVM_ARRANGE = 0x00001016,
    LVM_EDITLABELA = 0x00001017,
    LVM_GETEDITCONTROL = 0x00001018,
    LVM_GETCOLUMNA = 0x00001019,
    LVM_SETCOLUMNA = 0x0000101A,
    LVM_INSERTCOLUMNA = 0x0000101B,
    LVM_DELETECOLUMN = 0x0000101C,
    LVM_GETCOLUMNWIDTH = 0x0000101D,
    LVM_SETCOLUMNWIDTH = 0x0000101E,
    LVM_GETHEADER = 0x0000101F,
    LVM_CREATEDRAGIMAGE = 0x00001021,
    LVM_GETVIEWRECT = 0x00001022,
    LVM_GETTEXTCOLOR = 0x00001023,
    LVM_SETTEXTCOLOR = 0x00001024,
    LVM_GETTEXTBKCOLOR = 0x00001025,
    LVM_SETTEXTBKCOLOR = 0x00001026,
    LVM_GETTOPINDEX = 0x00001027,
    LVM_GETCOUNTPERPAGE = 0x00001028,
    LVM_GETORIGIN = 0x00001029,
    LVM_UPDATE = 0x0000102A,
    LVM_SETITEMSTATE = 0x0000102B,
    LVM_GETITEMSTATE = 0x0000102C,
    LVM_GETITEMTEXTA = 0x0000102D,
    LVM_SETITEMTEXTA = 0x0000102E,
    LVM_SETITEMCOUNT = 0x0000102F,
    LVM_SORTITEMS = 0x00001030,
    LVM_SETITEMPOSITION32 = 0x00001031,
    LVM_GETSELECTEDCOUNT = 0x00001032,
    LVM_GETITEMSPACING = 0x00001033,
    LVM_GETISEARCHSTRINGA = 0x00001034,
    LVM_SETICONSPACING = 0x00001035,
    LVM_SETEXTENDEDLISTVIEWSTYLE = 0x00001036,
    LVM_GETEXTENDEDLISTVIEWSTYLE = 0x00001037,
    LVM_GETSUBITEMRECT = 0x00001038,
    LVM_SUBITEMHITTEST = 0x00001039,
    LVM_SETCOLUMNORDERARRAY = 0x0000103A,
    LVM_GETCOLUMNORDERARRAY = 0x0000103B,
    LVM_SETHOTITEM = 0x0000103C,
    LVM_GETHOTITEM = 0x0000103D,
    LVM_SETHOTCURSOR = 0x0000103E,
    LVM_GETHOTCURSOR = 0x0000103F,
    LVM_APPROXIMATEVIEWRECT = 0x00001040,
    LVM_SETWORKAREAS = 0x00001041,
    LVM_GETSELECTIONMARK = 0x00001042,
    LVM_SETSELECTIONMARK = 0x00001043,
    LVM_SETBKIMAGEA = 0x00001044,
    LVM_GETBKIMAGEA = 0x00001045,
    LVM_GETWORKAREAS = 0x00001046,
    LVM_SETHOVERTIME = 0x00001047,
    LVM_GETHOVERTIME = 0x00001048,
    LVM_GETNUMBEROFWORKAREAS = 0x00001049,
    LVM_SETTOOLTIPS = 0x0000104A,
    LVM_GETITEMW = 0x0000104B,
    LVM_SETITEMW = 0x0000104C,
    LVM_INSERTITEMW = 0x0000104D,
    LVM_GETTOOLTIPS = 0x0000104E,
    LVM_FINDITEMW = 0x00001053,
    LVM_GETSTRINGWIDTHW = 0x00001057,
    LVM_GETCOLUMNW = 0x0000105F,
    LVM_SETCOLUMNW = 0x00001060,
    LVM_INSERTCOLUMNW = 0x00001061,
    LVM_GETITEMTEXTW = 0x00001073,
    LVM_SETITEMTEXTW = 0x00001074,
    LVM_GETISEARCHSTRINGW = 0x00001075,
    LVM_EDITLABELW = 0x00001076,
    LVM_GETBKIMAGEW = 0x0000108B,
    LVM_SETSELECTEDCOLUMN = 0x0000108C,
    LVM_SETTILEWIDTH = 0x0000108D,
    LVM_SETVIEW = 0x0000108E,
    LVM_GETVIEW = 0x0000108F,
    LVM_INSERTGROUP = 0x00001091,
    LVM_SETGROUPINFO = 0x00001093,
    LVM_GETGROUPINFO = 0x00001095,
    LVM_REMOVEGROUP = 0x00001096,
    LVM_MOVEGROUP = 0x00001097,
    LVM_MOVEITEMTOGROUP = 0x0000109A,
    LVM_SETGROUPMETRICS = 0x0000109B,
    LVM_GETGROUPMETRICS = 0x0000109C,
    LVM_ENABLEGROUPVIEW = 0x0000109D,
    LVM_SORTGROUPS = 0x0000109E,
    LVM_INSERTGROUPSORTED = 0x0000109F,
    LVM_REMOVEALLGROUPS = 0x000010A0,
    LVM_HASGROUP = 0x000010A1,
    LVM_SETTILEVIEWINFO = 0x000010A2,
    LVM_GETTILEVIEWINFO = 0x000010A3,
    LVM_SETTILEINFO = 0x000010A4,
    LVM_GETTILEINFO = 0x000010A5,
    LVM_SETINSERTMARK = 0x000010A6,
    LVM_GETINSERTMARK = 0x000010A7,
    LVM_INSERTMARKHITTEST = 0x000010A8,
    LVM_GETINSERTMARKRECT = 0x000010A9,
    LVM_SETINSERTMARKCOLOR = 0x000010AA,
    LVM_GETINSERTMARKCOLOR = 0x000010AB,
    LVM_SETINFOTIP = 0x000010AD,
    LVM_GETSELECTEDCOLUMN = 0x000010AE,
    LVM_ISGROUPVIEWENABLED = 0x000010AF,
    LVM_GETOUTLINECOLOR = 0x000010B0,
    LVM_SETOUTLINECOLOR = 0x000010B1,
    LVM_CANCELEDITLABEL = 0x000010B3,
    LVM_MAPINDEXTOID = 0x000010B4,
    LVM_MAPIDTOINDEX = 0x000010B5,
    LVM_ISITEMVISIBLE = 0x000010B6,
    OCM__BASE = 0x00002000,
    LVM_SETUNICODEFORMAT = 0x00002005,
    LVM_GETUNICODEFORMAT = 0x00002006,
    OCM_CTLCOLOR = 0x00002019,
    OCM_DRAWITEM = 0x0000202B,
    OCM_MEASUREITEM = 0x0000202C,
    OCM_DELETEITEM = 0x0000202D,
    OCM_VKEYTOITEM = 0x0000202E,
    OCM_CHARTOITEM = 0x0000202F,
    OCM_COMPAREITEM = 0x00002039,
    OCM_NOTIFY = 0x0000204E,
    OCM_COMMAND = 0x00002111,
    OCM_HSCROLL = 0x00002114,
    OCM_VSCROLL = 0x00002115,
    OCM_CTLCOLORMSGBOX = 0x00002132,
    OCM_CTLCOLOREDIT = 0x00002133,
    OCM_CTLCOLORLISTBOX = 0x00002134,
    OCM_CTLCOLORBTN = 0x00002135,
    OCM_CTLCOLORDLG = 0x00002136,
    OCM_CTLCOLORSCROLLBAR = 0x00002137,
    OCM_CTLCOLORSTATIC = 0x00002138,
    OCM_PARENTNOTIFY = 0x00002210,
    WM_APP = 0x00008000,
    WM_RASDIALEVENT = 0x0000CCCD,
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
/// </summary>
[Flags]
internal enum WINDOWSTYLE : uint
{
    /// <summary>
    /// The window has a thin-line border.
    /// </summary>
    WS_BORDER = 0x00800000,

    /// <summary>
    /// The window has a title bar (includes the WS_BORDER style).
    /// </summary>
    WS_CAPTION = 0x00C00000,

    /// <summary>
    /// The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
    /// </summary>
    WS_CHILD = 0x40000000,

    /// <summary>
    /// Same as the WS_CHILD style.
    /// </summary>
    WS_CHILDWINDOW = WS_CHILD,

    /// <summary>
    /// Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.
    /// </summary>
    WS_CLIPCHILDREN = 0x02000000,

    /// <summary>
    /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
    /// </summary>
    WS_CLIPSIBLINGS = 0x04000000,

    /// <summary>
    /// The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.
    /// </summary>
    WS_DISABLED = 0x08000000,

    /// <summary>
    /// The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
    /// </summary>
    WS_DLGFRAME = 0x00400000,

    /// <summary>
    /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
    /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
    /// </summary>
    WS_GROUP = 0x00020000,

    /// <summary>
    /// The window has a horizontal scroll bar.
    /// </summary>
    WS_HSCROLL = 0x00100000,

    /// <summary>
    /// The window is initially minimized. Same as the WS_MINIMIZE style.
    /// </summary>
    WS_ICONIC = 0x20000000,

    /// <summary>
    /// The window is initially maximized.
    /// </summary>
    WS_MAXIMIZE = 0x01000000,

    /// <summary>
    /// The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
    /// </summary>
    WS_MAXIMIZEBOX = 0x00010000,

    /// <summary>
    /// The window is initially minimized. Same as the WS_ICONIC style.
    /// </summary>
    WS_MINIMIZE = WS_ICONIC,

    /// <summary>
    /// The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
    /// </summary>
    WS_MINIMIZEBOX = 0x00020000,

    /// <summary>
    /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
    /// </summary>
    WS_OVERLAPPED = 0x00000000,

    /// <summary>
    /// The window is an overlapped window. Same as the WS_TILEDWINDOW style.
    /// </summary>
    WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

    /// <summary>
    /// The window is a pop-up window. This style cannot be used with the WS_CHILD style.
    /// </summary>
    WS_POPUP = 0x80000000,

    /// <summary>
    /// The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.
    /// </summary>
    WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

    /// <summary>
    /// The window has a sizing border. Same as the WS_THICKFRAME style.
    /// </summary>
    WS_SIZEBOX = 0x00040000,

    /// <summary>
    /// The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
    /// </summary>
    WS_SYSMENU = 0x00080000,

    /// <summary>
    /// The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
    /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function. For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
    /// </summary>
    WS_TABSTOP = 0x00010000,

    /// <summary>
    /// The window has a sizing border. Same as the WS_SIZEBOX style.
    /// </summary>
    WS_THICKFRAME = WS_SIZEBOX,

    /// <summary>
    /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style.
    /// </summary>
    WS_TILED = WS_OVERLAPPED,

    /// <summary>
    /// The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style.
    /// </summary>
    WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

    /// <summary>
    /// The window is initially visible.
    /// This style can be turned on and off by using the ShowWindow or SetWindowPos function.
    /// </summary>
    WS_VISIBLE = 0x10000000,

    /// <summary>
    /// The window has a vertical scroll bar.
    /// </summary>
    WS_VSCROLL = 0x00200000,
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
/// </summary>
[Flags]
internal enum WINDOWSTYLEEX : uint
{
    /// <summary>
    /// The window accepts drag-drop files.
    /// </summary>
    WS_EX_ACCEPTFILES = 0x00000010,

    /// <summary>
    /// Forces a top-level window onto the taskbar when the window is visible.
    /// </summary>
    WS_EX_APPWINDOW = 0x00040000,

    /// <summary>
    /// The window has a border with a sunken edge.
    /// </summary>
    WS_EX_CLIENTEDGE = 0x00000200,

    /// <summary>
    /// Paints all descendants of a window in bottom-to-top painting order using double-buffering. Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects, but only if the descendent window also has the WS_EX_TRANSPARENT bit set. Double-buffering allows the window and its descendents to be painted without flicker. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
    /// Windows 2000: This style is not supported.
    /// </summary>
    WS_EX_COMPOSITED = 0x02000000,

    /// <summary>
    /// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
    /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
    /// </summary>
    WS_EX_CONTEXTHELP = 0x00000400,

    /// <summary>
    /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
    /// </summary>
    WS_EX_CONTROLPARENT = 0x00010000,

    /// <summary>
    /// The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
    /// </summary>
    WS_EX_DLGMODALFRAME = 0x00000001,

    /// <summary>
    /// The window is a layered window. This style cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
    /// Windows 8: The WS_EX_LAYERED style is supported for top-level windows and child windows. Previous Windows versions support WS_EX_LAYERED only for top-level windows.
    /// </summary>
    WS_EX_LAYERED = 0x00080000,

    /// <summary>
    /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge. Increasing horizontal values advance to the left.
    /// </summary>
    WS_EX_LAYOUTRTL = 0x00400000,

    /// <summary>
    /// The window has generic left-aligned properties. This is the default.
    /// </summary>
    WS_EX_LEFT = 0x00000000,

    /// <summary>
    /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
    /// </summary>
    WS_EX_LEFTSCROLLBAR = 0x00004000,

    /// <summary>
    /// The window text is displayed using left-to-right reading-order properties. This is the default.
    /// </summary>
    WS_EX_LTRREADING = WS_EX_LEFT,

    /// <summary>
    /// The window is a MDI child window.
    /// </summary>
    WS_EX_MDICHILD = 0x00000040,

    /// <summary>
    /// A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
    /// The window should not be activated through programmatic access or via keyboard navigation by accessible technology, such as Narrator.
    /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
    /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
    /// </summary>
    WS_EX_NOACTIVATE = 0x08000000,

    /// <summary>
    /// The window does not pass its window layout to its child windows.
    /// </summary>
    WS_EX_NOINHERITLAYOUT = 0x00100000,

    /// <summary>
    /// The child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
    /// </summary>
    WS_EX_NOPARENTNOTIFY = 0x00000004,

    /// <summary>
    /// The window does not render to a redirection surface. This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
    /// </summary>
    WS_EX_NOREDIRECTIONBITMAP = 0x00200000,

    /// <summary>
    /// The window is an overlapped window.
    /// </summary>
    WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

    /// <summary>
    /// The window is palette window, which is a modeless dialog box that presents an array of commands.
    /// </summary>
    WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

    /// <summary>
    /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
    /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
    /// </summary>
    WS_EX_RIGHT = 0x00001000,

    /// <summary>
    /// The vertical scroll bar (if present) is to the right of the client area. This is the default.
    /// </summary>
    WS_EX_RIGHTSCROLLBAR = WS_EX_LEFT,

    /// <summary>
    /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
    /// </summary>
    WS_EX_RTLREADING = 0x00002000,

    /// <summary>
    /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
    /// </summary>
    WS_EX_STATICEDGE = 0x00020000,

    /// <summary>
    /// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.
    /// </summary>
    WS_EX_TOOLWINDOW = 0x00000080,

    /// <summary>
    /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
    /// </summary>
    WS_EX_TOPMOST = 0x00000008,

    /// <summary>
    /// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
    /// To achieve transparency without these restrictions, use the SetWindowRgn function.
    /// </summary>
    WS_EX_TRANSPARENT = 0x00000020,

    /// <summary>
    /// The window has a border with a raised edge.
    /// </summary>
    WS_EX_WINDOWEDGE = 0x00000100,
}
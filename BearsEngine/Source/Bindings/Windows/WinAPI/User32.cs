using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace BearsEngine.WinAPI;

#region Enums

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
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/window-class-styles
/// The class styles define additional elements of the window class. Two or more styles can be combined by using the bitwise OR (|) operator. To assign a style to a window class, assign the style to the style member of the WNDCLASSEX structure.
/// </summary>
[Flags]
internal enum CLASSSTLYE : uint
{
    /// <summary>
    /// Aligns the window's client area on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.
    /// </summary>
    CS_BYTEALIGNCLIENT = 0x1000,

    /// <summary>
    /// Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.
    /// </summary>
    CS_BYTEALIGNWINDOW = 0x2000,

    /// <summary>
    /// Allocates one device context to be shared by all windows in the class. Because window classes are process specific, it is possible for multiple threads of an application to create a window of the same class. It is also possible for the threads to attempt to use the device context simultaneously. When this happens, the system allows only one thread to successfully finish its drawing operation.
    /// </summary>
    CS_CLASSDC = 0x0040,

    /// <summary>
    /// Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class.
    /// </summary>
    CS_DBLCLKS = 0x0008,

    /// <summary>
    /// Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW. Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z-order relationship to other windows. Windows created from a class with this style must be top-level windows; they may not be child windows.
    /// </summary>
    CS_DROPSHADOW = 0x00020000,

    /// <summary>
    /// Indicates that the window class is an application global class. For more information, see the "Application Global Classes" section of About Window Classes.
    /// </summary>
    CS_GLOBALCLASS = 0x4000,

    /// <summary>
    /// Redraws the entire window if a movement or size adjustment changes the width of the client area.
    /// </summary>
    CS_HREDRAW = 0x0002,

    /// <summary>
    /// Disables Close on the window menu.
    /// </summary>
    CS_NOCLOSE = 0x0200,

    /// <summary>
    /// Allocates a unique device context for each window in the class.
    /// </summary>
    CS_OWNDC = 0x0020,

    /// <summary>
    /// Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent. A window with the CS_PARENTDC style bit receives a regular device context from the system's cache of device contexts. It does not give the child the parent's device context or device context settings. Specifying CS_PARENTDC enhances an application's performance.
    /// </summary>
    CS_PARENTDC = 0x0080,

    /// <summary>
    /// Saves, as a bitmap, the portion of the screen image obscured by a window of this class. When the window is removed, the system uses the saved bitmap to restore the screen image, including other windows that were obscured. Therefore, the system does not send WM_PAINT messages to windows that were obscured if the memory used by the bitmap has not been discarded and if other screen actions have not invalidated the stored image.
    /// This style is useful for small windows (for example, menus or dialog boxes) that are displayed briefly and then removed before other screen activity takes place. This style increases the time required to display the window, because the system must first allocate memory to store the bitmap.
    /// </summary>
    CS_SAVEBITS = 0x0800,

    /// <summary>
    /// Redraws the entire window if a movement or size adjustment changes the height of the client area.
    /// </summary>
    CS_VREDRAW = 0x0001,
}

/// <summary>
/// The device type, which determines the event-specific information and what structure will be returned in 
/// </summary>
internal enum DEVBROADCASTTYPE
{
    /// <summary>
    /// Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.
    /// </summary>
    DBT_DEVTYP_DEVICEINTERFACE = 5,

    /// <summary>
    /// File system handle. This structure is a DEV_BROADCAST_HANDLE structure.
    /// </summary>
    DBT_DEVTYP_HANDLE = 6,

    /// <summary>
    /// OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.
    /// </summary>
    DBT_DEVTYP_OEM = 0,

    /// <summary>
    /// Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.
    /// </summary>
    DBT_DEVTYP_PORT = 3,

    /// <summary>
    /// Logical volume. This structure is a DEV_BROADCAST_VOLUME structure.
    /// </summary>
    DBT_DEVTYP_VOLUME = 2,
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
/// For use in <see cref="User32.LoadImage"/>
/// </summary>
internal enum IMAGE_FLAG : uint
{
    /// <summary>
    /// When the uType parameter specifies IMAGE_BITMAP, causes the function to return a DIB section bitmap rather than a compatible bitmap. This flag is useful for loading a bitmap without mapping it to the colors of the display device.
    /// </summary>
    LR_CREATEDIBSECTION = 0x00002000,
    /// <summary>
    /// The default flag; it does nothing. All it means is "not LR_MONOCHROME".
    /// </summary>
    LR_DEFAULTCOLOR = 0x00000000,
    /// <summary>
    /// Uses the width or height specified by the system metric values for cursors or icons, if the cxDesired or cyDesired values are set to zero. If this flag is not specified and cxDesired and cyDesired are set to zero, the function uses the actual resource size. If the resource contains multiple images, the function uses the size of the first image.
    /// </summary>
    LR_DEFAULTSIZE = 0x00000040,
    /// <summary>
    /// Loads the stand-alone image from the file specified by lpszName (icon, cursor, or bitmap file).
    /// </summary>
    LR_LOADFROMFILE = 0x00000010,
    /// <summary>
    /// Searches the color table for the image and replaces the following shades of gray with the corresponding 3-D color.
    /// Dk Gray, RGB(128,128,128) with COLOR_3DSHADOW
    /// Gray, RGB(192,192,192) with COLOR_3DFACE
    /// Lt Gray, RGB(223,223,223) with COLOR_3DLIGHT
    /// Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
    /// </summary>
    LR_LOADMAP3DCOLORS = 0x00001000,
    /// <summary>
    /// Retrieves the color value of the first pixel in the image and replaces the corresponding entry in the color table with the default window color (COLOR_WINDOW). All pixels in the image that use that entry become the default window color. This value applies only to images that have corresponding color tables.
    /// Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
    /// If fuLoad includes both the LR_LOADTRANSPARENT and LR_LOADMAP3DCOLORS values, LR_LOADTRANSPARENT takes precedence. However, the color table entry is replaced with COLOR_3DFACE rather than COLOR_WINDOW.
    /// </summary>
    LR_LOADTRANSPARENT = 0x00000020,
    /// <summary>
    /// Loads the image in black and white.
    /// </summary>
    LR_MONOCHROME = 0x00000001,
    /// <summary>
    /// Shares the image handle if the image is loaded multiple times. If LR_SHARED is not set, a second call to LoadImage for the same resource will load the image again and return a different handle.
    /// When you use this flag, the system will destroy the resource when it is no longer needed.
    /// Do not use LR_SHARED for images that have non-standard sizes, that may change after loading, or that are loaded from a file.
    /// When loading a system icon or cursor, you must use LR_SHARED or the function will fail to load the resource.
    /// This function finds the first image in the cache with the requested resource name, regardless of the size requested.
    /// </summary>
    LR_SHARED = 0x00008000,
    /// <summary>
    /// Uses true VGA colors.
    /// </summary>
    LR_VGACOLOR = 0x00000080,
}

/// <summary>
/// The type of image to be loaded in <see cref="User32.LoadImage"/>
/// </summary>
internal enum IMAGE_TYPE : uint
{
    /// <summary>
    /// Loads a bitmap.
    /// </summary>
    IMAGE_BITMAP = 0,
    /// <summary>
    /// Loads a cursor.
    /// </summary>
    IMAGE_CURSOR = 2,
    /// <summary>
    /// Loads an icon.
    /// </summary>
    IMAGE_ICON = 1,
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
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadcursorw
/// For use with LoadCursor or LoadImage
/// </summary>
public enum PredefinedCursors : ushort
{
    /// <summary>
    /// Standard arrow and small hourglass
    /// </summary>
    IDC_APPSTARTING = 32650,

    /// <summary>
    /// Standard arrow
    /// </summary>
    IDC_ARROW = 32512,

    /// <summary>
    /// Crosshair
    /// </summary>
    IDC_CROSS = 32515,

    /// <summary>
    /// Hand
    /// </summary>
    IDC_HAND = 32649,

    /// <summary>
    /// Arrow and question mark
    /// </summary>
    IDC_HELP = 32651,

    /// <summary>
    /// I-beam
    /// </summary>
    IDC_IBEAM = 32513,

    /// <summary>
    /// Slashed circle
    /// </summary>
    IDC_NO = 32648,

    /// <summary>
    /// Obsolete for applications marked version 4.0 or later. Use IDC_SIZEALL.
    /// </summary>
    IDC_SIZE = 32640,

    /// <summary>
    /// Four-pointed arrow pointing north, south, east, and west
    /// </summary>
    IDC_SIZEALL = 32646,

    /// <summary>
    /// Double-pointed arrow pointing northeast and southwest
    /// </summary>
    IDC_SIZENESW = 32643,

    /// <summary>
    /// Double-pointed arrow pointing north and south
    /// </summary>
    IDC_SIZENS = 32645,

    /// <summary>
    /// Double-pointed arrow pointing northwest and southeast
    /// </summary>
    IDC_SIZENWSE = 32642,

    /// <summary>
    /// Double-pointed arrow pointing west and east
    /// </summary>
    IDC_SIZEWE = 32644,

    /// <summary>
    /// Vertical arrow
    /// </summary>
    IDC_UPARROW = 32516,

    /// <summary>
    /// Hourglass
    /// </summary>
    IDC_WAIT = 32514
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadiconw
/// For use with LoadIcon / LoadImage
/// </summary>
public enum PredefinedIcons : ushort
{
    /// <summary>
    /// Default application icon.
    /// </summary>
    IDI_APPLICATION = 32512,

    /// <summary>
    /// Asterisk icon. Same as IDI_INFORMATION.
    /// </summary>
    IDI_ASTERISK = 32516,

    /// <summary>
    /// Hand-shaped icon.
    /// </summary>
    IDI_ERROR = 32513,

    /// <summary>
    /// Exclamation point icon. Same as IDI_WARNING.
    /// </summary>
    IDI_EXCLAMATION = 32515,

    /// <summary>
    /// Hand-shaped icon. Same as IDI_ERROR.
    /// </summary>
    IDI_HAND = IDI_ERROR,

    /// <summary>
    /// Asterisk icon.
    /// </summary>
    IDI_INFORMATION = IDI_ASTERISK,

    /// <summary>
    /// Question mark icon.
    /// </summary>
    IDI_QUESTION = 32514,

    /// <summary>
    /// Security Shield icon.
    /// </summary>
    IDI_SHIELD = 32518,

    /// <summary>
    /// Exclamation point icon.
    /// </summary>
    IDI_WARNING = IDI_EXCLAMATION,

    /// <summary>
    /// Default application icon.
    /// Windows 2000:  Windows logo icon.
    /// </summary>
    IDI_WINLOGO = 32517,
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
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsyscolor
/// For use with <see cref="User32.GetSysColor"/>.
/// </summary>
internal enum SYSCOLORINDEX : int
{
    /// <summary>
    /// Dark shadow for three-dimensional display elements.
    /// </summary>
    COLOR_3DDKSHADOW = 21,

    /// <summary>
    /// Face color for three-dimensional display elements and for dialog box backgrounds.
    /// </summary>
    COLOR_3DFACE = 15,

    /// <summary>
    /// Highlight color for three-dimensional display elements (for edges facing the light source.)
    /// </summary>
    COLOR_3DHIGHLIGHT = 20,

    /// <summary>
    /// Highlight color for three-dimensional display elements (for edges facing the light source.)
    /// </summary>
    COLOR_3DHILIGHT = COLOR_3DHIGHLIGHT,

    /// <summary>
    /// Light color for three-dimensional display elements (for edges facing the light source.)
    /// </summary>
    COLOR_3DLIGHT = 22,

    /// <summary>
    /// Shadow color for three-dimensional display elements (for edges facing away from the light source).
    /// </summary>
    COLOR_3DSHADOW = 16,

    /// <summary>
    /// Active window border.
    /// </summary>
    COLOR_ACTIVEBORDER = 10,

    /// <summary>
    /// Active window title bar.
    /// The associated foreground color is COLOR_CAPTIONTEXT.
    /// Specifies the left side color in the color gradient of an active window's title bar if the gradient effect is enabled.
    /// </summary>
    COLOR_ACTIVECAPTION = 2,

    /// <summary>
    /// Background color of multiple document interface (MDI) applications.
    /// </summary>
    COLOR_APPWORKSPACE = 12,

    /// <summary>
    /// Desktop
    /// </summary>
    COLOR_BACKGROUND = 1,

    /// <summary>
    /// Face color for three-dimensional display elements and for dialog box backgrounds. The associated foreground color is COLOR_BTNTEXT.
    /// </summary>
    COLOR_BTNFACE = COLOR_3DFACE,

    /// <summary>
    /// Highlight color for three-dimensional display elements (for edges facing the light source.)
    /// </summary>
    COLOR_BTNHIGHLIGHT = COLOR_3DHIGHLIGHT,

    /// <summary>
    /// Highlight color for three-dimensional display elements (for edges facing the light source.)
    /// </summary>
    COLOR_BTNHILIGHT = COLOR_3DHIGHLIGHT,

    /// <summary>
    /// Shadow color for three-dimensional display elements (for edges facing away from the light source).
    /// </summary>
    COLOR_BTNSHADOW = COLOR_3DSHADOW,

    /// <summary>
    /// Text on push buttons. The associated background color is COLOR_BTNFACE.
    /// </summary>
    COLOR_BTNTEXT = 18,

    /// <summary>
    /// Text in caption, size box, and scroll bar arrow box. The associated background color is COLOR_ACTIVECAPTION.
    /// </summary>
    COLOR_CAPTIONTEXT = 9,

    /// <summary>
    /// Desktop.
    /// </summary>
    COLOR_DESKTOP = COLOR_BACKGROUND,

    /// <summary>
    /// Right side color in the color gradient of an active window's title bar. COLOR_ACTIVECAPTION specifies the left side color. Use SPI_GETGRADIENTCAPTIONS with the SystemParametersInfo function to determine whether the gradient effect is enabled.
    /// </summary>
    COLOR_GRADIENTACTIVECAPTION = 27,

    /// <summary>
    /// Right side color in the color gradient of an inactive window's title bar. COLOR_INACTIVECAPTION specifies the left side color.
    /// </summary>
    COLOR_GRADIENTINACTIVECAPTION = 28,

    /// <summary>
    /// Grayed (disabled) text. This color is set to 0 if the current display driver does not support a solid gray color.
    /// </summary>
    COLOR_GRAYTEXT = 17,

    /// <summary>
    /// Item(s) selected in a control. The associated foreground color is COLOR_HIGHLIGHTTEXT.
    /// </summary>
    COLOR_HIGHLIGHT = 13,

    /// <summary>
    /// Text of item(s) selected in a control. The associated background color is COLOR_HIGHLIGHT.
    /// </summary>
    COLOR_HIGHLIGHTTEXT = 14,

    /// <summary>
    /// Color for a hyperlink or hot-tracked item. The associated background color is COLOR_WINDOW.
    /// </summary>
    COLOR_HOTLIGHT = 26,

    /// <summary>
    /// Inactive window border.
    /// </summary>
    COLOR_INACTIVEBORDER = 11,

    /// <summary>
    /// Inactive window caption.
    /// The associated foreground color is COLOR_INACTIVECAPTIONTEXT.
    /// Specifies the left side color in the color gradient of an inactive window's title bar if the gradient effect is enabled.
    /// </summary>
    COLOR_INACTIVECAPTION = 3,

    /// <summary>
    /// Color of text in an inactive caption. The associated background color is COLOR_INACTIVECAPTION.
    /// </summary>
    COLOR_INACTIVECAPTIONTEXT = 19,

    /// <summary>
    /// Background color for tooltip controls. The associated foreground color is COLOR_INFOTEXT.
    /// </summary>
    COLOR_INFOBK = 24,

    /// <summary>
    /// Text color for tooltip controls. The associated background color is COLOR_INFOBK.
    /// </summary>
    COLOR_INFOTEXT = 23,

    /// <summary>
    /// Menu background. The associated foreground color is COLOR_MENUTEXT.
    /// </summary>
    COLOR_MENU = 4,

    /// <summary>
    /// The color used to highlight menu items when the menu appears as a flat menu (see SystemParametersInfo). The highlighted menu item is outlined with COLOR_HIGHLIGHT.
    /// Windows 2000:  This value is not supported.
    /// </summary>
    COLOR_MENUHILIGHT = 29,

    /// <summary>
    /// The background color for the menu bar when menus appear as flat menus (see SystemParametersInfo). However, COLOR_MENU continues to specify the background color of the menu popup.
    /// Windows 2000:  This value is not supported.
    /// </summary>
    COLOR_MENUBAR = 30,

    /// <summary>
    /// Text in menus. The associated background color is COLOR_MENU.
    /// </summary>
    COLOR_MENUTEXT = 7,

    /// <summary>
    /// Scroll bar gray area.
    /// </summary>
    COLOR_SCROLLBAR = 0,

    /// <summary>
    /// Window background. The associated foreground colors are COLOR_WINDOWTEXT and COLOR_HOTLITE.
    /// </summary>
    COLOR_WINDOW = 5,

    /// <summary>
    /// Window frame.
    /// </summary>
    COLOR_WINDOWFRAME = 6,

    /// <summary>
    /// Text in windows. The associated background color is COLOR_WINDOW.
    /// </summary>
    COLOR_WINDOWTEXT = 8,
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

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-sizing
/// The edge of the window that is being sized. Provided by wParam within a WM_SIZING message.
/// </summary>
internal enum WM_SIZING_WPARAM
{
    /// <summary>
    /// Bottom edge
    /// </summary>
    WMSZ_BOTTOM = 6,

    /// <summary>
    ///Bottom-left corner
    /// </summary>
    WMSZ_BOTTOMLEFT = 7,

    /// <summary>
    /// Bottom-right corner
    /// </summary>
    WMSZ_BOTTOMRIGHT = 8,

    /// <summary>
    /// Left edge
    /// </summary>
    WMSZ_LEFT = 1,

    /// <summary>
    /// Right edge
    /// </summary>
    WMSZ_RIGHT = 2,

    /// <summary>
    /// Top edge
    /// </summary>
    WMSZ_TOP = 3,

    /// <summary>
    /// Top-left corner
    /// </summary>
    WMSZ_TOPLEFT = 4,

    /// <summary>
    /// Top-right corner
    /// </summary>
    WMSZ_TOPRIGHT = 5,

    //9 seems to be generated if the window gets resized by dragging the title bar when maximised?
}
#endregion

#region Delegates
/// <summary>
/// This function is an application-defined callback function that processes WM_TIMER messages.
/// </summary>
/// <param name="hwnd">Handle to the window associated with the timer.</param>
/// <param name="uMsg">Specifies the WM_TIMER message.</param>
/// <param name="idEvent">Identifier of the timer.</param>
/// <param name="dwTime">Specifies the number of milliseconds that have elapsed since the system was started. This is the value returned by the GetTickCount function.</param>
[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate void TIMERPROC(IntPtr hwnd, WINDOWMESSAGE uMsg, UIntPtr idEvent, int dwTime);

/// <summary>
/// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633573(v=vs.85)
/// An application-defined function that processes messages sent to a window. The WNDPROC type defines a pointer to this callback function.
/// </summary>
/// <param name="hwnd"></param>
/// <param name="uMsg"></param>
/// <param name="wParam"></param>
/// <param name="lParam"></param>
/// <returns></returns>
[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate IntPtr WNDPROC(IntPtr hwnd, WINDOWMESSAGE uMsg, IntPtr wParam, IntPtr lParam);
#endregion

#region Structs
/// <summary>
/// Contains information about a console screen buffer. https://learn.microsoft.com/en-us/windows/console/console-screen-buffer-info-str
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct CONSOLE_SCREEN_BUFFER_INFO
{
    /// <summary>
    /// A COORD structure that contains the size of the console screen buffer, in character columns and rows.
    /// </summary>
    public COORD dwSize;

    /// <summary>
    /// A COORD structure that contains the column and row coordinates of the cursor in the console screen buffer.
    /// </summary>
    public COORD dwCursorPosition;

    /// <summary>
    /// The attributes of the characters written to a screen buffer by the WriteFile and WriteConsole functions, or echoed to a screen buffer by the ReadFile and ReadConsole functions. For more information, see Character Attributes.
    /// </summary>
    public short wAttributes;

    /// <summary>
    /// A SMALL_RECT structure that contains the console screen buffer coordinates of the upper-left and lower-right corners of the display window.
    /// </summary>
    public SMALL_RECT srWindow;

    /// <summary>
    /// A COORD structure that contains the maximum size of the console window, in character columns and rows, given the current screen buffer size and font and the screen size.
    /// </summary>
    public COORD dwMaximumWindowSize;
}

/// <summary>
/// Defines the coordinates of a character cell in a console screen buffer. The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
/// </summary>
internal struct COORD
{
    /// <summary>
    /// The horizontal coordinate or column value. The units depend on the function call.
    /// </summary>
    public short X;

    /// <summary>
    /// The vertical coordinate or row value. The units depend on the function call.
    /// </summary>
    public short Y;
};

/// <summary>
/// Contains global cursor information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct CURSORINFO
{
    public enum Flags : int
    {
        /// <summary>
        /// The cursor is hidden.
        /// </summary>
        CURSOR_HIDDEN = 0,
        /// <summary>
        /// The cursor is showing.
        /// </summary>
        CURSOR_SHOWING = 0x00000001,
        /// <summary>
        /// Windows 8: The cursor is suppressed. This flag indicates that the system is not drawing the cursor because the user is providing input through touch or pen instead of the mouse.
        /// </summary>
        CURSOR_SUPPRESSED = 0x00000002,
    }

    /// <summary>
    /// The size of the structure, in bytes. The caller must set this to sizeof(CURSORINFO).
    /// </summary>
    public int cbSize;

    /// <summary>
    /// The cursor state.
    /// </summary>
    public Flags flags;

    /// <summary>
    /// A handle to the cursor.
    /// </summary>
    public IntPtr hCursor;

    /// <summary>
    /// A structure that receives the screen coordinates of the cursor.
    /// </summary>
    public POINT ptScreenPos;
}

/// <summary>
/// Contains information about a class of devices.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct DEV_BROADCAST_DEVICEINTERFACE_A
{
    /// <summary>
    /// The size of this structure, in bytes. This is the size of the members plus the actual length of the dbcc_name string (the null character is accounted for by the declaration of dbcc_name as a one-character array.)
    /// </summary>
    internal int dbcc_size;

    /// <summary>
    /// Set to DBT_DEVTYP_DEVICEINTERFACE.
    /// </summary>
    internal DEVBROADCASTTYPE dbcc_devicetype;

    /// <summary>
    /// Reserved; do not use.
    /// </summary>
    private int dbcc_reserved;

    /// <summary>
    /// The GUID for the interface device class.
    /// </summary>
    internal Guid dbcc_classguid;

    /// <summary>
    /// A null-terminated string that specifies the name of the device.
    /// </summary>
    internal char dbcc_name;
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msg
/// Contains message information from a thread's message queue.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct MSG
{
    /// <summary>
    /// A handle to the window whose window procedure receives the message. This member is NULL when the message is a thread message.
    /// </summary>
    internal IntPtr hwnd;

    /// <summary>
    /// The message identifier. Applications can only use the low word; the high word is reserved by the system.
    /// </summary>
    internal WINDOWMESSAGE message;

    /// <summary>
    /// Additional information about the message. The exact meaning depends on the value of the message member.
    /// </summary>
    internal IntPtr wParam;

    /// <summary>
    /// Additional information about the message. The exact meaning depends on the value of the message member.
    /// </summary>
    internal IntPtr lParam;

    /// <summary>
    /// The time at which the message was posted.
    /// </summary>
    internal uint time;

    /// <summary>
    /// The cursor position, in screen coordinates, when the message was posted.
    /// </summary>
    internal POINT pt;

    private readonly uint lPrivate;
}

/// <summary>
/// https://docs.microsoft.com/en-us/previous-versions/dd162805(v=vs.85)
/// The POINT structure defines the x- and y- coordinates of a point.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct POINT
{
    /// <summary>
    /// The x-coordinate of the point.
    /// </summary>
    public int X;

    /// <summary>
    /// The y-coordinate of the point.
    /// </summary>
    public int Y;
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
/// Defines information for the raw input devices. For use with User32 RegisterRawInputDevices.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RAWINPUTDEVICE
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
    /// Mode flag that specifies how to interpret the information provided by usUsagePage and usUsage. For use with User32 RegisterRawInputDevices.
    /// </summary>
    /// <remarks>If RIDEV_NOLEGACY is set for a mouse or a keyboard, the system does not generate any legacy message for that device for the application. For example, if the mouse TLC is set with RIDEV_NOLEGACY, WM_LBUTTONDOWN and related legacy mouse messages are not generated. Likewise, if the keyboard TLC is set with RIDEV_NOLEGACY, WM_KEYDOWN and related legacy keyboard messages are not generated.
    /// If RIDEV_REMOVE is set and the hwndTarget member is not set to NULL, then RegisterRawInputDevices function will fail.</remarks>
    [Flags]
    public enum FLAGS : int
    {
        DEFAULT = 0,
        /// <summary>
        /// If set, this removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection.
        /// </summary>
        RIDEV_REMOVE = 0x00000001,
        /// <summary>
        /// If set, this specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with RIDEV_PAGEONLY.
        /// </summary>
        RIDEV_EXCLUDE = 0x00000010,
        /// <summary>
        /// If set, this specifies all devices whose top level collection is from the specified usUsagePage. Note that usUsage must be zero. To exclude a particular top level collection, use RIDEV_EXCLUDE.
        /// </summary>
        RIDEV_PAGEONLY = 0x00000020,
        /// <summary>
        /// If set, this prevents any devices specified by usUsagePage or usUsage from generating legacy messages. This is only for the mouse and keyboard. See Remarks.
        /// </summary>
        RIDEV_NOLEGACY = 0x00000030,
        /// <summary>
        /// If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that hwndTarget must be specified.
        /// </summary>
        RIDEV_INPUTSINK = 0x00000100,
        /// <summary>
        /// If set, the mouse button click does not activate the other window. RIDEV_CAPTUREMOUSE can be specified only if RIDEV_NOLEGACY is specified for a mouse device.
        /// </summary>
        RIDEV_CAPTUREMOUSE = 0x00000200, // effective when mouse nolegacy is specified, otherwise it would be an error
        /// <summary>
        /// If set, the application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. RIDEV_NOHOTKEYS can be specified even if RIDEV_NOLEGACY is not specified and hwndTarget is NULL.
        /// </summary>
        RIDEV_NOHOTKEYS = 0x00000200, // effective for keyboard.
        /// <summary>
        /// If set, the application command keys are handled. RIDEV_APPKEYS can be specified only if RIDEV_NOLEGACY is specified for a keyboard device.
        /// </summary>
        RIDEV_APPKEYS = 0x00000400, // effective for keyboard.
        /// <summary>
        /// If set, this enables the caller to receive input in the background only if the foreground application does not process it. In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input. This flag is not supported until Windows Vista
        /// </summary>
        RIDEV_EXINPUTSINK = 0x00001000,
        /// <summary>
        /// If set, this enables the caller to receive WM_INPUT_DEVICE_CHANGE notifications for device arrival and device removal. This flag is not supported until Windows Vista
        /// </summary>
        RIDEV_DEVNOTIFY = 0x00002000,
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-id
    /// https://github.com/tpn/winsdk-10/blob/master/Include/10.0.10240.0/shared/hidusage.h
    /// </summary>
    public enum USAGE : ushort
    {
        //
        // Generic Desktop Page (0x01)
        //
        HID_USAGE_GENERIC_POINTER = 0x01,
        HID_USAGE_GENERIC_MOUSE = 0x02,
        HID_USAGE_GENERIC_JOYSTICK = 0x04,
        HID_USAGE_GENERIC_GAMEPAD = 0x05,
        HID_USAGE_GENERIC_KEYBOARD = 0x06,
        HID_USAGE_GENERIC_KEYPAD = 0x07,
        HID_USAGE_GENERIC_PORTABLE_DEVICE_CONTROL = 0x0D,
        HID_USAGE_GENERIC_SYSTEM_CTL = 0x80,

        HID_USAGE_GENERIC_X = 0x30,
        HID_USAGE_GENERIC_Y = 0x31,
        HID_USAGE_GENERIC_Z = 0x32,
        HID_USAGE_GENERIC_RX = 0x33,
        HID_USAGE_GENERIC_RY = 0x34,
        HID_USAGE_GENERIC_RZ = 0x35,
        HID_USAGE_GENERIC_SLIDER = 0x36,
        HID_USAGE_GENERIC_DIAL = 0x37,
        HID_USAGE_GENERIC_WHEEL = 0x38,
        HID_USAGE_GENERIC_HATSWITCH = 0x39,
        HID_USAGE_GENERIC_COUNTED_BUFFER = 0x3A,
        HID_USAGE_GENERIC_BYTE_COUNT = 0x3B,
        HID_USAGE_GENERIC_MOTION_WAKEUP = 0x3C,
        HID_USAGE_GENERIC_VX = 0x40,
        HID_USAGE_GENERIC_VY = 0x41,
        HID_USAGE_GENERIC_VZ = 0x42,
        HID_USAGE_GENERIC_VBRX = 0x43,
        HID_USAGE_GENERIC_VBRY = 0x44,
        HID_USAGE_GENERIC_VBRZ = 0x45,
        HID_USAGE_GENERIC_VNO = 0x46,
        HID_USAGE_GENERIC_RESOLUTION_MULTIPLIER = 0x48,
        HID_USAGE_GENERIC_SYSCTL_POWER = 0x81,
        HID_USAGE_GENERIC_SYSCTL_SLEEP = 0x82,
        HID_USAGE_GENERIC_SYSCTL_WAKE = 0x83,
        HID_USAGE_GENERIC_SYSCTL_CONTEXT_MENU = 0x84,
        HID_USAGE_GENERIC_SYSCTL_MAIN_MENU = 0x85,
        HID_USAGE_GENERIC_SYSCTL_APP_MENU = 0x86,
        HID_USAGE_GENERIC_SYSCTL_HELP_MENU = 0x87,
        HID_USAGE_GENERIC_SYSCTL_MENU_EXIT = 0x88,
        HID_USAGE_GENERIC_SYSCTL_MENU_SELECT = 0x89,
        HID_USAGE_GENERIC_SYSCTL_MENU_RIGHT = 0x8A,
        HID_USAGE_GENERIC_SYSCTL_MENU_LEFT = 0x8B,
        HID_USAGE_GENERIC_SYSCTL_MENU_UP = 0x8C,
        HID_USAGE_GENERIC_SYSCTL_MENU_DOWN = 0x8D,
        HID_USAGE_GENERIC_SYSTEM_DISPLAY_ROTATION_LOCK_BUTTON = 0xC9,
        HID_USAGE_GENERIC_SYSTEM_DISPLAY_ROTATION_LOCK_SLIDER_SWITCH = 0xCA,
        HID_USAGE_GENERIC_CONTROL_ENABLE = 0xCB,

        //
        // Simulation Controls Page (0x02)
        //
        HID_USAGE_SIMULATION_RUDDER = 0xBA,
        HID_USAGE_SIMULATION_THROTTLE = 0xBB,


        //
        // Virtual Reality Controls Page (0x03)
        //


        //
        // Sport Controls Page (0x04)
        //


        //
        // Game Controls Page (0x05)
        //


        //
        // Keyboard/Keypad Page (0x07)
        //

        // Error "keys"
        HID_USAGE_KEYBOARD_NOEVENT = 0x00,
        HID_USAGE_KEYBOARD_ROLLOVER = 0x01,
        HID_USAGE_KEYBOARD_POSTFAIL = 0x02,
        HID_USAGE_KEYBOARD_UNDEFINED = 0x03,

        // Letters
        HID_USAGE_KEYBOARD_aA = 0x04,
        HID_USAGE_KEYBOARD_zZ = 0x1D,

        // Numbers
        HID_USAGE_KEYBOARD_ONE = 0x1E,
        HID_USAGE_KEYBOARD_ZERO = 0x27,

        // Modifier Keys
        HID_USAGE_KEYBOARD_LCTRL = 0xE0,
        HID_USAGE_KEYBOARD_LSHFT = 0xE1,
        HID_USAGE_KEYBOARD_LALT = 0xE2,
        HID_USAGE_KEYBOARD_LGUI = 0xE3,
        HID_USAGE_KEYBOARD_RCTRL = 0xE4,
        HID_USAGE_KEYBOARD_RSHFT = 0xE5,
        HID_USAGE_KEYBOARD_RALT = 0xE6,
        HID_USAGE_KEYBOARD_RGUI = 0xE7,
        HID_USAGE_KEYBOARD_SCROLL_LOCK = 0x47,
        HID_USAGE_KEYBOARD_NUM_LOCK = 0x53,
        HID_USAGE_KEYBOARD_CAPS_LOCK = 0x39,

        // Function keys
        HID_USAGE_KEYBOARD_F1 = 0x3A,
        HID_USAGE_KEYBOARD_F2 = 0x3B,
        HID_USAGE_KEYBOARD_F3 = 0x3C,
        HID_USAGE_KEYBOARD_F4 = 0x3D,
        HID_USAGE_KEYBOARD_F5 = 0x3E,
        HID_USAGE_KEYBOARD_F6 = 0x3F,
        HID_USAGE_KEYBOARD_F7 = 0x40,
        HID_USAGE_KEYBOARD_F8 = 0x41,
        HID_USAGE_KEYBOARD_F9 = 0x42,
        HID_USAGE_KEYBOARD_F10 = 0x43,
        HID_USAGE_KEYBOARD_F11 = 0x44,
        HID_USAGE_KEYBOARD_F12 = 0x45,
        HID_USAGE_KEYBOARD_F13 = 0x68,
        HID_USAGE_KEYBOARD_F14 = 0x69,
        HID_USAGE_KEYBOARD_F15 = 0x6A,
        HID_USAGE_KEYBOARD_F16 = 0x6B,
        HID_USAGE_KEYBOARD_F17 = 0x6C,
        HID_USAGE_KEYBOARD_F18 = 0x6D,
        HID_USAGE_KEYBOARD_F19 = 0x6E,
        HID_USAGE_KEYBOARD_F20 = 0x6F,
        HID_USAGE_KEYBOARD_F21 = 0x70,
        HID_USAGE_KEYBOARD_F22 = 0x71,
        HID_USAGE_KEYBOARD_F23 = 0x72,
        HID_USAGE_KEYBOARD_F24 = 0x73,

        HID_USAGE_KEYBOARD_RETURN = 0x28,
        HID_USAGE_KEYBOARD_ESCAPE = 0x29,
        HID_USAGE_KEYBOARD_DELETE = 0x2A,

        HID_USAGE_KEYBOARD_PRINT_SCREEN = 0x46,
        HID_USAGE_KEYBOARD_DELETE_FORWARD = 0x4C,


        //
        // LED Page (0x08)
        //
        HID_USAGE_LED_NUM_LOCK = 0x01,
        HID_USAGE_LED_CAPS_LOCK = 0x02,
        HID_USAGE_LED_SCROLL_LOCK = 0x03,
        HID_USAGE_LED_COMPOSE = 0x04,
        HID_USAGE_LED_KANA = 0x05,
        HID_USAGE_LED_POWER = 0x06,
        HID_USAGE_LED_SHIFT = 0x07,
        HID_USAGE_LED_DO_NOT_DISTURB = 0x08,
        HID_USAGE_LED_MUTE = 0x09,
        HID_USAGE_LED_TONE_ENABLE = 0x0A,
        HID_USAGE_LED_HIGH_CUT_FILTER = 0x0B,
        HID_USAGE_LED_LOW_CUT_FILTER = 0x0C,
        HID_USAGE_LED_EQUALIZER_ENABLE = 0x0D,
        HID_USAGE_LED_SOUND_FIELD_ON = 0x0E,
        HID_USAGE_LED_SURROUND_FIELD_ON = 0x0F,
        HID_USAGE_LED_REPEAT = 0x10,
        HID_USAGE_LED_STEREO = 0x11,
        HID_USAGE_LED_SAMPLING_RATE_DETECT = 0x12,
        HID_USAGE_LED_SPINNING = 0x13,
        HID_USAGE_LED_CAV = 0x14,
        HID_USAGE_LED_CLV = 0x15,
        HID_USAGE_LED_RECORDING_FORMAT_DET = 0x16,
        HID_USAGE_LED_OFF_HOOK = 0x17,
        HID_USAGE_LED_RING = 0x18,
        HID_USAGE_LED_MESSAGE_WAITING = 0x19,
        HID_USAGE_LED_DATA_MODE = 0x1A,
        HID_USAGE_LED_BATTERY_OPERATION = 0x1B,
        HID_USAGE_LED_BATTERY_OK = 0x1C,
        HID_USAGE_LED_BATTERY_LOW = 0x1D,
        HID_USAGE_LED_SPEAKER = 0x1E,
        HID_USAGE_LED_HEAD_SET = 0x1F,
        HID_USAGE_LED_HOLD = 0x20,
        HID_USAGE_LED_MICROPHONE = 0x21,
        HID_USAGE_LED_COVERAGE = 0x22,
        HID_USAGE_LED_NIGHT_MODE = 0x23,
        HID_USAGE_LED_SEND_CALLS = 0x24,
        HID_USAGE_LED_CALL_PICKUP = 0x25,
        HID_USAGE_LED_CONFERENCE = 0x26,
        HID_USAGE_LED_STAND_BY = 0x27,
        HID_USAGE_LED_CAMERA_ON = 0x28,
        HID_USAGE_LED_CAMERA_OFF = 0x29,
        HID_USAGE_LED_ON_LINE = 0x2A,
        HID_USAGE_LED_OFF_LINE = 0x2B,
        HID_USAGE_LED_BUSY = 0x2C,
        HID_USAGE_LED_READY = 0x2D,
        HID_USAGE_LED_PAPER_OUT = 0x2E,
        HID_USAGE_LED_PAPER_JAM = 0x2F,
        HID_USAGE_LED_REMOTE = 0x30,
        HID_USAGE_LED_FORWARD = 0x31,
        HID_USAGE_LED_REVERSE = 0x32,
        HID_USAGE_LED_STOP = 0x33,
        HID_USAGE_LED_REWIND = 0x34,
        HID_USAGE_LED_FAST_FORWARD = 0x35,
        HID_USAGE_LED_PLAY = 0x36,
        HID_USAGE_LED_PAUSE = 0x37,
        HID_USAGE_LED_RECORD = 0x38,
        HID_USAGE_LED_ERROR = 0x39,
        HID_USAGE_LED_SELECTED_INDICATOR = 0x3A,
        HID_USAGE_LED_IN_USE_INDICATOR = 0x3B,
        HID_USAGE_LED_MULTI_MODE_INDICATOR = 0x3C,
        HID_USAGE_LED_INDICATOR_ON = 0x3D,
        HID_USAGE_LED_INDICATOR_FLASH = 0x3E,
        HID_USAGE_LED_INDICATOR_SLOW_BLINK = 0x3F,
        HID_USAGE_LED_INDICATOR_FAST_BLINK = 0x40,
        HID_USAGE_LED_INDICATOR_OFF = 0x41,
        HID_USAGE_LED_FLASH_ON_TIME = 0x42,
        HID_USAGE_LED_SLOW_BLINK_ON_TIME = 0x43,
        HID_USAGE_LED_SLOW_BLINK_OFF_TIME = 0x44,
        HID_USAGE_LED_FAST_BLINK_ON_TIME = 0x45,
        HID_USAGE_LED_FAST_BLINK_OFF_TIME = 0x46,
        HID_USAGE_LED_INDICATOR_COLOR = 0x47,
        HID_USAGE_LED_RED = 0x48,
        HID_USAGE_LED_GREEN = 0x49,
        HID_USAGE_LED_AMBER = 0x4A,
        HID_USAGE_LED_GENERIC_INDICATOR = 0x4B,

        //
        //  Button Page (0x09)
        //
        //  There is no need to label these usages.
        //


        //
        //  Ordinal Page (0x0A)
        //
        //  There is no need to label these usages.
        //


        //
        //  Telephony Device Page (0x0B)
        //
        HID_USAGE_TELEPHONY_PHONE = 0x01,
        HID_USAGE_TELEPHONY_ANSWERING_MACHINE = 0x02,
        HID_USAGE_TELEPHONY_MESSAGE_CONTROLS = 0x03,
        HID_USAGE_TELEPHONY_HANDSET = 0x04,
        HID_USAGE_TELEPHONY_HEADSET = 0x05,
        HID_USAGE_TELEPHONY_KEYPAD = 0x06,
        HID_USAGE_TELEPHONY_PROGRAMMABLE_BUTTON = 0x07,
        HID_USAGE_TELEPHONY_REDIAL = 0x24,
        HID_USAGE_TELEPHONY_TRANSFER = 0x25,
        HID_USAGE_TELEPHONY_DROP = 0x26,
        HID_USAGE_TELEPHONY_LINE = 0x2A,
        HID_USAGE_TELEPHONY_RING_ENABLE = 0x2D,
        HID_USAGE_TELEPHONY_SEND = 0x31,
        HID_USAGE_TELEPHONY_KEYPAD_0 = 0xB0,
        HID_USAGE_TELEPHONY_KEYPAD_D = 0xBF,
        HID_USAGE_TELEPHONY_HOST_AVAILABLE = 0xF1,


        //
        // Consumer Controls Page (0x0C)
        //
        HID_USAGE_CONSUMERCTRL = 0x01,

        // channel
        HID_USAGE_CONSUMER_CHANNEL_INCREMENT = 0x9C,
        HID_USAGE_CONSUMER_CHANNEL_DECREMENT = 0x9D,

        // transport control
        HID_USAGE_CONSUMER_PLAY = 0xB0,
        HID_USAGE_CONSUMER_PAUSE = 0xB1,
        HID_USAGE_CONSUMER_RECORD = 0xB2,
        HID_USAGE_CONSUMER_FAST_FORWARD = 0xB3,
        HID_USAGE_CONSUMER_REWIND = 0xB4,
        HID_USAGE_CONSUMER_SCAN_NEXT_TRACK = 0xB5,
        HID_USAGE_CONSUMER_SCAN_PREV_TRACK = 0xB6,
        HID_USAGE_CONSUMER_STOP = 0xB7,
        HID_USAGE_CONSUMER_PLAY_PAUSE = 0xCD,

        // audio
        HID_USAGE_CONSUMER_VOLUME = 0xE0,
        HID_USAGE_CONSUMER_BALANCE = 0xE1,
        HID_USAGE_CONSUMER_MUTE = 0xE2,
        HID_USAGE_CONSUMER_BASS = 0xE3,
        HID_USAGE_CONSUMER_TREBLE = 0xE4,
        HID_USAGE_CONSUMER_BASS_BOOST = 0xE5,
        HID_USAGE_CONSUMER_SURROUND_MODE = 0xE6,
        HID_USAGE_CONSUMER_LOUDNESS = 0xE7,
        HID_USAGE_CONSUMER_MPX = 0xE8,
        HID_USAGE_CONSUMER_VOLUME_INCREMENT = 0xE9,
        HID_USAGE_CONSUMER_VOLUME_DECREMENT = 0xEA,

        // supplementary audio
        HID_USAGE_CONSUMER_BASS_INCREMENT = 0x152,
        HID_USAGE_CONSUMER_BASS_DECREMENT = 0x153,
        HID_USAGE_CONSUMER_TREBLE_INCREMENT = 0x154,
        HID_USAGE_CONSUMER_TREBLE_DECREMENT = 0x155,

        // Application Launch
        HID_USAGE_CONSUMER_AL_CONFIGURATION = 0x183,
        HID_USAGE_CONSUMER_AL_EMAIL = 0x18A,
        HID_USAGE_CONSUMER_AL_CALCULATOR = 0x192,
        HID_USAGE_CONSUMER_AL_BROWSER = 0x194,

        // Application Control
        HID_USAGE_CONSUMER_AC_SEARCH = 0x221,
        HID_USAGE_CONSUMER_AC_GOTO = 0x222,
        HID_USAGE_CONSUMER_AC_HOME = 0x223,
        HID_USAGE_CONSUMER_AC_BACK = 0x224,
        HID_USAGE_CONSUMER_AC_FORWARD = 0x225,
        HID_USAGE_CONSUMER_AC_STOP = 0x226,
        HID_USAGE_CONSUMER_AC_REFRESH = 0x227,
        HID_USAGE_CONSUMER_AC_PREVIOUS = 0x228,
        HID_USAGE_CONSUMER_AC_NEXT = 0x229,
        HID_USAGE_CONSUMER_AC_BOOKMARKS = 0x22A,
        HID_USAGE_CONSUMER_AC_PAN = 0x238,

        // Keyboard Extended Attributes (defined on consumer page in HUTRR42)
        HID_USAGE_CONSUMER_EXTENDED_KEYBOARD_ATTRIBUTES_COLLECTION = 0x2C0,
        HID_USAGE_CONSUMER_KEYBOARD_FORM_FACTOR = 0x2C1,
        HID_USAGE_CONSUMER_KEYBOARD_KEY_TYPE = 0x2C2,
        HID_USAGE_CONSUMER_KEYBOARD_PHYSICAL_LAYOUT = 0x2C3,
        HID_USAGE_CONSUMER_VENDOR_SPECIFIC_KEYBOARD_PHYSICAL_LAYOUT = 0x2C4,
        HID_USAGE_CONSUMER_KEYBOARD_IETF_LANGUAGE_TAG_INDEX = 0x2C5,
        HID_USAGE_CONSUMER_IMPLEMENTED_KEYBOARD_INPUT_ASSIST_CONTROLS = 0x2C6,

        //
        // Digitizer Page (0x0D)
        //
        HID_USAGE_DIGITIZER_PEN = 0x02,
        HID_USAGE_DIGITIZER_IN_RANGE = 0x32,
        HID_USAGE_DIGITIZER_TIP_SWITCH = 0x42,
        HID_USAGE_DIGITIZER_BARREL_SWITCH = 0x44,


        //
        // Sensor Page (0x20)
        //


        //
        // Camera Control Page (0x90)
        //
        HID_USAGE_CAMERA_AUTO_FOCUS = 0x20,
        HID_USAGE_CAMERA_SHUTTER = 0x21,

        //
        // Microsoft Bluetooth Handsfree Page (0xFFF3)
        //
        HID_USAGE_MS_BTH_HF_DIALNUMBER = 0x21,
        HID_USAGE_MS_BTH_HF_DIALMEMORY = 0x22,
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-page
    /// https://github.com/tpn/winsdk-10/blob/master/Include/10.0.10240.0/shared/hidusage.h
    /// HID usages are organized into usage pages of related controls. A specific control usage is defined by its usage page, a usage ID, a name, and a description. A usage page value is a 16-bit unsigned value. For use with User32 RegisterRawInputDevices.
    /// </summary>
    internal enum USAGEPAGE : ushort
    {
        /// <summary>Unknown usage page.</summary>
        HID_USAGE_PAGE_UNDEFINED = 0x00,
        /// <summary>Generic desktop controls.</summary>
        HID_USAGE_PAGE_GENERIC = 0x01,
        /// <summary>Simulation controls.</summary>
        HID_USAGE_PAGE_SIMULATION = 0x02,
        /// <summary>Virtual reality controls.</summary>
        HID_USAGE_PAGE_VR = 0x03,
        /// <summary>Sports controls.</summary>
        HID_USAGE_PAGE_SPORT = 0x04,
        /// <summary>Games controls.</summary>
        HID_USAGE_PAGE_GAME = 0x05,
        /// <summary>Keyboard controls.</summary>
        HID_USAGE_PAGE_KEYBOARD = 0x07,
        /// <summary>LED controls.</summary>
        HID_USAGE_PAGE_LED = 0x08,
        /// <summary>Button.</summary>
        HID_USAGE_PAGE_BUTTON = 0x09,
        /// <summary>Ordinal.</summary>
        HID_USAGE_PAGE_ORDINAL = 0x0A,
        /// <summary>Telephony.</summary>
        HID_USAGE_PAGE_TELEPHONY = 0x0B,
        /// <summary>Consumer.</summary>
        HID_USAGE_PAGE_CONSUMER = 0x0C,
        /// <summary>Digitizer.</summary>
        HID_USAGE_PAGE_DIGITIZER = 0x0D,
        /// <summary>Physical interface device.</summary>
        HID_USAGE_PAGE_PID = 0x0F,
        /// <summary>Unicode.</summary>
        HID_USAGE_PAGE_UNICODE = 0x10,
        /// <summary>Alphanumeric display.</summary>
        HID_USAGE_PAGE_ALPHANUMERIC = 0x14,
        HID_USAGE_PAGE_SENSOR = 0x20,
        /// <summary>Medical instruments.</summary>
        HID_USAGE_PAGE_MEDICAL = 0x40,
        /// <summary>Monitor page 0.</summary>
        HID_USAGE_PAGE_MONITOR_PAGE_0 = 0x80,
        /// <summary>Monitor page 1.</summary>
        HID_USAGE_PAGE_MONITOR_PAGE_1 = 0x81,
        /// <summary>Monitor page 2.</summary>
        HID_USAGE_PAGE_MONITOR_PAGE_2 = 0x82,
        /// <summary>Monitor page 3.</summary>
        HID_USAGE_PAGE_MONITOR_PAGE_3 = 0x83,
        /// <summary>Power page 0.</summary>
        HID_USAGE_PAGE_POWER_PAGE_0 = 0x84,
        /// <summary>Power page 1.</summary>
        HID_USAGE_PAGE_POWER_PAGE_1 = 0x85,
        /// <summary>Power page 2.</summary>
        HID_USAGE_PAGE_POWER_PAGE_2 = 0x86,
        /// <summary>Power page 3.</summary>
        HID_USAGE_PAGE_POWER_PAGE_3 = 0x87,
        /// <summary>Bar code scanner.</summary>
        HID_USAGE_PAGE_BARCODE_SCANNER = 0x8C,
        /// <summary>Scale page.</summary>
        HID_USAGE_PAGE_WEIGHING_DEVICE = 0x8D,
        /// <summary>Magnetic strip reading devices.</summary>
        HID_USAGE_PAGE_MAGNETIC_STRIPE_READER = 0x8E,
        HID_USAGE_PAGE_CAMERA_CONTROL = 0x90,
        HID_USAGE_PAGE_MICROSOFT_BLUETOOTH_HANDSFREE = 0xFFF3,
        HID_USAGE_PAGE_VENDOR_DEFINED_BEGIN = 0xFF00,
        HID_USAGE_PAGE_VENDOR_DEFINED_END = 0xFFFF,
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/top-level-collections
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-page
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-architecture#hid-clients-supported-in-windows
    /// Top level collection Usage page for the raw input device. See HID Clients Supported in Windows for details on possible values.
    /// </summary>
    internal USAGEPAGE usUsagePage;

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/top-level-collections
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-id
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-architecture#hid-clients-supported-in-windows
    /// Top level collection Usage ID for the raw input device. See HID Clients Supported in Windows for details on possible values.
    /// </summary>
    internal USAGE usUsage;

    /// <summary>
    /// Mode flag that specifies how to interpret the information provided by usUsagePage and usUsage. It can be zero (the default) or one of the following values. By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.
    /// RIDEV_REMOVE: If set, this removes the top level collection from the inclusion list.This tells the operating system to stop reading from a device which matches the top level collection.
    /// RIDEV_EXCLUDE: If set, this specifies the top level collections to exclude when reading a complete usage page.This flag only affects a TLC whose usage page is already specified with RIDEV_PAGEONLY.
    /// RIDEV_PAGEONLY: If set, this specifies all devices whose top level collection is from the specified usUsagePage. Note that usUsage must be zero. To exclude a particular top level collection, use RIDEV_EXCLUDE.
    /// RIDEV_NOLEGACY: If set, this prevents any devices specified by usUsagePage or usUsage from generating legacy messages. This is only for the mouse and keyboard. See Remarks.
    /// RIDEV_INPUTSINK: If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that hwndTarget must be specified.
    /// RIDEV_CAPTUREMOUSE: If set, the mouse button click does not activate the other window. RIDEV_CAPTUREMOUSE can be specified only if RIDEV_NOLEGACY is specified for a mouse device.
    /// RIDEV_NOHOTKEYS: If set, the application-defined keyboard device hotkeys are not handled.However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled.By default, all keyboard hotkeys are handled.RIDEV_NOHOTKEYS can be specified even if RIDEV_NOLEGACY is not specified and hwndTarget is NULL.
    /// RIDEV_APPKEYS: If set, the application command keys are handled.RIDEV_APPKEYS can be specified only if RIDEV_NOLEGACY is specified for a keyboard device.
    /// RIDEV_EXINPUTSINK: If set, this enables the caller to receive input in the background only if the foreground application does not process it.In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input. This flag is not supported until Windows Vista
    /// RIDEV_DEVNOTIFY: If set, this enables the caller to receive WM_INPUT_DEVICE_CHANGE notifications for device arrival and device removal. Windows XP: This flag is not supported until Windows Vista
    /// </summary>
    internal FLAGS dwFlags;

    /// <summary>
    /// Handle to the target window. If NULL it follows the keyboard focus.
    /// </summary>
    internal IntPtr hwndTarget;
}

/// <summary>
/// The RECT structure defines a rectangle by the coordinates of its upper-left and lower-right corners. https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RECT
{
    /// <summary>
    /// Specifies the x-coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public int left;

    /// <summary>
    /// Specifies the y-coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public int top;

    /// <summary>
    /// Specifies the x-coordinate of the lower-right corner of the rectangle.
    /// </summary>
    public int right;

    /// <summary>
    /// Specifies the y-coordinate of the lower-right corner of the rectangle.
    /// </summary>
    public int bottom;

    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    public int Width => right - left;

    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    public int Height => bottom - top;

    /// <summary>
    /// The point representing the centre of the rectangle;
    /// </summary>
    public POINT Centre => new() { X = (right + left) / 2, Y = (bottom + top) / 2 };

    /// <summary>
    /// The unmanaged size in memory of this object.
    /// </summary>
    public static int UnmanagedSize => Marshal.SizeOf(default(RECT));
}

/// <summary>
/// Used by the <see cref="User32.TrackMouseEvent"/> function to track when the mouse pointer leaves a window or hovers over a window for a specified amount of time.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct TRACKMOUSEEVENT
{
    [Flags]
    public enum FLAGS : uint
    {
        /// <summary>
        /// The caller wants to cancel a prior tracking request. The caller should also specify the type of tracking that it wants to cancel. For example, to cancel hover tracking, the caller must pass the TME_CANCEL and TME_HOVER flags.
        /// </summary>
        TME_CANCEL = 0x80000000,
        /// <summary>
        /// The caller wants hover notification. Notification is delivered as a WM_MOUSEHOVER message. 
        /// If the caller requests hover tracking while hover tracking is already active, the hover timer will be reset. 
        /// This flag is ignored if the mouse pointer is not over the specified window or area.
        /// </summary>
        TME_HOVER = 0x00000001,
        /// <summary>
        /// The caller wants leave notification. Notification is delivered as a WM_MOUSELEAVE message. If the mouse is not over the specified window or area, a leave notification is generated immediately and no further tracking is performed.
        /// </summary>
        TME_LEAVE = 0x00000002,
        /// <summary>
        /// The caller wants hover and leave notification for the nonclient areas. Notification is delivered as WM_NCMOUSEHOVER and WM_NCMOUSELEAVE messages.
        /// </summary>
        TME_NONCLIENT = 0x00000010,
        /// <summary>
        /// The function fills in the structure instead of treating it as a tracking request. The structure is filled such that had that structure been passed to TrackMouseEvent, it would generate the current tracking. The only anomaly is that the hover time-out returned is always the actual time-out and not HOVER_DEFAULT, if HOVER_DEFAULT was specified during the original TrackMouseEvent request.
        /// </summary>
        TME_QUERY = 0x40000000,
    }

    /// <summary>
    /// The size of the TRACKMOUSEEVENT structure, in bytes.
    /// </summary>
    public int cbSize;
    /// <summary>
    /// The services requested.
    /// </summary>
    public FLAGS dwFlags;
    /// <summary>
    /// A handle to the window to track.
    /// </summary>
    public IntPtr hwndTrack;
    /// <summary>
    /// The hover time-out (if TME_HOVER was specified in dwFlags), in milliseconds. Can be HOVER_DEFAULT, which means to use the system default hover time-out.
    /// </summary>
    public int dwHoverTime;
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-wndclassexw
/// Contains window class information. It is used with the RegisterClassEx and GetClassInfoEx  functions.
/// The WNDCLASSEX structure is similar to the WNDCLASS structure.There are two differences.WNDCLASSEX includes the cbSize member, which specifies the size of the structure, and the hIconSm member, which contains a handle to a small icon associated with the window class.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal struct WNDCLASSEX
{
    /// <summary>
    /// The size, in bytes, of this structure. Set this member to sizeof(WNDCLASSEX). Be sure to set this member before calling the GetClassInfoEx function.
    /// </summary>
    public uint cbSize;

    /// <summary>
    /// The class style(s). This member can be any combination of the Class Styles.
    /// </summary>
    public CLASSSTLYE style;

    /// <summary>
    /// A pointer to the window procedure. You must use the CallWindowProc function to call the window procedure. For more information, see WindowProc.
    /// </summary>
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public WNDPROC lpfnWndProc;

    /// <summary>
    /// The number of extra bytes to allocate following the window-class structure. The system initializes the bytes to zero.
    /// </summary>
    public int cbClsExtra;

    /// <summary>
    /// The number of extra bytes to allocate following the window instance. The system initializes the bytes to zero. If an application uses WNDCLASSEX to register a dialog box created by using the CLASS directive in the resource file, it must set this member to DLGWINDOWEXTRA.
    /// </summary>
    public int cbWndExtra;

    /// <summary>
    /// A handle to the instance that contains the window procedure for the class.
    /// </summary>
    public IntPtr hInstance;

    /// <summary>
    /// A handle to the class icon. This member must be a handle to an icon resource. If this member is NULL, the system provides a default icon.
    /// </summary>
    public IntPtr hIcon;

    /// <summary>
    /// A handle to the class cursor. This member must be a handle to a cursor resource. If this member is NULL, an application must explicitly set the cursor shape whenever the mouse moves into the application's window.
    /// </summary>
    public IntPtr hCursor;

    /// <summary>
    /// A handle to the class background brush. This member can be a handle to the brush to be used for painting the background, or it can be a color value. A color value must be one of the following standard system colors (the value 1 must be added to the chosen color). The system automatically deletes class background brushes when the class is unregistered by using UnregisterClass. An application should not delete these brushes. When this member is NULL, an application must paint its own background whenever it is requested to paint in its client area.To determine whether the background must be painted, an application can either process the WM_ERASEBKGND message or test the fErase member of the PAINTSTRUCT structure filled by the BeginPaint function.
    /// </summary>
    public IntPtr hbrBackground;

    /// <summary>
    /// Pointer to a null-terminated character string that specifies the resource name of the class menu, as the name appears in the resource file. If you use an integer to identify the menu, use the MAKEINTRESOURCE macro. If this member is NULL, windows belonging to this class have no default menu.
    /// </summary>
    public IntPtr lpszMenuName;

    /// <summary>
    /// A pointer to a null-terminated string or is an atom. If this parameter is an atom, it must be a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpszClassName; the high-order word must be zero.
    /// If lpszClassName is a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
    /// The maximum length for lpszClassName is 256. If lpszClassName is greater than the maximum length, the RegisterClassEx function will fail.
    /// </summary>
    public IntPtr lpszClassName;

    /// <summary>
    /// A handle to a small icon that is associated with the window class. If this member is NULL, the system searches the icon resource specified by the hIcon member for an icon of the appropriate size to use as the small icon.
    /// </summary>
    public IntPtr hIconSm;
}
#endregion

/// <summary>
/// Windows management functions for message handling, timers, menus, and communications.
/// </summary>
[SuppressUnmanagedCodeSecurity]
internal static partial class User32
{
    private const string Library = "User32.dll";

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-adjustwindowrect
    /// Calculates the required size of the window rectangle, based on the desired client-rectangle size. The window rectangle can then be passed to the CreateWindow function to create a window whose client area is the desired size.
    ///To specify an extended window style, use the AdjustWindowRectEx function.
    /// </summary>
    /// <param name="lpRect">A pointer to a RECT structure that contains the coordinates of the top-left and bottom-right corners of the desired client area. When the function returns, the structure contains the coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.</param>
    /// <param name="dwStyle">The window style of the window whose required size is to be calculated. Note that you cannot specify the WS_OVERLAPPED style.</param>
    /// <param name="bMenu">Indicates whether the window has a menu.</param>
    ///<returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool AdjustWindowRect(ref RECT lpRect, WINDOWSTYLE dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-adjustwindowrectex
    /// Calculates the required size of the window rectangle, based on the desired size of the client rectangle. The window rectangle can then be passed to the CreateWindowEx function to create a window whose client area is the desired size.
    /// </summary>
    /// <param name="lpRect">A pointer to a RECT structure that contains the coordinates of the top-left and bottom-right corners of the desired client area. When the function returns, the structure contains the coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.</param>
    /// <param name="dwStyle">The window style of the window whose required size is to be calculated. Note that you cannot specify the WS_OVERLAPPED style.</param>
    /// <param name="bMenu">Indicates whether the window has a menu.</param>
    /// <param name="dwExStyle">The extended window style of the window whose required size is to be calculated.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool AdjustWindowRectEx(ref RECT lpRect, WINDOWSTYLE dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, WINDOWSTYLEEX dwExStyle);

    /// <summary>
    /// Brings the specified window to the top of the Z order. If the window is a top-level window, it is activated. If the window is a child window, the top-level parent window associated with the child window is activated.
    /// </summary>
    /// <param name="hWnd">A handle to the window to bring to the top of the Z order.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool BringWindowToTop(IntPtr hWnd);

    /// <summary>
    /// Passes message information to the specified window procedure.
    /// </summary>
    /// <param name="lpPrevWndFunc">The previous window procedure. If this value is obtained by calling the GetWindowLong function with the nIndex parameter set to GWL_WNDPROC or DWL_DLGPROC, it is actually either the address of a window or dialog box procedure, or a special internal value meaningful only to CallWindowProc.</param>
    /// <param name="hWnd">A handle to the window procedure to receive the message.</param>
    /// <param name="Msg">The message.</param>
    /// <param name="wParam">Additional message-specific information. The contents of this parameter depend on the value of the Msg parameter.</param>
    /// <param name="lParam">Additional message-specific information. The contents of this parameter depend on the value of the Msg parameter.</param>
    /// <returns>The return value specifies the result of the message processing and depends on the message sent.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, WINDOWMESSAGE Msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// The ChangeDisplaySettings function changes the settings of the default display device to the specified graphics mode.
    /// </summary>
    /// <param name="lpDevMode">Pointer to a <see cref="DEVMODE"/> structure that describes the new graphics mode. If lpDevMode is NULL, all the values currently in the registry will be used for the display setting. Passing NULL for the lpDevMode parameter and 0 for the dwFlags parameter is the easiest way to return to the default mode after a dynamic mode change.</param>
    /// <param name="dwFlags">Indicates how the graphics mode should be changed.</param>
    /// <returns>Returns a <see cref="DISPCHANGERESULT"/> to indicate the result.</returns>
    /// <remarks>To change the settings of a specified display device, use the ChangeDisplaySettingsEx function. To ensure that the DEVMODE structure passed to ChangeDisplaySettings is valid and contains only values supported by the display driver, use the DEVMODE returned by the EnumDisplaySettings function. When the display mode is changed dynamically, the WM_DISPLAYCHANGE message is sent to all running applications. </remarks>
    [DllImport(Library, SetLastError = true)]
    public static extern DISPCHANGERESULT ChangeDisplaySettings(DEVMODE lpDevMode, CHANGEDISPLAYSETTINGSFLAGS dwFlags);

    /// <summary>
    /// The ChangeDisplaySettings function changes the settings of the default display device to the specified graphics mode.
    /// </summary>
    /// <param name="lpDevMode">Pointer to a <see cref="DEVMODE"/> structure that describes the new graphics mode. If lpDevMode is NULL, all the values currently in the registry will be used for the display setting. Passing NULL for the lpDevMode parameter and 0 for the dwFlags parameter is the easiest way to return to the default mode after a dynamic mode change.</param>
    /// <param name="dwFlags">Indicates how the graphics mode should be changed.</param>
    /// <returns>Returns a <see cref="DISPCHANGERESULT"/> to indicate the result.</returns>
    /// <remarks>To change the settings of a specified display device, use the ChangeDisplaySettingsEx function. To ensure that the DEVMODE structure passed to ChangeDisplaySettings is valid and contains only values supported by the display driver, use the DEVMODE returned by the EnumDisplaySettings function. When the display mode is changed dynamically, the WM_DISPLAYCHANGE message is sent to all running applications. </remarks>
    [LibraryImport(Library, SetLastError = true)]
    public static partial DISPCHANGERESULT ChangeDisplaySettings(IntPtr lpDevMode, CHANGEDISPLAYSETTINGSFLAGS dwFlags);

    /// <summary>
    /// The ChangeDisplaySettingsEx function changes the settings of the specified display device to the specified graphics mode.
    /// </summary>
    /// <param name="lpszDeviceName">A pointer to a null-terminated string that specifies the display device whose graphics mode will change. Only display device names as returned by EnumDisplayDevices are valid. See EnumDisplayDevices for further information on the names associated with these display devices. The lpszDeviceName parameter can be NULL.A NULL value specifies the default display device. The default device can be determined by calling EnumDisplayDevices and checking for the DISPLAY_DEVICE_PRIMARY_DEVICE flag.</param>
    /// <param name="lpDevMode">A pointer to a DEVMODE structure that describes the new graphics mode. If lpDevMode is NULL, all the values currently in the registry will be used for the display setting. Passing NULL for the lpDevMode parameter and 0 for the dwFlags parameter is the easiest way to return to the default mode after a dynamic mode change.</param>
    /// <param name="hwnd">Reserved; must be NULL.</param>
    /// <param name="dwflags">Indicates how the graphics mode should be changed.</param>
    /// <param name="lParam">If dwFlags is CDS_VIDEOPARAMETERS, lParam is a pointer to a VIDEOPARAMETERS structure. Otherwise lParam must be NULL.</param>
    /// <returns>Returns a <see cref="DISPCHANGERESULT"/> to indicate the result.</returns>
    /// <remarks>To ensure that the DEVMODE structure passed to ChangeDisplaySettings is valid and contains only values supported by the display driver, use the DEVMODE returned by the EnumDisplaySettings function. When the display mode is changed dynamically, the WM_DISPLAYCHANGE message is sent to all running applications.</remarks>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern DISPCHANGERESULT ChangeDisplaySettingsEx([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName, DEVMODE lpDevMode, IntPtr hwnd, CHANGEDISPLAYSETTINGSFLAGS dwflags, IntPtr lParam);

    /// <summary>
    /// Converts the client-area coordinates of a specified point to screen coordinates.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose client area is used for the conversion.</param>
    /// <param name="point">A pointer to a POINT structure that contains the client coordinates to be converted. The new screen coordinates are copied into this structure if the function succeeds.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

    /// <summary>
    /// Confines the cursor to a rectangular area on the screen. If a subsequent cursor position (set by the SetCursorPos function or the mouse) lies outside the rectangle, the system automatically adjusts the position to keep the cursor inside the rectangular area.
    /// </summary>
    /// <param name="rcClip">A pointer to the structure that contains the screen coordinates of the upper-left and lower-right corners of the confining rectangle. If this parameter is NULL, the cursor is free to move anywhere on the screen.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, CharSet = CharSet.Auto)]
    public static extern bool ClipCursor(ref RECT rcClip);

    /// <summary>
    /// Confines the cursor to a rectangular area on the screen. If a subsequent cursor position (set by the SetCursorPos function or the mouse) lies outside the rectangle, the system automatically adjusts the position to keep the cursor inside the rectangular area.
    /// </summary>
    /// <param name="rcClip">A pointer to the structure that contains the screen coordinates of the upper-left and lower-right corners of the confining rectangle. If this parameter is NULL, the cursor is free to move anywhere on the screen.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, CharSet = CharSet.Auto)]
    public static extern bool ClipCursor(IntPtr rcClip);

    /// <summary>
    /// Creates an icon or cursor from an ICONINFO structure.
    /// </summary>
    /// <param name="piconinfo">A pointer to an ICONINFO structure the function uses to create the icon or cursor.</param>
    /// <returns>If the function succeeds, the return value is a handle to the icon or cursor that is created. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr CreateIconIndirect(ref ICONINFO piconinfo);

    /// <summary>
    /// Creates an overlapped, pop-up, or child window with an extended window style; otherwise, this function is identical to the CreateWindow function. For more information about creating a window and for full descriptions of the other parameters of CreateWindowEx, see CreateWindow.
    /// </summary>
    /// <param name="dwExStyle">The extended window style of the window being created. For a list of possible values, see Extended Window Styles.</param>
    /// <param name="lpClassName">A null-terminated string or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero. If lpClassName is a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, provided that the module that registers the class is also the module that creates the window. The class name can also be any of the predefined system class names.</param>
    /// <param name="lpWindowName">The window name. If the window style specifies a title bar, the window title pointed to by lpWindowName is displayed in the title bar. When using CreateWindow to create controls, such as buttons, check boxes, and static controls, use lpWindowName to specify the text of the control. When creating a static control with the SS_ICON style, use lpWindowName to specify the icon name or identifier. To specify an identifier, use the syntax "#num".</param>
    /// <param name="dwStyle">The style of the window being created. This parameter can be a combination of the window style values, plus the control styles indicated in the Remarks section.</param>
    /// <param name="X">The initial horizontal position of the window. For an overlapped or pop-up window, the x parameter is the initial x-coordinate of the window's upper-left corner, in screen coordinates. For a child window, x is the x-coordinate of the upper-left corner of the window relative to the upper-left corner of the parent window's client area. If x is set to CW_USEDEFAULT, the system selects the default position for the window's upper-left corner and ignores the y parameter. CW_USEDEFAULT is valid only for overlapped windows; if it is specified for a pop-up or child window, the x and y parameters are set to zero.</param>
    /// <param name="Y">The initial vertical position of the window. For an overlapped or pop-up window, the y parameter is the initial y-coordinate of the window's upper-left corner, in screen coordinates. For a child window, y is the initial y-coordinate of the upper-left corner of the child window relative to the upper-left corner of the parent window's client area. For a list box y is the initial y-coordinate of the upper-left corner of the list box's client area relative to the upper-left corner of the parent window's client area.
    /// If an overlapped window is created with the WS_VISIBLE style bit set and the x parameter is set to CW_USEDEFAULT, then the y parameter determines how the window is shown.If the y parameter is CW_USEDEFAULT, then the window manager calls ShowWindow with the SW_SHOW flag after the window has been created. If the y parameter is some other value, then the window manager calls ShowWindow with that value as the nCmdShow parameter.</param>
    /// <param name="nWidth">The width, in device units, of the window. For overlapped windows, nWidth is the window's width, in screen coordinates, or CW_USEDEFAULT. If nWidth is CW_USEDEFAULT, the system selects a default width and height for the window; the default width extends from the initial x-coordinates to the right edge of the screen; the default height extends from the initial y-coordinate to the top of the icon area. CW_USEDEFAULT is valid only for overlapped windows; if CW_USEDEFAULT is specified for a pop-up or child window, the nWidth and nHeight parameter are set to zero.</param>
    /// <param name="nHeight">The height, in device units, of the window. For overlapped windows, nHeight is the window's height, in screen coordinates. If the nWidth parameter is set to CW_USEDEFAULT, the system ignores nHeight.</param>
    /// <param name="hWndParent">A handle to the parent or owner window of the window being created. To create a child window or an owned window, supply a valid window handle. This parameter is optional for pop-up windows.
    /// To create a message-only window, supply HWND_MESSAGE or a handle to an existing message-only window.</param>
    /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier, depending on the window style. For an overlapped or pop-up window, hMenu identifies the menu to be used with the window; it can be NULL if the class menu is to be used. For a child window, hMenu specifies the child-window identifier, an integer value used by a dialog box control to notify its parent about events. The application determines the child-window identifier; it must be unique for all child windows with the same parent window.</param>
    /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
    /// <param name="lpParam">Pointer to a value to be passed to the window through the CREATESTRUCT structure (lpCreateParams member) pointed to by the lParam param of the WM_CREATE message. This message is sent to the created window by this function before it returns.
    /// If an application calls CreateWindow to create a MDI client window, lpParam should point to a CLIENTCREATESTRUCT structure.If an MDI client window calls CreateWindow to create an MDI child window, lpParam should point to a MDICREATESTRUCT structure.lpParam may be NULL if no additional data is needed.</param>
    /// <returns>If the function succeeds, the return value is a handle to the new window.
    /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial IntPtr CreateWindowEx(WINDOWSTYLEEX dwExStyle, IntPtr lpClassName, IntPtr lpWindowName, WINDOWSTYLE dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

    /// <summary>
    /// Calls the default window procedure to provide default processing for any window messages that an application does not process. This function ensures that every message is processed. DefWindowProc is called with the same parameters received by the window procedure.
    /// </summary>
    /// <param name="hWnd">A handle to the window procedure that received the message.</param>
    /// <param name="msg">The message.</param>
    /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
    /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
    /// <returns>The return value is the result of the message processing and depends on the message.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr DefWindowProc(IntPtr hWnd, WINDOWMESSAGE msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Destroys a cursor and frees any memory the cursor occupied. Do not use this function to destroy a shared cursor.
    /// </summary>
    /// <param name="hCursor">A handle to the cursor to be destroyed. The cursor must not be in use.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool DestroyCursor(IntPtr hCursor);

    /// <summary>
    /// Destroys an icon and frees any memory the icon occupied.
    /// </summary>
    /// <param name="hIcon">A handle to the icon to be destroyed. The icon must not be in use.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool DestroyIcon(IntPtr hIcon);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroywindow
    /// Destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).
    /// If the specified window is a parent or owner window, DestroyWindow automatically destroys the associated child or owned windows when it destroys the parent or owner window. The function first destroys child or owned windows, and then it destroys the parent or owner window.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library)]
    public static extern bool DestroyWindow(IntPtr hWnd);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-dispatchmessage
    /// Dispatches a message to a window procedure. It is typically used to dispatch a message retrieved by the GetMessage function.
    /// </summary>
    /// <param name="lpMsg">A pointer to a structure that contains the message.</param>
    /// <returns>The return value specifies the value returned by the window procedure. Although its meaning depends on the message being dispatched, the return value generally is ignored.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr DispatchMessage(ref MSG lpMsg);

    /// <summary>
    /// The EnumDisplayDevices function lets you obtain information about the display devices in the current session.
    /// </summary>
    /// <param name="lpDevice">A pointer to the device name. If NULL, function returns information for the display adapter(s) on the machine, based on iDevNum.</param>
    /// <param name="iDevNum">An index value that specifies the display device of interest. The operating system identifies each display device in the current session with an index value.The index values are consecutive integers, starting at 0. If the current session has three display devices, for example, they are specified by the index values 0, 1, and 2.</param>
    /// <param name="lpDisplayDevice">A pointer to a DISPLAY_DEVICE structure that receives information about the display device specified by iDevNum. Before calling EnumDisplayDevices, you must initialize the cb member of DISPLAY_DEVICE to the size, in bytes, of DISPLAY_DEVICE.</param>
    /// <param name="dwFlags">Set this flag to EDD_GET_DEVICE_INTERFACE_NAME (0x00000001) to retrieve the device interface name for GUID_DEVINTERFACE_MONITOR, which is registered by the operating system on a per monitor basis. The value is placed in the DeviceID member of the DISPLAY_DEVICE structure returned in lpDisplayDevice. The resulting device interface name can be used with SetupAPI functions and serves as a link between GDI monitor devices and SetupAPI monitor devices.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. The function fails if iDevNum is greater than the largest device index.</returns>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumDisplayDevices(IntPtr lpDevice, int iDevNum, [In, Out] DISPLAY_DEVICE lpDisplayDevice, ENUMDISPLAYDEVICEFLAG dwFlags);

    /// <summary>
    /// The EnumDisplaySettingsEx function retrieves information about one of the graphics modes for a display device. To retrieve information for all the graphics modes for a display device, make a series of calls to this function. This function differs from EnumDisplaySettings in that there is a dwFlags parameter.
    /// </summary>
    /// <param name="lpszDeviceName">A pointer to a null-terminated string that specifies the display device about which graphics mode the function will obtain information. This parameter is either NULL or a DISPLAY_DEVICE.DeviceName returned from EnumDisplayDevices.A NULL value specifies the current display device on the computer that the calling thread is running on.</param>
    /// <param name="iModeNum">Indicates the type of information to be retrieved. This value can be a graphics mode index or one of the <see cref="DisplayModeSettingsEnum"/> values.</param>
    /// <param name="lpDevMode">A pointer to a <see cref="DEVMODE"/> structure into which the function stores information about the specified graphics mode. Before calling EnumDisplaySettingsEx, set the dmSize member to sizeof (DEVMODE), and set the dmDriverExtra member to indicate the size, in bytes, of the additional space available to receive private driver data.</param>
    /// <param name="dwFlags">This parameter can be the following value.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
    [DllImport(Library, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumDisplaySettingsEx([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName, DisplayModeSettingsEnum iModeNum, [In, Out] DEVMODE lpDevMode, ENUMDISPLAYSETTINGSFLAG dwFlags);

    /// <summary>
    /// The EnumDisplaySettingsEx function retrieves information about one of the graphics modes for a display device. To retrieve information for all the graphics modes for a display device, make a series of calls to this function. This function differs from EnumDisplaySettings in that there is a dwFlags parameter.
    /// </summary>
    /// <param name="lpszDeviceName">A pointer to a null-terminated string that specifies the display device about which graphics mode the function will obtain information. This parameter is either NULL or a DISPLAY_DEVICE.DeviceName returned from EnumDisplayDevices.A NULL value specifies the current display device on the computer that the calling thread is running on.</param>
    /// <param name="iModeNum">Indicates the type of information to be retrieved. This value can be a graphics mode index or one of the <see cref="DisplayModeSettingsEnum"/> values.</param>
    /// <param name="lpDevMode">A pointer to a <see cref="DEVMODE"/> structure into which the function stores information about the specified graphics mode. Before calling EnumDisplaySettingsEx, set the dmSize member to sizeof (DEVMODE), and set the dmDriverExtra member to indicate the size, in bytes, of the additional space available to receive private driver data.</param>
    /// <param name="dwFlags">This parameter can be the following value.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool EnumDisplaySettingsEx([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName, int iModeNum, [In, Out] DEVMODE lpDevMode, ENUMDISPLAYSETTINGSFLAG dwFlags);

    /// <summary>
    /// Retrieves a handle to the top-level window whose class name and window name match the specified strings. This function does not search child windows. This function does not perform a case-sensitive search. To search child windows, beginning with a specified child window, use the FindWindowEx function.
    /// </summary>
    /// <param name="lpClassName">The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero. If lpClassName points to a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names. If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.</param>
    /// <param name="lpWindowName">The window name (the window's title). If this parameter is NULL, all window names match.</param>
    /// <returns>If the function succeeds, the return value is a handle to the window that has the specified class name and window name. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr FindWindow(string? lpClassName, string? lpWindowName);

    /// <summary>
    /// Retrieves a handle to the window (if any) that has captured the mouse. Only one window at a time can capture the mouse; this window receives mouse input whether or not the cursor is within its borders.
    /// </summary>
    /// <returns>The return value is a handle to the capture window associated with the current thread. If no window in the thread has captured the mouse, the return value is NULL.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr GetCapture();

    /// <summary>
    /// The GetClientRect function retrieves the coordinates of a window's client area. The client coordinates specify the upper-left and lower-right corners of the client area. Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0).
    /// </summary>
    /// <param name="windowHandle">A handle to the window whose client coordinates are to be retrieved.</param>
    /// <param name="clientRectangle">A pointer to a <see cref="RECT"/> structure that receives the client coordinates. The left and top members are zero. The right and bottom members contain the width and height of the window.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool GetClientRect(IntPtr windowHandle, out RECT clientRectangle);

    /// <summary>
    /// Retrieves a handle to the current cursor.
    /// </summary>
    /// <returns>
    /// The return value is the handle to the current cursor. If there is no cursor, the return value is null.
    /// </returns>
    [LibraryImport(Library)]
    public static partial IntPtr GetCursor();

    /// <summary>
    /// Retrieves information about the global cursor.
    /// </summary>
    /// <param name="pci">A pointer to a <see cref="CURSORINFO"/> structure that receives the information.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool GetCursorInfo(ref CURSORINFO pci);

    /// <summary>
    /// Retrieves the position of the mouse cursor, in screen coordinates. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
    /// </summary>
    /// <param name="point">Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.A pointer to a POINT structure that receives the screen coordinates of the cursor.</param>
    /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool GetCursorPos(ref POINT lpPoint);

    /// <summary>
    /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen. You can use the returned handle in subsequent GDI functions to draw in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
    /// </summary>
    /// <param name="hwnd">A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC for the entire screen.</param>
    /// <returns>If the function succeeds, the return value is a handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr GetDC(IntPtr hwnd);

    /// <summary>
    /// Retrieves the current double-click time for the mouse. A double-click is a series of two clicks of the mouse button, the second occurring within a specified time after the first. The double-click time is the maximum number of milliseconds that may occur between the first and second click of a double-click. The maximum double-click time is 5000 milliseconds.
    /// </summary>
    /// <returns>The return value specifies the current double-click time, in milliseconds. The maximum return value is 5000 milliseconds.</returns>
    [LibraryImport(Library)]
    public static partial int GetDoubleClickTime();

    /// <summary>
    /// Returns the dots per inch (dpi) value for the specified window.
    /// </summary>
    /// <param name="hWnd">The window that you want to get information about.</param>
    /// <returns>The DPI for the window, which depends on the DPI_AWARENESS of the window. See the Remarks section for more information. An invalid hwnd value will result in a return value of 0.</returns>
    [LibraryImport(Library)]
    public static partial int GetDpiForWindow(IntPtr hWnd);

    /// <summary>
    /// Retrieves information about the specified icon or cursor.
    /// </summary>
    /// <param name="hIcon">A handle to the icon or cursor.</param>
    /// <param name="pIconInfo">A pointer to an ICONINFO structure. The function fills in the structure's members.</param>
    /// <returns>If the function succeeds, the return value is nonzero and the function fills in the members of the specified ICONINFO structure. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref ICONINFO pIconInfo);

    /// <summary>
    /// Retrieves a string that represents the name of a key. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametexta
    /// </summary>
    /// <param name="lParam">The second parameter of the keyboard message (such as WM_KEYDOWN) to be processed. The function interprets the following bit positions in the lParam.
    /// 16-23	Scan code.
    /// 24	Extended-key flag. Distinguishes some keys on an enhanced keyboard.
    /// 25	"Do not care" bit. The application calling this function sets this bit to indicate that the function should not distinguish between left and right CTRL and SHIFT keys, for example.</param>
    /// <param name="lpString">The buffer that will receive the key name.</param>
    /// <param name="cchSize">The maximum length, in characters, of the key name, including the terminating null character. (This parameter should be equal to the size of the buffer pointed to by the lpString parameter.)</param>
    /// <returns>If the function succeeds, a null-terminated string is copied into the specified buffer, and the return value is the length of the string, in characters, not counting the terminating null character. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int GetKeyNameText(int lParam, [Out] StringBuilder lpString, int cchSize);

    /// <summary>
    /// Retrieves a string that represents the name of a key. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametexta
    /// </summary>
    /// <param name="extendedScanCode"></param>
    /// <returns>If the function succeeds, a null-terminated string is copied into the specified buffer, and the return value is the length of the string, in characters, not counting the terminating null character. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    public static string GetKeyNameText(uint extendedScanCode)
    {
        int StringMaxLength = 512;
        int key = (int)extendedScanCode;
        StringBuilder s = new(StringMaxLength);
        _ = GetKeyNameText(key, s, StringMaxLength);

        return s.ToString();
    }

    /// <summary>
    /// Retrieves a string that represents the name of a key. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametexta
    /// </summary>
    /// <param name="scanCode"></param>
    /// <param name="isE0"></param>
    /// <returns>If the function succeeds, a null-terminated string is copied into the specified buffer, and the return value is the length of the string, in characters, not counting the terminating null character. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    public static string GetKeyNameText(uint scanCode, bool isE0)
    {
        int StringMaxLength = 512;
        int key = (int)((scanCode << 16) | ((isE0 ? 1 : (uint)0) << 24));
        StringBuilder s = new(StringMaxLength);
        _ = GetKeyNameText(key, s, StringMaxLength);

        return s.ToString();
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessage
    /// Retrieves a message from the calling thread's message queue. The function dispatches incoming sent messages until a posted message is available for retrieval.
    /// Unlike GetMessage, the PeekMessage function does not wait for a message to be posted before returning.
    /// </summary>
    /// <param name="lpMsg">A pointer to an MSG structure that receives message information from the thread's message queue.</param>
    /// <param name="hWnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread
    /// If hWnd is NULL, GetMessage retrieves messages for any window that belongs to the current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL, both window messages and thread messages are processed.
    /// If hWnd is -1, GetMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL, that is, thread messages as posted by PostMessage (when the hWnd parameter is NULL) or PostThreadMessage.</param>
    /// <param name="wMsgFilterMin">The integer value of the lowest message value to be retrieved. Use WM_KEYFIRST (0x0100) to specify the first keyboard message or WM_MOUSEFIRST (0x0200) to specify the first mouse message.
    /// Use WM_INPUT here and in wMsgFilterMax to specify only the WM_INPUT messages.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <param name="wMsgFilterMax">The integer value of the highest message value to be retrieved. Use WM_KEYLAST to specify the last keyboard message or WM_MOUSELAST to specify the last mouse message.
    /// Use WM_INPUT here and in wMsgFilterMin to specify only the WM_INPUT messages.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <returns>If the function retrieves a message other than WM_QUIT, the return value is nonzero. If the function retrieves the WM_QUIT message, the return value is zero. If there is an error, the return value is -1. For example, the function fails if hWnd is an invalid window handle or lpMsg is an invalid pointer.To get extended error information, call GetLastError.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial int GetMessage(ref MSG lpMsg, IntPtr hWnd, int wMsgFilterMin, int wMsgFilterMax);

    /// <summary>
    /// The GetMonitorInfo function retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A pointer to a MONITORINFO or MONITORINFOEX structure that receives information about the specified display monitor.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
    [DllImport(Library)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Retrieves a history of up to 64 previous coordinates of the mouse or pen.
    /// </summary>
    /// <param name="cbSize">The size, in bytes, of the MOUSEMOVEPOINT structure.</param>
    /// <param name="pointsIn">A pointer to a MOUSEMOVEPOINT structure containing valid mouse coordinates (in screen coordinates). It may also contain a time stamp. The GetMouseMovePointsEx function searches for the point in the mouse coordinates history.If the function finds the point, it returns the last nBufPoints prior to and including the supplied point. If your application supplies a time stamp, the GetMouseMovePointsEx function will use it to differentiate between two equal points that were recorded at different times. An application should call this function using the mouse coordinates received from the WM_MOUSEMOVE message and convert them to screen coordinates.</param>
    /// <param name="pointsBufferOut">A pointer to a buffer that will receive the points. It should be at least cbSize* nBufPoints in size.</param>
    /// <param name="nBufPoints">The number of points to be retrieved.</param>
    /// <param name="resolution">The resolution desired.</param>
    /// <returns>If the function succeeds, the return value is the number of points in the buffer. Otherwise, the function returns –1. For extended error information, your application can call GetLastError.</returns>
    [LibraryImport(Library, SetLastError = true)]
public static     unsafe partial int GetMouseMovePointsEx(uint cbSize, MOUSEMOVEPOINT* lppt, MOUSEMOVEPOINT* lpptBuf, int nBufPoints, GMMP_RESOLUTION resolution);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getqueuestatus
    /// Retrieves the type of messages found in the calling thread's message queue.
    /// </summary>
    /// <param name="flags">The types of messages for which to check.</param>
    /// <returns>The high-order word of the return value indicates the types of messages currently in the queue. The low-order word indicates the types of messages that have been added to the queue and that are still in the queue since the last call to the GetQueueStatus, GetMessage, or PeekMessage function.</returns>
    [LibraryImport(Library)]
    public static partial uint GetQueueStatus(QUEUESTATUSFLAGS flags);

    /// <summary>
    /// Retrieves the raw input from the specified device.
    /// </summary>
    /// <param name="hRawInput">A handle to the RAWINPUT structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="uiCommand">The command flag.</param>
    /// <param name="pData">A pointer to the data that comes from the RAWINPUT structure. This depends on the value of uiCommand. If pData is NULL, the required size of the buffer is returned in *pcbSize.</param>
    /// <param name="pcbSize">The size, in bytes, of the data in pData.</param>
    /// <param name="cbSizeHeader">The size, in bytes, of the RAWINPUTHEADER structure..</param>
    /// <returns>If pData is NULL and the function is successful, the return value is 0. If pData is not NULL and the function is successful, the return value is the number of bytes copied into pData. If there is an error, the return value is (UINT)-1.</returns>
    [DllImport(Library)]
    public static extern int GetRawInputData(IntPtr hRawInput, RAWINPUTDATAFLAG uiCommand, [Out] IntPtr pData, [In, Out] ref int pcbSize, int cbSizeHeader);

    /// <summary>
    /// Retrieves the raw input from the specified device.
    /// </summary>
    /// <param name="hRawInput">A handle to the RAWINPUT structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="uiCommand">The command flag.</param>
    /// <param name="pData">A pointer to the data that comes from the RAWINPUT structure. This depends on the value of uiCommand. If pData is NULL, the required size of the buffer is returned in *pcbSize.</param>
    /// <param name="pcbSize">The size, in bytes, of the data in pData.</param>
    /// <param name="cbSizeHeader">The size, in bytes, of the RAWINPUTHEADER structure..</param>
    /// <returns>If pData is NULL and the function is successful, the return value is 0. If pData is not NULL and the function is successful, the return value is the number of bytes copied into pData. If there is an error, the return value is (UINT)-1.</returns>
    [DllImport(Library)]
    public static extern int GetRawInputData(IntPtr hRawInput, RAWINPUTDATAFLAG uiCommand, [Out] out RawInput pData, [In, Out] ref int pcbSize, int cbSizeHeader);

    /// <summary>
    /// Retrieves information about the raw input device.
    /// </summary>
    /// <param name="hDevice">A handle to the raw input device. This comes from the hDevice member of RAWINPUTHEADER or from GetRawInputDeviceList.</param>
    /// <param name="uiCommand">Specifies what data will be returned in pData.</param>
    /// <param name="pData">A pointer to a buffer that contains the information specified by uiCommand. If uiCommand is RIDI_DEVICEINFO, set the cbSize member of RID_DEVICE_INFO to sizeof(RID_DEVICE_INFO) before calling GetRawInputDeviceInfo.</param>
    /// <param name="pcbSize">The size, in bytes, of the data in pData.</param>
    /// <returns>If successful, this function returns a non-negative number indicating the number of bytes copied to pData. If pData is not large enough for the data, the function returns -1. If pData is NULL, the function returns a value of zero.In both of these cases, pcbSize is set to the minimum size required for the pData buffer. Call GetLastError to identify any other errors.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern uint GetRawInputDeviceInfo(IntPtr hDevice, [MarshalAs(UnmanagedType.U4)] RAWINPUTDEVICEINFOFLAG uiCommand, [In, Out] IntPtr pData, [In, Out] ref uint pcbSize);

    /// <summary>
    /// Retrieves information about the raw input device.
    /// </summary>
    /// <param name="hDevice">A handle to the raw input device. This comes from the hDevice member of RAWINPUTHEADER or from GetRawInputDeviceList.</param>
    /// <param name="uiCommand">Specifies what data will be returned in pData.</param>
    /// <param name="pData">A pointer to a buffer that contains the information specified by uiCommand. If uiCommand is RIDI_DEVICEINFO, set the cbSize member of RID_DEVICE_INFO to sizeof(RID_DEVICE_INFO) before calling GetRawInputDeviceInfo.</param>
    /// <param name="pcbSize">The size, in bytes, of the data in pData.</param>
    /// <returns>If successful, this function returns a non-negative number indicating the number of bytes copied to pData. If pData is not large enough for the data, the function returns -1. If pData is NULL, the function returns a value of zero.In both of these cases, pcbSize is set to the minimum size required for the pData buffer. Call GetLastError to identify any other errors.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int GetRawInputDeviceInfo(IntPtr hDevice, [MarshalAs(UnmanagedType.U4)] RAWINPUTDEVICEINFOFLAG uiCommand, [In, Out] RawInputDeviceInfo pData, [In, Out] ref int pcbSize);

    /// <summary>
    /// Enumerates the raw input devices attached to the system.
    /// </summary>
    /// <param name="pRawInputDeviceList">An array of RAWINPUTDEVICELIST structures for the devices attached to the system. If NULL, the number of devices are returned in *puiNumDevices.</param>
    /// <param name="puiNumDevices">If pRawInputDeviceList is NULL, the function populates this variable with the number of devices attached to the system; otherwise, this variable specifies the number of RAWINPUTDEVICELIST structures that can be contained in the buffer to which pRawInputDeviceList points. If this value is less than the number of devices attached to the system, the function returns the actual number of devices in this variable and fails with ERROR_INSUFFICIENT_BUFFER. If this value is greater than or equal to the number of devices attached to the system, then the value is unchanged, and the number of devices is reported as the return value.</param>
    /// <param name="cbSize">The size of a RAWINPUTDEVICELIST structure, in bytes.</param>
    /// <returns>If the function is successful, the return value is the number of devices stored in the buffer pointed to by pRawInputDeviceList. On any other error, the function returns (UINT)-1 and GetLastError returns the error indication.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int GetRawInputDeviceList([In, Out] RAWINPUTDEVICELIST[] pRawInputDeviceList, [In, Out] ref int puiNumDevices, int cbSize);

    /// <summary>
    /// Retrieves the current color of the specified display element. Display elements are the parts of a window and the display that appear on the system display screen. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsyscolor
    /// </summary>
    /// <param name="nIndex">The display element whose color is to be retrieved.</param>
    /// <returns>The function returns the red, green, blue (RGB) color value of the given element.
    /// If the nIndex parameter is out of range, the return value is zero.Because zero is also a valid RGB value, you cannot use GetSysColor to determine whether a system color is supported by the current platform.Instead, use the GetSysColorBrush function, which returns NULL if the color is not supported.</returns>
    [LibraryImport(Library)]
    public static partial uint GetSysColor(SYSCOLORINDEX nIndex);

    /// <summary>
    /// The GetSysColorBrush function retrieves a handle identifying a logical brush that corresponds to the specified color index. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsyscolorbrush
    /// </summary>
    /// <param name="nIndex">A color index. This value corresponds to the color used to paint one of the window elements. See GetSysColor for system color index values.</param>
    /// <returns>The return value identifies a logical brush if the nIndex parameter is supported by the current platform. Otherwise, it returns NULL.</returns>
    [LibraryImport(Library)]
    public static partial uint GetSysColorBrush(uint nIndex);

    /// <summary>
    /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory. 
    /// Note: To write code that is compatible with both 32-bit and 64-bit versions of Windows, use GetWindowLongPtr (instead of GetWindowLong). When compiling for 32-bit Windows, GetWindowLongPtr is defined as a call to the GetWindowLong function.
    /// </summary>
    /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="nIndex">The zero-based offset to the value to be retrieved.</param>
    /// <returns>If the function succeeds, the return value is the requested value. If the function fails, the return value is zero.To get extended error information, call GetLastError. If SetWindowLong or SetWindowLongPtr has not been called previously, GetWindowLongPtr returns zero for values in the extra window or class memory.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial UIntPtr GetWindowLongPtr(IntPtr hWnd, GWL nIndex);

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="lpRect">A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    /// <summary>
    /// Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another application.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control containing the text.</param>
    /// <param name="lpString">The buffer that will receive the text. If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
    /// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character. If the text exceeds this limit, it is truncated.</param>
    /// <returns>If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window or control handle is invalid, the return value is zero. To get extended error information, call GetLastError. This function cannot retrieve the text of an edit control in another application.</returns>
    [DllImport(Library, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    /// <summary>
    /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). If the specified window is a control, the function retrieves the length of the text within the control. However, GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control.</param>
    /// <returns>If the function succeeds, the return value is the length, in characters, of the text. Under certain conditions, this value might be greater than the length of the text (see Remarks). If the window has no text, the return value is zero. Function failure is indicated by a return value of zero and a GetLastError result that is nonzero.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial int GetWindowTextLength(IntPtr hWnd);

    /// <summary>
    /// Determines the visibility state of the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window to be tested.</param>
    /// <returns>If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style, the return value is nonzero. Otherwise, the return value is zero. Because the return value specifies whether the window has the WS_VISIBLE style, it may be nonzero even if the window is totally obscured by other windows.</returns>
    [DllImport(Library)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    /// <summary>
    /// Destroys the specified timer.
    /// </summary>
    /// <param name="hWnd">A handle to the window associated with the specified timer. This value must be the same as the hWnd value passed to the SetTimer function that created the timer.</param>
    /// <param name="uIDEvent">The timer to be destroyed. If the window handle passed to SetTimer is valid, this parameter must be the same as the nIDEvent value passed to SetTimer.If the application calls SetTimer with hWnd set to NULL, this parameter must be the timer identifier returned by SetTimer.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool KillTimer(IntPtr hWnd, UIntPtr uIDEvent);

    /// <summary>
    /// Creates a cursor based on data contained in a file.
    /// </summary>
    /// <param name="lpFileName">The source of the file data to be used to create the cursor. The data in the file must be in either .CUR or .ANI format. If the high-order word of lpFileName is nonzero, it is a pointer to a string that is a fully qualified name of a file containing cursor data.</param>
    /// <returns>If the function is successful, the return value is a handle to the new cursor. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr LoadCursorFromFile(string lpFileName);

    /// <summary>
    /// Loads an icon, cursor, animated cursor, or bitmap.
    /// </summary>
    /// <param name="hInst">A handle to the module of either a DLL or executable (.exe) that contains the image to be loaded. For more information, see GetModuleHandle. Note that as of 32-bit Windows, an instance handle (HINSTANCE), such as the application instance handle exposed by system function call of WinMain, and a module handle (HMODULE) are the same thing.
    /// To load an OEM image, set this parameter to NULL.
    /// To load a stand-alone resource (icon, cursor, or bitmap file) — for example, c:\myimage.bmp — set this parameter to NULL.</param>
    /// <param name="name"></param>
    /// <param name="type">The type of image to be loaded.</param>
    /// <param name="cx">The width, in pixels, of the icon or cursor. If this parameter is zero and the fuLoad parameter is LR_DEFAULTSIZE, the function uses the SM_CXICON or SM_CXCURSOR system metric value to set the width. If this parameter is zero and LR_DEFAULTSIZE is not used, the function uses the actual resource width.</param>
    /// <param name="cy">The height, in pixels, of the icon or cursor. If this parameter is zero and the fuLoad parameter is LR_DEFAULTSIZE, the function uses the SM_CYICON or SM_CYCURSOR system metric value to set the height. If this parameter is zero and LR_DEFAULTSIZE is not used, the function uses the actual resource height.</param>
    /// <param name="fuLoad"></param>
    /// <returns>If the function succeeds, the return value is the handle of the newly loaded image. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial IntPtr LoadImage(IntPtr hInst, ushort name, IMAGE_TYPE type, int cx, int cy, IMAGE_FLAG fuLoad);

    /// <summary>
    /// Loads a standard windows icon.
    /// </summary>
    /// <param name="icon">The icon to use</param>
    /// <returns>If the function succeeds, the return value is the handle of the newly loaded image. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    public static IntPtr LoadImage(PredefinedIcons icon) => LoadImage(IntPtr.Zero, (ushort)icon, IMAGE_TYPE.IMAGE_ICON, 0, 0, IMAGE_FLAG.LR_SHARED);

    /// <summary>
    /// Loads a standard windows cursor.
    /// </summary>
    /// <param name="cursor">The cursor to use</param>
    /// <returns>If the function succeeds, the return value is the handle of the newly loaded image. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    public static IntPtr LoadImage(PredefinedCursors cursor) => LoadImage(IntPtr.Zero, (ushort)cursor, IMAGE_TYPE.IMAGE_CURSOR, 0, 0, IMAGE_FLAG.LR_SHARED);

    /// <summary>
    /// Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code.
    /// To specify a handle to the keyboard layout to use for translating the specified code, use the MapVirtualKeyEx function.
    /// </summary>
    /// <param name="uCode">The virtual key code or scan code for a key. How this value is interpreted depends on the value of the uMapType parameter.
    /// Starting with Windows Vista, the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan code.</param>
    /// <param name="uMapType">The translation to be performed.</param>
    /// <returns>The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode and uMapType. If there is no translation, the return value is zero.</returns>
    [LibraryImport(Library)]
    public static partial uint MapVirtualKey(VIRTUALKEYCODE uCode, VIRTUALKEYMAPTYPE uMapType);

    /// <summary>
    /// The MonitorFromWindow function retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window of interest.</param>
    /// <param name="dwFlags">Determines the function's return value if the window does not intersect any display monitor.</param>
    /// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window. If the window does not intersect a display monitor, the return value depends on the value of dwFlags.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr MonitorFromWindow(IntPtr hwnd, MONITORFROMWINDOWFLAGS dwFlags);

    /// <summary>
    /// Changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="X">The new position of the left side of the window.</param>
    /// <param name="Y">The new position of the top of the window.</param>
    /// <param name="nWidth">The new width of the window.</param>
    /// <param name="nHeight">The new height of the window.</param>
    /// <param name="bRepaint">Indicates whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    /// <summary>
    /// Dispatches incoming sent messages, checks the thread message queue for a posted message, and retrieves the message (if any exist).
    /// </summary>
    /// <param name="lpMsg">A pointer to an MSG structure that receives message information.</param>
    /// <param name="hWnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread.
    /// If hWnd is NULL, PeekMessage retrieves messages for any window that belongs to the current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL, both window messages and thread messages are processed.
    /// If hWnd is -1, PeekMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL, that is, thread messages as posted by PostMessage (when the hWnd parameter is NULL) or PostThreadMessage.</param>
    /// <param name="wMsgFilterMin">The value of the first message in the range of messages to be examined. Use WM_KEYFIRST (0x0100) to specify the first keyboard message or WM_MOUSEFIRST (0x0200) to specify the first mouse message.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, PeekMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <param name="wMsgFilterMax">The value of the last message in the range of messages to be examined. Use WM_KEYLAST to specify the last keyboard message or WM_MOUSELAST to specify the last mouse message.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, PeekMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <param name="wRemoveMsg">Specifies how messages are to be handled.</param>
    /// <returns>If a message is available, the return value is nonzero. If no messages are available, the return value is zero.</returns>
    [LibraryImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool PeekMessage(ref MSG lpMsg, IntPtr hWnd, WINDOWMESSAGE wMsgFilterMin, WINDOWMESSAGE wMsgFilterMax, PEEKMESSAGEFLAGS wRemoveMsg);

    /// <summary>
    /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
    /// To post a message in the message queue associated with a thread, use the PostThreadMessage function.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose window procedure is to receive the message.</param>
    /// <param name="Msg">The message to be posted. For lists of the system-provided messages, see System-Defined Messages.
    /// https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues </param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.</returns>
    [LibraryImport(Library, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool PostMessage(IntPtr hWnd, WINDOWMESSAGE Msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Indicates to the system that a thread has made a request to terminate (quit). It is typically used in response to a WM_DESTROY message.
    /// </summary>
    /// <param name="nExitCode">The application exit code. This value is used as the wParam parameter of the WM_QUIT message.</param>
    [LibraryImport(Library)]
    public static partial void PostQuitMessage(int nExitCode);

    /// <summary>
    /// Registers a window class for subsequent use in calls to the CreateWindow or CreateWindowEx function.
    /// </summary>
    /// <param name="unnamedParam1">A pointer to a WNDCLASSEX structure. You must fill the structure with the appropriate class attributes before passing it to the function.</param>
    /// <returns>If the function succeeds, the return value is a class atom that uniquely identifies the class being registered. This atom can only be used by the CreateWindow, CreateWindowEx, GetClassInfo, GetClassInfoEx, FindWindow, FindWindowEx, and UnregisterClass functions and the IActiveIMMap::FilterClientWindows method.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern ushort RegisterClassEx(ref WNDCLASSEX unnamedParam1);

    /// <summary>
    /// Registers the device or type of device for which a window will receive notifications.
    /// </summary>
    /// <param name="hRecipient">A handle to the window or service that will receive device events for the devices specified in the NotificationFilter parameter. The same window handle can be used in multiple calls to RegisterDeviceNotification.
    /// Services can specify either a window handle or service status handle.</param>
    /// <param name="NotificationFilter">A pointer to a block of data that specifies the type of device for which notifications should be sent. This block always begins with the DEV_BROADCAST_HDR structure. The data following this header is dependent on the value of the dbch_devicetype member, which can be DBT_DEVTYP_DEVICEINTERFACE or DBT_DEVTYP_HANDLE. For more information, see Remarks.</param>
    /// <param name="Flags">This parameter can be one of the following values:
    /// DEVICE_NOTIFY_WINDOW_HANDLE: The hRecipient parameter is a window handle.
    /// DEVICE_NOTIFY_SERVICE_HANDLE: The hRecipient parameter is a service status handle.
    /// DEVICE_NOTIFY_ALL_INTERFACE_CLASSES: Notifies the recipient of device interface events for all device interface classes. (The dbcc_classguid member is ignored.) This value can be used only if the dbch_devicetype member is DBT_DEVTYP_DEVICEINTERFACE.
    /// </param>
    /// <returns>If the function succeeds, the return value is a device notification handle. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</remarks>
    [LibraryImport(Library)]
    public static partial IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr NotificationFilter, DEVICENOTIFYFLAGS Flags);

    /// <summary>
    /// Registers the devices that supply the raw input data.
    /// </summary>
    /// <param name="pRawInputDevices">An array of RAWINPUTDEVICE structures that represent the devices that supply the raw input.</param>
    /// <param name="uiNumDevices">The number of RAWINPUTDEVICE structures pointed to by pRawInputDevices.</param>
    /// <param name="cbSize">The size, in bytes, of a RAWINPUTDEVICE structure.</param>
    /// <returns>TRUE if the function succeeds; otherwise, FALSE. If the function fails, call GetLastError for more information.</returns>
    /// <remarks>To receive WM_INPUT messages, an application must first register the raw input devices using RegisterRawInputDevices. By default, an application does not receive raw input.
    /// To receive WM_INPUT_DEVICE_CHANGE messages, an application must specify the RIDEV_DEVNOTIFY flag for each device class that is specified by the usUsagePage and usUsage fields of the RAWINPUTDEVICE structure.By default, an application does not receive WM_INPUT_DEVICE_CHANGE notifications for raw input device arrival and removal.
    /// If a RAWINPUTDEVICE structure has the RIDEV_REMOVE flag set and the hwndTarget parameter is not set to NULL, then parameter validation will fail.
    /// Only one window per raw input device class may be registered to receive raw input within a process(the window passed in the last call to RegisterRawInputDevices). Because of this, RegisterRawInputDevices should not be used from a library, as it may interfere with any raw input processing logic already present in applications that load it</remarks>
    [LibraryImport(Library, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, int uiNumDevices, int cbSize);

    /// <summary>
    /// Releases the mouse capture from a window in the current thread and restores normal mouse input processing. A window that has captured the mouse receives all mouse input, regardless of the position of the cursor, except when a mouse button is clicked while the cursor hot spot is in the window of another thread.
    /// </summary>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool ReleaseCapture();

    /// <summary>
    /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications. The effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs. It has no effect on class or private DCs.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
    /// <param name="hDC">A handle to the DC to be released.</param>
    /// <returns>The return value indicates whether the DC was released. If the DC was released, the return value is 1. If the DC was not released, the return value is zero.</returns>
    [LibraryImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    /// <summary>
    /// Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message. To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function.To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows. Message sending is subject to UIPI.The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.</param>
    /// <param name="Msg">The message to be sent.</param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr SendMessage(IntPtr hWnd, WINDOWMESSAGE Msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Sets the mouse capture to the specified window belonging to the current thread. SetCapture captures mouse input either when the mouse is over the capturing window, or when the mouse button was pressed while the mouse was over the capturing window and the button is still down. Only one window at a time can capture the mouse.
    /// If the mouse cursor is over a window created by another thread, the system will dRect mouse input to the specified window only if a mouse button is down.
    /// </summary>
    /// <param name="hWnd">A handle to the window in the current thread that is to capture the mouse.</param>
    /// <returns>The return value is a handle to the window that had previously captured the mouse. If there is no such window, the return value is NULL.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr SetCapture(IntPtr hWnd);

    /// <summary>
    /// Sets the cursor shape.
    /// </summary>
    /// <param name="hCursor">
    /// A handle to the cursor. The cursor must have been created by the 
    /// CreateCursor function or loaded by the LoadCursor or LoadImage 
    /// function. If this parameter is IntPtr.Zero, the cursor is removed 
    /// from the screen.
    /// </param>
    /// <returns>The return value is the handle to the previous cursor, if there was one. If there was no previous cursor, the return value is null.</returns>
    /// <remarks>
    /// The cursor is set only if the new cursor is different from the 
    /// previous cursor; otherwise, the function returns immediately.
    /// 
    /// The cursor is a shared resource. A window should set the cursor 
    /// shape only when the cursor is in its client area or when the window 
    /// is capturing mouse input. In systems without a mouse, the window 
    /// should restore the previous cursor before the cursor leaves the 
    /// client area or before it relinquishes control to another window.
    /// 
    /// If your application must set the cursor while it is in a window, 
    /// make sure the class cursor for the specified window's class is set 
    /// to NULL. If the class cursor is not NULL, the system restores the 
    /// class cursor each time the mouse is moved.
    /// 
    /// The cursor is not shown on the screen if the internal cursor 
    /// display count is less than zero. This occurs if the application 
    /// uses the ShowCursor function to hide the cursor more times than to 
    /// show the cursor.
    /// </remarks>
    [LibraryImport(Library)]
    public static partial IntPtr SetCursor(IntPtr hCursor);

    /// <summary>
    /// Moves the cursor to the specified screen coordinates. If the new coordinates are not within the screen rectangle set by the most recent ClipCursor function call, the system automatically adjusts the coordinates so that the cursor stays within the rectangle.
    /// </summary>
    /// <param name="X">The new x-coordinate of the cursor, in screen coordinates.</param>
    /// <param name="Y">The new y-coordinate of the cursor, in screen coordinates.</param>
    /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetCursorPos(int X, int Y);

    /// <summary>
    /// Sets the keyboard focus to the specified window. The window must be attached to the calling thread's message queue.
    /// </summary>
    /// <param name="hWnd">A handle to the window that will receive the keyboard input. If this parameter is NULL, keystrokes are ignored.</param>
    /// <returns>If the function succeeds, the return value is the handle to the window that previously had the keyboard focus. If the hWnd parameter is invalid or the window is not attached to the calling thread's message queue, the return value is NULL. To get extended error information, call GetLastError function. Extended error ERROR_INVALID_PARAMETER(0x57) means that window is in disabled state.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial IntPtr SetFocus(IntPtr hWnd);

    /// <summary>
    /// Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
    /// </summary>
    /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
    /// <returns>If the window was brought to the foreground, the return value is nonzero. If the window was not brought to the foreground, the return value is zero.</returns>
    /// <remarks>The system restricts which processes can set the foreground window. An application cannot force a window to the foreground while the user is working with another window. Instead, Windows flashes the taskbar button of the window to notify the user. A process can set the foreground window only if one of the following conditions is true:
    /// <para>The process is the foreground process.</para>
    /// <para>The process was started by the foreground process.</para>
    /// <para>The process received the last input event.</para>
    /// <para>There is no foreground process.</para>
    /// <para>The process is being debugged.</para>
    /// <para>The foreground process is not a Modern Application or the Start Screen.</para>
    /// <para>The foreground is not locked (see LockSetForegroundWindow).</para>
    /// <para>The foreground lock time-out has expired(see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</para>
    /// <para>No menus are active.</para></remarks>
    [DllImport(Library)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    /// <summary>
    /// Changes the parent window of the specified child window.
    /// </summary>
    /// <param name="child">A handle to the child window.</param>
    /// <param name="newParent">A handle to the new parent window. If this parameter is NULL, the desktop window becomes the new parent window. If this parameter is HWND_MESSAGE, the child window becomes a message-only window.</param>
    /// <returns>If the function succeeds, the return value is a handle to the previous parent window. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    /// <summary>
    /// Sets the process-default DPI awareness to system-DPI awareness. This is equivalent to calling SetProcessDpiAwarenessContext with a DPI_AWARENESS_CONTEXT value of DPI_AWARENESS_CONTEXT_SYSTEM_AWARE.
    /// </summary>
    /// <returns>If the function succeeds, the return value is nonzero. Otherwise, the return value is zero.</returns>
    [DllImport(Library)]
    public static extern bool SetProcessDPIAware();

    /// <summary>
    /// It is recommended that you set the process-default DPI awareness via application manifest. See Setting the default DPI awareness for a process for more information. Setting the process-default DPI awareness via API call can lead to unexpected application behavior. Sets the current process to a specified dots per inch(dpi) awareness context. The DPI awareness contexts are from the DPI_AWARENESS_CONTEXT value.
    /// </summary>
    /// <param name="value">A DPI_AWARENESS_CONTEXT handle to set.</param>
    /// <returns>This function returns TRUE if the operation was successful, and FALSE otherwise. To get extended error information, call GetLastError. Possible errors are ERROR_INVALID_PARAMETER for an invalid input, and ERROR_ACCESS_DENIED if the default API awareness mode for the process has already been set(via a previous API call or within the application manifest).</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetProcessDpiAwarenessContext(IntPtr value);

    /// <summary>
    /// It is recommended that you set the process-default DPI awareness via application manifest. See Setting the default DPI awareness for a process for more information. Setting the process-default DPI awareness via API call can lead to unexpected application behavior. Sets the current process to a specified dots per inch(dpi) awareness context. The DPI awareness contexts are from the DPI_AWARENESS_CONTEXT value.
    /// </summary>
    /// <param name="value">A DPI_AWARENESS_CONTEXT handle to set.</param>
    /// <returns>This function returns TRUE if the operation was successful, and FALSE otherwise. To get extended error information, call GetLastError. Possible errors are ERROR_INVALID_PARAMETER for an invalid input, and ERROR_ACCESS_DENIED if the default API awareness mode for the process has already been set(via a previous API call or within the application manifest).</returns>
    public static bool SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT value) => SetProcessDpiAwarenessContext(new IntPtr((int)value));

    /// <summary>
    /// Set the DPI awareness for the current thread to the provided value.
    /// </summary>
    /// <param name="dpiContext">The new DPI_AWARENESS_CONTEXT for the current thread. This context includes the DPI_AWARENESS value.</param>
    /// <returns>The old DPI_AWARENESS_CONTEXT for the thread. If the dpiContext is invalid, the thread will not be updated and the return value will be NULL. You can use this value to restore the old DPI_AWARENESS_CONTEXT after overriding it with a predefined value.</returns>
    [LibraryImport(Library)]
    public static partial IntPtr SetThreadDpiAwarenessContext(IntPtr dpiContext);

    /// <summary>
    /// Set the DPI awareness for the current thread to the provided value.
    /// </summary>
    /// <param name="dpiContext">The new DPI_AWARENESS_CONTEXT for the current thread. This context includes the DPI_AWARENESS value.</param>
    /// <returns>The old DPI_AWARENESS_CONTEXT for the thread. If the dpiContext is invalid, the thread will not be updated and the return value will be NULL. You can use this value to restore the old DPI_AWARENESS_CONTEXT after overriding it with a predefined value.</returns>
    public static IntPtr SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT dpiContext) => SetThreadDpiAwarenessContext(new IntPtr((int)dpiContext));

    /// <summary>
    /// Creates a timer with the specified time-out value.
    /// </summary>
    /// <param name="hWnd">A handle to the window to be associated with the timer. This window must be owned by the calling thread. If a NULL value for hWnd is passed in along with an nIDEvent of an existing timer, that timer will be replaced in the same way that an existing non-NULL hWnd timer will be.</param>
    /// <param name="nIDEvent">A nonzero timer identifier. If the hWnd parameter is NULL, and the nIDEvent does not match an existing timer then it is ignored and a new timer ID is generated. If the hWnd parameter is not NULL and the window specified by hWnd already has a timer with the value nIDEvent, then the existing timer is replaced by the new timer. When SetTimer replaces a timer, the timer is reset. Therefore, a message will be sent after the current time-out value elapses, but the previously set time-out value is ignored. If the call is not intended to replace an existing timer, nIDEvent should be 0 if the hWnd is NULL.</param>
    /// <param name="uElapse">The time-out value, in milliseconds. If uElapse is less than USER_TIMER_MINIMUM(0x0000000A), the timeout is set to USER_TIMER_MINIMUM.If uElapse is greater than USER_TIMER_MAXIMUM(0x7FFFFFFF), the timeout is set to USER_TIMER_MAXIMUM.</param>
    /// <param name="lpTimerFunc">A pointer to the function to be notified when the time-out value elapses. For more information about the function, see TimerProc. If lpTimerFunc is NULL, the system posts a WM_TIMER message to the application queue. The hwnd member of the message's MSG structure contains the value of the hWnd parameter.</param>
    /// <returns>If the function succeeds and the hWnd parameter is NULL, the return value is an integer identifying the new timer. An application can pass this value to the KillTimer function to destroy the timer. If the function succeeds and the hWnd parameter is not NULL, then the return value is a nonzero integer.An application can pass the value of the nIDEvent parameter to the KillTimer function to destroy the timer. If the function fails to create a timer, the return value is zero.To get extended error information, call GetLastError.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TIMERPROC lpTimerFunc);

    /// <summary>
    /// Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered according to their appearance on the screen. The topmost window receives the highest rank and is the first window in the Z order.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order.</param>
    /// <param name="X">The new position of the left side of the window, in client coordinates.</param>
    /// <param name="Y">The new position of the top of the window, in client coordinates.</param>
    /// <param name="cx">The new width of the window, in pixels.</param>
    /// <param name="cy">The new height of the window, in pixels.</param>
    /// <param name="uFlags">The window sizing and positioning flags.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SETWINDOWPOSFLAGS uFlags);

    /// <summary>
    /// Changes the text of the specified window's title bar (if it has one). If the specified window is a control, the text of the control is changed. However, SetWindowText cannot change the text of a control in another application.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control whose text is to be changed.</param>
    /// <param name="lpString">The new title or control text.</param>
    /// <returns> If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    /// <remarks>If the target window is owned by the current process, SetWindowText causes a WM_SETTEXT message to be sent to the specified window or control. If the control is a list box control created with the WS_CAPTION style, however, SetWindowText sets the text for the control, not for the list box entries. 
    /// To set the text of a control in another process, send the WM_SETTEXT message directly instead of calling SetWindowText.
    /// The SetWindowText function does not expand tab characters (ASCII code 0x09). Tab characters are displayed as vertical bar(|) characters.</remarks>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool SetWindowText(IntPtr hWnd, string lpString);

    /// <summary>
    /// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory. 
    /// Note: To write code that is compatible with both 32-bit and 64-bit versions of Windows, use SetWindowLongPtr. When compiling for 32-bit Windows, SetWindowLongPtr is defined as a call to the SetWindowLong function.
    /// </summary>
    /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs. The SetWindowLongPtr function fails if the process that owns the window specified by the hWnd parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.</param>
    /// <param name="nIndex">The zero-based offset to the value to be set.</param>
    /// <param name="dwNewLong">The replacement value.</param>
    /// <returns>If the function succeeds, the return value is the previous value of the specified offset. If the function fails, the return value is zero.To get extended error information, call GetLastError. If the previous value is zero and the function succeeds, the return value is zero, but the function does not clear the last error information. To determine success or failure, clear the last error information by calling SetLastError with 0, then call SetWindowLongPtr.Function failure will be indicated by a return value of zero and a GetLastError result that is nonzero.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial IntPtr SetWindowLongPtr(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong);

    /// <summary>
    /// Displays or hides the cursor.
    /// </summary>
    /// <param name="show">If bShow is TRUE, the display count is incremented by one. If bShow is FALSE, the display count is decremented by one.</param>
    /// <returns>The return value specifies the new display counter.</returns>
    /// <remarks>This function sets an internal display counter that determines whether the cursor should be displayed. The cursor is displayed only if the display count is greater than or equal to 0. If a mouse is installed, the initial display count is 0. If no mouse is installed, the display count is –1.</remarks>
    [DllImport(Library)]
    public static extern int ShowCursor(bool show);

    /// <summary>
    /// Sets the specified window's show state.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="nCmdShow">Controls how the window is to be shown. This parameter is ignored the first time an application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure. Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter.</param>
    /// <returns>If the window was previously visible, the return value is nonzero. If the window was previously hidden, the return value is zero.</returns>
    [DllImport(Library)]
    public static extern bool ShowWindow(IntPtr hWnd, SHOWWINDOWCOMMAND nCmdShow);

    /// <summary>
    /// Translates virtual-key messages into character messages. The character messages are posted to the calling thread's message queue, to be read the next time the thread calls the GetMessage or PeekMessage function.
    /// </summary>
    /// <param name="lpMsg">A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue by using the GetMessage or PeekMessage function.</param>
    /// <returns>If the message is translated (that is, a character message is posted to the thread's message queue), the return value is nonzero.
    /// If the message is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP, the return value is nonzero, regardless of the translation.
    /// If the message is not translated (that is, a character message is not posted to the thread's message queue), the return value is zero.</returns>
    [LibraryImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool TranslateMessage(ref MSG lpMsg);

    /// <summary>
    /// Posts messages when the mouse pointer leaves a window or hovers over a window for a specified amount of time.
    /// </summary>
    /// <param name="lpEventTrack">A pointer to a TRACKMOUSEEVENT structure that contains tracking information.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

    /// <summary>
    /// Unregisters a window class, freeing the memory required for the class.
    /// </summary>
    /// <param name="className">A null-terminated string or a class atom. If lpClassName is a string, it specifies the window class name. This class name must have been registered by a previous call to the RegisterClass or RegisterClassEx function. System classes, such as dialog box controls, cannot be unregistered. If this parameter is an atom, it must be a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero.</param>
    /// <param name="instance">A handle to the instance of the module that created the class.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the class could not be found or if a window still exists that was created with the class, the return value is zero.To get extended error information, call GetLastError.</returns>
    [LibraryImport(Library, SetLastError = true)]
    public static partial short UnregisterClass(IntPtr className, IntPtr instance);

    /// <summary>
    /// Closes the specified device notification handle.
    /// </summary>
    /// <param name="handle">Device notification handle returned by the RegisterDeviceNotification function.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool UnregisterDeviceNotification(IntPtr handle);

    /// <summary>
    /// The UpdateWindow function updates the client area of the specified window by sending a WM_PAINT message to the window if the window's update region is not empty. The function sends a WM_PAINT message directly to the window procedure of the specified window, bypassing the application queue. If the update region is empty, no message is sent.
    /// </summary>
    /// <param name="hWnd">Handle to the window to be updated.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
    [LibraryImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UpdateWindow(IntPtr hWnd);
}

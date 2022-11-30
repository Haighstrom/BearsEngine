using System.Runtime.InteropServices;
using static BearsEngine.Win32API.PIXELFORMATDESCRIPTOR;

namespace BearsEngine.Win32API;

internal enum User32Cursors : ushort
{
    StandardArrow = 100,
    IBeam = 101,
    Hourglass = 102,
    CrossHair = 103,
    UpArrow = 104,
    ArrowUpLeftAndDownRight = 105,
    ArrowUpRightAndDownLeft = 106,
    ArrowLeftAndRight = 107,
    ArrowUpAndDown = 108,
    ArrowAll = 109,
    NotAllowed = 110,
    ArrowAndHourglass = 111,
    ArrowAndQuestionMark = 112,
    Pen = 113,
    Hand = 114,
    Frame = 115,
    CD = 116,
    Location = 117,
    User = 118,
    ScrollingUpAndDown = 32652,
    ScrollingLeftAndRight = 32653,
    ScrollingAll = 32654,
    ScrollingUp = 32655,
    ScrollingDown = 32656,
    ScrollingLeft = 32657,
    ScrollingRight = 32658,
    ScrollingUpLeft = 32659,
    ScrollingUpRight = 32660,
    ScrollingDownLeft = 32661,
    ScrollingDownRight = 32662,
    SmallStar = 32664,
    SmallStarLeftQuestionMark = 32667,
    SmallStarRightQuestionMark = 32668,
    Dot = 32670,
}

internal enum User32Icons : ushort
{
    APPLICATION = 100,
    EXCLAMATION = 101,
    QUESTION = 102,
    ERROR = 103,
    INFORMATION = 104,
    SHIELD = 106,
}

internal enum GLAlphaFuncEnum : int
{
    /// <summary>
    /// Never passes.
    /// </summary>
    GL_NEVER = 0x0200,
    /// <summary>
    /// Passes if the incoming alpha value is less than the reference value.
    /// </summary>
    GL_LESS = 0x0201,
    /// <summary>
    /// Passes if the incoming alpha value is equal to the reference value.
    /// </summary>
    GL_EQUAL = 0x0202,
    /// <summary>
    /// Passes if the incoming alpha value is less than or equal to the reference value.
    /// </summary>
    GL_LEQUAL = 0x0203,
    /// <summary>
    /// Passes if the incoming alpha value is greater than the reference value.
    /// </summary>
    GL_GREATER = 0x0204,
    /// <summary>
    /// Passes if the incoming alpha value is not equal to the reference value.
    /// </summary>
    GL_NOTEQUAL = 0x0205,
    /// <summary>
    /// Passes if the incoming alpha value is greater than or equal to the reference value.
    /// </summary>
    GL_GEQUAL = 0x0206,
    /// <summary>
    /// Always passes (initial value).
    /// </summary>
    GL_ALWAYS = 0x0207,
}

/// <summary>
/// https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBlendFunc.xhtml
/// Specifies how the red, green, blue, and alpha source and destination blending factors are computed
/// For use with OpenGL32 glBlendFunc, glBlendFunci, glBlendFuncSeparate, glBlendFuncSeparatei
/// </summary>
public enum BlendScaleFactor : int
{
    GL_ZERO = 0,
    GL_ONE = 1,
    GL_SRC_COLOR = 0x0300,
    GL_ONE_MINUS_SRC_COLOR = 0x0301,
    GL_DST_COLOR = 0x0306,
    GL_ONE_MINUS_DST_COLOR = 0x0307,
    GL_SRC_ALPHA = 0x0302,
    GL_ONE_MINUS_SRC_ALPHA = 0x0303,
    GL_DST_ALPHA = 0x0304,
    GL_ONE_MINUS_DST_ALPHA = 0x0305,
    GL_CONSTANT_COLOR = 0x8001,
    GL_ONE_MINUS_CONSTANT_COLOR = 0x8002,
    GL_CONSTANT_ALPHA = 0x8003,
    GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004,
    GL_SRC_ALPHA_SATURATE = 0x0308,
    GL_SRC1_COLOR = 0x88F9,
    GL_ONE_MINUS_SRC1_COLOR = 0x88FA,
    GL_SRC1_ALPHA = 0x8589,
    GL_ONE_MINUS_SRC1_ALPHA = 0x88FB,
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/dbt/ns-dbt-dev_broadcast_hdr
/// For use with RegisterDeviceNotification
/// </summary>
internal enum DEV_BROADCAST_HDR_dbch_devicetype
{
    /// <summary>
    /// Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.
    /// </summary>
    DBT_DEVTYP_DEVICEINTERFACE = 0x00000005,
    /// <summary>
    /// File system handle. This structure is a DEV_BROADCAST_HANDLE structure.
    /// </summary>
    DBT_DEVTYP_HANDLE = 0x00000006,
    /// <summary>
    /// OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.
    /// </summary>
    DBT_DEVTYP_OEM = 0x00000000,
    /// <summary>
    /// Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.
    /// </summary>
    DBT_DEVTYP_PORT = 0x00000003,
    /// <summary>
    /// Logical volume. This structure is a DEV_BROADCAST_VOLUME structure.
    /// </summary>
    DBT_DEVTYP_VOLUME = 0x00000002,
}

//https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
internal enum DWMWINDOWATTRIBUTE : uint
{
    /// <summary>
    /// Use with DwmGetWindowAttribute. Discovers whether non-client rendering is enabled. The retrieved value is of type BOOL. TRUE if non-client rendering is enabled; otherwise, FALSE.
    /// </summary>
    DWMWA_NCRENDERING_ENABLED = 1,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Sets the non-client rendering policy. The pvAttribute parameter points to a value from the DWMNCRENDERINGPOLICY enumeration.
    /// </summary>
    DWMWA_NCRENDERING_POLICY,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Enables or forcibly disables DWM transitions. The pvAttribute parameter points to a value of type BOOL. TRUE to disable transitions, or FALSE to enable transitions.
    /// </summary>
    DWMWA_TRANSITIONS_FORCEDISABLED,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Enables content rendered in the non-client area to be visible on the frame drawn by DWM. The pvAttribute parameter points to a value of type BOOL. TRUE to enable content rendered in the non-client area to be visible on the frame; otherwise, FALSE.
    /// </summary>
    DWMWA_ALLOW_NCPAINT,
    /// <summary>
    /// Use with DwmGetWindowAttribute. Retrieves the bounds of the caption button area in the window-relative space. The retrieved value is of type RECT. If the window is minimized or otherwise not visible to the user, then the value of the RECT retrieved is undefined. You should check whether the retrieved RECT contains a boundary that you can work with, and if it doesn't then you can conclude that the window is minimized or otherwise not visible.
    /// </summary>
    DWMWA_CAPTION_BUTTON_BOUNDS,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies whether non-client content is right-to-left (RTL) mirrored. The pvAttribute parameter points to a value of type BOOL. TRUE if the non-client content is right-to-left (RTL) mirrored; otherwise, FALSE.
    /// </summary>
    DWMWA_NONCLIENT_RTL_LAYOUT,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Forces the window to display an iconic thumbnail or peek representation (a static bitmap), even if a live or snapshot representation of the window is available. This value is normally set during a window's creation, and not changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a value of type BOOL. TRUE to require a iconic thumbnail or peek representation; otherwise, FALSE.
    /// </summary>
    DWMWA_FORCE_ICONIC_REPRESENTATION,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Sets how Flip3D treats the window. The pvAttribute parameter points to a value from the DWMFLIP3DWINDOWPOLICY enumeration.
    /// </summary>
    DWMWA_FLIP3D_POLICY,
    /// <summary>
    /// Use with DwmGetWindowAttribute. Retrieves the extended frame bounds rectangle in screen space. The retrieved value is of type RECT.
    /// </summary>
    DWMWA_EXTENDED_FRAME_BOUNDS,
    /// <summary>
    /// Use with DwmSetWindowAttribute. The window will provide a bitmap for use by DWM as an iconic thumbnail or peek representation (a static bitmap) for the window. DWMWA_HAS_ICONIC_BITMAP can be specified with DWMWA_FORCE_ICONIC_REPRESENTATION. DWMWA_HAS_ICONIC_BITMAP normally is set during a window's creation and not changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a value of type BOOL. TRUE to inform DWM that the window will provide an iconic thumbnail or peek representation; otherwise, FALSE.
    /// Windows Vista and earlier: This value is not supported.
    /// </summary>
    DWMWA_HAS_ICONIC_BITMAP,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Do not show peek preview for the window. The peek view shows a full-sized preview of the window when the mouse hovers over the window's thumbnail in the taskbar. If this attribute is set, hovering the mouse pointer over the window's thumbnail dismisses peek (in case another window in the group has a peek preview showing). The pvAttribute parameter points to a value of type BOOL. TRUE to prevent peek functionality, or FALSE to allow it.
    /// Windows Vista and earlier: This value is not supported.
    /// </summary>
    DWMWA_DISALLOW_PEEK,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Prevents a window from fading to a glass sheet when peek is invoked. The pvAttribute parameter points to a value of type BOOL. TRUE to prevent the window from fading during another window's peek, or FALSE for normal behavior.
    /// Windows Vista and earlier: This value is not supported.
    /// </summary>
    DWMWA_EXCLUDED_FROM_PEEK,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Cloaks the window such that it is not visible to the user. The window is still composed by DWM.
    /// Using with DirectComposition: Use the DWMWA_CLOAK flag to cloak the layered child window when animating a representation of the window's content via a DirectComposition visual that has been associated with the layered child window. For more details on this usage case, see How to animate the bitmap of a layered child window.
    /// Windows 7 and earlier: This value is not supported.
    /// </summary>
    DWMWA_CLOAK,
    /// <summary>
    /// Use with DwmGetWindowAttribute. If the window is cloaked, provides one of the following values explaining why.
    /// DWM_CLOAKED_APP (value 0x0000001). The window was cloaked by its owner application.
    /// DWM_CLOAKED_SHELL (value 0x0000002). The window was cloaked by the Shell.
    /// DWM_CLOAKED_INHERITED (value 0x0000004). The cloak value was inherited from its owner window.
    /// Windows 7 and earlier: This value is not supported.
    /// </summary>
    DWMWA_CLOAKED,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Freeze the window's thumbnail image with its current visuals. Do no further live updates on the thumbnail image to match the window's contents.
    /// Windows 7 and earlier: This value is not supported.
    /// </summary>
    DWMWA_FREEZE_REPRESENTATION,
    /// <summary>
    /// [Documentation is blank for this value at time of writing]
    /// </summary>
    DWMWA_PASSIVE_UPDATE_MODE,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Enables a non-UWP window to use host backdrop brushes. If this flag is set, then a Win32 app that calls Windows::UI::Composition APIs can build transparency effects using the host backdrop brush (see Compositor.CreateHostBackdropBrush). The pvAttribute parameter points to a value of type BOOL. TRUE to enable host backdrop brushes for the window, or FALSE to disable it.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_USE_HOSTBACKDROPBRUSH,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Allows the window frame for this window to be drawn in dark mode colors when the dark mode system setting is enabled. For compatibility reasons, all windows default to light mode regardless of the system setting. The pvAttribute parameter points to a value of type BOOL. TRUE to honor dark mode for the window, FALSE to always use light mode.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the rounded corner preference for a window. The pvAttribute parameter points to a value of type DWM_WINDOW_CORNER_PREFERENCE.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_WINDOW_CORNER_PREFERENCE = 33,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the color of the window border. The pvAttribute parameter points to a value of type COLORREF. The app is responsible for changing the border color according to state changes, such as a change in window activation.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_BORDER_COLOR,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the color of the caption. The pvAttribute parameter points to a value of type COLORREF.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_CAPTION_COLOR,
    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the color of the caption text. The pvAttribute parameter points to a value of type COLORREF.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_TEXT_COLOR,
    /// <summary>
    /// Use with DwmGetWindowAttribute. Retrieves the width of the outer border that the DWM would draw around this window. The value can vary depending on the DPI of the window. The pvAttribute parameter points to a value of type UINT.
    /// This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
    /// <summary>
    /// IMPORTANT. This value is available in pre-release versions of the Windows Insider Preview.
    /// Use with DwmGetWindowAttribute or DwmSetWindowAttribute. Retrieves or specifies the system-drawn backdrop material of a window, including behind the non-client area. The pvAttribute parameter points to a value of type DWM_SYSTEMBACKDROP_TYPE.
    /// This value is supported starting with Windows 11 Build 22621.
    /// </summary>
    DWMWA_SYSTEMBACKDROP_TYPE,
    /// <summary>
    /// The maximum recognized DWMWINDOWATTRIBUTE value, used for validation purposes.
    /// </summary>
    DWMWA_LAST
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadimagew
/// The type of image to be loaded. For use in User32 LoadImage.
/// </summary>
internal enum LoadImage_Type : uint
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
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadimagew
/// For use in User32 LoadImage
/// </summary>
internal enum LoadImage_FULoad : uint
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

internal enum PROCESS_DPI_AWARENESS
{
    Process_DPI_Unaware = 0,
    Process_System_DPI_Aware = 1,
    Process_Per_Monitor_DPI_Aware = 2
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/window-class-styles
/// The class styles define additional elements of the window class. Two or more styles can be combined by using the bitwise OR (|) operator. To assign a style to a window class, assign the style to the style member of the WNDCLASSEX structure.
/// </summary>
[Flags]
internal enum WindowClassStyle : uint
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
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsyscolor
/// For use with GetSysColor
/// </summary>
internal enum GetSysColor_nIndex : int
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
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
/// Defines information for the raw input devices. For use with User32 RegisterRawInputDevices.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RAWINPUTDEVICE
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/top-level-collections
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-page
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-architecture#hid-clients-supported-in-windows
    /// Top level collection Usage page for the raw input device. See HID Clients Supported in Windows for details on possible values.
    /// </summary>
    internal RAWINPUTDEVICEUSAGEPAGE usUsagePage;
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/top-level-collections
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-id
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/hid/hid-architecture#hid-clients-supported-in-windows
    /// Top level collection Usage ID for the raw input device. See HID Clients Supported in Windows for details on possible values.
    /// </summary>
    internal RAWINPUTDEVICE_usUsage usUsage;
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
    internal RAWINPUTDEVICEFLAGS dwFlags;
    /// <summary>
    /// Handle to the target window. If NULL it follows the keyboard focus.
    /// </summary>
    internal IntPtr hwndTarget;
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
/// Mode flag that specifies how to interpret the information provided by usUsagePage and usUsage. For use with User32 RegisterRawInputDevices.
/// </summary>
/// <remarks>If RIDEV_NOLEGACY is set for a mouse or a keyboard, the system does not generate any legacy message for that device for the application. For example, if the mouse TLC is set with RIDEV_NOLEGACY, WM_LBUTTONDOWN and related legacy mouse messages are not generated. Likewise, if the keyboard TLC is set with RIDEV_NOLEGACY, WM_KEYDOWN and related legacy keyboard messages are not generated.
/// If RIDEV_REMOVE is set and the hwndTarget member is not set to NULL, then RegisterRawInputDevices function will fail.</remarks>
[Flags]
internal enum RAWINPUTDEVICEFLAGS : int
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
/// In the context of a usage page, a valid usage identifier, or usage ID, indicates a usage in a usage page. A usage ID of zero is reserved. A usage ID value is an unsigned 16-bit value.
/// </summary>
internal enum RAWINPUTDEVICE_usUsage : ushort
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
internal enum RAWINPUTDEVICEUSAGEPAGE : ushort
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
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rid_device_info
/// Defines the raw input data coming from any device.
/// For use with GetRawInputDeviceInfo.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal class RID_DEVICE_INFO
{
    /// <summary>
    /// The size, in bytes, of the RID_DEVICE_INFO structure.
    /// </summary>
    internal uint cbSize = (uint)Marshal.SizeOf(typeof(RID_DEVICE_INFO));
    /// <summary>
    /// The type of raw input data.
    /// </summary>
    internal RID_DEVICE_INFO_dwType dwType;
    /// <summary>
    /// Container for the relevant device info struct
    /// </summary>
    internal DeviceInfo DUMMYUNIONNAME;

    [StructLayout(LayoutKind.Explicit)]
    internal struct DeviceInfo
    {
        /// <summary>
        /// If dwType is RIM_TYPEMOUSE, this is the RID_DEVICE_INFO_MOUSE structure that defines the mouse.
        /// </summary>
        [FieldOffset(0)]
        internal RID_DEVICE_INFO_MOUSE mouse;
        /// <summary>
        /// If dwType is RIM_TYPEKEYBOARD, this is the RID_DEVICE_INFO_KEYBOARD structure that defines the keyboard.
        /// </summary>
        [FieldOffset(0)]
        internal RID_DEVICE_INFO_KEYBOARD keyboard;
        /// <summary>
        /// If dwType is RIM_TYPEHID, this is the RID_DEVICE_INFO_HID structure that defines the HID device.
        /// </summary>
        [FieldOffset(0)]
        internal RID_DEVICE_INFO_HID hid;
    };
}



/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rid_device_info
/// For use with RID_DEVICE_INFO
/// </summary>
internal enum RID_DEVICE_INFO_dwType : uint
{
    /// <summary>
    /// Data comes from a mouse.
    /// </summary>
    RIM_TYPEMOUSE = 0,
    /// <summary>
    /// Data comes from a keyboard.
    /// </summary>
    RIM_TYPEKEYBOARD = 1,
    /// <summary>
    /// Data comes from an HID that is not a keyboard or a mouse.
    /// </summary>
    RIM_TYPEHID = 2
}

/// <summary>
/// Defines the raw input data coming from the specified Human Interface Device (HID).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RID_DEVICE_INFO_HID
{
    /// <summary>
    /// Vendor ID for the HID.
    /// </summary>
    internal int VendorId;
    /// <summary>
    /// Product ID for the HID.
    /// </summary>
    internal int ProductId;
    /// <summary>
    /// Version number for the HID.
    /// </summary>
    internal int VersionNumber;
    /// <summary>
    /// Top-level collection Usage Page for the device.
    /// </summary>
    //internal UInt16 UsagePage;
    internal short UsagePage;
    /// <summary>
    /// Top-level collection Usage for the device.
    /// </summary>
    //internal UInt16 Usage;
    internal short Usage;
}

/// <summary>
/// Defines the raw input data coming from the specified keyboard.
/// </summary>
/// <remarks>
/// For the keyboard, the Usage Page is 1 and the Usage is 6.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct RID_DEVICE_INFO_KEYBOARD
{
    /// <summary>
    /// Type of the keyboard.
    /// </summary>
    internal int Type;
    /// <summary>
    /// Subtype of the keyboard.
    /// </summary>
    internal int SubType;
    /// <summary>
    /// Scan code mode.
    /// </summary>
    internal int KeyboardMode;
    /// <summary>
    /// Number of function keys on the keyboard.
    /// </summary>
    internal int NumberOfFunctionKeys;
    /// <summary>
    /// Number of LED indicators on the keyboard.
    /// </summary>
    internal int NumberOfIndicators;
    /// <summary>
    /// Total number of keys on the keyboard.
    /// </summary>
    internal int NumberOfKeysTotal;
}

/// <summary>
/// Defines the raw input data coming from the specified mouse.
/// </summary>
/// <remarks>
/// For the keyboard, the Usage Page is 1 and the Usage is 2.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct RID_DEVICE_INFO_MOUSE
{
    /// <summary>
    /// ID for the mouse device.
    /// </summary>
    internal int Id;
    /// <summary>
    /// Number of buttons for the mouse.
    /// </summary>
    internal int NumberOfButtons;
    /// <summary>
    /// Number of data points per second. This information may not be applicable for every mouse device.
    /// </summary>
    internal int SampleRate;
    /// <summary>
    /// TRUE if the mouse has a wheel for horizontal scrolling; otherwise, FALSE.
    /// </summary>
    /// <remarks>
    /// This member is only supported under Microsoft Windows Vista and later versions.
    /// </remarks>
    internal bool HasHorizontalWheel;
}

/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-sizing
/// The edge of the window that is being sized. Provided by wParam within a WM_SIZING message.
/// </summary>
internal enum WM_SIZING_wParam
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
    //9 seems to be generated if the window gets resized by dragging the title bar when maximised
}

#pragma warning disable 0649, 0169, IDE0044, IDE0051
/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerdevicenotificationa
/// https://docs.microsoft.com/en-us/windows/win32/api/dbt/ns-dbt-dev_broadcast_deviceinterface_a
/// https://docs.microsoft.com/en-us/windows/win32/api/dbt/ns-dbt-dev_broadcast_hdr
/// For use with User32 RegisterDeviceNotification
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct DEV_BROADCAST_DEVICEINTERFACE
{
    /// <summary>
    /// The size of this structure, in bytes. This is the size of the members plus the actual length of the dbcc_name string (the null character is accounted for by the declaration of dbcc_name as a one-character array.)
    /// </summary>
    internal int dbch_size;
    /// <summary>
    /// Set to DBT_DEVTYP_DEVICEINTERFACE.
    /// </summary>
    internal DEV_BROADCAST_HDR_dbch_devicetype dbch_devicetype;
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
    /// When this structure is returned to a window through the WM_DEVICECHANGE message, the dbcc_name string is converted to ANSI as appropriate. Services always receive a Unicode string, whether they call RegisterDeviceNotificationW or RegisterDeviceNotificationA.
    /// </summary>
    internal char dbcc_name;
}
#pragma warning restore 0649, 0169, IDE0044, IDE0051

#pragma warning disable 0649, 0169, IDE0044, IDE0051
/// <summary>
/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerdevicenotificationa
/// https://docs.microsoft.com/en-us/windows/win32/api/dbt/ns-dbt-dev_broadcast_hdr
/// For use with User32 RegisterDeviceNotification
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct DEV_BROADCAST_HDR
{
    /// <summary>
    /// The size of this structure, in bytes.
    /// If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the _DEV_BROADCAST_USERDEFINED structure.
    /// </summary>
    internal int dbch_size;
    /// <summary>
    /// The device type, which determines the event-specific information that follows the first three members. This member can be one of the following values.
    /// DBT_DEVTYP_DEVICEINTERFACE: Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.
    /// DBT_DEVTYP_HANDLE: File system handle.This structure is a DEV_BROADCAST_HANDLE structure.
    /// DBT_DEVTYP_OEM: OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.
    /// DBT_DEVTYP_PORT: Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.
    /// DBT_DEVTYP_VOLUME: Logical volume.This structure is a DEV_BROADCAST_VOLUME structure.
    /// </summary>
    private DEV_BROADCAST_HDR_dbch_devicetype dbch_devicetype;
    /// <summary>
    /// Reserved; do not use.
    /// </summary>
    private int dbcc_reserved;
}
#pragma warning restore 0649, 0169, IDE0044, IDE0051

[StructLayout(LayoutKind.Sequential)]
internal struct MINMAXINFO
{
    private POINT ptReserved;
    public POINT ptMaxSize;
    public POINT ptMaxPosition;
    public POINT ptMinTrackSize;
    public POINT ptMaxTrackSize;
}

//https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-windowinfo
[StructLayout(LayoutKind.Sequential)]
struct WINDOWINFO
{
    public uint cbSize;
    public RECT rcWindow;
    public RECT rcClient;
    public uint dwStyle;
    public uint dwExStyle;
    public uint dwWindowStatus;
    public uint cxWindowBorders;
    public uint cyWindowBorders;
    public ushort atomWindowType;
    public ushort wCreatorVersion;

    public WINDOWINFO(object? filler) : this()
    {
        cbSize = (uint)Marshal.SizeOf(typeof(WINDOWINFO));
    }

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
    internal uint cbSize;

    /// <summary>
    /// The class style(s). This member can be any combination of the Class Styles.
    /// </summary>
    internal WindowClassStyle style;

    /// <summary>
    /// A pointer to the window procedure. You must use the CallWindowProc function to call the window procedure. For more information, see WindowProc.
    /// </summary>
    [MarshalAs(UnmanagedType.FunctionPtr)]
    internal WNDPROC lpfnWndProc;

    /// <summary>
    /// The number of extra bytes to allocate following the window-class structure. The system initializes the bytes to zero.
    /// </summary>
    internal int cbClsExtra;

    /// <summary>
    /// The number of extra bytes to allocate following the window instance. The system initializes the bytes to zero. If an application uses WNDCLASSEX to register a dialog box created by using the CLASS directive in the resource file, it must set this member to DLGWINDOWEXTRA.
    /// </summary>
    internal int cbWndExtra;

    /// <summary>
    /// A handle to the instance that contains the window procedure for the class.
    /// </summary>
    internal IntPtr hInstance;

    /// <summary>
    /// A handle to the class icon. This member must be a handle to an icon resource. If this member is NULL, the system provides a default icon.
    /// </summary>
    internal IntPtr hIcon;

    /// <summary>
    /// A handle to the class cursor. This member must be a handle to a cursor resource. If this member is NULL, an application must explicitly set the cursor shape whenever the mouse moves into the application's window.
    /// </summary>
    internal IntPtr hCursor;

    /// <summary>
    /// A handle to the class background brush. This member can be a handle to the brush to be used for painting the background, or it can be a color value. A color value must be one of the following standard system colors (the value 1 must be added to the chosen color). The system automatically deletes class background brushes when the class is unregistered by using UnregisterClass. An application should not delete these brushes. When this member is NULL, an application must paint its own background whenever it is requested to paint in its client area.To determine whether the background must be painted, an application can either process the WM_ERASEBKGND message or test the fErase member of the PAINTSTRUCT structure filled by the BeginPaint function.
    /// </summary>
    internal IntPtr hbrBackground;

    /// <summary>
    /// Pointer to a null-terminated character string that specifies the resource name of the class menu, as the name appears in the resource file. If you use an integer to identify the menu, use the MAKEINTRESOURCE macro. If this member is NULL, windows belonging to this class have no default menu.
    /// </summary>
    internal IntPtr lpszMenuName;

    /// <summary>
    /// A pointer to a null-terminated string or is an atom. If this parameter is an atom, it must be a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpszClassName; the high-order word must be zero.
    /// If lpszClassName is a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
    /// The maximum length for lpszClassName is 256. If lpszClassName is greater than the maximum length, the RegisterClassEx function will fail.
    /// </summary>
    internal IntPtr lpszClassName;

    /// <summary>
    /// A handle to a small icon that is associated with the window class. If this member is NULL, the system searches the icon resource specified by the hIcon member for an icon of the appropriate size to use as the small icon.
    /// </summary>
    internal IntPtr hIconSm;
}

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

/// <summary>
/// Used in GL.Arb.CompressedTexImage1D, GL.Arb.CompressedTexImage2D and 123 other functions
/// </summary>
public enum TextureTarget : int
{
    /// <summary>
    /// Original was GL_TEXTURE_1D = 0x0DE0
    /// </summary>
    Texture1D = 0x0DE0,
    /// <summary>
    /// Original was GL_TEXTURE_2D = 0x0DE1
    /// </summary>
    Texture2D = 0x0DE1,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_1D = 0x8063
    /// </summary>
    ProxyTexture1D = 0x8063,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_1D_EXT = 0x8063
    /// </summary>
    ProxyTexture1DExt = 0x8063,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_2D = 0x8064
    /// </summary>
    ProxyTexture2D = 0x8064,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_2D_EXT = 0x8064
    /// </summary>
    ProxyTexture2DExt = 0x8064,
    /// <summary>
    /// Original was GL_TEXTURE_3D = 0x806F
    /// </summary>
    Texture3D = 0x806F,
    /// <summary>
    /// Original was GL_TEXTURE_3D_EXT = 0x806F
    /// </summary>
    Texture3DExt = 0x806F,
    /// <summary>
    /// Original was GL_TEXTURE_3D_OES = 0x806F
    /// </summary>
    Texture3DOes = 0x806F,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_3D = 0x8070
    /// </summary>
    ProxyTexture3D = 0x8070,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_3D_EXT = 0x8070
    /// </summary>
    ProxyTexture3DExt = 0x8070,
    /// <summary>
    /// Original was GL_DETAIL_TEXTURE_2D_SGIS = 0x8095
    /// </summary>
    DetailTexture2DSgis = 0x8095,
    /// <summary>
    /// Original was GL_TEXTURE_4D_SGIS = 0x8134
    /// </summary>
    Texture4DSgis = 0x8134,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_4D_SGIS = 0x8135
    /// </summary>
    ProxyTexture4DSgis = 0x8135,
    /// <summary>
    /// Original was GL_TEXTURE_MIN_LOD = 0x813A
    /// </summary>
    TextureMinLod = 0x813A,
    /// <summary>
    /// Original was GL_TEXTURE_MIN_LOD_SGIS = 0x813A
    /// </summary>
    TextureMinLodSgis = 0x813A,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_LOD = 0x813B
    /// </summary>
    TextureMaxLod = 0x813B,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_LOD_SGIS = 0x813B
    /// </summary>
    TextureMaxLodSgis = 0x813B,
    /// <summary>
    /// Original was GL_TEXTURE_BASE_LEVEL = 0x813C
    /// </summary>
    TextureBaseLevel = 0x813C,
    /// <summary>
    /// Original was GL_TEXTURE_BASE_LEVEL_SGIS = 0x813C
    /// </summary>
    TextureBaseLevelSgis = 0x813C,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_LEVEL = 0x813D
    /// </summary>
    TextureMaxLevel = 0x813D,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_LEVEL_SGIS = 0x813D
    /// </summary>
    TextureMaxLevelSgis = 0x813D,
    /// <summary>
    /// Original was GL_TEXTURE_RECTANGLE = 0x84F5
    /// </summary>
    TextureRectangle = 0x84F5,
    /// <summary>
    /// Original was GL_TEXTURE_RECTANGLE_ARB = 0x84F5
    /// </summary>
    TextureRectangleArb = 0x84F5,
    /// <summary>
    /// Original was GL_TEXTURE_RECTANGLE_NV = 0x84F5
    /// </summary>
    TextureRectangleNv = 0x84F5,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_RECTANGLE = 0x84F7
    /// </summary>
    ProxyTextureRectangle = 0x84F7,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP = 0x8513
    /// </summary>
    TextureCubeMap = 0x8513,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_CUBE_MAP = 0x8514
    /// </summary>
    TextureBindingCubeMap = 0x8514,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515
    /// </summary>
    TextureCubeMapPositiveX = 0x8515,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516
    /// </summary>
    TextureCubeMapNegativeX = 0x8516,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517
    /// </summary>
    TextureCubeMapPositiveY = 0x8517,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518
    /// </summary>
    TextureCubeMapNegativeY = 0x8518,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519
    /// </summary>
    TextureCubeMapPositiveZ = 0x8519,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A
    /// </summary>
    TextureCubeMapNegativeZ = 0x851A,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_CUBE_MAP = 0x851B
    /// </summary>
    ProxyTextureCubeMap = 0x851B,
    /// <summary>
    /// Original was GL_TEXTURE_1D_ARRAY = 0x8C18
    /// </summary>
    Texture1DArray = 0x8C18,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_1D_ARRAY = 0x8C19
    /// </summary>
    ProxyTexture1DArray = 0x8C19,
    /// <summary>
    /// Original was GL_TEXTURE_2D_ARRAY = 0x8C1A
    /// </summary>
    Texture2DArray = 0x8C1A,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_2D_ARRAY = 0x8C1B
    /// </summary>
    ProxyTexture2DArray = 0x8C1B,
    /// <summary>
    /// Original was GL_TEXTURE_BUFFER = 0x8C2A
    /// </summary>
    TextureBuffer = 0x8C2A,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009
    /// </summary>
    TextureCubeMapArray = 0x9009,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_CUBE_MAP_ARRAY = 0x900B
    /// </summary>
    ProxyTextureCubeMapArray = 0x900B,
    /// <summary>
    /// Original was GL_TEXTURE_2D_MULTISAMPLE = 0x9100
    /// </summary>
    Texture2DMultisample = 0x9100,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_2D_MULTISAMPLE = 0x9101
    /// </summary>
    ProxyTexture2DMultisample = 0x9101,
    /// <summary>
    /// Original was GL_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102
    /// </summary>
    Texture2DMultisampleArray = 0x9102,
    /// <summary>
    /// Original was GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9103
    /// </summary>
    ProxyTexture2DMultisampleArray = 0x9103
}

[Flags]
internal enum ABS : int
{
    Autohide = 0x0000001,
    AlwaysOnTop = 0x0000002
}

internal struct CreateStruct
{
    internal IntPtr lpCreateParams;
    /// <summary>
    /// Handle to the module that owns the new window.
    /// </summary>
    internal IntPtr hInstance;
    /// <summary>
    /// Handle to the menu to be used by the new window.
    /// </summary>
    internal IntPtr hMenu;
    /// <summary>
    /// Handle to the parent window, if the window is a child window.
    /// If the window is owned, this member identifies the owner window.
    /// If the window is not a child or owned window, this member is NULL.
    /// </summary>
    internal IntPtr hwndParent;
    /// <summary>
    /// Specifies the height of the new window, in pixels.
    /// </summary>
    internal int cy;
    /// <summary>
    /// Specifies the width of the new window, in pixels.
    /// </summary>
    internal int cx;
    /// <summary>
    /// Specifies the y-coordinate of the upper left corner of the new window.
    /// If the new window is a child window, coordinates are relative to the parent window.
    /// Otherwise, the coordinates are relative to the screen origin.
    /// </summary>
    internal int y;
    /// <summary>
    /// Specifies the x-coordinate of the upper left corner of the new window.
    /// If the new window is a child window, coordinates are relative to the parent window.
    /// Otherwise, the coordinates are relative to the screen origin.
    /// </summary>
    internal int x;
    /// <summary>
    /// Specifies the style for the new window.
    /// </summary>
    internal int style;
    /// <summary>
    /// Pointer to a null-terminated string that specifies the name of the new window.
    /// </summary>
    [MarshalAs(UnmanagedType.LPTStr)]
    internal string lpszName;
    /// <summary>
    /// Either a pointer to a null-terminated string or an atom that specifies the class name
    /// of the new window.
    /// <remarks>
    /// Note  Because the lpszClass member can contain a pointer to a local (and thus inaccessable) atom,
    /// do not obtain the class name by using this member. Use the GetClassName function instead.
    /// </remarks>
    /// </summary>
    [MarshalAs(UnmanagedType.LPTStr)]
    internal string lpszClass;
    /// <summary>
    /// Specifies the extended window style for the new window.
    /// </summary>
    internal int dwExStyle;
}

[StructLayout(LayoutKind.Sequential)]
internal struct COLORREF
{
    public byte R;
    public byte G;
    public byte B;
}






[Flags]
internal enum DesiredAccess : uint
{
    GenericRead = 0x80000000,
    GenericWrite = 0x40000000,
    GenericExecute = 0x20000000,
    GenericAll = 0x10000000
}

#pragma warning disable 0649, 0169
internal struct DevBroadcastHDR
{
    internal int Size;
    internal DeviceBroadcastType DeviceType;
    int dbcc_reserved;
    internal Guid ClassGuid;
    internal char dbcc_name;
}
#pragma warning restore 0649, 0169

internal enum DeviceBroadcastType
{
    OEM = 0,
    VOLUME = 2,
    PORT = 3,
    INTERFACE = 5,
    HANDLE = 6,
}


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class DEVMODE
{
    internal DEVMODE()
    {
        Size = (short)Marshal.SizeOf(this);
    }

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    internal string DeviceName;
    internal short SpecVersion;
    internal short DriverVersion;
    private short Size;
    internal short DriverExtra;
    internal DeviceModeEnum Fields;

    //internal short Orientation;
    //internal short PaperSize;
    //internal short PaperLength;
    //internal short PaperWidth;
    //internal short Scale;
    //internal short Copies;
    //internal short DefaultSource;
    //internal short PrintQuality;

    internal POINT Position;
    internal int DisplayOrientation;
    internal int DisplayFixedOutput;

    internal short Color;
    internal short Duplex;
    internal short YResolution;
    internal short TTOption;
    internal short Collate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    internal string FormName;
    internal short LogPixels;
    internal int BitsPerPel;
    internal int PelsWidth;
    internal int PelsHeight;
    internal int DisplayFlags;
    internal int DisplayFrequency;
    internal int ICMMethod;
    internal int ICMIntent;
    internal int MediaType;
    internal int DitherType;
    internal int Reserved1;
    internal int Reserved2;
    internal int PanningWidth;
    internal int PanningHeight;
}

[Flags]
internal enum DeviceModeEnum : int
{
    DM_LOGPIXELS = 0x00020000,
    DM_BITSPERPEL = 0x00040000,
    DM_PELSWIDTH = 0x00080000,
    DM_PELSHEIGHT = 0x00100000,
    DM_DISPLAYFLAGS = 0x00200000,
    DM_DISPLAYFREQUENCY = 0x00400000,
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class DISPLAY_DEVICE
{
    internal DISPLAY_DEVICE()
    {
        size = (short)Marshal.SizeOf(this);
    }
    readonly int size;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    internal string DeviceName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    internal string DeviceString;
    internal DisplayDeviceStateFlags StateFlags;    // Int32
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    internal string DeviceID;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    internal string DeviceKey;
}

[Flags]
internal enum DisplayDeviceStateFlags
{
    None = 0x00000000,
    AttachedToDesktop = 0x00000001,
    MultiDriver = 0x00000002,
    PrimaryDevice = 0x00000004,
    MirroringDriver = 0x00000008,
    VgaCompatible = 0x00000010,
    Removable = 0x00000020,
    ModesPruned = 0x08000000,
    Remote = 0x04000000,
    Disconnect = 0x02000000,

    // Child device state
    Active = 0x00000001,
    Attached = 0x00000002,
}

internal enum DisplayModeSettingsEnum
{
    CurrentSettings = -1,
    RegistrySettings = -2
}



[StructLayout(LayoutKind.Sequential)]
internal struct ICONINFO
{
    public bool IsIcon;
    public int xHotspot;
    public int yHotspot;
    public IntPtr MaskBitmap;
    public IntPtr ColorBitmap;
};



internal struct MONITORINFO
{
    public int Size;
    public RECT Monitor;
    public RECT Work;
    public int Flags;

    public static readonly int UnmanagedSize = Marshal.SizeOf(default(MONITORINFO));
}

[StructLayout(LayoutKind.Sequential)]
public struct MOUSEMOVEPOINT
{
    /// <summary>
    /// The x-coordinate of the mouse.
    /// </summary>
    public int X;

    /// <summary>
    /// The y-coordinate of the mouse.
    /// </summary>
    public int Y;

    /// <summary>
    /// The time stamp of the mouse coordinate.
    /// </summary>
    public int Time;

    /// <summary>
    /// Additional information associated with this coordinate.
    /// </summary>
    public IntPtr ExtraInfo;

    /// <summary>
    /// Returns the size of a MouseMovePoint in bytes.
    /// </summary>
    public static readonly int SizeInBytes = Marshal.SizeOf(default(MOUSEMOVEPOINT));
}

/// <summary>
/// Used with SetWindowLong call
/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms633588(v=vs.85).aspx
/// </summary>
internal enum NIndex : int
{
    CBCLSExtra = -20,
    CBWNDExtra = -18,
    HBrBackground = -10,
    HCursor = -12,
    HIcon = -14,
    HIconSm = -34,
    HModule = -16,
    MenuName = -8,
    Style = -26,
    WndProc = -24 
}



[StructLayout(LayoutKind.Explicit)]
internal struct RawHID
{
}

#pragma warning disable 0649
internal struct RawInput
{
    internal RawInputHeader Header;
    internal RawInputData Data;
}
#pragma warning restore 0649

[StructLayout(LayoutKind.Explicit)]
internal struct RawInputData
{
    [FieldOffset(0)]
    internal RawMouse Mouse;
    [FieldOffset(0)]
    internal RawKeyboard Keyboard;
    [FieldOffset(0)]
    internal RawHID HID;
}

/// <summary>
/// Defines the raw input data coming from any device.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal class RawInputDeviceInfo
{
    /// <summary>
    /// Size, in bytes, of the RawInputDeviceInfo structure.
    /// </summary>
    internal int Size = Marshal.SizeOf(typeof(RawInputDeviceInfo));
    /// <summary>
    /// Type of raw input data.
    /// </summary>
    internal RawInputDeviceType Type;
    internal DeviceStruct Device;
    [StructLayout(LayoutKind.Explicit)]
    internal struct DeviceStruct
    {
        [FieldOffset(0)]
        internal RawInputMouseDeviceInfo Mouse;
        [FieldOffset(0)]
        internal RawInputKeyboardDeviceInfo Keyboard;
        [FieldOffset(0)]
        internal RawInputHIDDeviceInfo HID;
    };
}



/// <summary>
/// Contains information about a raw input device.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RAWINPUTDEVICELIST
{
    /// <summary>
    /// Handle to the raw input device.
    /// </summary>
    internal IntPtr Device;
    /// <summary>
    /// Type of device.
    /// </summary>
    internal RawInputDeviceType Type;

    internal static readonly int Size = Marshal.SizeOf(typeof(RAWINPUTDEVICELIST));
}

internal enum RawInputDeviceType : int
{
    MOUSE = 0,
    KEYBOARD = 1,
    HID = 2
}

[StructLayout(LayoutKind.Sequential)]
internal struct RawInputHeader
{
    /// <summary>
    /// Type of raw input.
    /// </summary>
    internal RawInputDeviceType Type;
    /// <summary>
    /// Size, in bytes, of the entire input packet of data. This includes the RawInput struct plus possible extra input reports in the RAWHID variable length array.
    /// </summary>
    internal int Size;
    /// <summary>
    /// Handle to the device generating the raw input data.
    /// </summary>
    internal IntPtr Device;
    /// <summary>
    /// Value passed in the wParam parameter of the WM_INPUT message.
    /// </summary>
    internal IntPtr Param;

    internal static int SIZE = Marshal.SizeOf(typeof(RawInputHeader));
}

/// <summary>
/// Defines the raw input data coming from the specified Human Interface Device (HID).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RawInputHIDDeviceInfo
{
    /// <summary>
    /// Vendor ID for the HID.
    /// </summary>
    internal int VendorId;
    /// <summary>
    /// Product ID for the HID.
    /// </summary>
    internal int ProductId;
    /// <summary>
    /// Version number for the HID.
    /// </summary>
    internal int VersionNumber;
    /// <summary>
    /// Top-level collection Usage Page for the device.
    /// </summary>
    //internal UInt16 UsagePage;
    internal short UsagePage;
    /// <summary>
    /// Top-level collection Usage for the device.
    /// </summary>
    //internal UInt16 Usage;
    internal short Usage;
}

internal enum RawInputKeyboardDataFlags : short
{
    MAKE = 0,
    BREAK = 1,
    E0 = 2,
    E1 = 4,
    TERMSRV_SET_LED = 8,
    TERMSRV_SHADOW = 0x10
}

/// <summary>
/// Defines the raw input data coming from the specified keyboard.
/// </summary>
/// <remarks>
/// For the keyboard, the Usage Page is 1 and the Usage is 6.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct RawInputKeyboardDeviceInfo
{
    /// <summary>
    /// Type of the keyboard.
    /// </summary>
    internal int Type;
    /// <summary>
    /// Subtype of the keyboard.
    /// </summary>
    internal int SubType;
    /// <summary>
    /// Scan code mode.
    /// </summary>
    internal int KeyboardMode;
    /// <summary>
    /// Number of function keys on the keyboard.
    /// </summary>
    internal int NumberOfFunctionKeys;
    /// <summary>
    /// Number of LED indicators on the keyboard.
    /// </summary>
    internal int NumberOfIndicators;
    /// <summary>
    /// Total number of keys on the keyboard.
    /// </summary>
    internal int NumberOfKeysTotal;
}

/// <summary>
/// Defines the raw input data coming from the specified mouse.
/// </summary>
/// <remarks>
/// For the keyboard, the Usage Page is 1 and the Usage is 2.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct RawInputMouseDeviceInfo
{
    /// <summary>
    /// ID for the mouse device.
    /// </summary>
    internal int Id;
    /// <summary>
    /// Number of buttons for the mouse.
    /// </summary>
    internal int NumberOfButtons;
    /// <summary>
    /// Number of data points per second. This information may not be applicable for every mouse device.
    /// </summary>
    internal int SampleRate;
    /// <summary>
    /// TRUE if the mouse has a wheel for horizontal scrolling; otherwise, FALSE.
    /// </summary>
    /// <remarks>
    /// This member is only supported under Microsoft Windows Vista and later versions.
    /// </remarks>
    internal bool HasHorizontalWheel;
}

[Flags]
internal enum RawInputMouseState : ushort
{
    LEFT_BUTTON_DOWN = 0x0001,  // Left Button changed to down.
    LEFT_BUTTON_UP = 0x0002,  // Left Button changed to up.
    RIGHT_BUTTON_DOWN = 0x0004,  // Right Button changed to down.
    RIGHT_BUTTON_UP = 0x0008,  // Right Button changed to up.
    MIDDLE_BUTTON_DOWN = 0x0010,  // Middle Button changed to down.
    MIDDLE_BUTTON_UP = 0x0020,  // Middle Button changed to up.

    BUTTON_1_DOWN = LEFT_BUTTON_DOWN,
    BUTTON_1_UP = LEFT_BUTTON_UP,
    BUTTON_2_DOWN = RIGHT_BUTTON_DOWN,
    BUTTON_2_UP = RIGHT_BUTTON_UP,
    BUTTON_3_DOWN = MIDDLE_BUTTON_DOWN,
    BUTTON_3_UP = MIDDLE_BUTTON_UP,

    BUTTON_4_DOWN = 0x0040,
    BUTTON_4_UP = 0x0080,
    BUTTON_5_DOWN = 0x0100,
    BUTTON_5_UP = 0x0200,

    WHEEL = 0x0400,
    HWHEEL = 0x0800,
}

[StructLayout(LayoutKind.Sequential)]
internal struct RawKeyboard
{
    /// <summary>
    /// Scan code from the key depression. The scan code for keyboard overrun is KEYBOARD_OVERRUN_MAKE_CODE.
    /// </summary>
    //internal UInt16 MakeCode;
    internal short MakeCode;
    /// <summary>
    /// Flags for scan code information. It can be one or more of the following.
    /// RI_KEY_MAKE
    /// RI_KEY_BREAK
    /// RI_KEY_E0
    /// RI_KEY_E1
    /// RI_KEY_TERMSRV_SET_LED
    /// RI_KEY_TERMSRV_SHADOW
    /// </summary>
    internal RawInputKeyboardDataFlags Flags;
    /// <summary>
    /// Reserved; must be zero.
    /// </summary>
    ushort Reserved;
    /// <summary>
    /// Microsoft Windows message compatible virtual-key code. For more information, see Virtual-Key Codes.
    /// </summary>
    //internal UInt16 VKey;
    internal VIRTUALKEYCODE VKey;
    /// <summary>
    /// Corresponding window message, for example WM_KEYDOWN, WM_SYSKEYDOWN, and so forth.
    /// </summary>
    //internal UInt32 Message;
    internal int Message;
    /// <summary>
    /// Device-specific additional information for the event.
    /// </summary>
    //internal ULONG ExtraInformation;
    internal int ExtraInformation;
}

[StructLayout(LayoutKind.Explicit)]
internal struct RawMouse
{
    /// <summary>
    /// Mouse state. This member can be any reasonable combination of the following. 
    /// MOUSE_ATTRIBUTES_CHANGED
    /// Mouse attributes changed; application needs to query the mouse attributes.
    /// MOUSE_MOVE_RELATIVE
    /// Mouse movement data is relative to the last mouse position.
    /// MOUSE_MOVE_ABSOLUTE
    /// Mouse movement data is based on absolute position.
    /// MOUSE_VIRTUAL_DESKTOP
    /// Mouse coordinates are mapped to the virtual desktop (for a multiple monitor system).
    /// </summary>
    [FieldOffset(0)]
    internal RawMouseFlags Flags;  // UInt16 in winuser.h, but only Int32 works -- UInt16 returns 0.

    [FieldOffset(4)]
    internal RawInputMouseState ButtonFlags;

    /// <summary>
    /// If usButtonFlags is RI_MOUSE_WHEEL, this member is a signed value that specifies the wheel delta.
    /// </summary>
    [FieldOffset(6)]
    internal ushort ButtonData;

    /// <summary>
    /// Raw state of the mouse buttons.
    /// </summary>
    [FieldOffset(8)]
    internal uint RawButtons;

    /// <summary>
    /// Motion in the X direction. This is signed relative motion or absolute motion, depending on the value of usFlags.
    /// </summary>
    [FieldOffset(12)]
    internal int LastX;

    /// <summary>
    /// Motion in the Y direction. This is signed relative motion or absolute motion, depending on the value of usFlags.
    /// </summary>
    [FieldOffset(16)]
    internal int LastY;

    /// <summary>
    /// Device-specific additional information for the event.
    /// </summary>
    [FieldOffset(20)]
    internal uint ExtraInformation;
}

/// <summary>
/// Mouse indicator flags (found in winuser.h).
/// </summary>
[Flags]
internal enum RawMouseFlags : ushort
{
    /// <summary>
    /// LastX/Y indicate relative motion.
    /// </summary>
    MOUSE_MOVE_RELATIVE = 0x00,
    /// <summary>
    /// LastX/Y indicate absolute motion.
    /// </summary>
    MOUSE_MOVE_ABSOLUTE = 0x01,
    /// <summary>
    /// The coordinates are mapped to the virtual desktop.
    /// </summary>
    MOUSE_VIRTUAL_DESKTOP = 0x02,
    /// <summary>
    /// Requery for mouse attributes.
    /// </summary>
    MOUSE_ATTRIBUTES_CHANGED = 0x04,
}

[Flags]
internal enum SETWINDOWPOSFLAGS : int
{
    /// <summary>
    /// Retains the current size (ignores the cx and cy parameters).
    /// </summary>
    NOSIZE = 0x0001,
    /// <summary>
    /// Retains the current position (ignores the x and y parameters).
    /// </summary>
    NOMOVE = 0x0002,
    /// <summary>
    /// Retains the current Z order (ignores the hwndInsertAfter parameter).
    /// </summary>
    NOZORDER = 0x0004,
    /// <summary>
    /// Does not redraw changes. If this flag is set, no repainting of any kind occurs.
    /// This applies to the client area, the nonclient area (including the title bar and scroll bars),
    /// and any part of the parent window uncovered as a result of the window being moved.
    /// When this flag is set, the application must explicitly invalidate or redraw any parts
    /// of the window and parent window that need redrawing.
    /// </summary>
    NOREDRAW = 0x0008,
    /// <summary>
    /// Does not activate the window. If this flag is not set,
    /// the window is activated and moved to the top of either the topmost or non-topmost group
    /// (depending on the setting of the hwndInsertAfter member).
    /// </summary>
    NOACTIVATE = 0x0010,
    /// <summary>
    /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
    /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
    /// </summary>
    FRAMECHANGED = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
    /// <summary>
    /// Displays the window.
    /// </summary>
    SHOWWINDOW = 0x0040,
    /// <summary>
    /// Hides the window.
    /// </summary>
    HIDEWINDOW = 0x0080,
    /// <summary>
    /// Discards the entire contents of the client area. If this flag is not specified,
    /// the valid contents of the client area are saved and copied back into the client area 
    /// after the window is sized or repositioned.
    /// </summary>
    NOCOPYBITS = 0x0100,
    /// <summary>
    /// Does not change the owner window's position in the Z order.
    /// </summary>
    NOOWNERZORDER = 0x0200, /* Don't do owner Z ordering */
    /// <summary>
    /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
    /// </summary>
    NOSENDCHANGING = 0x0400, /* Don't send WM_WINDOWPOSCHANGING */

    /// <summary>
    /// Draws a frame (defined in the window's class description) around the window.
    /// </summary>
    DRAWFRAME = FRAMECHANGED,
    /// <summary>
    /// Same as the NOOWNERZORDER flag.
    /// </summary>
    NOREPOSITION = NOOWNERZORDER,

    DEFERERASE = 0x2000,
    ASYNCWINDOWPOS = 0x4000
}

internal enum SizeMessage
{
    MAXHIDE = 4,
    MAXIMIZED = 2,
    MAXSHOW = 3,
    MINIMIZED = 1,
    RESTORED = 0
}

internal struct StyleStruct
{
    public WINDOWSTYLE Old;
    public WINDOWSTYLE New;
}

internal struct ExtendedStyleStruct
{
    public WINDOWSTYLEEX Old;
    public WINDOWSTYLEEX New;
}

//todo: complete this
internal enum SystemErrorCode : uint
{
    /// <summary>
    /// The operation completed successfully.
    /// </summary>
    ERROR_SUCCESS = 0,
    /// <summary>
    /// The handle is invalid.
    /// </summary>
    ERROR_INVALID_HANDLE = 6,
    /// <summary>
    /// The point passed to GetMouseMovePoints is not in the buffer.
    /// </summary>
    ERROR_POINT_NOT_FOUND = 1171,
    /// <summary>
    /// The pixel format is invalid.
    /// </summary>
    ERROR_INVALID_PIXEL_FORMAT = 2000
}

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate void TimerProc(IntPtr hwnd, WINDOWMESSAGE uMsg, UIntPtr idEvent, int dwTime);

internal enum VIRTUALKEYCODE : short
{
    /*
        * Virtual Key, Standard Set
        */
    LBUTTON = 0x01,
    RBUTTON = 0x02,
    CANCEL = 0x03,
    MBUTTON = 0x04,   /* NOT contiguous with L & RBUTTON */

    XBUTTON1 = 0x05,   /* NOT contiguous with L & RBUTTON */
    XBUTTON2 = 0x06,   /* NOT contiguous with L & RBUTTON */

    /*
        * 0x07 : unassigned
        */

    BACK = 0x08,
    TAB = 0x09,

    /*
        * 0x0A - 0x0B : reserved
        */

    CLEAR = 0x0C,
    RETURN = 0x0D,

    SHIFT = 0x10,
    CONTROL = 0x11,
    MENU = 0x12,
    PAUSE = 0x13,
    CAPITAL = 0x14,

    KANA = 0x15,
    HANGUL = 0x15,
    JUNJA = 0x17,
    FINAL = 0x18,
    HANJA = 0x19,
    KANJI = 0x19,

    ESCAPE = 0x1B,

    CONVERT = 0x1C,
    NONCONVERT = 0x1D,
    ACCEPT = 0x1E,
    MODECHANGE = 0x1F,

    SPACE = 0x20,
    PRIOR = 0x21,
    NEXT = 0x22,
    END = 0x23,
    HOME = 0x24,
    LEFT = 0x25,
    UP = 0x26,
    RIGHT = 0x27,
    DOWN = 0x28,
    SELECT = 0x29,
    PRINT = 0x2A,
    EXECUTE = 0x2B,
    SNAPSHOT = 0x2C,
    INSERT = 0x2D,
    DELETE = 0x2E,
    HELP = 0x2F,

    NUM0 = 0x30,
    NUM1 = 0x31,
    NUM2 = 0x32,
    NUM3 = 0x33,
    NUM4 = 0x34,
    NUM5 = 0x35,
    NUM6 = 0x36,
    NUM7 = 0x37,
    NUM8 = 0x38,
    NUM9 = 0x39,

    A = 0x41,
    B = 0x42,
    C = 0x43,
    D = 0x44,
    E = 0x45,
    F = 0x46,
    G = 0x47,
    H = 0x48,
    I = 0x49,
    J = 0x4A,
    K = 0x4B,
    L = 0x4C,
    M = 0x4D,
    N = 0x4E,
    O = 0x4F,
    P = 0x50,
    Q = 0x51,
    R = 0x52,
    S = 0x53,
    T = 0x54,
    U = 0x55,
    V = 0x56,
    W = 0x57,
    X = 0x58,
    Y = 0x59,
    Z = 0x5A,

    LWIN = 0x5B,
    RWIN = 0x5C,
    APPS = 0x5D,

    /*
        * 0x5E : reserved
        */

    SLEEP = 0x5F,

    NUMPAD0 = 0x60,
    NUMPAD1 = 0x61,
    NUMPAD2 = 0x62,
    NUMPAD3 = 0x63,
    NUMPAD4 = 0x64,
    NUMPAD5 = 0x65,
    NUMPAD6 = 0x66,
    NUMPAD7 = 0x67,
    NUMPAD8 = 0x68,
    NUMPAD9 = 0x69,
    MULTIPLY = 0x6A,
    ADD = 0x6B,
    SEPARATOR = 0x6C,
    SUBTRACT = 0x6D,
    DECIMAL = 0x6E,
    DIVIDE = 0x6F,
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,
    F8 = 0x77,
    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,
    F13 = 0x7C,
    F14 = 0x7D,
    F15 = 0x7E,
    F16 = 0x7F,
    F17 = 0x80,
    F18 = 0x81,
    F19 = 0x82,
    F20 = 0x83,
    F21 = 0x84,
    F22 = 0x85,
    F23 = 0x86,
    F24 = 0x87,

    /*
        * 0x88 - 0x8F : unassigned
        */

    NUMLOCK = 0x90,
    SCROLL = 0x91,

    /*
        * NEC PC-9800 kbd definitions
        */
    OEM_NEC_EQUAL = 0x92,  // '=' key on numpad

    /*
        * Fujitsu/OASYS kbd definitions
        */
    OEM_FJ_JISHO = 0x92,  // 'Dictionary' key
    OEM_FJ_MASSHOU = 0x93,  // 'Unregister word' key
    OEM_FJ_TOUROKU = 0x94,  // 'Register word' key
    OEM_FJ_LOYA = 0x95,  // 'Left OYAYUBI' key
    OEM_FJ_ROYA = 0x96,  // 'Right OYAYUBI' key

    /*
        * 0x97 - 0x9F : unassigned
        */

    /*
        * L* & R* - left and right Alt, Ctrl and Shift virtual keys.
        * Used only as parameters to GetAsyncKeyState() and GetKeyState().
        * No other API or message will distinguish left and right keys in this way.
        */
    LSHIFT = 0xA0,
    RSHIFT = 0xA1,
    LCONTROL = 0xA2,
    RCONTROL = 0xA3,
    LMENU = 0xA4,
    RMENU = 0xA5,

    BROWSER_BACK = 0xA6,
    BROWSER_FORWARD = 0xA7,
    BROWSER_REFRESH = 0xA8,
    BROWSER_STOP = 0xA9,
    BROWSER_SEARCH = 0xAA,
    BROWSER_FAVORITES = 0xAB,
    BROWSER_HOME = 0xAC,

    VOLUME_MUTE = 0xAD,
    VOLUME_DOWN = 0xAE,
    VOLUME_UP = 0xAF,
    MEDIA_NEXT_TRACK = 0xB0,
    MEDIA_PREV_TRACK = 0xB1,
    MEDIA_STOP = 0xB2,
    MEDIA_PLAY_PAUSE = 0xB3,
    LAUNCH_MAIL = 0xB4,
    LAUNCH_MEDIA_SELECT = 0xB5,
    LAUNCH_APP1 = 0xB6,
    LAUNCH_APP2 = 0xB7,

    /*
        * 0xB8 - 0xB9 : reserved
        */

    OEM_1 = 0xBA,   // ';:' for US
    OEM_PLUS = 0xBB,   // '+' any country
    OEM_COMMA = 0xBC,   // ',' any country
    OEM_MINUS = 0xBD,   // '-' any country
    OEM_PERIOD = 0xBE,   // '.' any country
    OEM_2 = 0xBF,   // '/?' for US
    OEM_3 = 0xC0,   // '`~' for US

    /*
        * 0xC1 - 0xD7 : reserved
        */

    /*
        * 0xD8 - 0xDA : unassigned
        */

    OEM_4 = 0xDB,  //  '[{' for US
    OEM_5 = 0xDC,  //  '\|' for US
    OEM_6 = 0xDD,  //  ']}' for US
    OEM_7 = 0xDE,  //  ''"' for US
    OEM_8 = 0xDF,

    /*
        * 0xE0 : reserved
        */

    /*
        * Various extended or enhanced keyboards
        */
    OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
    OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
    ICO_HELP = 0xE3,  //  Help key on ICO
    ICO_00 = 0xE4,  //  00 key on ICO

    PROCESSKEY = 0xE5,

    ICO_CLEAR = 0xE6,


    PACKET = 0xE7,

    /*
        * 0xE8 : unassigned
        */

    /*
        * Nokia/Ericsson definitions
        */
    OEM_RESET = 0xE9,
    OEM_JUMP = 0xEA,
    OEM_PA1 = 0xEB,
    OEM_PA2 = 0xEC,
    OEM_PA3 = 0xED,
    OEM_WSCTRL = 0xEE,
    OEM_CUSEL = 0xEF,
    OEM_ATTN = 0xF0,
    OEM_FINISH = 0xF1,
    OEM_COPY = 0xF2,
    OEM_AUTO = 0xF3,
    OEM_ENLW = 0xF4,
    OEM_BACKTAB = 0xF5,

    ATTN = 0xF6,
    CRSEL = 0xF7,
    EXSEL = 0xF8,
    EREOF = 0xF9,
    PLAY = 0xFA,
    ZOOM = 0xFB,
    NONAME = 0xFC,
    PA1 = 0xFD,
    OEM_CLEAR = 0xFE,

    Last
}

[StructLayout(LayoutKind.Sequential)]
internal struct WindowPosition
{
    /// <summary>
    /// Handle to the window.
    /// </summary>
    internal IntPtr hwnd;
    /// <summary>
    /// Specifies the position of the window in Z order (front-to-back position).
    /// This member can be a handle to the window behind which this window is placed,
    /// or can be one of the special values listed with the SetWindowPos function.
    /// </summary>
    internal IntPtr hwndInsertAfter;
    /// <summary>
    /// Specifies the position of the left edge of the window.
    /// </summary>
    internal int x;
    /// <summary>
    /// Specifies the position of the top edge of the window.
    /// </summary>
    internal int y;
    /// <summary>
    /// Specifies the window width, in pixels.
    /// </summary>
    internal int cx;
    /// <summary>
    /// Specifies the window height, in pixels.
    /// </summary>
    internal int cy;
    /// <summary>
    /// Specifies the window position.
    /// </summary>
    [MarshalAs(UnmanagedType.U4)]
    internal SETWINDOWPOSFLAGS flags;
}

public enum AlphaFunction : int
{
    /// <summary>
    /// Original was GL_NEVER = 0x0200
    /// </summary>
    Never = 0x0200,
    /// <summary>
    /// Original was GL_LESS = 0x0201
    /// </summary>
    Less = 0x0201,
    /// <summary>
    /// Original was GL_EQUAL = 0x0202
    /// </summary>
    Equal = 0x0202,
    /// <summary>
    /// Original was GL_LEQUAL = 0x0203
    /// </summary>
    Lequal = 0x0203,
    /// <summary>
    /// Original was GL_GREATER = 0x0204
    /// </summary>
    Greater = 0x0204,
    /// <summary>
    /// Original was GL_NOTEQUAL = 0x0205
    /// </summary>
    Notequal = 0x0205,
    /// <summary>
    /// Original was GL_GEQUAL = 0x0206
    /// </summary>
    Gequal = 0x0206,
    /// <summary>
    /// Original was GL_ALWAYS = 0x0207
    /// </summary>
    Always = 0x0207,
}

public enum BufferTarget : int
{
    /// <summary>
    /// Original was GL_ARRAY_BUFFER = 0x8892
    /// </summary>
    ArrayBuffer = 0x8892,
    /// <summary>
    /// Original was GL_ELEMENT_ARRAY_BUFFER = 0x8893
    /// </summary>
    ElementArrayBuffer = 0x8893,
    /// <summary>
    /// Original was GL_PIXEL_PACK_BUFFER = 0x88EB
    /// </summary>
    PixelPackBuffer = 0x88EB,
    /// <summary>
    /// Original was GL_PIXEL_UNPACK_BUFFER = 0x88EC
    /// </summary>
    PixelUnpackBuffer = 0x88EC,
    /// <summary>
    /// Original was GL_UNIFORM_BUFFER = 0x8A11
    /// </summary>
    UniformBuffer = 0x8A11,
    /// <summary>
    /// Original was GL_TEXTURE_BUFFER = 0x8C2A
    /// </summary>
    TextureBuffer = 0x8C2A,
    /// <summary>
    /// Original was GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E
    /// </summary>
    TransformFeedbackBuffer = 0x8C8E,
    /// <summary>
    /// Original was GL_COPY_READ_BUFFER = 0x8F36
    /// </summary>
    CopyReadBuffer = 0x8F36,
    /// <summary>
    /// Original was GL_COPY_WRITE_BUFFER = 0x8F37
    /// </summary>
    CopyWriteBuffer = 0x8F37,
    /// <summary>
    /// Original was GL_DRAW_INDIRECT_BUFFER = 0x8F3F
    /// </summary>
    DrawIndirectBuffer = 0x8F3F,
    /// <summary>
    /// Original was GL_SHADER_STORAGE_BUFFER = 0x90D2
    /// </summary>
    ShaderStorageBuffer = 0x90D2,
    /// <summary>
    /// Original was GL_DISPATCH_INDIRECT_BUFFER = 0x90EE
    /// </summary>
    DispatchIndirectBuffer = 0x90EE,
    /// <summary>
    /// Original was GL_QUERY_BUFFER = 0x9192
    /// </summary>
    QueryBuffer = 0x9192,
    /// <summary>
    /// Original was GL_ATOMIC_COUNTER_BUFFER = 0x92C0
    /// </summary>
    AtomicCounterBuffer = 0x92C0,
}

public enum BufferUsageHint : int
{
    /// <summary>
    /// Original was GL_STREAM_DRAW = 0x88E0
    /// </summary>
    StreamDraw = 0x88E0,
    /// <summary>
    /// Original was GL_STREAM_READ = 0x88E1
    /// </summary>
    StreamRead = 0x88E1,
    /// <summary>
    /// Original was GL_STREAM_COPY = 0x88E2
    /// </summary>
    StreamCopy = 0x88E2,
    /// <summary>
    /// Original was GL_STATIC_DRAW = 0x88E4
    /// </summary>
    StaticDraw = 0x88E4,
    /// <summary>
    /// Original was GL_STATIC_READ = 0x88E5
    /// </summary>
    StaticRead = 0x88E5,
    /// <summary>
    /// Original was GL_STATIC_COPY = 0x88E6
    /// </summary>
    StaticCopy = 0x88E6,
    /// <summary>
    /// Original was GL_DYNAMIC_DRAW = 0x88E8
    /// </summary>
    DynamicDraw = 0x88E8,
    /// <summary>
    /// Original was GL_DYNAMIC_READ = 0x88E9
    /// </summary>
    DynamicRead = 0x88E9,
    /// <summary>
    /// Original was GL_DYNAMIC_COPY = 0x88EA
    /// </summary>
    DynamicCopy = 0x88EA,
}

[Flags]
public enum DataType : uint
{
    /// <summary>
    /// Original was GL_BYTE = 0x1400
    /// </summary>
    Byte = 0x1400,
    /// <summary>
    /// Original was GL_UNSIGNED_BYTE = 0x1401
    /// </summary>
    UnsignedByte = 0x1401,
    /// <summary>
    /// Original was GL_SHORT = 0x1402
    /// </summary>
    Short = 0x1402,
    /// <summary>
    /// Original was GL_UNSIGNED_SHORT = 0x1403
    /// </summary>
    UnsignedShort = 0x1403,
    /// <summary>
    /// Original was GL_INT = 0x1404
    /// </summary>
    Int = 0x1404,
    /// <summary>
    /// Original was GL_UNSIGNED_INT = 0x1405
    /// </summary>
    UnsignedInt = 0x1405,
    /// <summary>
    /// Original was GL_FLOAT = 0x1406
    /// </summary>
    Float = 0x1406,
    /// <summary>
    /// Original was GL_2_BYTES = 0x1407
    /// </summary>
    TwoBytes = 0x1407,
    /// <summary>
    /// Original was GL_3_BYTES = 0x1408
    /// </summary>
    ThreeBytes = 0x1408,
    /// <summary>
    /// Original was GL_4_BYTES = 0x1409
    /// </summary>
    FourBytes = 0x1409,
    /// <summary>
    /// Original was GL_DOUBLE = 0x140A
    /// </summary>
    Double = 0x140A
}

/// <summary>
/// GL_DEBUG_SEVERITY_HIGH and friends, for calls to glDebugMessageCallback and related error handling
/// </summary>
public enum DebugSeverity : int
{
    /// <summary>
    /// Original was GL_DONT_CARE = 0x1100
    /// </summary>
    DontCare = 0x1100,

    /// <summary>
    /// Original was GL_DEBUG_SEVERITY_HIGH = 0x9146
    /// </summary>
    High = 0x9146,

    /// <summary>
    /// Original was GL_DEBUG_SEVERITY_MEDIUM = 0x9147
    /// </summary>
    Medium = 0x9147,

    /// <summary>
    /// Original was GL_DEBUG_SEVERITY_LOW = 0x9148
    /// </summary>
    Low = 0x9148,

    /// <summary>
    /// Original was GL_DEBUG_SEVERITY_NOTIFICATION = 0x826B
    /// </summary>
    Notification = 0x826B,
}

/// <summary>
/// GL_DEBUG_SOURCE_API and friends, for calls to glDebugMessageCallback and related error handling
/// </summary>
public enum DebugSource : int
{
    /// <summary>
    /// Original was GL_DONT_CARE = 0x1100
    /// </summary>
    DontCare = 0x1100,

    /// <summary>
    /// Original was GL_DEBUG_SOURCE_API = 0x8246
    /// </summary>
    API = 0x8246,

    /// <summary>
    /// Original was GL_DEBUG_SOURCE_WINDOW_SYSTEM = 0x8247
    /// </summary>
    WindowSystem = 0x8247,

    /// <summary>
    /// Original was GL_DEBUG_SOURCE_THIRD_PARTY = 0x8249
    /// </summary>
    ThirdParty = 0x8249,

    /// <summary>
    /// Original was GL_DEBUG_SOURCE_APPLICATION = 0x824A
    /// </summary>
    Application = 0x824A,

    /// <summary>
    /// Original was GL_DEBUG_SOURCE_OTHER = 0x824B
    /// </summary>
    Other = 0x824B,
}

/// <summary>
/// GL_DEBUG_TYPE_ERROR and friends, for calls to glDebugMessageCallback and related error handling
/// </summary>
public enum DebugType : int
{
    /// <summary>
    /// Original was GL_DONT_CARE = 0x1100
    /// </summary>
    DontCare = 0x1100,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_ERROR = 0x824C
    /// </summary>
    Error = 0x824C,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR = 0x824D
    /// </summary>
    DeprecatedBehaviour = 0x824D,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR = 0x824E
    /// </summary>
    UndefinedBehaviour = 0x824E,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_PORTABILITY = 0x824F
    /// </summary>
    Portability = 0x824F,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_PEFORMANCE = 0x8250
    /// </summary>
    Performance = 0x8250,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_MARKER = 0x824B
    /// </summary>
    Marker = 0x824B,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_OTHER = 0x8251
    /// </summary>
    Other = 0x8251,

    /// <summary>
    /// Original was GL_DEBUG_TYPE_POP_GROUP = 0x824B
    /// </summary>
    //PopGroup = 0x824B,   Unknown value 

    /// <summary>
    /// Original was GL_DEBUG_TYPE_PUSH_GROUP = 0x824B
    /// </summary>
    //PushGroup = 0x824B      unknown value      
}

public enum EnableCap : int
{
    /// <summary>
    /// Original was GL_POINT_SMOOTH = 0x0B10
    /// </summary>
    PointSmooth = 0x0B10,
    /// <summary>
    /// Original was GL_LINE_SMOOTH = 0x0B20
    /// </summary>
    LineSmooth = 0x0B20,
    /// <summary>
    /// Original was GL_LINE_STIPPLE = 0x0B24
    /// </summary>
    LineStipple = 0x0B24,
    /// <summary>
    /// Original was GL_POLYGON_SMOOTH = 0x0B41
    /// </summary>
    PolygonSmooth = 0x0B41,
    /// <summary>
    /// Original was GL_POLYGON_STIPPLE = 0x0B42
    /// </summary>
    PolygonStipple = 0x0B42,
    /// <summary>
    /// Original was GL_CULL_FACE = 0x0B44
    /// </summary>
    CullFace = 0x0B44,
    /// <summary>
    /// Original was GL_LIGHTING = 0x0B50
    /// </summary>
    Lighting = 0x0B50,
    /// <summary>
    /// Original was GL_COLOR_MATERIAL = 0x0B57
    /// </summary>
    ColorMaterial = 0x0B57,
    /// <summary>
    /// Original was GL_FOG = 0x0B60
    /// </summary>
    Fog = 0x0B60,
    /// <summary>
    /// Original was GL_DEPTH_TEST = 0x0B71
    /// </summary>
    DepthTest = 0x0B71,
    /// <summary>
    /// Original was GL_STENCIL_TEST = 0x0B90
    /// </summary>
    StencilTest = 0x0B90,
    /// <summary>
    /// Original was GL_NORMALIZE = 0x0BA1
    /// </summary>
    Normalize = 0x0BA1,
    /// <summary>
    /// Original was GL_ALPHA_TEST = 0x0BC0
    /// </summary>
    AlphaTest = 0x0BC0,
    /// <summary>
    /// Original was GL_DITHER = 0x0BD0
    /// </summary>
    Dither = 0x0BD0,
    /// <summary>
    /// Original was GL_BLEND = 0x0BE2
    /// </summary>
    Blend = 0x0BE2,
    /// <summary>
    /// Original was GL_INDEX_LOGIC_OP = 0x0BF1
    /// </summary>
    IndexLogicOp = 0x0BF1,
    /// <summary>
    /// Original was GL_COLOR_LOGIC_OP = 0x0BF2
    /// </summary>
    ColorLogicOp = 0x0BF2,
    /// <summary>
    /// Original was GL_SCISSOR_TEST = 0x0C11
    /// </summary>
    ScissorTest = 0x0C11,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_S = 0x0C60
    /// </summary>
    TextureGenS = 0x0C60,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_T = 0x0C61
    /// </summary>
    TextureGenT = 0x0C61,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_R = 0x0C62
    /// </summary>
    TextureGenR = 0x0C62,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_Q = 0x0C63
    /// </summary>
    TextureGenQ = 0x0C63,
    /// <summary>
    /// Original was GL_AUTO_NORMAL = 0x0D80
    /// </summary>
    AutoNormal = 0x0D80,
    /// <summary>
    /// Original was GL_MAP1_COLOR_4 = 0x0D90
    /// </summary>
    Map1Color4 = 0x0D90,
    /// <summary>
    /// Original was GL_MAP1_INDEX = 0x0D91
    /// </summary>
    Map1Index = 0x0D91,
    /// <summary>
    /// Original was GL_MAP1_NORMAL = 0x0D92
    /// </summary>
    Map1Normal = 0x0D92,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_1 = 0x0D93
    /// </summary>
    Map1TextureCoord1 = 0x0D93,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_2 = 0x0D94
    /// </summary>
    Map1TextureCoord2 = 0x0D94,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_3 = 0x0D95
    /// </summary>
    Map1TextureCoord3 = 0x0D95,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_4 = 0x0D96
    /// </summary>
    Map1TextureCoord4 = 0x0D96,
    /// <summary>
    /// Original was GL_MAP1_VERTEX_3 = 0x0D97
    /// </summary>
    Map1Vertex3 = 0x0D97,
    /// <summary>
    /// Original was GL_MAP1_VERTEX_4 = 0x0D98
    /// </summary>
    Map1Vertex4 = 0x0D98,
    /// <summary>
    /// Original was GL_MAP2_COLOR_4 = 0x0DB0
    /// </summary>
    Map2Color4 = 0x0DB0,
    /// <summary>
    /// Original was GL_MAP2_INDEX = 0x0DB1
    /// </summary>
    Map2Index = 0x0DB1,
    /// <summary>
    /// Original was GL_MAP2_NORMAL = 0x0DB2
    /// </summary>
    Map2Normal = 0x0DB2,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_1 = 0x0DB3
    /// </summary>
    Map2TextureCoord1 = 0x0DB3,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_2 = 0x0DB4
    /// </summary>
    Map2TextureCoord2 = 0x0DB4,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_3 = 0x0DB5
    /// </summary>
    Map2TextureCoord3 = 0x0DB5,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_4 = 0x0DB6
    /// </summary>
    Map2TextureCoord4 = 0x0DB6,
    /// <summary>
    /// Original was GL_MAP2_VERTEX_3 = 0x0DB7
    /// </summary>
    Map2Vertex3 = 0x0DB7,
    /// <summary>
    /// Original was GL_MAP2_VERTEX_4 = 0x0DB8
    /// </summary>
    Map2Vertex4 = 0x0DB8,
    /// <summary>
    /// Original was GL_TEXTURE_1D = 0x0DE0
    /// </summary>
    Texture1D = 0x0DE0,
    /// <summary>
    /// Original was GL_TEXTURE_2D = 0x0DE1
    /// </summary>
    Texture2D = 0x0DE1,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_POINT = 0x2A01
    /// </summary>
    PolygonOffsetPoint = 0x2A01,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_LINE = 0x2A02
    /// </summary>
    PolygonOffsetLine = 0x2A02,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE0 = 0x3000
    /// </summary>
    ClipDistance0 = 0x3000,
    /// <summary>
    /// Original was GL_CLIP_PLANE0 = 0x3000
    /// </summary>
    ClipPlane0 = 0x3000,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE1 = 0x3001
    /// </summary>
    ClipDistance1 = 0x3001,
    /// <summary>
    /// Original was GL_CLIP_PLANE1 = 0x3001
    /// </summary>
    ClipPlane1 = 0x3001,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE2 = 0x3002
    /// </summary>
    ClipDistance2 = 0x3002,
    /// <summary>
    /// Original was GL_CLIP_PLANE2 = 0x3002
    /// </summary>
    ClipPlane2 = 0x3002,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE3 = 0x3003
    /// </summary>
    ClipDistance3 = 0x3003,
    /// <summary>
    /// Original was GL_CLIP_PLANE3 = 0x3003
    /// </summary>
    ClipPlane3 = 0x3003,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE4 = 0x3004
    /// </summary>
    ClipDistance4 = 0x3004,
    /// <summary>
    /// Original was GL_CLIP_PLANE4 = 0x3004
    /// </summary>
    ClipPlane4 = 0x3004,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE5 = 0x3005
    /// </summary>
    ClipDistance5 = 0x3005,
    /// <summary>
    /// Original was GL_CLIP_PLANE5 = 0x3005
    /// </summary>
    ClipPlane5 = 0x3005,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE6 = 0x3006
    /// </summary>
    ClipDistance6 = 0x3006,
    /// <summary>
    /// Original was GL_CLIP_DISTANCE7 = 0x3007
    /// </summary>
    ClipDistance7 = 0x3007,
    /// <summary>
    /// Original was GL_LIGHT0 = 0x4000
    /// </summary>
    Light0 = 0x4000,
    /// <summary>
    /// Original was GL_LIGHT1 = 0x4001
    /// </summary>
    Light1 = 0x4001,
    /// <summary>
    /// Original was GL_LIGHT2 = 0x4002
    /// </summary>
    Light2 = 0x4002,
    /// <summary>
    /// Original was GL_LIGHT3 = 0x4003
    /// </summary>
    Light3 = 0x4003,
    /// <summary>
    /// Original was GL_LIGHT4 = 0x4004
    /// </summary>
    Light4 = 0x4004,
    /// <summary>
    /// Original was GL_LIGHT5 = 0x4005
    /// </summary>
    Light5 = 0x4005,
    /// <summary>
    /// Original was GL_LIGHT6 = 0x4006
    /// </summary>
    Light6 = 0x4006,
    /// <summary>
    /// Original was GL_LIGHT7 = 0x4007
    /// </summary>
    Light7 = 0x4007,
    /// <summary>
    /// Original was GL_CONVOLUTION_1D = 0x8010
    /// </summary>
    Convolution1D = 0x8010,
    /// <summary>
    /// Original was GL_CONVOLUTION_1D_EXT = 0x8010
    /// </summary>
    Convolution1DExt = 0x8010,
    /// <summary>
    /// Original was GL_CONVOLUTION_2D = 0x8011
    /// </summary>
    Convolution2D = 0x8011,
    /// <summary>
    /// Original was GL_CONVOLUTION_2D_EXT = 0x8011
    /// </summary>
    Convolution2DExt = 0x8011,
    /// <summary>
    /// Original was GL_SEPARABLE_2D = 0x8012
    /// </summary>
    Separable2D = 0x8012,
    /// <summary>
    /// Original was GL_SEPARABLE_2D_EXT = 0x8012
    /// </summary>
    Separable2DExt = 0x8012,
    /// <summary>
    /// Original was GL_HISTOGRAM = 0x8024
    /// </summary>
    Histogram = 0x8024,
    /// <summary>
    /// Original was GL_HISTOGRAM_EXT = 0x8024
    /// </summary>
    HistogramExt = 0x8024,
    /// <summary>
    /// Original was GL_MINMAX_EXT = 0x802E
    /// </summary>
    MinmaxExt = 0x802E,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_FILL = 0x8037
    /// </summary>
    PolygonOffsetFill = 0x8037,
    /// <summary>
    /// Original was GL_RESCALE_NORMAL = 0x803A
    /// </summary>
    RescaleNormal = 0x803A,
    /// <summary>
    /// Original was GL_RESCALE_NORMAL_EXT = 0x803A
    /// </summary>
    RescaleNormalExt = 0x803A,
    /// <summary>
    /// Original was GL_TEXTURE_3D_EXT = 0x806F
    /// </summary>
    Texture3DExt = 0x806F,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY = 0x8074
    /// </summary>
    VertexArray = 0x8074,
    /// <summary>
    /// Original was GL_NORMAL_ARRAY = 0x8075
    /// </summary>
    NormalArray = 0x8075,
    /// <summary>
    /// Original was GL_COLOR_ARRAY = 0x8076
    /// </summary>
    ColorArray = 0x8076,
    /// <summary>
    /// Original was GL_INDEX_ARRAY = 0x8077
    /// </summary>
    IndexArray = 0x8077,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY = 0x8078
    /// </summary>
    TextureCoordArray = 0x8078,
    /// <summary>
    /// Original was GL_EDGE_FLAG_ARRAY = 0x8079
    /// </summary>
    EdgeFlagArray = 0x8079,
    /// <summary>
    /// Original was GL_INTERLACE_SGIX = 0x8094
    /// </summary>
    InterlaceSgix = 0x8094,
    /// <summary>
    /// Original was GL_MULTISAMPLE = 0x809D
    /// </summary>
    Multisample = 0x809D,
    /// <summary>
    /// Original was GL_MULTISAMPLE_SGIS = 0x809D
    /// </summary>
    MultisampleSgis = 0x809D,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E
    /// </summary>
    SampleAlphaToCoverage = 0x809E,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_MASK_SGIS = 0x809E
    /// </summary>
    SampleAlphaToMaskSgis = 0x809E,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_ONE = 0x809F
    /// </summary>
    SampleAlphaToOne = 0x809F,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_ONE_SGIS = 0x809F
    /// </summary>
    SampleAlphaToOneSgis = 0x809F,
    /// <summary>
    /// Original was GL_SAMPLE_COVERAGE = 0x80A0
    /// </summary>
    SampleCoverage = 0x80A0,
    /// <summary>
    /// Original was GL_SAMPLE_MASK_SGIS = 0x80A0
    /// </summary>
    SampleMaskSgis = 0x80A0,
    /// <summary>
    /// Original was GL_TEXTURE_COLOR_TABLE_SGI = 0x80BC
    /// </summary>
    TextureColorTableSgi = 0x80BC,
    /// <summary>
    /// Original was GL_COLOR_TABLE = 0x80D0
    /// </summary>
    ColorTable = 0x80D0,
    /// <summary>
    /// Original was GL_COLOR_TABLE_SGI = 0x80D0
    /// </summary>
    ColorTableSgi = 0x80D0,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_COLOR_TABLE = 0x80D1
    /// </summary>
    PostConvolutionColorTable = 0x80D1,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1
    /// </summary>
    PostConvolutionColorTableSgi = 0x80D1,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_COLOR_TABLE = 0x80D2
    /// </summary>
    PostColorMatrixColorTable = 0x80D2,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2
    /// </summary>
    PostColorMatrixColorTableSgi = 0x80D2,
    /// <summary>
    /// Original was GL_TEXTURE_4D_SGIS = 0x8134
    /// </summary>
    Texture4DSgis = 0x8134,
    /// <summary>
    /// Original was GL_PIXEL_TEX_GEN_SGIX = 0x8139
    /// </summary>
    PixelTexGenSgix = 0x8139,
    /// <summary>
    /// Original was GL_SPRITE_SGIX = 0x8148
    /// </summary>
    SpriteSgix = 0x8148,
    /// <summary>
    /// Original was GL_REFERENCE_PLANE_SGIX = 0x817D
    /// </summary>
    ReferencePlaneSgix = 0x817D,
    /// <summary>
    /// Original was GL_IR_INSTRUMENT1_SGIX = 0x817F
    /// </summary>
    IrInstrument1Sgix = 0x817F,
    /// <summary>
    /// Original was GL_CALLIGRAPHIC_FRAGMENT_SGIX = 0x8183
    /// </summary>
    CalligraphicFragmentSgix = 0x8183,
    /// <summary>
    /// Original was GL_FRAMEZOOM_SGIX = 0x818B
    /// </summary>
    FramezoomSgix = 0x818B,
    /// <summary>
    /// Original was GL_FOG_OFFSET_SGIX = 0x8198
    /// </summary>
    FogOffsetSgix = 0x8198,
    /// <summary>
    /// Original was GL_SHARED_TEXTURE_PALETTE_EXT = 0x81FB
    /// </summary>
    SharedTexturePaletteExt = 0x81FB,
    /// <summary>
    /// Original was GL_DEBUG_OUTPUT_SYNCHRONOUS = 0x8242
    /// </summary>
    DebugOutputSynchronous = 0x8242,
    /// <summary>
    /// Original was GL_ASYNC_HISTOGRAM_SGIX = 0x832C
    /// </summary>
    AsyncHistogramSgix = 0x832C,
    /// <summary>
    /// Original was GL_PIXEL_TEXTURE_SGIS = 0x8353
    /// </summary>
    PixelTextureSgis = 0x8353,
    /// <summary>
    /// Original was GL_ASYNC_TEX_IMAGE_SGIX = 0x835C
    /// </summary>
    AsyncTexImageSgix = 0x835C,
    /// <summary>
    /// Original was GL_ASYNC_DRAW_PIXELS_SGIX = 0x835D
    /// </summary>
    AsyncDrawPixelsSgix = 0x835D,
    /// <summary>
    /// Original was GL_ASYNC_READ_PIXELS_SGIX = 0x835E
    /// </summary>
    AsyncReadPixelsSgix = 0x835E,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHTING_SGIX = 0x8400
    /// </summary>
    FragmentLightingSgix = 0x8400,
    /// <summary>
    /// Original was GL_FRAGMENT_COLOR_MATERIAL_SGIX = 0x8401
    /// </summary>
    FragmentColorMaterialSgix = 0x8401,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT0_SGIX = 0x840C
    /// </summary>
    FragmentLight0Sgix = 0x840C,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT1_SGIX = 0x840D
    /// </summary>
    FragmentLight1Sgix = 0x840D,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT2_SGIX = 0x840E
    /// </summary>
    FragmentLight2Sgix = 0x840E,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT3_SGIX = 0x840F
    /// </summary>
    FragmentLight3Sgix = 0x840F,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT4_SGIX = 0x8410
    /// </summary>
    FragmentLight4Sgix = 0x8410,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT5_SGIX = 0x8411
    /// </summary>
    FragmentLight5Sgix = 0x8411,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT6_SGIX = 0x8412
    /// </summary>
    FragmentLight6Sgix = 0x8412,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT7_SGIX = 0x8413
    /// </summary>
    FragmentLight7Sgix = 0x8413,
    /// <summary>
    /// Original was GL_FOG_COORD_ARRAY = 0x8457
    /// </summary>
    FogCoordArray = 0x8457,
    /// <summary>
    /// Original was GL_COLOR_SUM = 0x8458
    /// </summary>
    ColorSum = 0x8458,
    /// <summary>
    /// Original was GL_SECONDARY_COLOR_ARRAY = 0x845E
    /// </summary>
    SecondaryColorArray = 0x845E,
    /// <summary>
    /// Original was GL_TEXTURE_RECTANGLE = 0x84F5
    /// </summary>
    TextureRectangle = 0x84F5,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP = 0x8513
    /// </summary>
    TextureCubeMap = 0x8513,
    /// <summary>
    /// Original was GL_PROGRAM_POINT_SIZE = 0x8642
    /// </summary>
    ProgramPointSize = 0x8642,
    /// <summary>
    /// Original was GL_VERTEX_PROGRAM_POINT_SIZE = 0x8642
    /// </summary>
    VertexProgramPointSize = 0x8642,
    /// <summary>
    /// Original was GL_VERTEX_PROGRAM_TWO_SIDE = 0x8643
    /// </summary>
    VertexProgramTwoSide = 0x8643,
    /// <summary>
    /// Original was GL_DEPTH_CLAMP = 0x864F
    /// </summary>
    DepthClamp = 0x864F,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_SEAMLESS = 0x884F
    /// </summary>
    TextureCubeMapSeamless = 0x884F,
    /// <summary>
    /// Original was GL_POINT_SPRITE = 0x8861
    /// </summary>
    PointSprite = 0x8861,
    /// <summary>
    /// Original was GL_SAMPLE_SHADING = 0x8C36
    /// </summary>
    SampleShading = 0x8C36,
    /// <summary>
    /// Original was GL_RASTERIZER_DISCARD = 0x8C89
    /// </summary>
    RasterizerDiscard = 0x8C89,
    /// <summary>
    /// Original was GL_PRIMITIVE_RESTART_FIXED_INDEX = 0x8D69
    /// </summary>
    PrimitiveRestartFixedIndex = 0x8D69,
    /// <summary>
    /// Original was GL_FRAMEBUFFER_SRGB = 0x8DB9
    /// </summary>
    FramebufferSrgb = 0x8DB9,
    /// <summary>
    /// Original was GL_SAMPLE_MASK = 0x8E51
    /// </summary>
    SampleMask = 0x8E51,
    /// <summary>
    /// Original was GL_PRIMITIVE_RESTART = 0x8F9D
    /// </summary>
    PrimitiveRestart = 0x8F9D,
    /// <summary>
    /// Original was GL_DEBUG_OUTPUT = 0x92E0
    /// </summary>
    DebugOutput = 0x92E0,
}

public enum FramebufferAttachment : int
{
    /// <summary>
    /// Original was GL_FRONT_LEFT = 0x0400
    /// </summary>
    FrontLeft = 0x0400,
    /// <summary>
    /// Original was GL_FRONT_RIGHT = 0x0401
    /// </summary>
    FrontRight = 0x0401,
    /// <summary>
    /// Original was GL_BACK_LEFT = 0x0402
    /// </summary>
    BackLeft = 0x0402,
    /// <summary>
    /// Original was GL_BACK_RIGHT = 0x0403
    /// </summary>
    BackRight = 0x0403,
    /// <summary>
    /// Original was GL_AUX0 = 0x0409
    /// </summary>
    Aux0 = 0x0409,
    /// <summary>
    /// Original was GL_AUX1 = 0x040A
    /// </summary>
    Aux1 = 0x040A,
    /// <summary>
    /// Original was GL_AUX2 = 0x040B
    /// </summary>
    Aux2 = 0x040B,
    /// <summary>
    /// Original was GL_AUX3 = 0x040C
    /// </summary>
    Aux3 = 0x040C,
    /// <summary>
    /// Original was GL_COLOR = 0x1800
    /// </summary>
    Colour = 0x1800,
    /// <summary>
    /// Original was GL_DEPTH = 0x1801
    /// </summary>
    Depth = 0x1801,
    /// <summary>
    /// Original was GL_STENCIL = 0x1802
    /// </summary>
    Stencil = 0x1802,
    /// <summary>
    /// Original was GL_DEPTH_STENCIL_ATTACHMENT = 0x821A
    /// </summary>
    DepthStencilAttachment = 0x821A,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT0 = 0x8CE0
    /// </summary>
    ColourAttachment0 = 0x8CE0,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT0_EXT = 0x8CE0
    /// </summary>
    ColourAttachment0Ext = 0x8CE0,
    /// <summary>
    /// Original was  GL_COLOR_ATTACHMENT1 = 0x8CE1
    /// </summary>
    ColourAttachment1 = 0x8CE1,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT1_EXT = 0x8CE1
    /// </summary>
    ColourAttachment1Ext = 0x8CE1,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT2 = 0x8CE2
    /// </summary>
    ColourAttachment2 = 0x8CE2,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT2_EXT = 0x8CE2
    /// </summary>
    ColourAttachment2Ext = 0x8CE2,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT3 = 0x8CE3
    /// </summary>
    ColourAttachment3 = 0x8CE3,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT3_EXT = 0x8CE3
    /// </summary>
    ColourAttachment3Ext = 0x8CE3,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT4 = 0x8CE4
    /// </summary>
    ColourAttachment4 = 0x8CE4,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT4_EXT = 0x8CE4
    /// </summary>
    ColourAttachment4Ext = 0x8CE4,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT5 = 0x8CE5
    /// </summary>
    ColourAttachment5 = 0x8CE5,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT5_EXT = 0x8CE5
    /// </summary>
    ColourAttachment5Ext = 0x8CE5,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT6 = 0x8CE6
    /// </summary>
    ColourAttachment6 = 0x8CE6,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT6_EXT = 0x8CE6
    /// </summary>
    ColourAttachment6Ext = 0x8CE6,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT7 = 0x8CE7
    /// </summary>
    ColourAttachment7 = 0x8CE7,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT7_EXT = 0x8CE7
    /// </summary>
    ColourAttachment7Ext = 0x8CE7,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT8 = 0x8CE8
    /// </summary>
    ColourAttachment8 = 0x8CE8,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT8_EXT = 0x8CE8
    /// </summary>
    ColourAttachment8Ext = 0x8CE8,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT9 = 0x8CE9
    /// </summary>
    ColourAttachment9 = 0x8CE9,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT9_EXT = 0x8CE9
    /// </summary>
    ColourAttachment9Ext = 0x8CE9,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT10 = 0x8CEA
    /// </summary>
    ColourAttachment10 = 0x8CEA,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT10_EXT = 0x8CEA
    /// </summary>
    ColourAttachment10Ext = 0x8CEA,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT11 = 0x8CEB
    /// </summary>
    ColourAttachment11 = 0x8CEB,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT11_EXT = 0x8CEB
    /// </summary>
    ColourAttachment11Ext = 0x8CEB,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT12 = 0x8CEC
    /// </summary>
    ColourAttachment12 = 0x8CEC,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT12_EXT = 0x8CEC
    /// </summary>
    ColourAttachment12Ext = 0x8CEC,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT13 = 0x8CED
    /// </summary>
    ColourAttachment13 = 0x8CED,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT13_EXT = 0x8CED
    /// </summary>
    ColourAttachment13Ext = 0x8CED,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT14 = 0x8CEE
    /// </summary>
    ColourAttachment14 = 0x8CEE,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT14_EXT = 0x8CEE
    /// </summary>
    ColourAttachment14Ext = 0x8CEE,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT15 = 0x8CEF
    /// </summary>
    ColourAttachment15 = 0x8CEF,
    /// <summary>
    /// Original was GL_COLOR_ATTACHMENT15_EXT = 0x8CEF
    /// </summary>
    ColourAttachment15Ext = 0x8CEF,
    /// <summary>
    /// Original was GL_DEPTH_ATTACHMENT = 0x8D00
    /// </summary>
    DepthAttachment = 0x8D00,
    /// <summary>
    /// Original was GL_DEPTH_ATTACHMENT_EXT = 0x8D00
    /// </summary>
    DepthAttachmentExt = 0x8D00,
    /// <summary>
    /// Original was GL_STENCIL_ATTACHMENT = 0x8D20
    /// </summary>
    StencilAttachment = 0x8D20,
    /// <summary>
    /// Original was GL_STENCIL_ATTACHMENT_EXT = 0x8D20
    /// </summary>
    StencilAttachmentExt = 0x8D20
}

public enum FramebufferTarget : int
{
    /// <summary>
    /// Original was GL_READ_FRAMEBUFFER = 0x8CA8
    /// </summary>
    ReadFramebuffer = 0x8CA8,
    /// <summary>
    /// Original was GL_DRAW_FRAMEBUFFER = 0x8CA9
    /// </summary>
    DrawFramebuffer = 0x8CA9,
    /// <summary>
    /// Original was GL_FRAMEBUFFER = 0x8D40
    /// </summary>
    Framebuffer = 0x8D40,
    /// <summary>
    /// Original was GL_FRAMEBUFFER_EXT = 0x8D40
    /// </summary>
    FramebufferExt = 0x8D40,
}

public enum FrontFaceDirection : int
{
    /// <summary>
    /// Original was GL_CW = 0x0900
    /// </summary>
    Cw = 0x0900,
    /// <summary>
    /// Original was GL_CCW = 0x0901
    /// </summary>
    Ccw = 0x0901,
}

public enum GLEnumPName : int
{
    /// <summary>
    /// Original was GL_CURRENT_COLOR = 0x0B00
    /// </summary>
    CurrentColor = 0x0B00,
    /// <summary>
    /// Original was GL_CURRENT_INDEX = 0x0B01
    /// </summary>
    CurrentIndex = 0x0B01,
    /// <summary>
    /// Original was GL_CURRENT_NORMAL = 0x0B02
    /// </summary>
    CurrentNormal = 0x0B02,
    /// <summary>
    /// Original was GL_CURRENT_TEXTURE_COORDS = 0x0B03
    /// </summary>
    CurrentTextureCoords = 0x0B03,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_COLOR = 0x0B04
    /// </summary>
    CurrentRasterColor = 0x0B04,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_INDEX = 0x0B05
    /// </summary>
    CurrentRasterIndex = 0x0B05,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06
    /// </summary>
    CurrentRasterTextureCoords = 0x0B06,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_POSITION = 0x0B07
    /// </summary>
    CurrentRasterPosition = 0x0B07,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_POSITION_VALID = 0x0B08
    /// </summary>
    CurrentRasterPositionValid = 0x0B08,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_DISTANCE = 0x0B09
    /// </summary>
    CurrentRasterDistance = 0x0B09,
    /// <summary>
    /// Original was GL_POINT_SMOOTH = 0x0B10
    /// </summary>
    PointSmooth = 0x0B10,
    /// <summary>
    /// Original was GL_POINT_SIZE = 0x0B11
    /// </summary>
    PointSize = 0x0B11,
    /// <summary>
    /// Original was GL_POINT_SIZE_RANGE = 0x0B12
    /// </summary>
    PointSizeRange = 0x0B12,
    /// <summary>
    /// Original was GL_SMOOTH_POINT_SIZE_RANGE = 0x0B12
    /// </summary>
    SmoothPointSizeRange = 0x0B12,
    /// <summary>
    /// Original was GL_POINT_SIZE_GRANULARITY = 0x0B13
    /// </summary>
    PointSizeGranularity = 0x0B13,
    /// <summary>
    /// Original was GL_SMOOTH_POINT_SIZE_GRANULARITY = 0x0B13
    /// </summary>
    SmoothPointSizeGranularity = 0x0B13,
    /// <summary>
    /// Original was GL_LINE_SMOOTH = 0x0B20
    /// </summary>
    LineSmooth = 0x0B20,
    /// <summary>
    /// Original was GL_LINE_WIDTH = 0x0B21
    /// </summary>
    LineWidth = 0x0B21,
    /// <summary>
    /// Original was GL_LINE_WIDTH_RANGE = 0x0B22
    /// </summary>
    LineWidthRange = 0x0B22,
    /// <summary>
    /// Original was GL_SMOOTH_LINE_WIDTH_RANGE = 0x0B22
    /// </summary>
    SmoothLineWidthRange = 0x0B22,
    /// <summary>
    /// Original was GL_LINE_WIDTH_GRANULARITY = 0x0B23
    /// </summary>
    LineWidthGranularity = 0x0B23,
    /// <summary>
    /// Original was GL_SMOOTH_LINE_WIDTH_GRANULARITY = 0x0B23
    /// </summary>
    SmoothLineWidthGranularity = 0x0B23,
    /// <summary>
    /// Original was GL_LINE_STIPPLE = 0x0B24
    /// </summary>
    LineStipple = 0x0B24,
    /// <summary>
    /// Original was GL_LINE_STIPPLE_PATTERN = 0x0B25
    /// </summary>
    LineStipplePattern = 0x0B25,
    /// <summary>
    /// Original was GL_LINE_STIPPLE_REPEAT = 0x0B26
    /// </summary>
    LineStippleRepeat = 0x0B26,
    /// <summary>
    /// Original was GL_LIST_MODE = 0x0B30
    /// </summary>
    ListMode = 0x0B30,
    /// <summary>
    /// Original was GL_MAX_LIST_NESTING = 0x0B31
    /// </summary>
    MaxListNesting = 0x0B31,
    /// <summary>
    /// Original was GL_LIST_BASE = 0x0B32
    /// </summary>
    ListBase = 0x0B32,
    /// <summary>
    /// Original was GL_LIST_INDEX = 0x0B33
    /// </summary>
    ListIndex = 0x0B33,
    /// <summary>
    /// Original was GL_POLYGON_MODE = 0x0B40
    /// </summary>
    PolygonMode = 0x0B40,
    /// <summary>
    /// Original was GL_POLYGON_SMOOTH = 0x0B41
    /// </summary>
    PolygonSmooth = 0x0B41,
    /// <summary>
    /// Original was GL_POLYGON_STIPPLE = 0x0B42
    /// </summary>
    PolygonStipple = 0x0B42,
    /// <summary>
    /// Original was GL_EDGE_FLAG = 0x0B43
    /// </summary>
    EdgeFlag = 0x0B43,
    /// <summary>
    /// Original was GL_CULL_FACE = 0x0B44
    /// </summary>
    CullFace = 0x0B44,
    /// <summary>
    /// Original was GL_CULL_FACE_MODE = 0x0B45
    /// </summary>
    CullFaceMode = 0x0B45,
    /// <summary>
    /// Original was GL_FRONT_FACE = 0x0B46
    /// </summary>
    FrontFace = 0x0B46,
    /// <summary>
    /// Original was GL_LIGHTING = 0x0B50
    /// </summary>
    Lighting = 0x0B50,
    /// <summary>
    /// Original was GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51
    /// </summary>
    LightModelLocalViewer = 0x0B51,
    /// <summary>
    /// Original was GL_LIGHT_MODEL_TWO_SIDE = 0x0B52
    /// </summary>
    LightModelTwoSide = 0x0B52,
    /// <summary>
    /// Original was GL_LIGHT_MODEL_AMBIENT = 0x0B53
    /// </summary>
    LightModelAmbient = 0x0B53,
    /// <summary>
    /// Original was GL_SHADE_MODEL = 0x0B54
    /// </summary>
    ShadeModel = 0x0B54,
    /// <summary>
    /// Original was GL_COLOR_MATERIAL_FACE = 0x0B55
    /// </summary>
    ColorMaterialFace = 0x0B55,
    /// <summary>
    /// Original was GL_COLOR_MATERIAL_PARAMETER = 0x0B56
    /// </summary>
    ColorMaterialParameter = 0x0B56,
    /// <summary>
    /// Original was GL_COLOR_MATERIAL = 0x0B57
    /// </summary>
    ColorMaterial = 0x0B57,
    /// <summary>
    /// Original was GL_FOG = 0x0B60
    /// </summary>
    Fog = 0x0B60,
    /// <summary>
    /// Original was GL_FOG_INDEX = 0x0B61
    /// </summary>
    FogIndex = 0x0B61,
    /// <summary>
    /// Original was GL_FOG_DENSITY = 0x0B62
    /// </summary>
    FogDensity = 0x0B62,
    /// <summary>
    /// Original was GL_FOG_START = 0x0B63
    /// </summary>
    FogStart = 0x0B63,
    /// <summary>
    /// Original was GL_FOG_END = 0x0B64
    /// </summary>
    FogEnd = 0x0B64,
    /// <summary>
    /// Original was GL_FOG_MODE = 0x0B65
    /// </summary>
    FogMode = 0x0B65,
    /// <summary>
    /// Original was GL_FOG_COLOR = 0x0B66
    /// </summary>
    FogColor = 0x0B66,
    /// <summary>
    /// Original was GL_DEPTH_RANGE = 0x0B70
    /// </summary>
    DepthRange = 0x0B70,
    /// <summary>
    /// Original was GL_DEPTH_TEST = 0x0B71
    /// </summary>
    DepthTest = 0x0B71,
    /// <summary>
    /// Original was GL_DEPTH_WRITEMASK = 0x0B72
    /// </summary>
    DepthWritemask = 0x0B72,
    /// <summary>
    /// Original was GL_DEPTH_CLEAR_VALUE = 0x0B73
    /// </summary>
    DepthClearValue = 0x0B73,
    /// <summary>
    /// Original was GL_DEPTH_FUNC = 0x0B74
    /// </summary>
    DepthFunc = 0x0B74,
    /// <summary>
    /// Original was GL_ACCUM_CLEAR_VALUE = 0x0B80
    /// </summary>
    AccumClearValue = 0x0B80,
    /// <summary>
    /// Original was GL_STENCIL_TEST = 0x0B90
    /// </summary>
    StencilTest = 0x0B90,
    /// <summary>
    /// Original was GL_STENCIL_CLEAR_VALUE = 0x0B91
    /// </summary>
    StencilClearValue = 0x0B91,
    /// <summary>
    /// Original was GL_STENCIL_FUNC = 0x0B92
    /// </summary>
    StencilFunc = 0x0B92,
    /// <summary>
    /// Original was GL_STENCIL_VALUE_MASK = 0x0B93
    /// </summary>
    StencilValueMask = 0x0B93,
    /// <summary>
    /// Original was GL_STENCIL_FAIL = 0x0B94
    /// </summary>
    StencilFail = 0x0B94,
    /// <summary>
    /// Original was GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95
    /// </summary>
    StencilPassDepthFail = 0x0B95,
    /// <summary>
    /// Original was GL_STENCIL_PASS_DEPTH_PASS = 0x0B96
    /// </summary>
    StencilPassDepthPass = 0x0B96,
    /// <summary>
    /// Original was GL_STENCIL_REF = 0x0B97
    /// </summary>
    StencilRef = 0x0B97,
    /// <summary>
    /// Original was GL_STENCIL_WRITEMASK = 0x0B98
    /// </summary>
    StencilWritemask = 0x0B98,
    /// <summary>
    /// Original was GL_MATRIX_MODE = 0x0BA0
    /// </summary>
    MatrixMode = 0x0BA0,
    /// <summary>
    /// Original was GL_NORMALIZE = 0x0BA1
    /// </summary>
    Normalize = 0x0BA1,
    /// <summary>
    /// Original was GL_VIEWPORT = 0x0BA2
    /// </summary>
    Viewport = 0x0BA2,
    /// <summary>
    /// Original was GL_MODELVIEW0_STACK_DEPTH_EXT = 0x0BA3
    /// </summary>
    Modelview0StackDepthExt = 0x0BA3,
    /// <summary>
    /// Original was GL_MODELVIEW_STACK_DEPTH = 0x0BA3
    /// </summary>
    ModelviewStackDepth = 0x0BA3,
    /// <summary>
    /// Original was GL_PROJECTION_STACK_DEPTH = 0x0BA4
    /// </summary>
    ProjectionStackDepth = 0x0BA4,
    /// <summary>
    /// Original was GL_TEXTURE_STACK_DEPTH = 0x0BA5
    /// </summary>
    TextureStackDepth = 0x0BA5,
    /// <summary>
    /// Original was GL_MODELVIEW0_MATRIX_EXT = 0x0BA6
    /// </summary>
    Modelview0MatrixExt = 0x0BA6,
    /// <summary>
    /// Original was GL_MODELVIEW_MATRIX = 0x0BA6
    /// </summary>
    ModelviewMatrix = 0x0BA6,
    /// <summary>
    /// Original was GL_PROJECTION_MATRIX = 0x0BA7
    /// </summary>
    ProjectionMatrix = 0x0BA7,
    /// <summary>
    /// Original was GL_TEXTURE_MATRIX = 0x0BA8
    /// </summary>
    TextureMatrix = 0x0BA8,
    /// <summary>
    /// Original was GL_ATTRIB_STACK_DEPTH = 0x0BB0
    /// </summary>
    AttribStackDepth = 0x0BB0,
    /// <summary>
    /// Original was GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1
    /// </summary>
    ClientAttribStackDepth = 0x0BB1,
    /// <summary>
    /// Original was GL_ALPHA_TEST = 0x0BC0
    /// </summary>
    AlphaTest = 0x0BC0,
    /// <summary>
    /// Original was GL_ALPHA_TEST_QCOM = 0x0BC0
    /// </summary>
    AlphaTestQcom = 0x0BC0,
    /// <summary>
    /// Original was GL_ALPHA_TEST_FUNC = 0x0BC1
    /// </summary>
    AlphaTestFunc = 0x0BC1,
    /// <summary>
    /// Original was GL_ALPHA_TEST_FUNC_QCOM = 0x0BC1
    /// </summary>
    AlphaTestFuncQcom = 0x0BC1,
    /// <summary>
    /// Original was GL_ALPHA_TEST_REF = 0x0BC2
    /// </summary>
    AlphaTestRef = 0x0BC2,
    /// <summary>
    /// Original was GL_ALPHA_TEST_REF_QCOM = 0x0BC2
    /// </summary>
    AlphaTestRefQcom = 0x0BC2,
    /// <summary>
    /// Original was GL_DITHER = 0x0BD0
    /// </summary>
    Dither = 0x0BD0,
    /// <summary>
    /// Original was GL_BLEND_DST = 0x0BE0
    /// </summary>
    BlendDst = 0x0BE0,
    /// <summary>
    /// Original was GL_BLEND_SRC = 0x0BE1
    /// </summary>
    BlendSrc = 0x0BE1,
    /// <summary>
    /// Original was GL_BLEND = 0x0BE2
    /// </summary>
    Blend = 0x0BE2,
    /// <summary>
    /// Original was GL_LOGIC_OP_MODE = 0x0BF0
    /// </summary>
    LogicOpMode = 0x0BF0,
    /// <summary>
    /// Original was GL_INDEX_LOGIC_OP = 0x0BF1
    /// </summary>
    IndexLogicOp = 0x0BF1,
    /// <summary>
    /// Original was GL_LOGIC_OP = 0x0BF1
    /// </summary>
    LogicOp = 0x0BF1,
    /// <summary>
    /// Original was GL_COLOR_LOGIC_OP = 0x0BF2
    /// </summary>
    ColorLogicOp = 0x0BF2,
    /// <summary>
    /// Original was GL_AUX_BUFFERS = 0x0C00
    /// </summary>
    AuxBuffers = 0x0C00,
    /// <summary>
    /// Original was GL_DRAW_BUFFER = 0x0C01
    /// </summary>
    DrawBuffer = 0x0C01,
    /// <summary>
    /// Original was GL_DRAW_BUFFER_EXT = 0x0C01
    /// </summary>
    DrawBufferExt = 0x0C01,
    /// <summary>
    /// Original was GL_READ_BUFFER = 0x0C02
    /// </summary>
    ReadBuffer = 0x0C02,
    /// <summary>
    /// Original was GL_READ_BUFFER_EXT = 0x0C02
    /// </summary>
    ReadBufferExt = 0x0C02,
    /// <summary>
    /// Original was GL_READ_BUFFER_NV = 0x0C02
    /// </summary>
    ReadBufferNv = 0x0C02,
    /// <summary>
    /// Original was GL_SCISSOR_BOX = 0x0C10
    /// </summary>
    ScissorBox = 0x0C10,
    /// <summary>
    /// Original was GL_SCISSOR_TEST = 0x0C11
    /// </summary>
    ScissorTest = 0x0C11,
    /// <summary>
    /// Original was GL_INDEX_CLEAR_VALUE = 0x0C20
    /// </summary>
    IndexClearValue = 0x0C20,
    /// <summary>
    /// Original was GL_INDEX_WRITEMASK = 0x0C21
    /// </summary>
    IndexWritemask = 0x0C21,
    /// <summary>
    /// Original was GL_COLOR_CLEAR_VALUE = 0x0C22
    /// </summary>
    ColorClearValue = 0x0C22,
    /// <summary>
    /// Original was GL_COLOR_WRITEMASK = 0x0C23
    /// </summary>
    ColorWritemask = 0x0C23,
    /// <summary>
    /// Original was GL_INDEX_MODE = 0x0C30
    /// </summary>
    IndexMode = 0x0C30,
    /// <summary>
    /// Original was GL_RGBA_MODE = 0x0C31
    /// </summary>
    RgbaMode = 0x0C31,
    /// <summary>
    /// Original was GL_DOUBLEBUFFER = 0x0C32
    /// </summary>
    Doublebuffer = 0x0C32,
    /// <summary>
    /// Original was GL_STEREO = 0x0C33
    /// </summary>
    Stereo = 0x0C33,
    /// <summary>
    /// Original was GL_RENDER_MODE = 0x0C40
    /// </summary>
    RenderMode = 0x0C40,
    /// <summary>
    /// Original was GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50
    /// </summary>
    PerspectiveCorrectionHint = 0x0C50,
    /// <summary>
    /// Original was GL_POINT_SMOOTH_HINT = 0x0C51
    /// </summary>
    PointSmoothHint = 0x0C51,
    /// <summary>
    /// Original was GL_LINE_SMOOTH_HINT = 0x0C52
    /// </summary>
    LineSmoothHint = 0x0C52,
    /// <summary>
    /// Original was GL_POLYGON_SMOOTH_HINT = 0x0C53
    /// </summary>
    PolygonSmoothHint = 0x0C53,
    /// <summary>
    /// Original was GL_FOG_HINT = 0x0C54
    /// </summary>
    FogHint = 0x0C54,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_S = 0x0C60
    /// </summary>
    TextureGenS = 0x0C60,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_T = 0x0C61
    /// </summary>
    TextureGenT = 0x0C61,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_R = 0x0C62
    /// </summary>
    TextureGenR = 0x0C62,
    /// <summary>
    /// Original was GL_TEXTURE_GEN_Q = 0x0C63
    /// </summary>
    TextureGenQ = 0x0C63,
    /// <summary>
    /// Original was GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0
    /// </summary>
    PixelMapIToISize = 0x0CB0,
    /// <summary>
    /// Original was GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1
    /// </summary>
    PixelMapSToSSize = 0x0CB1,
    /// <summary>
    /// Original was GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2
    /// </summary>
    PixelMapIToRSize = 0x0CB2,
    /// <summary>
    /// Original was GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3
    /// </summary>
    PixelMapIToGSize = 0x0CB3,
    /// <summary>
    /// Original was GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4
    /// </summary>
    PixelMapIToBSize = 0x0CB4,
    /// <summary>
    /// Original was GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5
    /// </summary>
    PixelMapIToASize = 0x0CB5,
    /// <summary>
    /// Original was GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6
    /// </summary>
    PixelMapRToRSize = 0x0CB6,
    /// <summary>
    /// Original was GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7
    /// </summary>
    PixelMapGToGSize = 0x0CB7,
    /// <summary>
    /// Original was GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8
    /// </summary>
    PixelMapBToBSize = 0x0CB8,
    /// <summary>
    /// Original was GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9
    /// </summary>
    PixelMapAToASize = 0x0CB9,
    /// <summary>
    /// Original was GL_UNPACK_SWAP_BYTES = 0x0CF0
    /// </summary>
    UnpackSwapBytes = 0x0CF0,
    /// <summary>
    /// Original was GL_UNPACK_LSB_FIRST = 0x0CF1
    /// </summary>
    UnpackLsbFirst = 0x0CF1,
    /// <summary>
    /// Original was GL_UNPACK_ROW_LENGTH = 0x0CF2
    /// </summary>
    UnpackRowLength = 0x0CF2,
    /// <summary>
    /// Original was GL_UNPACK_SKIP_ROWS = 0x0CF3
    /// </summary>
    UnpackSkipRows = 0x0CF3,
    /// <summary>
    /// Original was GL_UNPACK_SKIP_PIXELS = 0x0CF4
    /// </summary>
    UnpackSkipPixels = 0x0CF4,
    /// <summary>
    /// Original was GL_UNPACK_ALIGNMENT = 0x0CF5
    /// </summary>
    UnpackAlignment = 0x0CF5,
    /// <summary>
    /// Original was GL_PACK_SWAP_BYTES = 0x0D00
    /// </summary>
    PackSwapBytes = 0x0D00,
    /// <summary>
    /// Original was GL_PACK_LSB_FIRST = 0x0D01
    /// </summary>
    PackLsbFirst = 0x0D01,
    /// <summary>
    /// Original was GL_PACK_ROW_LENGTH = 0x0D02
    /// </summary>
    PackRowLength = 0x0D02,
    /// <summary>
    /// Original was GL_PACK_SKIP_ROWS = 0x0D03
    /// </summary>
    PackSkipRows = 0x0D03,
    /// <summary>
    /// Original was GL_PACK_SKIP_PIXELS = 0x0D04
    /// </summary>
    PackSkipPixels = 0x0D04,
    /// <summary>
    /// Original was GL_PACK_ALIGNMENT = 0x0D05
    /// </summary>
    PackAlignment = 0x0D05,
    /// <summary>
    /// Original was GL_MAP_COLOR = 0x0D10
    /// </summary>
    MapColor = 0x0D10,
    /// <summary>
    /// Original was GL_MAP_STENCIL = 0x0D11
    /// </summary>
    MapStencil = 0x0D11,
    /// <summary>
    /// Original was GL_INDEX_SHIFT = 0x0D12
    /// </summary>
    IndexShift = 0x0D12,
    /// <summary>
    /// Original was GL_INDEX_OFFSET = 0x0D13
    /// </summary>
    IndexOffset = 0x0D13,
    /// <summary>
    /// Original was GL_RED_SCALE = 0x0D14
    /// </summary>
    RedScale = 0x0D14,
    /// <summary>
    /// Original was GL_RED_BIAS = 0x0D15
    /// </summary>
    RedBias = 0x0D15,
    /// <summary>
    /// Original was GL_ZOOM_X = 0x0D16
    /// </summary>
    ZoomX = 0x0D16,
    /// <summary>
    /// Original was GL_ZOOM_Y = 0x0D17
    /// </summary>
    ZoomY = 0x0D17,
    /// <summary>
    /// Original was GL_GREEN_SCALE = 0x0D18
    /// </summary>
    GreenScale = 0x0D18,
    /// <summary>
    /// Original was GL_GREEN_BIAS = 0x0D19
    /// </summary>
    GreenBias = 0x0D19,
    /// <summary>
    /// Original was GL_BLUE_SCALE = 0x0D1A
    /// </summary>
    BlueScale = 0x0D1A,
    /// <summary>
    /// Original was GL_BLUE_BIAS = 0x0D1B
    /// </summary>
    BlueBias = 0x0D1B,
    /// <summary>
    /// Original was GL_ALPHA_SCALE = 0x0D1C
    /// </summary>
    AlphaScale = 0x0D1C,
    /// <summary>
    /// Original was GL_ALPHA_BIAS = 0x0D1D
    /// </summary>
    AlphaBias = 0x0D1D,
    /// <summary>
    /// Original was GL_DEPTH_SCALE = 0x0D1E
    /// </summary>
    DepthScale = 0x0D1E,
    /// <summary>
    /// Original was GL_DEPTH_BIAS = 0x0D1F
    /// </summary>
    DepthBias = 0x0D1F,
    /// <summary>
    /// Original was GL_MAX_EVAL_ORDER = 0x0D30
    /// </summary>
    MaxEvalOrder = 0x0D30,
    /// <summary>
    /// Original was GL_MAX_LIGHTS = 0x0D31
    /// </summary>
    MaxLights = 0x0D31,
    /// <summary>
    /// Original was GL_MAX_CLIP_DISTANCES = 0x0D32
    /// </summary>
    MaxClipDistances = 0x0D32,
    /// <summary>
    /// Original was GL_MAX_CLIP_PLANES = 0x0D32
    /// </summary>
    MaxClipPlanes = 0x0D32,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_SIZE = 0x0D33
    /// </summary>
    MaxTextureSize = 0x0D33,
    /// <summary>
    /// Original was GL_MAX_PIXEL_MAP_TABLE = 0x0D34
    /// </summary>
    MaxPixelMapTable = 0x0D34,
    /// <summary>
    /// Original was GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35
    /// </summary>
    MaxAttribStackDepth = 0x0D35,
    /// <summary>
    /// Original was GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36
    /// </summary>
    MaxModelviewStackDepth = 0x0D36,
    /// <summary>
    /// Original was GL_MAX_NAME_STACK_DEPTH = 0x0D37
    /// </summary>
    MaxNameStackDepth = 0x0D37,
    /// <summary>
    /// Original was GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38
    /// </summary>
    MaxProjectionStackDepth = 0x0D38,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39
    /// </summary>
    MaxTextureStackDepth = 0x0D39,
    /// <summary>
    /// Original was GL_MAX_VIEWPORT_DIMS = 0x0D3A
    /// </summary>
    MaxViewportDims = 0x0D3A,
    /// <summary>
    /// Original was GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B
    /// </summary>
    MaxClientAttribStackDepth = 0x0D3B,
    /// <summary>
    /// Original was GL_SUBPIXEL_BITS = 0x0D50
    /// </summary>
    SubpixelBits = 0x0D50,
    /// <summary>
    /// Original was GL_INDEX_BITS = 0x0D51
    /// </summary>
    IndexBits = 0x0D51,
    /// <summary>
    /// Original was GL_RED_BITS = 0x0D52
    /// </summary>
    RedBits = 0x0D52,
    /// <summary>
    /// Original was GL_GREEN_BITS = 0x0D53
    /// </summary>
    GreenBits = 0x0D53,
    /// <summary>
    /// Original was GL_BLUE_BITS = 0x0D54
    /// </summary>
    BlueBits = 0x0D54,
    /// <summary>
    /// Original was GL_ALPHA_BITS = 0x0D55
    /// </summary>
    AlphaBits = 0x0D55,
    /// <summary>
    /// Original was GL_DEPTH_BITS = 0x0D56
    /// </summary>
    DepthBits = 0x0D56,
    /// <summary>
    /// Original was GL_STENCIL_BITS = 0x0D57
    /// </summary>
    StencilBits = 0x0D57,
    /// <summary>
    /// Original was GL_ACCUM_RED_BITS = 0x0D58
    /// </summary>
    AccumRedBits = 0x0D58,
    /// <summary>
    /// Original was GL_ACCUM_GREEN_BITS = 0x0D59
    /// </summary>
    AccumGreenBits = 0x0D59,
    /// <summary>
    /// Original was GL_ACCUM_BLUE_BITS = 0x0D5A
    /// </summary>
    AccumBlueBits = 0x0D5A,
    /// <summary>
    /// Original was GL_ACCUM_ALPHA_BITS = 0x0D5B
    /// </summary>
    AccumAlphaBits = 0x0D5B,
    /// <summary>
    /// Original was GL_NAME_STACK_DEPTH = 0x0D70
    /// </summary>
    NameStackDepth = 0x0D70,
    /// <summary>
    /// Original was GL_AUTO_NORMAL = 0x0D80
    /// </summary>
    AutoNormal = 0x0D80,
    /// <summary>
    /// Original was GL_MAP1_COLOR_4 = 0x0D90
    /// </summary>
    Map1Color4 = 0x0D90,
    /// <summary>
    /// Original was GL_MAP1_INDEX = 0x0D91
    /// </summary>
    Map1Index = 0x0D91,
    /// <summary>
    /// Original was GL_MAP1_NORMAL = 0x0D92
    /// </summary>
    Map1Normal = 0x0D92,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_1 = 0x0D93
    /// </summary>
    Map1TextureCoord1 = 0x0D93,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_2 = 0x0D94
    /// </summary>
    Map1TextureCoord2 = 0x0D94,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_3 = 0x0D95
    /// </summary>
    Map1TextureCoord3 = 0x0D95,
    /// <summary>
    /// Original was GL_MAP1_TEXTURE_COORD_4 = 0x0D96
    /// </summary>
    Map1TextureCoord4 = 0x0D96,
    /// <summary>
    /// Original was GL_MAP1_VERTEX_3 = 0x0D97
    /// </summary>
    Map1Vertex3 = 0x0D97,
    /// <summary>
    /// Original was GL_MAP1_VERTEX_4 = 0x0D98
    /// </summary>
    Map1Vertex4 = 0x0D98,
    /// <summary>
    /// Original was GL_MAP2_COLOR_4 = 0x0DB0
    /// </summary>
    Map2Color4 = 0x0DB0,
    /// <summary>
    /// Original was GL_MAP2_INDEX = 0x0DB1
    /// </summary>
    Map2Index = 0x0DB1,
    /// <summary>
    /// Original was GL_MAP2_NORMAL = 0x0DB2
    /// </summary>
    Map2Normal = 0x0DB2,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_1 = 0x0DB3
    /// </summary>
    Map2TextureCoord1 = 0x0DB3,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_2 = 0x0DB4
    /// </summary>
    Map2TextureCoord2 = 0x0DB4,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_3 = 0x0DB5
    /// </summary>
    Map2TextureCoord3 = 0x0DB5,
    /// <summary>
    /// Original was GL_MAP2_TEXTURE_COORD_4 = 0x0DB6
    /// </summary>
    Map2TextureCoord4 = 0x0DB6,
    /// <summary>
    /// Original was GL_MAP2_VERTEX_3 = 0x0DB7
    /// </summary>
    Map2Vertex3 = 0x0DB7,
    /// <summary>
    /// Original was GL_MAP2_VERTEX_4 = 0x0DB8
    /// </summary>
    Map2Vertex4 = 0x0DB8,
    /// <summary>
    /// Original was GL_MAP1_GRID_DOMAIN = 0x0DD0
    /// </summary>
    Map1GridDomain = 0x0DD0,
    /// <summary>
    /// Original was GL_MAP1_GRID_SEGMENTS = 0x0DD1
    /// </summary>
    Map1GridSegments = 0x0DD1,
    /// <summary>
    /// Original was GL_MAP2_GRID_DOMAIN = 0x0DD2
    /// </summary>
    Map2GridDomain = 0x0DD2,
    /// <summary>
    /// Original was GL_MAP2_GRID_SEGMENTS = 0x0DD3
    /// </summary>
    Map2GridSegments = 0x0DD3,
    /// <summary>
    /// Original was GL_TEXTURE_1D = 0x0DE0
    /// </summary>
    Texture1D = 0x0DE0,
    /// <summary>
    /// Original was GL_TEXTURE_2D = 0x0DE1
    /// </summary>
    Texture2D = 0x0DE1,
    /// <summary>
    /// Original was GL_FEEDBACK_BUFFER_SIZE = 0x0DF1
    /// </summary>
    FeedbackBufferSize = 0x0DF1,
    /// <summary>
    /// Original was GL_FEEDBACK_BUFFER_TYPE = 0x0DF2
    /// </summary>
    FeedbackBufferType = 0x0DF2,
    /// <summary>
    /// Original was GL_SELECTION_BUFFER_SIZE = 0x0DF4
    /// </summary>
    SelectionBufferSize = 0x0DF4,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_UNITS = 0x2A00
    /// </summary>
    PolygonOffsetUnits = 0x2A00,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_POINT = 0x2A01
    /// </summary>
    PolygonOffsetPoint = 0x2A01,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_LINE = 0x2A02
    /// </summary>
    PolygonOffsetLine = 0x2A02,
    /// <summary>
    /// Original was GL_CLIP_PLANE0 = 0x3000
    /// </summary>
    ClipPlane0 = 0x3000,
    /// <summary>
    /// Original was GL_CLIP_PLANE1 = 0x3001
    /// </summary>
    ClipPlane1 = 0x3001,
    /// <summary>
    /// Original was GL_CLIP_PLANE2 = 0x3002
    /// </summary>
    ClipPlane2 = 0x3002,
    /// <summary>
    /// Original was GL_CLIP_PLANE3 = 0x3003
    /// </summary>
    ClipPlane3 = 0x3003,
    /// <summary>
    /// Original was GL_CLIP_PLANE4 = 0x3004
    /// </summary>
    ClipPlane4 = 0x3004,
    /// <summary>
    /// Original was GL_CLIP_PLANE5 = 0x3005
    /// </summary>
    ClipPlane5 = 0x3005,
    /// <summary>
    /// Original was GL_LIGHT0 = 0x4000
    /// </summary>
    Light0 = 0x4000,
    /// <summary>
    /// Original was GL_LIGHT1 = 0x4001
    /// </summary>
    Light1 = 0x4001,
    /// <summary>
    /// Original was GL_LIGHT2 = 0x4002
    /// </summary>
    Light2 = 0x4002,
    /// <summary>
    /// Original was GL_LIGHT3 = 0x4003
    /// </summary>
    Light3 = 0x4003,
    /// <summary>
    /// Original was GL_LIGHT4 = 0x4004
    /// </summary>
    Light4 = 0x4004,
    /// <summary>
    /// Original was GL_LIGHT5 = 0x4005
    /// </summary>
    Light5 = 0x4005,
    /// <summary>
    /// Original was GL_LIGHT6 = 0x4006
    /// </summary>
    Light6 = 0x4006,
    /// <summary>
    /// Original was GL_LIGHT7 = 0x4007
    /// </summary>
    Light7 = 0x4007,
    /// <summary>
    /// Original was GL_BLEND_COLOR_EXT = 0x8005
    /// </summary>
    BlendColorExt = 0x8005,
    /// <summary>
    /// Original was GL_BLEND_EQUATION_EXT = 0x8009
    /// </summary>
    BlendEquationExt = 0x8009,
    /// <summary>
    /// Original was GL_BLEND_EQUATION_RGB = 0x8009
    /// </summary>
    BlendEquationRgb = 0x8009,
    /// <summary>
    /// Original was GL_PACK_CMYK_HINT_EXT = 0x800E
    /// </summary>
    PackCmykHintExt = 0x800E,
    /// <summary>
    /// Original was GL_UNPACK_CMYK_HINT_EXT = 0x800F
    /// </summary>
    UnpackCmykHintExt = 0x800F,
    /// <summary>
    /// Original was GL_CONVOLUTION_1D_EXT = 0x8010
    /// </summary>
    Convolution1DExt = 0x8010,
    /// <summary>
    /// Original was GL_CONVOLUTION_2D_EXT = 0x8011
    /// </summary>
    Convolution2DExt = 0x8011,
    /// <summary>
    /// Original was GL_SEPARABLE_2D_EXT = 0x8012
    /// </summary>
    Separable2DExt = 0x8012,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_RED_SCALE_EXT = 0x801C
    /// </summary>
    PostConvolutionRedScaleExt = 0x801C,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_GREEN_SCALE_EXT = 0x801D
    /// </summary>
    PostConvolutionGreenScaleExt = 0x801D,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_BLUE_SCALE_EXT = 0x801E
    /// </summary>
    PostConvolutionBlueScaleExt = 0x801E,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_ALPHA_SCALE_EXT = 0x801F
    /// </summary>
    PostConvolutionAlphaScaleExt = 0x801F,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_RED_BIAS_EXT = 0x8020
    /// </summary>
    PostConvolutionRedBiasExt = 0x8020,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_GREEN_BIAS_EXT = 0x8021
    /// </summary>
    PostConvolutionGreenBiasExt = 0x8021,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_BLUE_BIAS_EXT = 0x8022
    /// </summary>
    PostConvolutionBlueBiasExt = 0x8022,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_ALPHA_BIAS_EXT = 0x8023
    /// </summary>
    PostConvolutionAlphaBiasExt = 0x8023,
    /// <summary>
    /// Original was GL_HISTOGRAM_EXT = 0x8024
    /// </summary>
    HistogramExt = 0x8024,
    /// <summary>
    /// Original was GL_MINMAX_EXT = 0x802E
    /// </summary>
    MinmaxExt = 0x802E,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_FILL = 0x8037
    /// </summary>
    PolygonOffsetFill = 0x8037,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_FACTOR = 0x8038
    /// </summary>
    PolygonOffsetFactor = 0x8038,
    /// <summary>
    /// Original was GL_POLYGON_OFFSET_BIAS_EXT = 0x8039
    /// </summary>
    PolygonOffsetBiasExt = 0x8039,
    /// <summary>
    /// Original was GL_RESCALE_NORMAL_EXT = 0x803A
    /// </summary>
    RescaleNormalExt = 0x803A,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_1D = 0x8068
    /// </summary>
    TextureBinding1D = 0x8068,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_2D = 0x8069
    /// </summary>
    TextureBinding2D = 0x8069,
    /// <summary>
    /// Original was GL_TEXTURE_3D_BINDING_EXT = 0x806A
    /// </summary>
    Texture3DBindingExt = 0x806A,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_3D = 0x806A
    /// </summary>
    TextureBinding3D = 0x806A,
    /// <summary>
    /// Original was GL_PACK_SKIP_IMAGES_EXT = 0x806B
    /// </summary>
    PackSkipImagesExt = 0x806B,
    /// <summary>
    /// Original was GL_PACK_IMAGE_HEIGHT_EXT = 0x806C
    /// </summary>
    PackImageHeightExt = 0x806C,
    /// <summary>
    /// Original was GL_UNPACK_SKIP_IMAGES_EXT = 0x806D
    /// </summary>
    UnpackSkipImagesExt = 0x806D,
    /// <summary>
    /// Original was GL_UNPACK_IMAGE_HEIGHT_EXT = 0x806E
    /// </summary>
    UnpackImageHeightExt = 0x806E,
    /// <summary>
    /// Original was GL_TEXTURE_3D_EXT = 0x806F
    /// </summary>
    Texture3DExt = 0x806F,
    /// <summary>
    /// Original was GL_MAX_3D_TEXTURE_SIZE = 0x8073
    /// </summary>
    Max3DTextureSize = 0x8073,
    /// <summary>
    /// Original was GL_MAX_3D_TEXTURE_SIZE_EXT = 0x8073
    /// </summary>
    Max3DTextureSizeExt = 0x8073,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY = 0x8074
    /// </summary>
    VertexArray = 0x8074,
    /// <summary>
    /// Original was GL_NORMAL_ARRAY = 0x8075
    /// </summary>
    NormalArray = 0x8075,
    /// <summary>
    /// Original was GL_COLOR_ARRAY = 0x8076
    /// </summary>
    ColorArray = 0x8076,
    /// <summary>
    /// Original was GL_INDEX_ARRAY = 0x8077
    /// </summary>
    IndexArray = 0x8077,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY = 0x8078
    /// </summary>
    TextureCoordArray = 0x8078,
    /// <summary>
    /// Original was GL_EDGE_FLAG_ARRAY = 0x8079
    /// </summary>
    EdgeFlagArray = 0x8079,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY_SIZE = 0x807A
    /// </summary>
    VertexArraySize = 0x807A,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY_TYPE = 0x807B
    /// </summary>
    VertexArrayType = 0x807B,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY_STRIDE = 0x807C
    /// </summary>
    VertexArrayStride = 0x807C,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY_COUNT_EXT = 0x807D
    /// </summary>
    VertexArrayCountExt = 0x807D,
    /// <summary>
    /// Original was GL_NORMAL_ARRAY_TYPE = 0x807E
    /// </summary>
    NormalArrayType = 0x807E,
    /// <summary>
    /// Original was GL_NORMAL_ARRAY_STRIDE = 0x807F
    /// </summary>
    NormalArrayStride = 0x807F,
    /// <summary>
    /// Original was GL_NORMAL_ARRAY_COUNT_EXT = 0x8080
    /// </summary>
    NormalArrayCountExt = 0x8080,
    /// <summary>
    /// Original was GL_COLOR_ARRAY_SIZE = 0x8081
    /// </summary>
    ColorArraySize = 0x8081,
    /// <summary>
    /// Original was GL_COLOR_ARRAY_TYPE = 0x8082
    /// </summary>
    ColorArrayType = 0x8082,
    /// <summary>
    /// Original was GL_COLOR_ARRAY_STRIDE = 0x8083
    /// </summary>
    ColorArrayStride = 0x8083,
    /// <summary>
    /// Original was GL_COLOR_ARRAY_COUNT_EXT = 0x8084
    /// </summary>
    ColorArrayCountExt = 0x8084,
    /// <summary>
    /// Original was GL_INDEX_ARRAY_TYPE = 0x8085
    /// </summary>
    IndexArrayType = 0x8085,
    /// <summary>
    /// Original was GL_INDEX_ARRAY_STRIDE = 0x8086
    /// </summary>
    IndexArrayStride = 0x8086,
    /// <summary>
    /// Original was GL_INDEX_ARRAY_COUNT_EXT = 0x8087
    /// </summary>
    IndexArrayCountExt = 0x8087,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088
    /// </summary>
    TextureCoordArraySize = 0x8088,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089
    /// </summary>
    TextureCoordArrayType = 0x8089,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A
    /// </summary>
    TextureCoordArrayStride = 0x808A,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B
    /// </summary>
    TextureCoordArrayCountExt = 0x808B,
    /// <summary>
    /// Original was GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C
    /// </summary>
    EdgeFlagArrayStride = 0x808C,
    /// <summary>
    /// Original was GL_EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D
    /// </summary>
    EdgeFlagArrayCountExt = 0x808D,
    /// <summary>
    /// Original was GL_INTERLACE_SGIX = 0x8094
    /// </summary>
    InterlaceSgix = 0x8094,
    /// <summary>
    /// Original was GL_DETAIL_TEXTURE_2D_BINDING_SGIS = 0x8096
    /// </summary>
    DetailTexture2DBindingSgis = 0x8096,
    /// <summary>
    /// Original was GL_MULTISAMPLE = 0x809D
    /// </summary>
    Multisample = 0x809D,
    /// <summary>
    /// Original was GL_MULTISAMPLE_SGIS = 0x809D
    /// </summary>
    MultisampleSgis = 0x809D,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E
    /// </summary>
    SampleAlphaToCoverage = 0x809E,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_MASK_SGIS = 0x809E
    /// </summary>
    SampleAlphaToMaskSgis = 0x809E,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_ONE = 0x809F
    /// </summary>
    SampleAlphaToOne = 0x809F,
    /// <summary>
    /// Original was GL_SAMPLE_ALPHA_TO_ONE_SGIS = 0x809F
    /// </summary>
    SampleAlphaToOneSgis = 0x809F,
    /// <summary>
    /// Original was GL_SAMPLE_COVERAGE = 0x80A0
    /// </summary>
    SampleCoverage = 0x80A0,
    /// <summary>
    /// Original was GL_SAMPLE_MASK_SGIS = 0x80A0
    /// </summary>
    SampleMaskSgis = 0x80A0,
    /// <summary>
    /// Original was GL_SAMPLE_BUFFERS = 0x80A8
    /// </summary>
    SampleBuffers = 0x80A8,
    /// <summary>
    /// Original was GL_SAMPLE_BUFFERS_SGIS = 0x80A8
    /// </summary>
    SampleBuffersSgis = 0x80A8,
    /// <summary>
    /// Original was GL_SAMPLES = 0x80A9
    /// </summary>
    Samples = 0x80A9,
    /// <summary>
    /// Original was GL_SAMPLES_SGIS = 0x80A9
    /// </summary>
    SamplesSgis = 0x80A9,
    /// <summary>
    /// Original was GL_SAMPLE_COVERAGE_VALUE = 0x80AA
    /// </summary>
    SampleCoverageValue = 0x80AA,
    /// <summary>
    /// Original was GL_SAMPLE_MASK_VALUE_SGIS = 0x80AA
    /// </summary>
    SampleMaskValueSgis = 0x80AA,
    /// <summary>
    /// Original was GL_SAMPLE_COVERAGE_INVERT = 0x80AB
    /// </summary>
    SampleCoverageInvert = 0x80AB,
    /// <summary>
    /// Original was GL_SAMPLE_MASK_INVERT_SGIS = 0x80AB
    /// </summary>
    SampleMaskInvertSgis = 0x80AB,
    /// <summary>
    /// Original was GL_SAMPLE_PATTERN_SGIS = 0x80AC
    /// </summary>
    SamplePatternSgis = 0x80AC,
    /// <summary>
    /// Original was GL_COLOR_MATRIX_SGI = 0x80B1
    /// </summary>
    ColorMatrixSgi = 0x80B1,
    /// <summary>
    /// Original was GL_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B2
    /// </summary>
    ColorMatrixStackDepthSgi = 0x80B2,
    /// <summary>
    /// Original was GL_MAX_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B3
    /// </summary>
    MaxColorMatrixStackDepthSgi = 0x80B3,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_RED_SCALE_SGI = 0x80B4
    /// </summary>
    PostColorMatrixRedScaleSgi = 0x80B4,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_GREEN_SCALE_SGI = 0x80B5
    /// </summary>
    PostColorMatrixGreenScaleSgi = 0x80B5,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_BLUE_SCALE_SGI = 0x80B6
    /// </summary>
    PostColorMatrixBlueScaleSgi = 0x80B6,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_ALPHA_SCALE_SGI = 0x80B7
    /// </summary>
    PostColorMatrixAlphaScaleSgi = 0x80B7,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_RED_BIAS_SGI = 0x80B8
    /// </summary>
    PostColorMatrixRedBiasSgi = 0x80B8,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_GREEN_BIAS_SGI = 0x80B9
    /// </summary>
    PostColorMatrixGreenBiasSgi = 0x80B9,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_BLUE_BIAS_SGI = 0x80BA
    /// </summary>
    PostColorMatrixBlueBiasSgi = 0x80BA,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_ALPHA_BIAS_SGI = 0x80BB
    /// </summary>
    PostColorMatrixAlphaBiasSgi = 0x80BB,
    /// <summary>
    /// Original was GL_TEXTURE_COLOR_TABLE_SGI = 0x80BC
    /// </summary>
    TextureColorTableSgi = 0x80BC,
    /// <summary>
    /// Original was GL_BLEND_DST_RGB = 0x80C8
    /// </summary>
    BlendDstRgb = 0x80C8,
    /// <summary>
    /// Original was GL_BLEND_SRC_RGB = 0x80C9
    /// </summary>
    BlendSrcRgb = 0x80C9,
    /// <summary>
    /// Original was GL_BLEND_DST_ALPHA = 0x80CA
    /// </summary>
    BlendDstAlpha = 0x80CA,
    /// <summary>
    /// Original was GL_BLEND_SRC_ALPHA = 0x80CB
    /// </summary>
    BlendSrcAlpha = 0x80CB,
    /// <summary>
    /// Original was GL_COLOR_TABLE_SGI = 0x80D0
    /// </summary>
    ColorTableSgi = 0x80D0,
    /// <summary>
    /// Original was GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1
    /// </summary>
    PostConvolutionColorTableSgi = 0x80D1,
    /// <summary>
    /// Original was GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2
    /// </summary>
    PostColorMatrixColorTableSgi = 0x80D2,
    /// <summary>
    /// Original was GL_MAX_ELEMENTS_VERTICES = 0x80E8
    /// </summary>
    MaxElementsVertices = 0x80E8,
    /// <summary>
    /// Original was GL_MAX_ELEMENTS_INDICES = 0x80E9
    /// </summary>
    MaxElementsIndices = 0x80E9,
    /// <summary>
    /// Original was GL_POINT_SIZE_MIN = 0x8126
    /// </summary>
    PointSizeMin = 0x8126,
    /// <summary>
    /// Original was GL_POINT_SIZE_MIN_SGIS = 0x8126
    /// </summary>
    PointSizeMinSgis = 0x8126,
    /// <summary>
    /// Original was GL_POINT_SIZE_MAX = 0x8127
    /// </summary>
    PointSizeMax = 0x8127,
    /// <summary>
    /// Original was GL_POINT_SIZE_MAX_SGIS = 0x8127
    /// </summary>
    PointSizeMaxSgis = 0x8127,
    /// <summary>
    /// Original was GL_POINT_FADE_THRESHOLD_SIZE = 0x8128
    /// </summary>
    PointFadeThresholdSize = 0x8128,
    /// <summary>
    /// Original was GL_POINT_FADE_THRESHOLD_SIZE_SGIS = 0x8128
    /// </summary>
    PointFadeThresholdSizeSgis = 0x8128,
    /// <summary>
    /// Original was GL_DISTANCE_ATTENUATION_SGIS = 0x8129
    /// </summary>
    DistanceAttenuationSgis = 0x8129,
    /// <summary>
    /// Original was GL_POINT_DISTANCE_ATTENUATION = 0x8129
    /// </summary>
    PointDistanceAttenuation = 0x8129,
    /// <summary>
    /// Original was GL_FOG_FUNC_POINTS_SGIS = 0x812B
    /// </summary>
    FogFuncPointsSgis = 0x812B,
    /// <summary>
    /// Original was GL_MAX_FOG_FUNC_POINTS_SGIS = 0x812C
    /// </summary>
    MaxFogFuncPointsSgis = 0x812C,
    /// <summary>
    /// Original was GL_PACK_SKIP_VOLUMES_SGIS = 0x8130
    /// </summary>
    PackSkipVolumesSgis = 0x8130,
    /// <summary>
    /// Original was GL_PACK_IMAGE_DEPTH_SGIS = 0x8131
    /// </summary>
    PackImageDepthSgis = 0x8131,
    /// <summary>
    /// Original was GL_UNPACK_SKIP_VOLUMES_SGIS = 0x8132
    /// </summary>
    UnpackSkipVolumesSgis = 0x8132,
    /// <summary>
    /// Original was GL_UNPACK_IMAGE_DEPTH_SGIS = 0x8133
    /// </summary>
    UnpackImageDepthSgis = 0x8133,
    /// <summary>
    /// Original was GL_TEXTURE_4D_SGIS = 0x8134
    /// </summary>
    Texture4DSgis = 0x8134,
    /// <summary>
    /// Original was GL_MAX_4D_TEXTURE_SIZE_SGIS = 0x8138
    /// </summary>
    Max4DTextureSizeSgis = 0x8138,
    /// <summary>
    /// Original was GL_PIXEL_TEX_GEN_SGIX = 0x8139
    /// </summary>
    PixelTexGenSgix = 0x8139,
    /// <summary>
    /// Original was GL_PIXEL_TILE_BEST_ALIGNMENT_SGIX = 0x813E
    /// </summary>
    PixelTileBestAlignmentSgix = 0x813E,
    /// <summary>
    /// Original was GL_PIXEL_TILE_CACHE_INCREMENT_SGIX = 0x813F
    /// </summary>
    PixelTileCacheIncrementSgix = 0x813F,
    /// <summary>
    /// Original was GL_PIXEL_TILE_WIDTH_SGIX = 0x8140
    /// </summary>
    PixelTileWidthSgix = 0x8140,
    /// <summary>
    /// Original was GL_PIXEL_TILE_HEIGHT_SGIX = 0x8141
    /// </summary>
    PixelTileHeightSgix = 0x8141,
    /// <summary>
    /// Original was GL_PIXEL_TILE_GRID_WIDTH_SGIX = 0x8142
    /// </summary>
    PixelTileGridWidthSgix = 0x8142,
    /// <summary>
    /// Original was GL_PIXEL_TILE_GRID_HEIGHT_SGIX = 0x8143
    /// </summary>
    PixelTileGridHeightSgix = 0x8143,
    /// <summary>
    /// Original was GL_PIXEL_TILE_GRID_DEPTH_SGIX = 0x8144
    /// </summary>
    PixelTileGridDepthSgix = 0x8144,
    /// <summary>
    /// Original was GL_PIXEL_TILE_CACHE_SIZE_SGIX = 0x8145
    /// </summary>
    PixelTileCacheSizeSgix = 0x8145,
    /// <summary>
    /// Original was GL_SPRITE_SGIX = 0x8148
    /// </summary>
    SpriteSgix = 0x8148,
    /// <summary>
    /// Original was GL_SPRITE_MODE_SGIX = 0x8149
    /// </summary>
    SpriteModeSgix = 0x8149,
    /// <summary>
    /// Original was GL_SPRITE_AXIS_SGIX = 0x814A
    /// </summary>
    SpriteAxisSgix = 0x814A,
    /// <summary>
    /// Original was GL_SPRITE_TRANSLATION_SGIX = 0x814B
    /// </summary>
    SpriteTranslationSgix = 0x814B,
    /// <summary>
    /// Original was GL_TEXTURE_4D_BINDING_SGIS = 0x814F
    /// </summary>
    Texture4DBindingSgis = 0x814F,
    /// <summary>
    /// Original was GL_MAX_CLIPMAP_DEPTH_SGIX = 0x8177
    /// </summary>
    MaxClipmapDepthSgix = 0x8177,
    /// <summary>
    /// Original was GL_MAX_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8178
    /// </summary>
    MaxClipmapVirtualDepthSgix = 0x8178,
    /// <summary>
    /// Original was GL_POST_TEXTURE_FILTER_BIAS_RANGE_SGIX = 0x817B
    /// </summary>
    PostTextureFilterBiasRangeSgix = 0x817B,
    /// <summary>
    /// Original was GL_POST_TEXTURE_FILTER_SCALE_RANGE_SGIX = 0x817C
    /// </summary>
    PostTextureFilterScaleRangeSgix = 0x817C,
    /// <summary>
    /// Original was GL_REFERENCE_PLANE_SGIX = 0x817D
    /// </summary>
    ReferencePlaneSgix = 0x817D,
    /// <summary>
    /// Original was GL_REFERENCE_PLANE_EQUATION_SGIX = 0x817E
    /// </summary>
    ReferencePlaneEquationSgix = 0x817E,
    /// <summary>
    /// Original was GL_IR_INSTRUMENT1_SGIX = 0x817F
    /// </summary>
    IrInstrument1Sgix = 0x817F,
    /// <summary>
    /// Original was GL_INSTRUMENT_MEASUREMENTS_SGIX = 0x8181
    /// </summary>
    InstrumentMeasurementsSgix = 0x8181,
    /// <summary>
    /// Original was GL_CALLIGRAPHIC_FRAGMENT_SGIX = 0x8183
    /// </summary>
    CalligraphicFragmentSgix = 0x8183,
    /// <summary>
    /// Original was GL_FRAMEZOOM_SGIX = 0x818B
    /// </summary>
    FramezoomSgix = 0x818B,
    /// <summary>
    /// Original was GL_FRAMEZOOM_FACTOR_SGIX = 0x818C
    /// </summary>
    FramezoomFactorSgix = 0x818C,
    /// <summary>
    /// Original was GL_MAX_FRAMEZOOM_FACTOR_SGIX = 0x818D
    /// </summary>
    MaxFramezoomFactorSgix = 0x818D,
    /// <summary>
    /// Original was GL_GENERATE_MIPMAP_HINT = 0x8192
    /// </summary>
    GenerateMipmapHint = 0x8192,
    /// <summary>
    /// Original was GL_GENERATE_MIPMAP_HINT_SGIS = 0x8192
    /// </summary>
    GenerateMipmapHintSgis = 0x8192,
    /// <summary>
    /// Original was GL_DEFORMATIONS_MASK_SGIX = 0x8196
    /// </summary>
    DeformationsMaskSgix = 0x8196,
    /// <summary>
    /// Original was GL_FOG_OFFSET_SGIX = 0x8198
    /// </summary>
    FogOffsetSgix = 0x8198,
    /// <summary>
    /// Original was GL_FOG_OFFSET_VALUE_SGIX = 0x8199
    /// </summary>
    FogOffsetValueSgix = 0x8199,
    /// <summary>
    /// Original was GL_LIGHT_MODEL_COLOR_CONTROL = 0x81F8
    /// </summary>
    LightModelColorControl = 0x81F8,
    /// <summary>
    /// Original was GL_SHARED_TEXTURE_PALETTE_EXT = 0x81FB
    /// </summary>
    SharedTexturePaletteExt = 0x81FB,
    /// <summary>
    /// Original was GL_MAJOR_VERSION = 0x821B
    /// </summary>
    MajorVersion = 0x821B,
    /// <summary>
    /// Original was GL_MINOR_VERSION = 0x821C
    /// </summary>
    MinorVersion = 0x821C,
    /// <summary>
    /// Original was GL_NUM_EXTENSIONS = 0x821D
    /// </summary>
    NumExtensions = 0x821D,
    /// <summary>
    /// Original was GL_CONTEXT_FLAGS = 0x821E
    /// </summary>
    ContextFlags = 0x821E,
    /// <summary>
    /// Original was GL_PROGRAM_PIPELINE_BINDING = 0x825A
    /// </summary>
    ProgramPipelineBinding = 0x825A,
    /// <summary>
    /// Original was GL_MAX_VIEWPORTS = 0x825B
    /// </summary>
    MaxViewports = 0x825B,
    /// <summary>
    /// Original was GL_VIEWPORT_SUBPIXEL_BITS = 0x825C
    /// </summary>
    ViewportSubpixelBits = 0x825C,
    /// <summary>
    /// Original was GL_VIEWPORT_BOUNDS_RANGE = 0x825D
    /// </summary>
    ViewportBoundsRange = 0x825D,
    /// <summary>
    /// Original was GL_LAYER_PROVOKING_VERTEX = 0x825E
    /// </summary>
    LayerProvokingVertex = 0x825E,
    /// <summary>
    /// Original was GL_VIEWPORT_INDEX_PROVOKING_VERTEX = 0x825F
    /// </summary>
    ViewportIndexProvokingVertex = 0x825F,
    /// <summary>
    /// Original was GL_CONVOLUTION_HINT_SGIX = 0x8316
    /// </summary>
    ConvolutionHintSgix = 0x8316,
    /// <summary>
    /// Original was GL_ASYNC_MARKER_SGIX = 0x8329
    /// </summary>
    AsyncMarkerSgix = 0x8329,
    /// <summary>
    /// Original was GL_PIXEL_TEX_GEN_MODE_SGIX = 0x832B
    /// </summary>
    PixelTexGenModeSgix = 0x832B,
    /// <summary>
    /// Original was GL_ASYNC_HISTOGRAM_SGIX = 0x832C
    /// </summary>
    AsyncHistogramSgix = 0x832C,
    /// <summary>
    /// Original was GL_MAX_ASYNC_HISTOGRAM_SGIX = 0x832D
    /// </summary>
    MaxAsyncHistogramSgix = 0x832D,
    /// <summary>
    /// Original was GL_PIXEL_TEXTURE_SGIS = 0x8353
    /// </summary>
    PixelTextureSgis = 0x8353,
    /// <summary>
    /// Original was GL_ASYNC_TEX_IMAGE_SGIX = 0x835C
    /// </summary>
    AsyncTexImageSgix = 0x835C,
    /// <summary>
    /// Original was GL_ASYNC_DRAW_PIXELS_SGIX = 0x835D
    /// </summary>
    AsyncDrawPixelsSgix = 0x835D,
    /// <summary>
    /// Original was GL_ASYNC_READ_PIXELS_SGIX = 0x835E
    /// </summary>
    AsyncReadPixelsSgix = 0x835E,
    /// <summary>
    /// Original was GL_MAX_ASYNC_TEX_IMAGE_SGIX = 0x835F
    /// </summary>
    MaxAsyncTexImageSgix = 0x835F,
    /// <summary>
    /// Original was GL_MAX_ASYNC_DRAW_PIXELS_SGIX = 0x8360
    /// </summary>
    MaxAsyncDrawPixelsSgix = 0x8360,
    /// <summary>
    /// Original was GL_MAX_ASYNC_READ_PIXELS_SGIX = 0x8361
    /// </summary>
    MaxAsyncReadPixelsSgix = 0x8361,
    /// <summary>
    /// Original was GL_VERTEX_PRECLIP_SGIX = 0x83EE
    /// </summary>
    VertexPreclipSgix = 0x83EE,
    /// <summary>
    /// Original was GL_VERTEX_PRECLIP_HINT_SGIX = 0x83EF
    /// </summary>
    VertexPreclipHintSgix = 0x83EF,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHTING_SGIX = 0x8400
    /// </summary>
    FragmentLightingSgix = 0x8400,
    /// <summary>
    /// Original was GL_FRAGMENT_COLOR_MATERIAL_SGIX = 0x8401
    /// </summary>
    FragmentColorMaterialSgix = 0x8401,
    /// <summary>
    /// Original was GL_FRAGMENT_COLOR_MATERIAL_FACE_SGIX = 0x8402
    /// </summary>
    FragmentColorMaterialFaceSgix = 0x8402,
    /// <summary>
    /// Original was GL_FRAGMENT_COLOR_MATERIAL_PARAMETER_SGIX = 0x8403
    /// </summary>
    FragmentColorMaterialParameterSgix = 0x8403,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_LIGHTS_SGIX = 0x8404
    /// </summary>
    MaxFragmentLightsSgix = 0x8404,
    /// <summary>
    /// Original was GL_MAX_ACTIVE_LIGHTS_SGIX = 0x8405
    /// </summary>
    MaxActiveLightsSgix = 0x8405,
    /// <summary>
    /// Original was GL_LIGHT_ENV_MODE_SGIX = 0x8407
    /// </summary>
    LightEnvModeSgix = 0x8407,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT_MODEL_LOCAL_VIEWER_SGIX = 0x8408
    /// </summary>
    FragmentLightModelLocalViewerSgix = 0x8408,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT_MODEL_TWO_SIDE_SGIX = 0x8409
    /// </summary>
    FragmentLightModelTwoSideSgix = 0x8409,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT_MODEL_AMBIENT_SGIX = 0x840A
    /// </summary>
    FragmentLightModelAmbientSgix = 0x840A,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT_MODEL_NORMAL_INTERPOLATION_SGIX = 0x840B
    /// </summary>
    FragmentLightModelNormalInterpolationSgix = 0x840B,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT0_SGIX = 0x840C
    /// </summary>
    FragmentLight0Sgix = 0x840C,
    /// <summary>
    /// Original was GL_PACK_RESAMPLE_SGIX = 0x842C
    /// </summary>
    PackResampleSgix = 0x842C,
    /// <summary>
    /// Original was GL_UNPACK_RESAMPLE_SGIX = 0x842D
    /// </summary>
    UnpackResampleSgix = 0x842D,
    /// <summary>
    /// Original was GL_CURRENT_FOG_COORD = 0x8453
    /// </summary>
    CurrentFogCoord = 0x8453,
    /// <summary>
    /// Original was GL_FOG_COORD_ARRAY_TYPE = 0x8454
    /// </summary>
    FogCoordArrayType = 0x8454,
    /// <summary>
    /// Original was GL_FOG_COORD_ARRAY_STRIDE = 0x8455
    /// </summary>
    FogCoordArrayStride = 0x8455,
    /// <summary>
    /// Original was GL_COLOR_SUM = 0x8458
    /// </summary>
    ColorSum = 0x8458,
    /// <summary>
    /// Original was GL_CURRENT_SECONDARY_COLOR = 0x8459
    /// </summary>
    CurrentSecondaryColor = 0x8459,
    /// <summary>
    /// Original was GL_SECONDARY_COLOR_ARRAY_SIZE = 0x845A
    /// </summary>
    SecondaryColorArraySize = 0x845A,
    /// <summary>
    /// Original was GL_SECONDARY_COLOR_ARRAY_TYPE = 0x845B
    /// </summary>
    SecondaryColorArrayType = 0x845B,
    /// <summary>
    /// Original was GL_SECONDARY_COLOR_ARRAY_STRIDE = 0x845C
    /// </summary>
    SecondaryColorArrayStride = 0x845C,
    /// <summary>
    /// Original was GL_CURRENT_RASTER_SECONDARY_COLOR = 0x845F
    /// </summary>
    CurrentRasterSecondaryColor = 0x845F,
    /// <summary>
    /// Original was GL_ALIASED_POINT_SIZE_RANGE = 0x846D
    /// </summary>
    AliasedPointSizeRange = 0x846D,
    /// <summary>
    /// Original was GL_ALIASED_LINE_WIDTH_RANGE = 0x846E
    /// </summary>
    AliasedLineWidthRange = 0x846E,
    /// <summary>
    /// Original was GL_ACTIVE_TEXTURE = 0x84E0
    /// </summary>
    ActiveTexture = 0x84E0,
    /// <summary>
    /// Original was GL_CLIENT_ACTIVE_TEXTURE = 0x84E1
    /// </summary>
    ClientActiveTexture = 0x84E1,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_UNITS = 0x84E2
    /// </summary>
    MaxTextureUnits = 0x84E2,
    /// <summary>
    /// Original was GL_TRANSPOSE_MODELVIEW_MATRIX = 0x84E3
    /// </summary>
    TransposeModelviewMatrix = 0x84E3,
    /// <summary>
    /// Original was GL_TRANSPOSE_PROJECTION_MATRIX = 0x84E4
    /// </summary>
    TransposeProjectionMatrix = 0x84E4,
    /// <summary>
    /// Original was GL_TRANSPOSE_TEXTURE_MATRIX = 0x84E5
    /// </summary>
    TransposeTextureMatrix = 0x84E5,
    /// <summary>
    /// Original was GL_TRANSPOSE_COLOR_MATRIX = 0x84E6
    /// </summary>
    TransposeColorMatrix = 0x84E6,
    /// <summary>
    /// Original was GL_MAX_RENDERBUFFER_SIZE = 0x84E8
    /// </summary>
    MaxRenderbufferSize = 0x84E8,
    /// <summary>
    /// Original was GL_MAX_RENDERBUFFER_SIZE_EXT = 0x84E8
    /// </summary>
    MaxRenderbufferSizeExt = 0x84E8,
    /// <summary>
    /// Original was GL_TEXTURE_COMPRESSION_HINT = 0x84EF
    /// </summary>
    TextureCompressionHint = 0x84EF,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_RECTANGLE = 0x84F6
    /// </summary>
    TextureBindingRectangle = 0x84F6,
    /// <summary>
    /// Original was GL_MAX_RECTANGLE_TEXTURE_SIZE = 0x84F8
    /// </summary>
    MaxRectangleTextureSize = 0x84F8,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_LOD_BIAS = 0x84FD
    /// </summary>
    MaxTextureLodBias = 0x84FD,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP = 0x8513
    /// </summary>
    TextureCubeMap = 0x8513,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_CUBE_MAP = 0x8514
    /// </summary>
    TextureBindingCubeMap = 0x8514,
    /// <summary>
    /// Original was GL_MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C
    /// </summary>
    MaxCubeMapTextureSize = 0x851C,
    /// <summary>
    /// Original was GL_PACK_SUBSAMPLE_RATE_SGIX = 0x85A0
    /// </summary>
    PackSubsampleRateSgix = 0x85A0,
    /// <summary>
    /// Original was GL_UNPACK_SUBSAMPLE_RATE_SGIX = 0x85A1
    /// </summary>
    UnpackSubsampleRateSgix = 0x85A1,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY_BINDING = 0x85B5
    /// </summary>
    VertexArrayBinding = 0x85B5,
    /// <summary>
    /// Original was GL_PROGRAM_POINT_SIZE = 0x8642
    /// </summary>
    ProgramPointSize = 0x8642,
    /// <summary>
    /// Original was GL_DEPTH_CLAMP = 0x864F
    /// </summary>
    DepthClamp = 0x864F,
    /// <summary>
    /// Original was GL_NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2
    /// </summary>
    NumCompressedTextureFormats = 0x86A2,
    /// <summary>
    /// Original was GL_COMPRESSED_TEXTURE_FORMATS = 0x86A3
    /// </summary>
    CompressedTextureFormats = 0x86A3,
    /// <summary>
    /// Original was GL_NUM_PROGRAM_BINARY_FORMATS = 0x87FE
    /// </summary>
    NumProgramBinaryFormats = 0x87FE,
    /// <summary>
    /// Original was GL_PROGRAM_BINARY_FORMATS = 0x87FF
    /// </summary>
    ProgramBinaryFormats = 0x87FF,
    /// <summary>
    /// Original was GL_STENCIL_BACK_FUNC = 0x8800
    /// </summary>
    StencilBackFunc = 0x8800,
    /// <summary>
    /// Original was GL_STENCIL_BACK_FAIL = 0x8801
    /// </summary>
    StencilBackFail = 0x8801,
    /// <summary>
    /// Original was GL_STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802
    /// </summary>
    StencilBackPassDepthFail = 0x8802,
    /// <summary>
    /// Original was GL_STENCIL_BACK_PASS_DEPTH_PASS = 0x8803
    /// </summary>
    StencilBackPassDepthPass = 0x8803,
    /// <summary>
    /// Original was GL_RGBA_FLOAT_MODE = 0x8820
    /// </summary>
    RgbaFloatMode = 0x8820,
    /// <summary>
    /// Original was GL_MAX_DRAW_BUFFERS = 0x8824
    /// </summary>
    MaxDrawBuffers = 0x8824,
    /// <summary>
    /// Original was GL_DRAW_BUFFER0 = 0x8825
    /// </summary>
    DrawBuffer0 = 0x8825,
    /// <summary>
    /// Original was GL_DRAW_BUFFER1 = 0x8826
    /// </summary>
    DrawBuffer1 = 0x8826,
    /// <summary>
    /// Original was GL_DRAW_BUFFER2 = 0x8827
    /// </summary>
    DrawBuffer2 = 0x8827,
    /// <summary>
    /// Original was GL_DRAW_BUFFER3 = 0x8828
    /// </summary>
    DrawBuffer3 = 0x8828,
    /// <summary>
    /// Original was GL_DRAW_BUFFER4 = 0x8829
    /// </summary>
    DrawBuffer4 = 0x8829,
    /// <summary>
    /// Original was GL_DRAW_BUFFER5 = 0x882A
    /// </summary>
    DrawBuffer5 = 0x882A,
    /// <summary>
    /// Original was GL_DRAW_BUFFER6 = 0x882B
    /// </summary>
    DrawBuffer6 = 0x882B,
    /// <summary>
    /// Original was GL_DRAW_BUFFER7 = 0x882C
    /// </summary>
    DrawBuffer7 = 0x882C,
    /// <summary>
    /// Original was GL_DRAW_BUFFER8 = 0x882D
    /// </summary>
    DrawBuffer8 = 0x882D,
    /// <summary>
    /// Original was GL_DRAW_BUFFER9 = 0x882E
    /// </summary>
    DrawBuffer9 = 0x882E,
    /// <summary>
    /// Original was GL_DRAW_BUFFER10 = 0x882F
    /// </summary>
    DrawBuffer10 = 0x882F,
    /// <summary>
    /// Original was GL_DRAW_BUFFER11 = 0x8830
    /// </summary>
    DrawBuffer11 = 0x8830,
    /// <summary>
    /// Original was GL_DRAW_BUFFER12 = 0x8831
    /// </summary>
    DrawBuffer12 = 0x8831,
    /// <summary>
    /// Original was GL_DRAW_BUFFER13 = 0x8832
    /// </summary>
    DrawBuffer13 = 0x8832,
    /// <summary>
    /// Original was GL_DRAW_BUFFER14 = 0x8833
    /// </summary>
    DrawBuffer14 = 0x8833,
    /// <summary>
    /// Original was GL_DRAW_BUFFER15 = 0x8834
    /// </summary>
    DrawBuffer15 = 0x8834,
    /// <summary>
    /// Original was GL_BLEND_EQUATION_ALPHA = 0x883D
    /// </summary>
    BlendEquationAlpha = 0x883D,
    /// <summary>
    /// Original was GL_TEXTURE_CUBE_MAP_SEAMLESS = 0x884F
    /// </summary>
    TextureCubeMapSeamless = 0x884F,
    /// <summary>
    /// Original was GL_POINT_SPRITE = 0x8861
    /// </summary>
    PointSprite = 0x8861,
    /// <summary>
    /// Original was GL_MAX_VERTEX_ATTRIBS = 0x8869
    /// </summary>
    MaxVertexAttribs = 0x8869,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_INPUT_COMPONENTS = 0x886C
    /// </summary>
    MaxTessControlInputComponents = 0x886C,
    /// <summary>
    /// Original was GL_MAX_TESS_EVALUATION_INPUT_COMPONENTS = 0x886D
    /// </summary>
    MaxTessEvaluationInputComponents = 0x886D,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_COORDS = 0x8871
    /// </summary>
    MaxTextureCoords = 0x8871,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_IMAGE_UNITS = 0x8872
    /// </summary>
    MaxTextureImageUnits = 0x8872,
    /// <summary>
    /// Original was GL_ARRAY_BUFFER_BINDING = 0x8894
    /// </summary>
    ArrayBufferBinding = 0x8894,
    /// <summary>
    /// Original was GL_ELEMENT_ARRAY_BUFFER_BINDING = 0x8895
    /// </summary>
    ElementArrayBufferBinding = 0x8895,
    /// <summary>
    /// Original was GL_VERTEX_ARRAY_BUFFER_BINDING = 0x8896
    /// </summary>
    VertexArrayBufferBinding = 0x8896,
    /// <summary>
    /// Original was GL_NORMAL_ARRAY_BUFFER_BINDING = 0x8897
    /// </summary>
    NormalArrayBufferBinding = 0x8897,
    /// <summary>
    /// Original was GL_COLOR_ARRAY_BUFFER_BINDING = 0x8898
    /// </summary>
    ColorArrayBufferBinding = 0x8898,
    /// <summary>
    /// Original was GL_INDEX_ARRAY_BUFFER_BINDING = 0x8899
    /// </summary>
    IndexArrayBufferBinding = 0x8899,
    /// <summary>
    /// Original was GL_TEXTURE_COORD_ARRAY_BUFFER_BINDING = 0x889A
    /// </summary>
    TextureCoordArrayBufferBinding = 0x889A,
    /// <summary>
    /// Original was GL_EDGE_FLAG_ARRAY_BUFFER_BINDING = 0x889B
    /// </summary>
    EdgeFlagArrayBufferBinding = 0x889B,
    /// <summary>
    /// Original was GL_SECONDARY_COLOR_ARRAY_BUFFER_BINDING = 0x889C
    /// </summary>
    SecondaryColorArrayBufferBinding = 0x889C,
    /// <summary>
    /// Original was GL_FOG_COORD_ARRAY_BUFFER_BINDING = 0x889D
    /// </summary>
    FogCoordArrayBufferBinding = 0x889D,
    /// <summary>
    /// Original was GL_WEIGHT_ARRAY_BUFFER_BINDING = 0x889E
    /// </summary>
    WeightArrayBufferBinding = 0x889E,
    /// <summary>
    /// Original was GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F
    /// </summary>
    VertexAttribArrayBufferBinding = 0x889F,
    /// <summary>
    /// Original was GL_PIXEL_PACK_BUFFER_BINDING = 0x88ED
    /// </summary>
    PixelPackBufferBinding = 0x88ED,
    /// <summary>
    /// Original was GL_PIXEL_UNPACK_BUFFER_BINDING = 0x88EF
    /// </summary>
    PixelUnpackBufferBinding = 0x88EF,
    /// <summary>
    /// Original was GL_MAX_DUAL_SOURCE_DRAW_BUFFERS = 0x88FC
    /// </summary>
    MaxDualSourceDrawBuffers = 0x88FC,
    /// <summary>
    /// Original was GL_MAX_ARRAY_TEXTURE_LAYERS = 0x88FF
    /// </summary>
    MaxArrayTextureLayers = 0x88FF,
    /// <summary>
    /// Original was GL_MIN_PROGRAM_TEXEL_OFFSET = 0x8904
    /// </summary>
    MinProgramTexelOffset = 0x8904,
    /// <summary>
    /// Original was GL_MAX_PROGRAM_TEXEL_OFFSET = 0x8905
    /// </summary>
    MaxProgramTexelOffset = 0x8905,
    /// <summary>
    /// Original was GL_SAMPLER_BINDING = 0x8919
    /// </summary>
    SamplerBinding = 0x8919,
    /// <summary>
    /// Original was GL_CLAMP_VERTEX_COLOR = 0x891A
    /// </summary>
    ClampVertexColor = 0x891A,
    /// <summary>
    /// Original was GL_CLAMP_FRAGMENT_COLOR = 0x891B
    /// </summary>
    ClampFragmentColor = 0x891B,
    /// <summary>
    /// Original was GL_CLAMP_READ_COLOR = 0x891C
    /// </summary>
    ClampReadColor = 0x891C,
    /// <summary>
    /// Original was GL_MAX_VERTEX_UNIFORM_BLOCKS = 0x8A2B
    /// </summary>
    MaxVertexUniformBlocks = 0x8A2B,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_UNIFORM_BLOCKS = 0x8A2C
    /// </summary>
    MaxGeometryUniformBlocks = 0x8A2C,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_UNIFORM_BLOCKS = 0x8A2D
    /// </summary>
    MaxFragmentUniformBlocks = 0x8A2D,
    /// <summary>
    /// Original was GL_MAX_COMBINED_UNIFORM_BLOCKS = 0x8A2E
    /// </summary>
    MaxCombinedUniformBlocks = 0x8A2E,
    /// <summary>
    /// Original was GL_MAX_UNIFORM_BUFFER_BINDINGS = 0x8A2F
    /// </summary>
    MaxUniformBufferBindings = 0x8A2F,
    /// <summary>
    /// Original was GL_MAX_UNIFORM_BLOCK_SIZE = 0x8A30
    /// </summary>
    MaxUniformBlockSize = 0x8A30,
    /// <summary>
    /// Original was GL_MAX_COMBINED_VERTEX_UNIFORM_COMPONENTS = 0x8A31
    /// </summary>
    MaxCombinedVertexUniformComponents = 0x8A31,
    /// <summary>
    /// Original was GL_MAX_COMBINED_GEOMETRY_UNIFORM_COMPONENTS = 0x8A32
    /// </summary>
    MaxCombinedGeometryUniformComponents = 0x8A32,
    /// <summary>
    /// Original was GL_MAX_COMBINED_FRAGMENT_UNIFORM_COMPONENTS = 0x8A33
    /// </summary>
    MaxCombinedFragmentUniformComponents = 0x8A33,
    /// <summary>
    /// Original was GL_UNIFORM_BUFFER_OFFSET_ALIGNMENT = 0x8A34
    /// </summary>
    UniformBufferOffsetAlignment = 0x8A34,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_UNIFORM_COMPONENTS = 0x8B49
    /// </summary>
    MaxFragmentUniformComponents = 0x8B49,
    /// <summary>
    /// Original was GL_MAX_VERTEX_UNIFORM_COMPONENTS = 0x8B4A
    /// </summary>
    MaxVertexUniformComponents = 0x8B4A,
    /// <summary>
    /// Original was GL_MAX_VARYING_COMPONENTS = 0x8B4B
    /// </summary>
    MaxVaryingComponents = 0x8B4B,
    /// <summary>
    /// Original was GL_MAX_VARYING_FLOATS = 0x8B4B
    /// </summary>
    MaxVaryingFloats = 0x8B4B,
    /// <summary>
    /// Original was GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C
    /// </summary>
    MaxVertexTextureImageUnits = 0x8B4C,
    /// <summary>
    /// Original was GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D
    /// </summary>
    MaxCombinedTextureImageUnits = 0x8B4D,
    /// <summary>
    /// Original was GL_FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B
    /// </summary>
    FragmentShaderDerivativeHint = 0x8B8B,
    /// <summary>
    /// Original was GL_CURRENT_PROGRAM = 0x8B8D
    /// </summary>
    CurrentProgram = 0x8B8D,
    /// <summary>
    /// Original was GL_IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A
    /// </summary>
    ImplementationColorReadType = 0x8B9A,
    /// <summary>
    /// Original was GL_IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B
    /// </summary>
    ImplementationColorReadFormat = 0x8B9B,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_1D_ARRAY = 0x8C1C
    /// </summary>
    TextureBinding1DArray = 0x8C1C,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_2D_ARRAY = 0x8C1D
    /// </summary>
    TextureBinding2DArray = 0x8C1D,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS = 0x8C29
    /// </summary>
    MaxGeometryTextureImageUnits = 0x8C29,
    /// <summary>
    /// Original was GL_TEXTURE_BUFFER = 0x8C2A
    /// </summary>
    TextureBuffer = 0x8C2A,
    /// <summary>
    /// Original was GL_MAX_TEXTURE_BUFFER_SIZE = 0x8C2B
    /// </summary>
    MaxTextureBufferSize = 0x8C2B,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_BUFFER = 0x8C2C
    /// </summary>
    TextureBindingBuffer = 0x8C2C,
    /// <summary>
    /// Original was GL_TEXTURE_BUFFER_DATA_STORE_BINDING = 0x8C2D
    /// </summary>
    TextureBufferDataStoreBinding = 0x8C2D,
    /// <summary>
    /// Original was GL_SAMPLE_SHADING = 0x8C36
    /// </summary>
    SampleShading = 0x8C36,
    /// <summary>
    /// Original was GL_MIN_SAMPLE_SHADING_VALUE = 0x8C37
    /// </summary>
    MinSampleShadingValue = 0x8C37,
    /// <summary>
    /// Original was GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS = 0x8C80
    /// </summary>
    MaxTransformFeedbackSeparateComponents = 0x8C80,
    /// <summary>
    /// Original was GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A
    /// </summary>
    MaxTransformFeedbackInterleavedComponents = 0x8C8A,
    /// <summary>
    /// Original was GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS = 0x8C8B
    /// </summary>
    MaxTransformFeedbackSeparateAttribs = 0x8C8B,
    /// <summary>
    /// Original was GL_STENCIL_BACK_REF = 0x8CA3
    /// </summary>
    StencilBackRef = 0x8CA3,
    /// <summary>
    /// Original was GL_STENCIL_BACK_VALUE_MASK = 0x8CA4
    /// </summary>
    StencilBackValueMask = 0x8CA4,
    /// <summary>
    /// Original was GL_STENCIL_BACK_WRITEMASK = 0x8CA5
    /// </summary>
    StencilBackWritemask = 0x8CA5,
    /// <summary>
    /// Original was GL_DRAW_FRAMEBUFFER_BINDING = 0x8CA6
    /// </summary>
    DrawFramebufferBinding = 0x8CA6,
    /// <summary>
    /// Original was GL_FRAMEBUFFER_BINDING = 0x8CA6
    /// </summary>
    FramebufferBinding = 0x8CA6,
    /// <summary>
    /// Original was GL_FRAMEBUFFER_BINDING_EXT = 0x8CA6
    /// </summary>
    FramebufferBindingExt = 0x8CA6,
    /// <summary>
    /// Original was GL_RENDERBUFFER_BINDING = 0x8CA7
    /// </summary>
    RenderbufferBinding = 0x8CA7,
    /// <summary>
    /// Original was GL_RENDERBUFFER_BINDING_EXT = 0x8CA7
    /// </summary>
    RenderbufferBindingExt = 0x8CA7,
    /// <summary>
    /// Original was GL_READ_FRAMEBUFFER_BINDING = 0x8CAA
    /// </summary>
    ReadFramebufferBinding = 0x8CAA,
    /// <summary>
    /// Original was GL_MAX_COLOR_ATTACHMENTS = 0x8CDF
    /// </summary>
    MaxColorAttachments = 0x8CDF,
    /// <summary>
    /// Original was GL_MAX_COLOR_ATTACHMENTS_EXT = 0x8CDF
    /// </summary>
    MaxColorAttachmentsExt = 0x8CDF,
    /// <summary>
    /// Original was GL_MAX_SAMPLES = 0x8D57
    /// </summary>
    MaxSamples = 0x8D57,
    /// <summary>
    /// Original was GL_FRAMEBUFFER_SRGB = 0x8DB9
    /// </summary>
    FramebufferSrgb = 0x8DB9,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_VARYING_COMPONENTS = 0x8DDD
    /// </summary>
    MaxGeometryVaryingComponents = 0x8DDD,
    /// <summary>
    /// Original was GL_MAX_VERTEX_VARYING_COMPONENTS = 0x8DDE
    /// </summary>
    MaxVertexVaryingComponents = 0x8DDE,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_UNIFORM_COMPONENTS = 0x8DDF
    /// </summary>
    MaxGeometryUniformComponents = 0x8DDF,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_OUTPUT_VERTICES = 0x8DE0
    /// </summary>
    MaxGeometryOutputVertices = 0x8DE0,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS = 0x8DE1
    /// </summary>
    MaxGeometryTotalOutputComponents = 0x8DE1,
    /// <summary>
    /// Original was GL_MAX_SUBROUTINES = 0x8DE7
    /// </summary>
    MaxSubroutines = 0x8DE7,
    /// <summary>
    /// Original was GL_MAX_SUBROUTINE_UNIFORM_LOCATIONS = 0x8DE8
    /// </summary>
    MaxSubroutineUniformLocations = 0x8DE8,
    /// <summary>
    /// Original was GL_SHADER_BINARY_FORMATS = 0x8DF8
    /// </summary>
    ShaderBinaryFormats = 0x8DF8,
    /// <summary>
    /// Original was GL_NUM_SHADER_BINARY_FORMATS = 0x8DF9
    /// </summary>
    NumShaderBinaryFormats = 0x8DF9,
    /// <summary>
    /// Original was GL_SHADER_COMPILER = 0x8DFA
    /// </summary>
    ShaderCompiler = 0x8DFA,
    /// <summary>
    /// Original was GL_MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB
    /// </summary>
    MaxVertexUniformVectors = 0x8DFB,
    /// <summary>
    /// Original was GL_MAX_VARYING_VECTORS = 0x8DFC
    /// </summary>
    MaxVaryingVectors = 0x8DFC,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD
    /// </summary>
    MaxFragmentUniformVectors = 0x8DFD,
    /// <summary>
    /// Original was GL_MAX_COMBINED_TESS_CONTROL_UNIFORM_COMPONENTS = 0x8E1E
    /// </summary>
    MaxCombinedTessControlUniformComponents = 0x8E1E,
    /// <summary>
    /// Original was GL_MAX_COMBINED_TESS_EVALUATION_UNIFORM_COMPONENTS = 0x8E1F
    /// </summary>
    MaxCombinedTessEvaluationUniformComponents = 0x8E1F,
    /// <summary>
    /// Original was GL_TRANSFORM_FEEDBACK_BUFFER_PAUSED = 0x8E23
    /// </summary>
    TransformFeedbackBufferPaused = 0x8E23,
    /// <summary>
    /// Original was GL_TRANSFORM_FEEDBACK_BUFFER_ACTIVE = 0x8E24
    /// </summary>
    TransformFeedbackBufferActive = 0x8E24,
    /// <summary>
    /// Original was GL_TRANSFORM_FEEDBACK_BINDING = 0x8E25
    /// </summary>
    TransformFeedbackBinding = 0x8E25,
    /// <summary>
    /// Original was GL_TIMESTAMP = 0x8E28
    /// </summary>
    Timestamp = 0x8E28,
    /// <summary>
    /// Original was GL_QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION = 0x8E4C
    /// </summary>
    QuadsFollowProvokingVertexConvention = 0x8E4C,
    /// <summary>
    /// Original was GL_PROVOKING_VERTEX = 0x8E4F
    /// </summary>
    ProvokingVertex = 0x8E4F,
    /// <summary>
    /// Original was GL_SAMPLE_MASK = 0x8E51
    /// </summary>
    SampleMask = 0x8E51,
    /// <summary>
    /// Original was GL_MAX_SAMPLE_MASK_WORDS = 0x8E59
    /// </summary>
    MaxSampleMaskWords = 0x8E59,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_SHADER_INVOCATIONS = 0x8E5A
    /// </summary>
    MaxGeometryShaderInvocations = 0x8E5A,
    /// <summary>
    /// Original was GL_MIN_FRAGMENT_INTERPOLATION_OFFSET = 0x8E5B
    /// </summary>
    MinFragmentInterpolationOffset = 0x8E5B,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_INTERPOLATION_OFFSET = 0x8E5C
    /// </summary>
    MaxFragmentInterpolationOffset = 0x8E5C,
    /// <summary>
    /// Original was GL_FRAGMENT_INTERPOLATION_OFFSET_BITS = 0x8E5D
    /// </summary>
    FragmentInterpolationOffsetBits = 0x8E5D,
    /// <summary>
    /// Original was GL_MIN_PROGRAM_TEXTURE_GATHER_OFFSET = 0x8E5E
    /// </summary>
    MinProgramTextureGatherOffset = 0x8E5E,
    /// <summary>
    /// Original was GL_MAX_PROGRAM_TEXTURE_GATHER_OFFSET = 0x8E5F
    /// </summary>
    MaxProgramTextureGatherOffset = 0x8E5F,
    /// <summary>
    /// Original was GL_MAX_TRANSFORM_FEEDBACK_BUFFERS = 0x8E70
    /// </summary>
    MaxTransformFeedbackBuffers = 0x8E70,
    /// <summary>
    /// Original was GL_MAX_VERTEX_STREAMS = 0x8E71
    /// </summary>
    MaxVertexStreams = 0x8E71,
    /// <summary>
    /// Original was GL_PATCH_VERTICES = 0x8E72
    /// </summary>
    PatchVertices = 0x8E72,
    /// <summary>
    /// Original was GL_PATCH_DEFAULT_INNER_LEVEL = 0x8E73
    /// </summary>
    PatchDefaultInnerLevel = 0x8E73,
    /// <summary>
    /// Original was GL_PATCH_DEFAULT_OUTER_LEVEL = 0x8E74
    /// </summary>
    PatchDefaultOuterLevel = 0x8E74,
    /// <summary>
    /// Original was GL_MAX_PATCH_VERTICES = 0x8E7D
    /// </summary>
    MaxPatchVertices = 0x8E7D,
    /// <summary>
    /// Original was GL_MAX_TESS_GEN_LEVEL = 0x8E7E
    /// </summary>
    MaxTessGenLevel = 0x8E7E,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_UNIFORM_COMPONENTS = 0x8E7F
    /// </summary>
    MaxTessControlUniformComponents = 0x8E7F,
    /// <summary>
    /// Original was GL_MAX_TESS_EVALUATION_UNIFORM_COMPONENTS = 0x8E80
    /// </summary>
    MaxTessEvaluationUniformComponents = 0x8E80,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_TEXTURE_IMAGE_UNITS = 0x8E81
    /// </summary>
    MaxTessControlTextureImageUnits = 0x8E81,
    /// <summary>
    /// Original was GL_MAX_TESS_EVALUATION_TEXTURE_IMAGE_UNITS = 0x8E82
    /// </summary>
    MaxTessEvaluationTextureImageUnits = 0x8E82,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_OUTPUT_COMPONENTS = 0x8E83
    /// </summary>
    MaxTessControlOutputComponents = 0x8E83,
    /// <summary>
    /// Original was GL_MAX_TESS_PATCH_COMPONENTS = 0x8E84
    /// </summary>
    MaxTessPatchComponents = 0x8E84,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_TOTAL_OUTPUT_COMPONENTS = 0x8E85
    /// </summary>
    MaxTessControlTotalOutputComponents = 0x8E85,
    /// <summary>
    /// Original was GL_MAX_TESS_EVALUATION_OUTPUT_COMPONENTS = 0x8E86
    /// </summary>
    MaxTessEvaluationOutputComponents = 0x8E86,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_UNIFORM_BLOCKS = 0x8E89
    /// </summary>
    MaxTessControlUniformBlocks = 0x8E89,
    /// <summary>
    /// Original was GL_MAX_TESS_EVALUATION_UNIFORM_BLOCKS = 0x8E8A
    /// </summary>
    MaxTessEvaluationUniformBlocks = 0x8E8A,
    /// <summary>
    /// Original was GL_DRAW_INDIRECT_BUFFER_BINDING = 0x8F43
    /// </summary>
    DrawIndirectBufferBinding = 0x8F43,
    /// <summary>
    /// Original was GL_MAX_VERTEX_IMAGE_UNIFORMS = 0x90CA
    /// </summary>
    MaxVertexImageUniforms = 0x90CA,
    /// <summary>
    /// Original was GL_MAX_TESS_CONTROL_IMAGE_UNIFORMS = 0x90CB
    /// </summary>
    MaxTessControlImageUniforms = 0x90CB,
    /// <summary>
    /// Original was GL_MAX_TESS_EVALUATION_IMAGE_UNIFORMS = 0x90CC
    /// </summary>
    MaxTessEvaluationImageUniforms = 0x90CC,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_IMAGE_UNIFORMS = 0x90CD
    /// </summary>
    MaxGeometryImageUniforms = 0x90CD,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_IMAGE_UNIFORMS = 0x90CE
    /// </summary>
    MaxFragmentImageUniforms = 0x90CE,
    /// <summary>
    /// Original was GL_MAX_COMBINED_IMAGE_UNIFORMS = 0x90CF
    /// </summary>
    MaxCombinedImageUniforms = 0x90CF,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_2D_MULTISAMPLE = 0x9104
    /// </summary>
    TextureBinding2DMultisample = 0x9104,
    /// <summary>
    /// Original was GL_TEXTURE_BINDING_2D_MULTISAMPLE_ARRAY = 0x9105
    /// </summary>
    TextureBinding2DMultisampleArray = 0x9105,
    /// <summary>
    /// Original was GL_MAX_COLOR_TEXTURE_SAMPLES = 0x910E
    /// </summary>
    MaxColorTextureSamples = 0x910E,
    /// <summary>
    /// Original was GL_MAX_DEPTH_TEXTURE_SAMPLES = 0x910F
    /// </summary>
    MaxDepthTextureSamples = 0x910F,
    /// <summary>
    /// Original was GL_MAX_INTEGER_SAMPLES = 0x9110
    /// </summary>
    MaxIntegerSamples = 0x9110,
    /// <summary>
    /// Original was GL_MAX_VERTEX_OUTPUT_COMPONENTS = 0x9122
    /// </summary>
    MaxVertexOutputComponents = 0x9122,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_INPUT_COMPONENTS = 0x9123
    /// </summary>
    MaxGeometryInputComponents = 0x9123,
    /// <summary>
    /// Original was GL_MAX_GEOMETRY_OUTPUT_COMPONENTS = 0x9124
    /// </summary>
    MaxGeometryOutputComponents = 0x9124,
    /// <summary>
    /// Original was GL_MAX_FRAGMENT_INPUT_COMPONENTS = 0x9125
    /// </summary>
    MaxFragmentInputComponents = 0x9125,
    /// <summary>
    /// Original was GL_MAX_COMPUTE_IMAGE_UNIFORMS = 0x91BD
    /// </summary>
    MaxComputeImageUniforms = 0x91BD,
}

public enum GetStringEnum : uint
{
    /// <summary>
    /// GL_VENDOR
    /// </summary>
    Vendor = 0x1F00,
    /// <summary>
    /// GL_RENDERER
    /// </summary>
    Renderer = 0x1F01,
    /// <summary>
    /// GL_VERSION
    /// </summary>
    Version = 0x1F02,
    /// <summary>
    /// GL_EXTENSIONS
    /// </summary>
    Extensions = 0x1F03,
    /// <summary>
    /// GL_SHADING_LANGUAGE_VERSION
    /// </summary>
    ShadingLanguageVersion = 0x8B8C
}

public enum LightName : int
{
    /// <summary>
    /// Original was GL_LIGHT0 = 0x4000
    /// </summary>
    Light0 = 0x4000,
    /// <summary>
    /// Original was GL_LIGHT1 = 0x4001
    /// </summary>
    Light1 = 0x4001,
    /// <summary>
    /// Original was GL_LIGHT2 = 0x4002
    /// </summary>
    Light2 = 0x4002,
    /// <summary>
    /// Original was GL_LIGHT3 = 0x4003
    /// </summary>
    Light3 = 0x4003,
    /// <summary>
    /// Original was GL_LIGHT4 = 0x4004
    /// </summary>
    Light4 = 0x4004,
    /// <summary>
    /// Original was GL_LIGHT5 = 0x4005
    /// </summary>
    Light5 = 0x4005,
    /// <summary>
    /// Original was GL_LIGHT6 = 0x4006
    /// </summary>
    Light6 = 0x4006,
    /// <summary>
    /// Original was GL_LIGHT7 = 0x4007
    /// </summary>
    Light7 = 0x4007,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT0_SGIX = 0x840C
    /// </summary>
    FragmentLight0Sgix = 0x840C,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT1_SGIX = 0x840D
    /// </summary>
    FragmentLight1Sgix = 0x840D,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT2_SGIX = 0x840E
    /// </summary>
    FragmentLight2Sgix = 0x840E,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT3_SGIX = 0x840F
    /// </summary>
    FragmentLight3Sgix = 0x840F,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT4_SGIX = 0x8410
    /// </summary>
    FragmentLight4Sgix = 0x8410,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT5_SGIX = 0x8411
    /// </summary>
    FragmentLight5Sgix = 0x8411,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT6_SGIX = 0x8412
    /// </summary>
    FragmentLight6Sgix = 0x8412,
    /// <summary>
    /// Original was GL_FRAGMENT_LIGHT7_SGIX = 0x8413
    /// </summary>
    FragmentLight7Sgix = 0x8413,
}

public enum LightParameter : int
{
    /// <summary>
    /// Original was GL_AMBIENT = 0x1200
    /// </summary>
    Ambient = 0x1200,
    /// <summary>
    /// Original was GL_DIFFUSE = 0x1201
    /// </summary>
    Diffuse = 0x1201,
    /// <summary>
    /// Original was GL_SPECULAR = 0x1202
    /// </summary>
    Specular = 0x1202,
    /// <summary>
    /// Original was GL_POSITION = 0x1203
    /// </summary>
    Position = 0x1203,
    /// <summary>
    /// Original was GL_SPOT_DIRECTION = 0x1204
    /// </summary>
    SpotDirection = 0x1204,
    /// <summary>
    /// Original was GL_SPOT_EXPONENT = 0x1205
    /// </summary>
    SpotExponent = 0x1205,
    /// <summary>
    /// Original was GL_SPOT_CUTOFF = 0x1206
    /// </summary>
    SpotCutoff = 0x1206,
    /// <summary>
    /// Original was GL_CONSTANT_ATTENUATION = 0x1207
    /// </summary>
    ConstantAttenuation = 0x1207,
    /// <summary>
    /// Original was GL_LINEAR_ATTENUATION = 0x1208
    /// </summary>
    LinearAttenuation = 0x1208,
    /// <summary>
    /// Original was GL_QUADRATIC_ATTENUATION = 0x1209
    /// </summary>
    QuadraticAttenuation = 0x1209,
}

/// <summary>
/// Used in GL.ColorMaterial, GL.GetMaterial and 8 other functions
/// </summary>
public enum MaterialFace : int
{
    /// <summary>
    /// Original was GL_FRONT = 0x0404
    /// </summary>
    Front = 0x0404,
    /// <summary>
    /// Original was GL_BACK = 0x0405
    /// </summary>
    Back = 0x0405,
    /// <summary>
    /// Original was GL_FRONT_AND_BACK = 0x0408
    /// </summary>
    FrontAndBack = 0x0408,
}

public enum MaterialParameter : int
{
    /// <summary>
    /// Original was GL_AMBIENT = 0x1200
    /// </summary>
    Ambient = 0x1200,
    /// <summary>
    /// Original was GL_DIFFUSE = 0x1201
    /// </summary>
    Diffuse = 0x1201,
    /// <summary>
    /// Original was GL_SPECULAR = 0x1202
    /// </summary>
    Specular = 0x1202,
    /// <summary>
    /// Original was GL_EMISSION = 0x1600
    /// </summary>
    Emission = 0x1600,
    /// <summary>
    /// Original was GL_SHININESS = 0x1601
    /// </summary>
    Shininess = 0x1601,
    /// <summary>
    /// Original was GL_AMBIENT_AND_DIFFUSE = 0x1602
    /// </summary>
    AmbientAndDiffuse = 0x1602,
    /// <summary>
    /// Original was GL_COLOR_INDEXES = 0x1603
    /// </summary>
    ColorIndexes = 0x1603,
}

public enum MatrixMode : int
{
    /// <summary>
    /// Original was MODELVIEW = 0x1700
    /// </summary>
    ModelView = 0x1700,
    /// <summary>
    /// Original was PROJECTION = 0x1701
    /// </summary>
    Projection = 0x1701,
    /// <summary>
    /// Original was TEXTURE = 0x1702
    /// </summary>
    Texture = 0x1702,
}

public enum MSAA_Samples
{
    Disabled = 0,
    x2 = 2,
    x4 = 4,
    x8 = 8,
    x16 = 16
}

public enum OpenGLErrorCode : uint
{
    NO_ERROR = 0,
    INVALID_ENUM = 1280,
    INVALID_VALUE = 1281,
    INVALID_OPERATION = 1282,
    STACK_OVERFLOW = 1283,
    STACK_UNDERFLOW = 1284,
    OUT_OF_MEMORY = 1285,
    INVALID_FRAMEBUFFER_OPERATION = 1286,
    CONTEXT_LOST = 1287,
    TABLE_TOO_LARGE = 32817
}

public enum PixelInternalFormat : int
{
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT = 0x1902
    /// </summary>
    DepthComponent = 0x1902,
    /// <summary>
    /// Original was GL_ALPHA = 0x1906
    /// </summary>
    Alpha = 0x1906,
    /// <summary>
    /// Original was GL_RGB = 0x1907
    /// </summary>
    Rgb = 0x1907,
    /// <summary>
    /// Original was GL_RGBA = 0x1908
    /// </summary>
    Rgba = 0x1908,
    /// <summary>
    /// Original was GL_LUMINANCE = 0x1909
    /// </summary>
    Luminance = 0x1909,
    /// <summary>
    /// Original was GL_LUMINANCE_ALPHA = 0x190A
    /// </summary>
    LuminanceAlpha = 0x190A,
    /// <summary>
    /// Original was GL_R3_G3_B2 = 0x2A10
    /// </summary>
    R3G3B2 = 0x2A10,
    /// <summary>
    /// Original was GL_ALPHA4 = 0x803B
    /// </summary>
    Alpha4 = 0x803B,
    /// <summary>
    /// Original was GL_ALPHA8 = 0x803C
    /// </summary>
    Alpha8 = 0x803C,
    /// <summary>
    /// Original was GL_ALPHA12 = 0x803D
    /// </summary>
    Alpha12 = 0x803D,
    /// <summary>
    /// Original was GL_ALPHA16 = 0x803E
    /// </summary>
    Alpha16 = 0x803E,
    /// <summary>
    /// Original was GL_LUMINANCE4 = 0x803F
    /// </summary>
    Luminance4 = 0x803F,
    /// <summary>
    /// Original was GL_LUMINANCE8 = 0x8040
    /// </summary>
    Luminance8 = 0x8040,
    /// <summary>
    /// Original was GL_LUMINANCE12 = 0x8041
    /// </summary>
    Luminance12 = 0x8041,
    /// <summary>
    /// Original was GL_LUMINANCE16 = 0x8042
    /// </summary>
    Luminance16 = 0x8042,
    /// <summary>
    /// Original was GL_LUMINANCE4_ALPHA4 = 0x8043
    /// </summary>
    Luminance4Alpha4 = 0x8043,
    /// <summary>
    /// Original was GL_LUMINANCE6_ALPHA2 = 0x8044
    /// </summary>
    Luminance6Alpha2 = 0x8044,
    /// <summary>
    /// Original was GL_LUMINANCE8_ALPHA8 = 0x8045
    /// </summary>
    Luminance8Alpha8 = 0x8045,
    /// <summary>
    /// Original was GL_LUMINANCE12_ALPHA4 = 0x8046
    /// </summary>
    Luminance12Alpha4 = 0x8046,
    /// <summary>
    /// Original was GL_LUMINANCE12_ALPHA12 = 0x8047
    /// </summary>
    Luminance12Alpha12 = 0x8047,
    /// <summary>
    /// Original was GL_LUMINANCE16_ALPHA16 = 0x8048
    /// </summary>
    Luminance16Alpha16 = 0x8048,
    /// <summary>
    /// Original was GL_INTENSITY = 0x8049
    /// </summary>
    Intensity = 0x8049,
    /// <summary>
    /// Original was GL_INTENSITY4 = 0x804A
    /// </summary>
    Intensity4 = 0x804A,
    /// <summary>
    /// Original was GL_INTENSITY8 = 0x804B
    /// </summary>
    Intensity8 = 0x804B,
    /// <summary>
    /// Original was GL_INTENSITY12 = 0x804C
    /// </summary>
    Intensity12 = 0x804C,
    /// <summary>
    /// Original was GL_INTENSITY16 = 0x804D
    /// </summary>
    Intensity16 = 0x804D,
    /// <summary>
    /// Original was GL_RGB2_EXT = 0x804E
    /// </summary>
    Rgb2Ext = 0x804E,
    /// <summary>
    /// Original was GL_RGB4 = 0x804F
    /// </summary>
    Rgb4 = 0x804F,
    /// <summary>
    /// Original was GL_RGB5 = 0x8050
    /// </summary>
    Rgb5 = 0x8050,
    /// <summary>
    /// Original was GL_RGB8 = 0x8051
    /// </summary>
    Rgb8 = 0x8051,
    /// <summary>
    /// Original was GL_RGB10 = 0x8052
    /// </summary>
    Rgb10 = 0x8052,
    /// <summary>
    /// Original was GL_RGB12 = 0x8053
    /// </summary>
    Rgb12 = 0x8053,
    /// <summary>
    /// Original was GL_RGB16 = 0x8054
    /// </summary>
    Rgb16 = 0x8054,
    /// <summary>
    /// Original was GL_RGBA2 = 0x8055
    /// </summary>
    Rgba2 = 0x8055,
    /// <summary>
    /// Original was GL_RGBA4 = 0x8056
    /// </summary>
    Rgba4 = 0x8056,
    /// <summary>
    /// Original was GL_RGB5_A1 = 0x8057
    /// </summary>
    Rgb5A1 = 0x8057,
    /// <summary>
    /// Original was GL_RGBA8 = 0x8058
    /// </summary>
    Rgba8 = 0x8058,
    /// <summary>
    /// Original was GL_RGB10_A2 = 0x8059
    /// </summary>
    Rgb10A2 = 0x8059,
    /// <summary>
    /// Original was GL_RGBA12 = 0x805A
    /// </summary>
    Rgba12 = 0x805A,
    /// <summary>
    /// Original was GL_RGBA16 = 0x805B
    /// </summary>
    Rgba16 = 0x805B,
    /// <summary>
    /// Original was GL_DUAL_ALPHA4_SGIS = 0x8110
    /// </summary>
    DualAlpha4Sgis = 0x8110,
    /// <summary>
    /// Original was GL_DUAL_ALPHA8_SGIS = 0x8111
    /// </summary>
    DualAlpha8Sgis = 0x8111,
    /// <summary>
    /// Original was GL_DUAL_ALPHA12_SGIS = 0x8112
    /// </summary>
    DualAlpha12Sgis = 0x8112,
    /// <summary>
    /// Original was GL_DUAL_ALPHA16_SGIS = 0x8113
    /// </summary>
    DualAlpha16Sgis = 0x8113,
    /// <summary>
    /// Original was GL_DUAL_LUMINANCE4_SGIS = 0x8114
    /// </summary>
    DualLuminance4Sgis = 0x8114,
    /// <summary>
    /// Original was GL_DUAL_LUMINANCE8_SGIS = 0x8115
    /// </summary>
    DualLuminance8Sgis = 0x8115,
    /// <summary>
    /// Original was GL_DUAL_LUMINANCE12_SGIS = 0x8116
    /// </summary>
    DualLuminance12Sgis = 0x8116,
    /// <summary>
    /// Original was GL_DUAL_LUMINANCE16_SGIS = 0x8117
    /// </summary>
    DualLuminance16Sgis = 0x8117,
    /// <summary>
    /// Original was GL_DUAL_INTENSITY4_SGIS = 0x8118
    /// </summary>
    DualIntensity4Sgis = 0x8118,
    /// <summary>
    /// Original was GL_DUAL_INTENSITY8_SGIS = 0x8119
    /// </summary>
    DualIntensity8Sgis = 0x8119,
    /// <summary>
    /// Original was GL_DUAL_INTENSITY12_SGIS = 0x811A
    /// </summary>
    DualIntensity12Sgis = 0x811A,
    /// <summary>
    /// Original was GL_DUAL_INTENSITY16_SGIS = 0x811B
    /// </summary>
    DualIntensity16Sgis = 0x811B,
    /// <summary>
    /// Original was GL_DUAL_LUMINANCE_ALPHA4_SGIS = 0x811C
    /// </summary>
    DualLuminanceAlpha4Sgis = 0x811C,
    /// <summary>
    /// Original was GL_DUAL_LUMINANCE_ALPHA8_SGIS = 0x811D
    /// </summary>
    DualLuminanceAlpha8Sgis = 0x811D,
    /// <summary>
    /// Original was GL_QUAD_ALPHA4_SGIS = 0x811E
    /// </summary>
    QuadAlpha4Sgis = 0x811E,
    /// <summary>
    /// Original was GL_QUAD_ALPHA8_SGIS = 0x811F
    /// </summary>
    QuadAlpha8Sgis = 0x811F,
    /// <summary>
    /// Original was GL_QUAD_LUMINANCE4_SGIS = 0x8120
    /// </summary>
    QuadLuminance4Sgis = 0x8120,
    /// <summary>
    /// Original was GL_QUAD_LUMINANCE8_SGIS = 0x8121
    /// </summary>
    QuadLuminance8Sgis = 0x8121,
    /// <summary>
    /// Original was GL_QUAD_INTENSITY4_SGIS = 0x8122
    /// </summary>
    QuadIntensity4Sgis = 0x8122,
    /// <summary>
    /// Original was GL_QUAD_INTENSITY8_SGIS = 0x8123
    /// </summary>
    QuadIntensity8Sgis = 0x8123,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT16 = 0x81a5
    /// </summary>
    DepthComponent16 = 0x81a5,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT16_SGIX = 0x81A5
    /// </summary>
    DepthComponent16Sgix = 0x81A5,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT24 = 0x81a6
    /// </summary>
    DepthComponent24 = 0x81a6,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT24_SGIX = 0x81A6
    /// </summary>
    DepthComponent24Sgix = 0x81A6,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT32 = 0x81a7
    /// </summary>
    DepthComponent32 = 0x81a7,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT32_SGIX = 0x81A7
    /// </summary>
    DepthComponent32Sgix = 0x81A7,
    /// <summary>
    /// Original was GL_COMPRESSED_RED = 0x8225
    /// </summary>
    CompressedRed = 0x8225,
    /// <summary>
    /// Original was GL_COMPRESSED_RG = 0x8226
    /// </summary>
    CompressedRg = 0x8226,
    /// <summary>
    /// Original was GL_R8 = 0x8229
    /// </summary>
    R8 = 0x8229,
    /// <summary>
    /// Original was GL_R16 = 0x822A
    /// </summary>
    R16 = 0x822A,
    /// <summary>
    /// Original was GL_RG8 = 0x822B
    /// </summary>
    Rg8 = 0x822B,
    /// <summary>
    /// Original was GL_RG16 = 0x822C
    /// </summary>
    Rg16 = 0x822C,
    /// <summary>
    /// Original was GL_R16F = 0x822D
    /// </summary>
    R16f = 0x822D,
    /// <summary>
    /// Original was GL_R32F = 0x822E
    /// </summary>
    R32f = 0x822E,
    /// <summary>
    /// Original was GL_RG16F = 0x822F
    /// </summary>
    Rg16f = 0x822F,
    /// <summary>
    /// Original was GL_RG32F = 0x8230
    /// </summary>
    Rg32f = 0x8230,
    /// <summary>
    /// Original was GL_R8I = 0x8231
    /// </summary>
    R8i = 0x8231,
    /// <summary>
    /// Original was GL_R8UI = 0x8232
    /// </summary>
    R8ui = 0x8232,
    /// <summary>
    /// Original was GL_R16I = 0x8233
    /// </summary>
    R16i = 0x8233,
    /// <summary>
    /// Original was GL_R16UI = 0x8234
    /// </summary>
    R16ui = 0x8234,
    /// <summary>
    /// Original was GL_R32I = 0x8235
    /// </summary>
    R32i = 0x8235,
    /// <summary>
    /// Original was GL_R32UI = 0x8236
    /// </summary>
    R32ui = 0x8236,
    /// <summary>
    /// Original was GL_RG8I = 0x8237
    /// </summary>
    Rg8i = 0x8237,
    /// <summary>
    /// Original was GL_RG8UI = 0x8238
    /// </summary>
    Rg8ui = 0x8238,
    /// <summary>
    /// Original was GL_RG16I = 0x8239
    /// </summary>
    Rg16i = 0x8239,
    /// <summary>
    /// Original was GL_RG16UI = 0x823A
    /// </summary>
    Rg16ui = 0x823A,
    /// <summary>
    /// Original was GL_RG32I = 0x823B
    /// </summary>
    Rg32i = 0x823B,
    /// <summary>
    /// Original was GL_RG32UI = 0x823C
    /// </summary>
    Rg32ui = 0x823C,
    /// <summary>
    /// Original was GL_COMPRESSED_RGB_S3TC_DXT1_EXT = 0x83F0
    /// </summary>
    CompressedRgbS3tcDxt1Ext = 0x83F0,
    /// <summary>
    /// Original was GL_COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1
    /// </summary>
    CompressedRgbaS3tcDxt1Ext = 0x83F1,
    /// <summary>
    /// Original was GL_COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2
    /// </summary>
    CompressedRgbaS3tcDxt3Ext = 0x83F2,
    /// <summary>
    /// Original was GL_COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3
    /// </summary>
    CompressedRgbaS3tcDxt5Ext = 0x83F3,
    /// <summary>
    /// Original was GL_RGB_ICC_SGIX = 0x8460
    /// </summary>
    RgbIccSgix = 0x8460,
    /// <summary>
    /// Original was GL_RGBA_ICC_SGIX = 0x8461
    /// </summary>
    RgbaIccSgix = 0x8461,
    /// <summary>
    /// Original was GL_ALPHA_ICC_SGIX = 0x8462
    /// </summary>
    AlphaIccSgix = 0x8462,
    /// <summary>
    /// Original was GL_LUMINANCE_ICC_SGIX = 0x8463
    /// </summary>
    LuminanceIccSgix = 0x8463,
    /// <summary>
    /// Original was GL_INTENSITY_ICC_SGIX = 0x8464
    /// </summary>
    IntensityIccSgix = 0x8464,
    /// <summary>
    /// Original was GL_LUMINANCE_ALPHA_ICC_SGIX = 0x8465
    /// </summary>
    LuminanceAlphaIccSgix = 0x8465,
    /// <summary>
    /// Original was GL_R5_G6_B5_ICC_SGIX = 0x8466
    /// </summary>
    R5G6B5IccSgix = 0x8466,
    /// <summary>
    /// Original was GL_R5_G6_B5_A8_ICC_SGIX = 0x8467
    /// </summary>
    R5G6B5A8IccSgix = 0x8467,
    /// <summary>
    /// Original was GL_ALPHA16_ICC_SGIX = 0x8468
    /// </summary>
    Alpha16IccSgix = 0x8468,
    /// <summary>
    /// Original was GL_LUMINANCE16_ICC_SGIX = 0x8469
    /// </summary>
    Luminance16IccSgix = 0x8469,
    /// <summary>
    /// Original was GL_INTENSITY16_ICC_SGIX = 0x846A
    /// </summary>
    Intensity16IccSgix = 0x846A,
    /// <summary>
    /// Original was GL_LUMINANCE16_ALPHA8_ICC_SGIX = 0x846B
    /// </summary>
    Luminance16Alpha8IccSgix = 0x846B,
    /// <summary>
    /// Original was GL_COMPRESSED_ALPHA = 0x84E9
    /// </summary>
    CompressedAlpha = 0x84E9,
    /// <summary>
    /// Original was GL_COMPRESSED_LUMINANCE = 0x84EA
    /// </summary>
    CompressedLuminance = 0x84EA,
    /// <summary>
    /// Original was GL_COMPRESSED_LUMINANCE_ALPHA = 0x84EB
    /// </summary>
    CompressedLuminanceAlpha = 0x84EB,
    /// <summary>
    /// Original was GL_COMPRESSED_INTENSITY = 0x84EC
    /// </summary>
    CompressedIntensity = 0x84EC,
    /// <summary>
    /// Original was GL_COMPRESSED_RGB = 0x84ED
    /// </summary>
    CompressedRgb = 0x84ED,
    /// <summary>
    /// Original was GL_COMPRESSED_RGBA = 0x84EE
    /// </summary>
    CompressedRgba = 0x84EE,
    /// <summary>
    /// Original was GL_DEPTH_STENCIL = 0x84F9
    /// </summary>
    DepthStencil = 0x84F9,
    /// <summary>
    /// Original was GL_RGBA32F = 0x8814
    /// </summary>
    Rgba32f = 0x8814,
    /// <summary>
    /// Original was GL_RGB32F = 0x8815
    /// </summary>
    Rgb32f = 0x8815,
    /// <summary>
    /// Original was GL_RGBA16F = 0x881A
    /// </summary>
    Rgba16f = 0x881A,
    /// <summary>
    /// Original was GL_RGB16F = 0x881B
    /// </summary>
    Rgb16f = 0x881B,
    /// <summary>
    /// Original was GL_DEPTH24_STENCIL8 = 0x88F0
    /// </summary>
    Depth24Stencil8 = 0x88F0,
    /// <summary>
    /// Original was GL_R11F_G11F_B10F = 0x8C3A
    /// </summary>
    R11fG11fB10f = 0x8C3A,
    /// <summary>
    /// Original was GL_RGB9_E5 = 0x8C3D
    /// </summary>
    Rgb9E5 = 0x8C3D,
    /// <summary>
    /// Original was GL_SRGB = 0x8C40
    /// </summary>
    Srgb = 0x8C40,
    /// <summary>
    /// Original was GL_SRGB8 = 0x8C41
    /// </summary>
    Srgb8 = 0x8C41,
    /// <summary>
    /// Original was GL_SRGB_ALPHA = 0x8C42
    /// </summary>
    SrgbAlpha = 0x8C42,
    /// <summary>
    /// Original was GL_SRGB8_ALPHA8 = 0x8C43
    /// </summary>
    Srgb8Alpha8 = 0x8C43,
    /// <summary>
    /// Original was GL_SLUMINANCE_ALPHA = 0x8C44
    /// </summary>
    SluminanceAlpha = 0x8C44,
    /// <summary>
    /// Original was GL_SLUMINANCE8_ALPHA8 = 0x8C45
    /// </summary>
    Sluminance8Alpha8 = 0x8C45,
    /// <summary>
    /// Original was GL_SLUMINANCE = 0x8C46
    /// </summary>
    Sluminance = 0x8C46,
    /// <summary>
    /// Original was GL_SLUMINANCE8 = 0x8C47
    /// </summary>
    Sluminance8 = 0x8C47,
    /// <summary>
    /// Original was GL_COMPRESSED_SRGB = 0x8C48
    /// </summary>
    CompressedSrgb = 0x8C48,
    /// <summary>
    /// Original was GL_COMPRESSED_SRGB_ALPHA = 0x8C49
    /// </summary>
    CompressedSrgbAlpha = 0x8C49,
    /// <summary>
    /// Original was GL_COMPRESSED_SLUMINANCE = 0x8C4A
    /// </summary>
    CompressedSluminance = 0x8C4A,
    /// <summary>
    /// Original was GL_COMPRESSED_SLUMINANCE_ALPHA = 0x8C4B
    /// </summary>
    CompressedSluminanceAlpha = 0x8C4B,
    /// <summary>
    /// Original was GL_COMPRESSED_SRGB_S3TC_DXT1_EXT = 0x8C4C
    /// </summary>
    CompressedSrgbS3tcDxt1Ext = 0x8C4C,
    /// <summary>
    /// Original was GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D
    /// </summary>
    CompressedSrgbAlphaS3tcDxt1Ext = 0x8C4D,
    /// <summary>
    /// Original was GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E
    /// </summary>
    CompressedSrgbAlphaS3tcDxt3Ext = 0x8C4E,
    /// <summary>
    /// Original was GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F
    /// </summary>
    CompressedSrgbAlphaS3tcDxt5Ext = 0x8C4F,
    /// <summary>
    /// Original was GL_DEPTH_COMPONENT32F = 0x8CAC
    /// </summary>
    DepthComponent32f = 0x8CAC,
    /// <summary>
    /// Original was GL_DEPTH32F_STENCIL8 = 0x8CAD
    /// </summary>
    Depth32fStencil8 = 0x8CAD,
    /// <summary>
    /// Original was GL_RGBA32UI = 0x8D70
    /// </summary>
    Rgba32ui = 0x8D70,
    /// <summary>
    /// Original was GL_RGB32UI = 0x8D71
    /// </summary>
    Rgb32ui = 0x8D71,
    /// <summary>
    /// Original was GL_RGBA16UI = 0x8D76
    /// </summary>
    Rgba16ui = 0x8D76,
    /// <summary>
    /// Original was GL_RGB16UI = 0x8D77
    /// </summary>
    Rgb16ui = 0x8D77,
    /// <summary>
    /// Original was GL_RGBA8UI = 0x8D7C
    /// </summary>
    Rgba8ui = 0x8D7C,
    /// <summary>
    /// Original was GL_RGB8UI = 0x8D7D
    /// </summary>
    Rgb8ui = 0x8D7D,
    /// <summary>
    /// Original was GL_RGBA32I = 0x8D82
    /// </summary>
    Rgba32i = 0x8D82,
    /// <summary>
    /// Original was GL_RGB32I = 0x8D83
    /// </summary>
    Rgb32i = 0x8D83,
    /// <summary>
    /// Original was GL_RGBA16I = 0x8D88
    /// </summary>
    Rgba16i = 0x8D88,
    /// <summary>
    /// Original was GL_RGB16I = 0x8D89
    /// </summary>
    Rgb16i = 0x8D89,
    /// <summary>
    /// Original was GL_RGBA8I = 0x8D8E
    /// </summary>
    Rgba8i = 0x8D8E,
    /// <summary>
    /// Original was GL_RGB8I = 0x8D8F
    /// </summary>
    Rgb8i = 0x8D8F,
    /// <summary>
    /// Original was GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD
    /// </summary>
    Float32UnsignedInt248Rev = 0x8DAD,
    /// <summary>
    /// Original was GL_COMPRESSED_RED_RGTC1 = 0x8DBB
    /// </summary>
    CompressedRedRgtc1 = 0x8DBB,
    /// <summary>
    /// Original was GL_COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC
    /// </summary>
    CompressedSignedRedRgtc1 = 0x8DBC,
    /// <summary>
    /// Original was GL_COMPRESSED_RG_RGTC2 = 0x8DBD
    /// </summary>
    CompressedRgRgtc2 = 0x8DBD,
    /// <summary>
    /// Original was GL_COMPRESSED_SIGNED_RG_RGTC2 = 0x8DBE
    /// </summary>
    CompressedSignedRgRgtc2 = 0x8DBE,
    /// <summary>
    /// Original was GL_COMPRESSED_RGBA_BPTC_UNORM = 0x8E8C
    /// </summary>
    CompressedRgbaBptcUnorm = 0x8E8C,
    /// <summary>
    /// Original was GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT = 0x8E8E
    /// </summary>
    CompressedRgbBptcSignedFloat = 0x8E8E,
    /// <summary>
    /// Original was GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT = 0x8E8F
    /// </summary>
    CompressedRgbBptcUnsignedFloat = 0x8E8F,
    /// <summary>
    /// Original was GL_R8_SNORM = 0x8F94
    /// </summary>
    R8Snorm = 0x8F94,
    /// <summary>
    /// Original was GL_RG8_SNORM = 0x8F95
    /// </summary>
    Rg8Snorm = 0x8F95,
    /// <summary>
    /// Original was GL_RGB8_SNORM = 0x8F96
    /// </summary>
    Rgb8Snorm = 0x8F96,
    /// <summary>
    /// Original was GL_RGBA8_SNORM = 0x8F97
    /// </summary>
    Rgba8Snorm = 0x8F97,
    /// <summary>
    /// Original was GL_R16_SNORM = 0x8F98
    /// </summary>
    R16Snorm = 0x8F98,
    /// <summary>
    /// Original was GL_RG16_SNORM = 0x8F99
    /// </summary>
    Rg16Snorm = 0x8F99,
    /// <summary>
    /// Original was GL_RGB16_SNORM = 0x8F9A
    /// </summary>
    Rgb16Snorm = 0x8F9A,
    /// <summary>
    /// Original was GL_RGBA16_SNORM = 0x8F9B
    /// </summary>
    Rgba16Snorm = 0x8F9B,
    /// <summary>
    /// Original was GL_RGB10_A2UI = 0x906F
    /// </summary>
    Rgb10A2ui = 0x906F,
    /// <summary>
    /// Original was GL_ONE = 1
    /// </summary>
    One = 1,
    /// <summary>
    /// Original was GL_TWO = 2
    /// </summary>
    Two = 2,
    /// <summary>
    /// Original was GL_THREE = 3
    /// </summary>
    Three = 3,
    /// <summary>
    /// Original was GL_FOUR = 4
    /// </summary>
    Four = 4,
}

public enum PixelFormat : int
{
    ColorIndex = 0X1900,
    StencilIndex = 0X1901,
    DepthComponent = 0X1902,
    Red = 0X1903,
    Green = 0X1904,
    Blue = 0X1905,
    Alpha = 0X1906,
    Rgb = 0X1907,
    Rgba = 0X1908,
    Luminance = 0X1909,
    LuminanceAlpha = 0X190a,
    AbgrExt = 0X8000,
    CmykExt = 0X800c,
    CmykaExt = 0X800D,
    Bgr = 0X80e0,
    Bgra = 0X80e1,
    Ycrcb422Sgix = 0X81bb,
    Ycrcb444Sgix = 0X81bc,
    Rg = 0X8227,
    RgInteger = 0X8228,
    DepthStencil = 0X84f9,
    RedInteger = 0X8d94,
    GreenInteger = 0X8d95,
    BlueInteger = 0X8d96,
    AlphaInteger = 0X8d97,
    RgbInteger = 0X8d98,
    RgbaInteger = 0X8d99,
    BgrInteger = 0X8d9a,
    BgraInteger = 0X8d9b,
}

public enum PixelStoreParameter : int
{
    GL_UNPACK_SWAP_BYTES = 0x0CF0,
    GL_UNPACK_LSB_FIRST = 0x0CF1,
    GL_UNPACK_ROW_LENGTH = 0x0CF2,
    GL_UNPACK_ROW_LENGTH_EXT = 0x0CF2,
    GL_UNPACK_SKIP_ROWS = 0x0CF3,
    GL_UNPACK_SKIP_ROWS_EXT = 0x0CF3,
    GL_UNPACK_SKIP_PIXELS = 0x0CF4,
    GL_UNPACK_SKIP_PIXELS_EXT = 0x0CF4,
    GL_UNPACK_ALIGNMENT = 0x0CF5,
    GL_PACK_SWAP_BYTES = 0x0D00,
    GL_PACK_LSB_FIRST = 0x0D01,
    GL_PACK_ROW_LENGTH = 0x0D02,
    GL_PACK_SKIP_ROWS = 0x0D03,
    GL_PACK_SKIP_PIXELS = 0x0D04,
    GL_PACK_ALIGNMENT = 0x0D05,
    GL_PACK_SKIP_IMAGES = 0x806B,
    GL_PACK_SKIP_IMAGES_EXT = 0x806B,
    GL_PACK_IMAGE_HEIGHT = 0x806C,
    GL_PACK_IMAGE_HEIGHT_EXT = 0x806C,
    GL_UNPACK_SKIP_IMAGES = 0x806D,
    GL_UNPACK_SKIP_IMAGES_EXT = 0x806D,
    GL_UNPACK_IMAGE_HEIGHT = 0x806E,
    GL_UNPACK_IMAGE_HEIGHT_EXT = 0x806E,
    GL_PACK_SKIP_VOLUMES_SGIS = 0x8130,
    GL_PACK_IMAGE_DEPTH_SGIS = 0x8131,
    GL_UNPACK_SKIP_VOLUMES_SGIS = 0x8132,
    GL_UNPACK_IMAGE_DEPTH_SGIS = 0x8133,
    GL_PIXEL_TILE_WIDTH_SGIX = 0x8140,
    GL_PIXEL_TILE_HEIGHT_SGIX = 0x8141,
    GL_PIXEL_TILE_GRID_WIDTH_SGIX = 0x8142,
    GL_PIXEL_TILE_GRID_HEIGHT_SGIX = 0x8143,
    GL_PIXEL_TILE_GRID_DEPTH_SGIX = 0x8144,
    GL_PIXEL_TILE_CACHE_SIZE_SGIX = 0x8145,
    GL_PACK_RESAMPLE_SGIX = 0x842C,
    GL_UNPACK_RESAMPLE_SGIX = 0x842D,
    GL_PACK_SUBSAMPLE_RATE_SGIX = 0x85A0,
    GL_UNPACK_SUBSAMPLE_RATE_SGIX = 0x85A1,
    GL_PACK_RESAMPLE_OML = 0x8984,
    GL_UNPACK_RESAMPLE_OML = 0x8985,
    GL_UNPACK_COMPRESSED_BLOCK_WIDTH = 0x9127,
    GL_UNPACK_COMPRESSED_BLOCK_HEIGHT = 0x9128,
    GL_UNPACK_COMPRESSED_BLOCK_DEPTH = 0x9129,
    GL_UNPACK_COMPRESSED_BLOCK_SIZE = 0x912A,
    GL_PACK_COMPRESSED_BLOCK_WIDTH = 0x912B,
    GL_PACK_COMPRESSED_BLOCK_HEIGHT = 0x912C,
    GL_PACK_COMPRESSED_BLOCK_DEPTH = 0x912D,
    GL_PACK_COMPRESSED_BLOCK_SIZE = 0x912E
}

public enum PixelType : int
{
    Byte = 0X1400,
    UnsignedByte = 0X1401,
    Short = 0X1402,
    UnsignedShort = 0X1403,
    Int = 0X1404,
    UnsignedInt = 0X1405,
    Float = 0X1406,
    HalfFloat = 0X140b,
    Bitmap = 0X1a00,
    UnsignedByte332 = 0X8032,
    UnsignedByte332Ext = 0X8032,
    UnsignedShort4444 = 0X8033,
    UnsignedShort4444Ext = 0X8033,
    UnsignedShort5551 = 0X8034,
    UnsignedShort5551Ext = 0X8034,
    UnsignedInt8888 = 0X8035,
    UnsignedInt8888Ext = 0X8035,
    UnsignedInt1010102 = 0X8036,
    UnsignedInt1010102Ext = 0X8036,
    UnsignedByte233Reversed = 0X8362,
    UnsignedShort565 = 0X8363,
    UnsignedShort565Reversed = 0X8364,
    UnsignedShort4444Reversed = 0X8365,
    UnsignedShort1555Reversed = 0X8366,
    UnsignedInt8888Reversed = 0X8367,
    UnsignedInt2101010Reversed = 0X8368,
    UnsignedInt248 = 0X84fa,
    UnsignedInt10F11F11FRev = 0X8c3b,
    UnsignedInt5999Rev = 0X8c3e,
    Float32UnsignedInt248Rev = 0X8Dad,
}

public enum PolygonMode : int
{
    /// <summary>
    /// Original was GL_POINT = 0x1B00
    /// </summary>
    Point = 0x1B00,
    /// <summary>
    /// Original was GL_LINE = 0x1B01
    /// </summary>
    Line = 0x1B01,
    /// <summary>
    /// Original was GL_FILL = 0x1B02
    /// </summary>
    Fill = 0x1B02,
}

public enum PrimitiveType : int
{
    //todo: complete this. Look at other functions for docs? 
    //https://registry.khronos.org/OpenGL-Refpages/gl2.1/xhtml/glBegin.xml
    //https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawArrays.xhtml
    /// <summary>
    /// Treats each vertex as a single point. Vertex n defines point n. N points are drawn.
    /// </summary>
    GL_POINTS = 0,
    /// <summary>
    /// GL_LINES
    /// </summary>
    GL_LINES = 1,
    /// <summary>
    /// GL_LINE_STRIP
    /// </summary>
    GL_LINE_STRIP = 3,
    /// <summary>
    /// GL_LINE_LOOP
    /// </summary>
    GL_LINE_LOOP = 2,
    /// <summary>
    /// GL_TRIANGLES
    /// </summary>
    GL_TRIANGLES = 4,
    /// <summary>
    /// GL_TRIANGLE_STRIP
    /// </summary>
    GL_TRIANGLE_STRIP = 5,
    /// <summary>
    /// GL_TRIANGLE_FAN
    /// </summary>
    GL_TRIANGLE_FAN = 6,
    /// <summary>
    /// GL_QUADS
    /// </summary>
    GL_QUADS = 7,
    /// <summary>
    /// GL_TRIANGLE_STRIP_ADJACENCY
    /// </summary>
    GL_LINE_STRIP_ADJACENCY = 11,
    /// <summary>
    /// GL_LINES_ADJACENCY
    /// </summary>
    GL_LINES_ADJACENCY = 10,
    /// <summary>
    /// GL_TRIANGLES_ADJACENCY
    /// </summary>
    GL_TRIANGLES_ADJACENCY = 12,
    /// <summary>
    /// GL_TRIANGLE_STRIP_ADJACENCY
    /// </summary>
    GL_TRIANGLE_STRIP_ADJACENCY  = 13,
    /// <summary>
    /// GL_PATCH
    /// </summary>
    Patches = 14
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct RgbaColor
{
    // Sigh: PixelFormat.Format32bppArgb is actually laid out BGRA...
    public byte Blue;
    public byte Green;
    public byte Red;
    public byte Alpha;
}

public enum ShadingModel : int
{
    /// <summary>
    /// Original was GL_FLAT = 0x1D00
    /// </summary>
    Flat = 0x1D00,
    /// <summary>
    /// Original was GL_SMOOTH = 0x1D01
    /// </summary>
    Smooth = 0x1D01,
}

public enum ShaderParameter : int
{
    /// <summary>
    /// GL_SHADER_TYPE
    /// </summary>
    ShaderType = 0x8B4F,
    /// <summary>
    /// GL_DELETE_STATUS
    /// </summary>
    DeleteStatus = 0x8B80,
    /// <summary>
    /// GL_COMPILE_STATUS
    /// </summary>
    CompileStatus = 0x8B81,
    /// <summary>
    /// GL_INFO_LOG_LENGTH
    /// </summary>
    InfoLogLengtth = 0x8B84,
    /// <summary>
    /// GL_SHADER_SOURCE_LENGTH
    /// </summary>
    ShaderSourceLength = 0x8B88,
}

public enum ShaderType : int
{
    FragmentShader = 0x8B30,
    VertexShader = 0x8B31,
    GeometryShader = 0x8DD9,
    TessEvaluationShader = 0x8E87,
    TessControlShader = 0x8E88,
    ComputeShader = 0x91B9,
}

public enum TexInternalFormat : int
{
    /// <summary>
    /// GL_RGBA8
    /// </summary>
    RGBA8 = 0x8058,
    /// <summary>
    /// GL_RGBA16
    /// </summary>
    RGBA16 = 0x805B,
    /// <summary>
    /// GL_R8
    /// </summary>
    R8 = 0x8229,
    /// <summary>
    /// GL_R16
    /// </summary>
    R16 = 0x822A,
    /// <summary>
    /// GL_RG8
    /// </summary>
    RG8 = 0x822B,
    /// <summary>
    /// GL_RG16
    /// </summary>
    RG16 = 0x822C,
    /// <summary>
    /// GL_R16F
    /// </summary>
    R16F = 0x822D,
    /// <summary>
    /// GL_R32F
    /// </summary>
    R32F = 0x822E,
    /// <summary>
    /// GL_RG16F
    /// </summary>
    RG16F = 0x822F,
    /// <summary>
    /// GL_RG32F
    /// </summary>
    RG32F = 0x8230,
    /// <summary>
    /// GL_R8I
    /// </summary>
    R8I = 0x8231,
    /// <summary>
    /// GL_R8UI
    /// </summary>
    R8UI = 0x8232,
    /// <summary>
    /// GL_R16I
    /// </summary>
    R16I = 0x8233,
    /// <summary>
    /// GL_R16UI
    /// </summary>
    R16UI = 0x8234,
    /// <summary>
    /// GL_R32I
    /// </summary>
    R32I = 0x8235,
    /// <summary>
    /// GL_R32UI
    /// </summary>
    R32UI = 0x8236,
    /// <summary>
    /// GL_RG8I
    /// </summary>
    RG8I = 0x8237,
    /// <summary>
    /// GL_RG8UI
    /// </summary>
    RG8UI = 0x8238,
    /// <summary>
    /// GL_RG16I
    /// </summary>
    RG16I = 0x8239,
    /// <summary>
    /// GL_RG16UI
    /// </summary>
    RG16UI = 0x823A,
    /// <summary>
    /// GL_RG32I
    /// </summary>
    RG32I = 0x823B,
    /// <summary>
    /// GL_RG32UI
    /// </summary>
    RG32UI = 0x823C,
    /// <summary>
    /// GL_RGBA32F
    /// </summary>
    RGBA32F = 0x8814,
    /// <summary>
    /// GL_RGBA16F
    /// </summary>
    RGBA16F = 0x881A,
    /// <summary>
    /// GL_RGBA32UI
    /// </summary>
    RGBA32UI = 0x8D70,
    /// <summary>
    /// GL_RGBA16UI
    /// </summary>
    RGBA16UI = 0x8D76,
    /// <summary>
    /// GL_RGBA8UI
    /// </summary>
    RGBA8UI = 0x8D7C,
    /// <summary>
    /// GL_RGBA32I
    /// </summary>
    RGBA32I = 0x8D82,
    /// <summary>
    /// GL_RGBA16I
    /// </summary>
    RGBA16I = 0x8D88,
    /// <summary>
    /// GL_RGBA8I
    /// </summary>
    RGBA8I = 0x8D8E
}

public enum TextureParameter : int
{
    /// <summary>
    /// GL_REPEAT: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    Repeat = 0x2901,
    /// <summary>
    /// GL_CLAMP_TO_BORDER: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    ClampToBorder = 0x812D,
    /// <summary>
    /// GL_CLAMP_TO_BORDER_ARB: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    ClampToBorderARB = 0x812D,
    /// <summary>
    /// GL_CLAMP_TO_BORDER_NV: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    ClampToBorderNV = 0x812D,
    /// <summary>
    /// GL_CLAMP_TO_BORDER_SGIS: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    ClampToBorderSGIS = 0x812D,
    /// <summary>
    /// GL_CLAMP_TO_EDGE: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    ClampToEdge = 0x812F,
    /// <summary>
    /// GL_CLAMP_TO_EDGE_SGIS: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    CLampToEdgeSGIS = 0x812F,
    /// <summary>
    /// GL_MIRRORED_REPEAT: Used with TextureParameterName TextureWrapS / TextureWrapT
    /// </summary>
    MirroredRepeat = 0x8370,
    

    /// <summary>
    /// GL_NEAREST: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    Nearest = 0x2600,
    /// <summary>
    /// GL_LINEAR: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    Linear = 0x2601,
    /// <summary>
    /// GL_LINEAR_DETAIL_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    LinearDetailSGIS = 0x8097,
    /// <summary>
    /// GL_LINEAR_DETAIL_ALPHA_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    LineaerDetailAlphaSGIS = 0x8098,
    /// <summary>
    /// GL_LINEAR_DETAIL_COLOR_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    LinearDetailColourSGIS = 0x8099,
    /// <summary>
    /// GL_LINEAR_SHARPEN_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    LinearSharpenSGIS = 0x80AD,
    /// <summary>
    /// GL_LINEAR_SHARPEN_ALPHA_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    LinearSharpenAlphaSGIS = 0x80AE,
    /// <summary>
    /// GL_LINEAR_SHARPEN_COLOR_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    LinearSharpenColourSGIS = 0x80AF,
    /// <summary>
    /// GL_FILTER4_SGIS: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    Filter4SGIS = 0x8146,
    /// <summary>
    /// GL_PIXEL_TEX_GEN_Q_CEILING_SGIX: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    PixelTexGenQCeilingSGIX = 0x8184,
    /// <summary>
    /// GL_PIXEL_TEX_GEN_Q_ROUND_SGIX: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    PixelTexGenQRoundSGIX = 0x8185,
    /// <summary>
    /// GL_PIXEL_TEX_GEN_Q_FLOOR_SGIX: Used with TextureParameterName TextureMagFilter / TextureMinFilter
    /// </summary>
    PixelTexGenQFloorSGIX = 0x8186,
    /// <summary>
    /// Original was GL_NEAREST_MIPMAP_NEAREST = 0x2700
    /// </summary>
    NearestMipmapNearest = 0x2700,
    /// <summary>
    /// Original was GL_LINEAR_MIPMAP_NEAREST = 0x2701
    /// </summary>
    LinearMipmapNearest = 0x2701,
    /// <summary>
    /// Original was GL_NEAREST_MIPMAP_LINEAR = 0x2702
    /// </summary>
    NearestMipmapLinear = 0x2702,
    /// <summary>
    /// Original was GL_LINEAR_MIPMAP_LINEAR = 0x2703
    /// </summary>
    LinearMipmapLinear = 0x2703,
    /// <summary>
    /// Original was GL_LINEAR_CLIPMAP_LINEAR_SGIX = 0x8170
    /// </summary>
    LinearClipmapLinearSGIX = 0x8170,
    /// <summary>
    /// Original was GL_NEAREST_CLIPMAP_NEAREST_SGIX = 0x844D
    /// </summary>
    NearestClipmapNearestSGIX = 0x844D,
    /// <summary>
    /// Original was GL_NEAREST_CLIPMAP_LINEAR_SGIX = 0x844E
    /// </summary>
    NearestClipmapLinearSGIX = 0x844E,
    /// <summary>
    /// Original was GL_LINEAR_CLIPMAP_NEAREST_SGIX = 0x844F
    /// </summary>
    LinearClipmapNearestSGIX = 0x844F,
    
}

public enum TextureParameterName : int
{
    /// <summary>
    /// Original was GL_TEXTURE_BORDER_COLOR = 0x1004
    /// </summary>
    TextureBorderColor = 0x1004,
    /// <summary>
    /// Original was GL_TEXTURE_MAG_FILTER = 0x2800
    /// </summary>
    TextureMagFilter = 0x2800,
    /// <summary>
    /// Original was GL_TEXTURE_MIN_FILTER = 0x2801
    /// </summary>
    TextureMinFilter = 0x2801,
    /// <summary>
    /// Original was GL_TEXTURE_WRAP_S = 0x2802
    /// </summary>
    TextureWrapS = 0x2802,
    /// <summary>
    /// Original was GL_TEXTURE_WRAP_T = 0x2803
    /// </summary>
    TextureWrapT = 0x2803,
    /// <summary>
    /// Original was GL_TEXTURE_PRIORITY = 0x8066
    /// </summary>
    TexturePriority = 0x8066,
    /// <summary>
    /// Original was GL_TEXTURE_PRIORITY_EXT = 0x8066
    /// </summary>
    TexturePriorityExt = 0x8066,
    /// <summary>
    /// Original was GL_TEXTURE_DEPTH = 0x8071
    /// </summary>
    TextureDepth = 0x8071,
    /// <summary>
    /// Original was GL_TEXTURE_WRAP_R = 0x8072
    /// </summary>
    TextureWrapR = 0x8072,
    /// <summary>
    /// Original was GL_TEXTURE_WRAP_R_EXT = 0x8072
    /// </summary>
    TextureWrapRExt = 0x8072,
    /// <summary>
    /// Original was GL_TEXTURE_WRAP_R_OES = 0x8072
    /// </summary>
    TextureWrapROes = 0x8072,
    /// <summary>
    /// Original was GL_DETAIL_TEXTURE_LEVEL_SGIS = 0x809A
    /// </summary>
    DetailTextureLevelSgis = 0x809A,
    /// <summary>
    /// Original was GL_DETAIL_TEXTURE_MODE_SGIS = 0x809B
    /// </summary>
    DetailTextureModeSgis = 0x809B,
    /// <summary>
    /// Original was GL_SHADOW_AMBIENT_SGIX = 0x80BF
    /// </summary>
    ShadowAmbientSgix = 0x80BF,
    /// <summary>
    /// Original was GL_TEXTURE_COMPARE_FAIL_VALUE = 0x80BF
    /// </summary>
    TextureCompareFailValue = 0x80BF,
    /// <summary>
    /// Original was GL_DUAL_TEXTURE_SELECT_SGIS = 0x8124
    /// </summary>
    DualTextureSelectSgis = 0x8124,
    /// <summary>
    /// Original was GL_QUAD_TEXTURE_SELECT_SGIS = 0x8125
    /// </summary>
    QuadTextureSelectSgis = 0x8125,
    /// <summary>
    /// Original was GL_CLAMP_TO_BORDER = 0x812D
    /// </summary>
    ClampToBorder = 0x812D,
    /// <summary>
    /// Original was GL_CLAMP_TO_EDGE = 0x812F
    /// </summary>
    ClampToEdge = 0x812F,
    /// <summary>
    /// Original was GL_TEXTURE_WRAP_Q_SGIS = 0x8137
    /// </summary>
    TextureWrapQSgis = 0x8137,
    /// <summary>
    /// Original was GL_TEXTURE_MIN_LOD = 0x813A
    /// </summary>
    TextureMinLod = 0x813A,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_LOD = 0x813B
    /// </summary>
    TextureMaxLod = 0x813B,
    /// <summary>
    /// Original was GL_TEXTURE_BASE_LEVEL = 0x813C
    /// </summary>
    TextureBaseLevel = 0x813C,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_LEVEL = 0x813D
    /// </summary>
    TextureMaxLevel = 0x813D,
    /// <summary>
    /// Original was GL_TEXTURE_CLIPMAP_CENTER_SGIX = 0x8171
    /// </summary>
    TextureClipmapCenterSgix = 0x8171,
    /// <summary>
    /// Original was GL_TEXTURE_CLIPMAP_FRAME_SGIX = 0x8172
    /// </summary>
    TextureClipmapFrameSgix = 0x8172,
    /// <summary>
    /// Original was GL_TEXTURE_CLIPMAP_OFFSET_SGIX = 0x8173
    /// </summary>
    TextureClipmapOffsetSgix = 0x8173,
    /// <summary>
    /// Original was GL_TEXTURE_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8174
    /// </summary>
    TextureClipmapVirtualDepthSgix = 0x8174,
    /// <summary>
    /// Original was GL_TEXTURE_CLIPMAP_LOD_OFFSET_SGIX = 0x8175
    /// </summary>
    TextureClipmapLodOffsetSgix = 0x8175,
    /// <summary>
    /// Original was GL_TEXTURE_CLIPMAP_DEPTH_SGIX = 0x8176
    /// </summary>
    TextureClipmapDepthSgix = 0x8176,
    /// <summary>
    /// Original was GL_POST_TEXTURE_FILTER_BIAS_SGIX = 0x8179
    /// </summary>
    PostTextureFilterBiasSgix = 0x8179,
    /// <summary>
    /// Original was GL_POST_TEXTURE_FILTER_SCALE_SGIX = 0x817A
    /// </summary>
    PostTextureFilterScaleSgix = 0x817A,
    /// <summary>
    /// Original was GL_TEXTURE_LOD_BIAS_S_SGIX = 0x818E
    /// </summary>
    TextureLodBiasSSgix = 0x818E,
    /// <summary>
    /// Original was GL_TEXTURE_LOD_BIAS_T_SGIX = 0x818F
    /// </summary>
    TextureLodBiasTSgix = 0x818F,
    /// <summary>
    /// Original was GL_TEXTURE_LOD_BIAS_R_SGIX = 0x8190
    /// </summary>
    TextureLodBiasRSgix = 0x8190,
    /// <summary>
    /// Original was GL_GENERATE_MIPMAP = 0x8191
    /// </summary>
    GenerateMipmap = 0x8191,
    /// <summary>
    /// Original was GL_GENERATE_MIPMAP_SGIS = 0x8191
    /// </summary>
    GenerateMipmapSgis = 0x8191,
    /// <summary>
    /// Original was GL_TEXTURE_COMPARE_SGIX = 0x819A
    /// </summary>
    TextureCompareSgix = 0x819A,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_CLAMP_S_SGIX = 0x8369
    /// </summary>
    TextureMaxClampSSgix = 0x8369,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_CLAMP_T_SGIX = 0x836A
    /// </summary>
    TextureMaxClampTSgix = 0x836A,
    /// <summary>
    /// Original was GL_TEXTURE_MAX_CLAMP_R_SGIX = 0x836B
    /// </summary>
    TextureMaxClampRSgix = 0x836B,
    /// <summary>
    /// Original was GL_TEXTURE_LOD_BIAS = 0x8501
    /// </summary>
    TextureLodBias = 0x8501,
    /// <summary>
    /// Original was GL_DEPTH_TEXTURE_MODE = 0x884B
    /// </summary>
    DepthTextureMode = 0x884B,
    /// <summary>
    /// Original was GL_TEXTURE_COMPARE_MODE = 0x884C
    /// </summary>
    TextureCompareMode = 0x884C,
    /// <summary>
    /// Original was GL_TEXTURE_COMPARE_FUNC = 0x884D
    /// </summary>
    TextureCompareFunc = 0x884D,
    /// <summary>
    /// Original was GL_TEXTURE_SWIZZLE_R = 0x8E42
    /// </summary>
    TextureSwizzleR = 0x8E42,
    /// <summary>
    /// Original was GL_TEXTURE_SWIZZLE_G = 0x8E43
    /// </summary>
    TextureSwizzleG = 0x8E43,
    /// <summary>
    /// Original was GL_TEXTURE_SWIZZLE_B = 0x8E44
    /// </summary>
    TextureSwizzleB = 0x8E44,
    /// <summary>
    /// Original was GL_TEXTURE_SWIZZLE_A = 0x8E45
    /// </summary>
    TextureSwizzleA = 0x8E45,
    /// <summary>
    /// Original was GL_TEXTURE_SWIZZLE_RGBA = 0x8E46
    /// </summary>
    TextureSwizzleRgba = 0x8E46,
}

public enum TextureTargetMultisample : int
{
    /// <summary>
    /// GL_TEXTURE_2D_MULTISAMPLE
    /// </summary>
    Texture2DMultisample = 0x9100,
    /// <summary>
    /// GL_PROXY_TEXTURE_2D_MULTISAMPLE
    /// </summary>
    ProxyTexture2DMultisample = 0x9101,
    /// <summary>
    /// GL_TEXTURE_2D_MULTISAMPLE_ARRAY
    /// </summary>
    Texture2DMultisampleArray = 0x9102,
    /// <summary>
    /// GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY
    /// </summary>
    ProxyTexture2DMultisampleArray = 0x9103,
}

public enum TextureUnit
{
    Texture0 = 33984,
    Texture1 = 33985,
    Texture2 = 33986,
    Texture3 = 33987,
    Texture4 = 33988,
    Texture5 = 33989,
    Texture6 = 33990,
    Texture7 = 33991,
    Texture8 = 33992,
    Texture9 = 33993,
    Texture10 = 33994,
    Texture11 = 33995,
    Texture12 = 33996,
    Texture13 = 33997,
    Texture14 = 33998,
    Texture15 = 33999,
    Texture16 = 34000,
    Texture17 = 34001,
    Texture18 = 34002,
    Texture19 = 34003,
    Texture20 = 34004,
    Texture21 = 34005,
    Texture22 = 34006,
    Texture23 = 34007,
    Texture24 = 34008,
    Texture25 = 34009,
    Texture26 = 34010,
    Texture27 = 34011,
    Texture28 = 34012,
    Texture29 = 34013,
    Texture30 = 34014,
    Texture31 = 34015,
}

[Flags]
public enum VertexArray : uint
{
    /// <summary>
    /// GL_VERTEX_ARRAY
    /// </summary>
    VertexArray = 0x8074,
    /// <summary>
    /// GL_NORMAL_ARRAY
    /// </summary>
    NormalArray = 0x8075,
    /// <summary>
    /// GL_COLOR_ARRAY
    /// </summary>
    ColourArray = 0x8076,
    /// <summary>
    /// GL_INDEX_ARRAY
    /// </summary>
    IndexArray = 0x8077,
    /// <summary>
    /// GL_TEXTURE_COORD_ARRAY
    /// </summary>
    TextureCoordArray = 0x8078,
    /// <summary>
    /// GL_EDGE_FLAG_ARRAY
    /// </summary>
    EdgeFlagArray = 0x8079,
    /// <summary>
    /// GL_VERTEX_ARRAY_SIZE
    /// </summary>
    VertexArraySize = 0x807A,
    /// <summary>
    /// GL_VERTEX_ARRAY_TYPE
    /// </summary>
    VertexArrayType = 0x807B,
    /// <summary>
    /// GL_VERTEX_ARRAY_STRIDE
    /// </summary>
    VertexArrayStride = 0x807C,
    /// <summary>
    /// GL_NORMAL_ARRAY_TYPE
    /// </summary>
    NormalArrayType = 0x807E,
    /// <summary>
    /// GL_NORMAL_ARRAY_STRIDE
    /// </summary>
    NormalArrayStride = 0x807F,
    /// <summary>
    /// GL_COLOR_ARRAY_SIZE
    /// </summary>
    ColourArraySize = 0x8081,
    /// <summary>
    /// GL_COLOR_ARRAY_TYPE
    /// </summary>
    ColourArrayType = 0x8082,
    /// <summary>
    /// GL_COLOR_ARRAY_STRIDE
    /// </summary>
    ColourArrayStride = 0x8083,
    /// <summary>
    /// GL_INDEX_ARRAY_TYPE
    /// </summary>
    IndexArrayType = 0x8085,
    /// <summary>
    /// GL_INDEX_ARRAY_STRIDE
    /// </summary>
    IndexArrayStride = 0x8086,
    /// <summary>
    /// GL_TEXTURE_COORD_ARRAY_SIZE
    /// </summary>
    TextureCoordArraySize = 0x8088,
    /// <summary>
    /// GL_TEXTURE_COORD_ARRAY_TYPE
    /// </summary>
    TextureCoordArrayType = 0x8089,
    /// <summary>
    /// GL_TEXTURE_COORD_ARRAY_STRIDE
    /// </summary>
    TextureCoordArrayStride = 0x808A,
    /// <summary>
    /// GL_EDGE_FLAG_ARRAY_STRIDE
    /// </summary>
    EdgeFlagArrayStride = 0x808C,
    /// <summary>
    /// GL_VERTEX_ARRAY_POINTER
    /// </summary>
    VertexArrayPointer = 0x808E,
    /// <summary>
    /// GL_NORMAL_ARRAY_POINTER
    /// </summary>
    NormalArrayPointer = 0x808F,
    /// <summary>
    /// GL_COLOR_ARRAY_POINTER
    /// </summary>
    ColourArrayPointer = 0x8090,
    /// <summary>
    /// GL_INDEX_ARRAY_POINTER
    /// </summary>
    IndexArrayPointer = 0x8091,
    /// <summary>
    /// GL_TEXTURE_COORD_ARRAY_POINTER
    /// </summary>
    TextureCoordArrayPointer = 0x8092,
    /// <summary>
    /// GL_EDGE_FLAG_ARRAY_POINTER
    /// </summary>
    EdgeFlagArrayPointer = 0x8093,
    /// <summary>
    /// GL_V2F
    /// </summary>
    V2F = 0x2A20,
    /// <summary>
    /// GL_V3F
    /// </summary>
    V3F = 0x2A21,
    /// <summary>
    /// GL_C4UB_V2F
    /// </summary>
    C4UB_V2F = 0x2A22,
    /// <summary>
    /// GL_C4UB_V3F
    /// </summary>
    C4UB_V3F = 0x2A23,
    /// <summary>
    /// GL_C3F_V3F
    /// </summary>
    C3F_V3F = 0x2A24,
    /// <summary>
    /// GL_N3F_V3F
    /// </summary>
    N3F_V3F = 0x2A25,
    /// <summary>
    /// GL_C4F_N3F_V3F
    /// </summary>
    C4F_N3F_V3F = 0x2A26,
    /// <summary>
    /// GL_T2F_V3F
    /// </summary>
    T2F_V3F = 0x2A27,
    /// <summary>
    /// GL_T4F_V4F
    /// </summary>
    T4F_V4F = 0x2A28,
    /// <summary>
    /// GL_T2F_C4UB_V3F
    /// </summary>
    T2F_C4UB_V3F = 0x2A29,
    /// <summary>
    /// GL_T2F_C3F_V3F
    /// </summary>
    T2F_C3F_V3F = 0x2A2A,
    /// <summary>
    /// GL_T2F_N3F_V3F
    /// </summary>
    T2F_N3F_V3F = 0x2A2B,
    /// <summary>
    /// GL_T2F_C4F_N3F_V3F
    /// </summary>
    T2F_C4F_N3F_V3F = 0x2A2C,
    /// <summary>
    /// GL_T4F_C4F_N3F_V4F
    /// </summary>
    T4F_C4F_N3F_V4F = 0x2A2D,
}

public enum VertexAttribPointerType : int
{
    Byte = 5120,
    UnsignedByte = 5121,
    Short = 5122,
    UnsignedShort = 5123,
    Int = 5124,
    UnsignedInt = 5125,
    Float = 5126,
    Double = 5130,
    HalfFloat = 5131,
    Fixed = 5132,
    UnsignedInt2101010Rev = 33640,
    Int2101010Rev = 36255,
}

public enum VSyncMode : int
{
    On = 1,
    Off = 0,
    Adaptive = -1
}

//todo: force the relevant function to take this enum (tricky because it's an array of ints, and half need to stay ints)
public enum ArbCreateContext
{
    CoreProfileBit = 0x0001,
    CompatibilityProfileBit = 0x0002,
    DebugBit = 0x0001,
    ForwardCompatibleBit = 0x0002,
    MajorVersion = 0x2091,
    MinorVersion = 0x2092,
    LayerPlane = 0x2093,
    ContextFlags = 0x2094,
    ErrorInvalidVersion = 0x2095,
    ProfileMask = 0x9126,
}

public enum ARB_multisample
{
    SampleBuffersArb = 0x2041,
    SamplesArb = 0x2042,
}

public enum WGL_ARB_pixel_format
{
    ShareStencilArb = 0x200d,
    AccumBitsArb = 0x201d,
    NumberUnderlaysArb = 0x2009,
    StereoArb = 0x2012,
    MaxPbufferHeightArb = 0x2030,
    TypeRgbaArb = 0x202b,
    SupportGdiArb = 0x200f,
    NeedSystemPaletteArb = 0x2005,
    AlphaBitsArb = 0x201b,
    ShareDepthArb = 0x200c,
    SupportOpenglArb = 0x2010,
    ColorBitsArb = 0x2014,
    AccumRedBitsArb = 0x201e,
    MaxPbufferWidthArb = 0x202f,
    NumberOverlaysArb = 0x2008,
    MaxPbufferPixelsArb = 0x202e,
    NeedPaletteArb = 0x2004,
    RedShiftArb = 0x2016,
    AccelerationArb = 0x2003,
    GreenBitsArb = 0x2017,
    TransparentGreenValueArb = 0x2038,
    PixelTypeArb = 0x2013,
    AuxBuffersArb = 0x2024,
    DrawToWindowArb = 0x2001,
    RedBitsArb = 0x2015,
    NumberPixelFormatsArb = 0x2000,
    GenericAccelerationArb = 0x2026,
    BlueBitsArb = 0x2019,
    PbufferLargestArb = 0x2033,
    AccumAlphaBitsArb = 0x2021,
    TransparentArb = 0x200a,
    FullAccelerationArb = 0x2027,
    ShareAccumArb = 0x200e,
    SwapExchangeArb = 0x2028,
    SwapUndefinedArb = 0x202a,
    TransparentAlphaValueArb = 0x203a,
    PbufferHeightArb = 0x2035,
    TransparentBlueValueArb = 0x2039,
    SwapMethodArb = 0x2007,
    StencilBitsArb = 0x2023,
    DepthBitsArb = 0x2022,
    GreenShiftArb = 0x2018,
    TransparentRedValueArb = 0x2037,
    DoubleBufferArb = 0x2011,
    NoAccelerationArb = 0x2025,
    TypeColorindexArb = 0x202c,
    SwapLayerBuffersArb = 0x2006,
    AccumBlueBitsArb = 0x2020,
    DrawToPbufferArb = 0x202d,
    AccumGreenBitsArb = 0x201f,
    PbufferWidthArb = 0x2034,
    TransparentIndexValueArb = 0x203b,
    AlphaShiftArb = 0x201c,
    DrawToBitmapArb = 0x2002,
    BlueShiftArb = 0x201a,
    SwapCopyArb = 0x2029,
}
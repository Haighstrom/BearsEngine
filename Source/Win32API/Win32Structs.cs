using System.Runtime.InteropServices;

namespace BearsEngine.Win32API;

/// <summary>
/// Contains information about a system appbar message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct APPBARDATA
{
    /// <summary>
    /// The size of the structure, in bytes.
    /// </summary>
    public uint cbSize;

    /// <summary>
    /// The handle to the appbar window. Not all messages use this member. See the individual message page to see if you need to provide an hWind value.
    /// </summary>
    public IntPtr hWnd;

    /// <summary>
    /// An application-defined message identifier. The application uses the specified identifier for notification messages that it sends to the appbar identified by the hWnd member. This member is used when sending the ABM_NEW message.
    /// </summary>
    public uint uCallbackMessage;

    /// <summary>
    /// A value that specifies an edge of the screen. This member is used when sending one of these messages: ABM_GETAUTOHIDEBAR, ABM_SETAUTOHIDEBAR, ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX, ABM_QUERYPOS, ABM_SETPOS
    /// </summary>
    public APPBARDATA_uEdge uEdge;

    /// <summary>
    /// A RECT structure whose use varies depending on the message:
    /// ABM_GETTASKBARPOS, ABM_QUERYPOS, ABM_SETPOS: The bounding rectangle, in screen coordinates, of an appbar or the Windows taskbar.
    /// ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX: The monitor on which the operation is being performed. This information can be retrieved through the GetMonitorInfo function.
    /// </summary>
    public RECT rc;

    /// <summary>
    /// A message-dependent value. This member is used with these messages: ABM_SETAUTOHIDEBAR ABM_SETAUTOHIDEBAREX ABM_SETSTATE. See the individual message pages for details.
    /// </summary>
    public int lParam;
}

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
/// The JOYCAPS structure contains information about the joystick capabilities.
/// </summary>
internal struct JOYCAPS
{
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
    public JOYCAPS_Caps wCaps;

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

    public static readonly int SizeInBytes;

    static JOYCAPS()
    {
        SizeInBytes = Marshal.SizeOf(default(JOYCAPS));
    }

    public int GetMin(int i)
    {
        return i switch
        {
            0 => wXmin,
            1 => wYmin,
            2 => wZmin,
            3 => wRmin,
            4 => wUmin,
            5 => wVmin,
            _ => 0,
        };
    }

    public int GetMax(int i)
    {
        return i switch
        {
            0 => wXmax,
            1 => wYmax,
            2 => wZmax,
            3 => wRmax,
            4 => wUmax,
            5 => wVmax,
            _ => 0,
        };
    }
}

/// <summary>
/// The JOYINFO structure contains information about the joystick position and button state.
/// </summary>
internal struct JOYINFO
{
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
    public JOYINFO_wButtons wButtons;
}

/// <summary>
/// The JOYINFOEX structure contains extended information about the joystick position, point-of-view position, and button state.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct JOYINFOEX
{
    /// <summary>
    /// Size, in bytes, of this structure.
    /// </summary>
    public int dwSize;

    /// <summary>
    /// Flags indicating the valid information returned in this structure. Members that do not contain valid information are set to zero.
    /// </summary>
    [MarshalAs(UnmanagedType.I4)]
    public JOYINFOEXdwFlags dwFlags;

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
/// The PIXELFORMATDESCRIPTOR structure describes the pixel format of a drawing surface
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal class PIXELFORMATDESCRIPTOR
{
    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-pixelformatdescriptor
    // https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-emf/1db036d6-2da8-4b92-b4f8-e9cab8cc93b7
    [Flags]
    public enum Flags : uint
    {
        /// <summary>
        /// The buffer can draw to a window or device surface.
        /// </summary>
        PFD_DRAW_TO_WINDOW = 0x00000004,

        /// <summary>
        /// The buffer can draw to a memory bitmap.
        /// </summary>
        PFD_DRAW_TO_BITMAP = 0x00000008,

        /// <summary>
        /// The buffer supports GDI drawing. This flag and PFD_DOUBLEBUFFER are mutually exclusive in the current generic implementation.
        /// </summary>
        PFD_SUPPORT_GDI = 0x00000010,

        /// <summary>
        /// The buffer supports OpenGL drawing.
        /// </summary>
        PFD_SUPPORT_OPENGL = 0x00000020,

        /// <summary>
        /// The pixel format is supported by a device driver that accelerates the generic implementation. If this flag is clear and the PFD_GENERIC_FORMAT flag is set, the pixel format is supported by the generic implementation only.
        /// </summary>
        PFD_GENERIC_ACCELERATED = 0x00001000,

        /// <summary>
        /// The pixel format is supported by the GDI software implementation, which is also known as the generic implementation. If this bit is clear, the pixel format is supported by a device driver or hardware.
        /// </summary>
        PFD_GENERIC_FORMAT = 0x00000040,

        /// <summary>
        /// The buffer uses RGBA pixels on a palette-managed device. A logical palette is required to achieve the best results for this pixel type. Colors in the palette should be specified according to the values of the cRedBits, cRedShift, cGreenBits, cGreenShift, cBluebits, and cBlueShift members. The palette should be created and realized in the device context before calling wglMakeCurrent.
        /// </summary>
        PFD_NEED_PALETTE = 0x00000080,

        /// <summary>
        /// 	Defined in the pixel format descriptors of hardware that supports one hardware palette in 256-color mode only. For such systems to use hardware acceleration, the hardware palette must be in a fixed order (for example, 3-3-2) when in RGBA mode or must match the logical palette when in color-index mode.When this flag is set, you must call SetSystemPaletteUse in your program to force a one-to-one mapping of the logical palette and the system palette. If your OpenGL hardware supports multiple hardware palettes and the device driver can allocate spare hardware palettes for OpenGL, this flag is typically clear. This flag is not set in the generic pixel formats.
        /// </summary>
        PFD_NEED_SYSTEM_PALETTE = 0x00000100,

        /// <summary>
        /// The buffer is double-buffered. This flag and PFD_SUPPORT_GDI are mutually exclusive in the current generic implementation.
        /// </summary>
        PFD_DOUBLEBUFFER = 0x00000001,

        /// <summary>
        /// The buffer is stereoscopic. This flag is not supported in the current generic implementation.
        /// </summary>
        PFD_STEREO = 0x00000002,

        /// <summary>
        /// Indicates whether a device can swap individual layer planes with pixel formats that include double-buffered overlay or underlay planes. Otherwise all layer planes are swapped together as a group. When this flag is set, wglSwapLayerBuffers is supported.
        /// </summary>
        PFD_SWAP_LAYER_BUFFERS = 0x00000800,

        /// <summary>
        /// You can specify this bit flag when calling ChoosePixelFormat. The requested pixel format can either have or not have a depth buffer. To select a pixel format without a depth buffer, you must specify this flag. The requested pixel format can be with or without a depth buffer. Otherwise, only pixel formats with a depth buffer are considered.
        /// </summary>
        PFD_DEPTH_DONTCARE = 0x20000000,

        /// <summary>
        /// You can specify this bit flag when calling ChoosePixelFormat. The requested pixel format can be either single- or double-buffered.
        /// </summary>
        PFD_DOUBLEBUFFER_DONTCARE = 0x40000000,

        /// <summary>
        /// You can specify this bit flag when calling ChoosePixelFormat. The requested pixel format can be either monoscopic or stereoscopic.
        /// </summary>
        PFD_STEREO_DONTCARE = 0x80000000,

        /// <summary>
        /// With the glAddSwapHintRectWIN extension function, this flag is included for the PIXELFORMATDESCRIPTOR pixel format structure. Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap. Swapping the color buffers causes the content of the back buffer to be copied to the front buffer. The content of the back buffer is not affected by the swap. PFD_SWAP_COPY is a hint only and might not be provided by a driver.
        /// </summary>
        PFD_SWAP_COPY = 0x00000400,

        /// <summary>
        /// With the glAddSwapHintRectWIN extension function, this flag is included for the PIXELFORMATDESCRIPTOR pixel format structure. Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap. Swapping the color buffers causes the exchange of the back buffer's content with the front buffer's content. Following the swap, the back buffer's content contains the front buffer's content before the swap. PFD_SWAP_EXCHANGE is a hint only and might not be provided by a driver.
        /// </summary>
        PFD_SWAP_EXCHANGE = 0x00000200,

        // Below only available in this documentation: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-emf/1db036d6-2da8-4b92-b4f8-e9cab8cc93b7
        /// <summary>
        /// The pixel buffer supports DirectDraw drawing, which allows applications to have low-level control of the output drawing surface.
        /// </summary>
        PFD_SUPPORT_DIRECTDRAW = 0x00002000,

        /// <summary>
        /// The pixel buffer supports Direct3D drawing, which accellerated rendering in three dimensions.
        /// </summary>
        PFD_DIRECT3D_ACCELERATED = 0x00004000,

        /// <summary>
        /// The pixel buffer supports compositing, which indicates that source pixels MAY overwrite or be combined with background pixels.
        /// </summary>
        PFD_SUPPORT_COMPOSITION = 0x00008000,
    };

    /// <summary>
    /// Type of pixel data
    /// </summary>
    public enum PixelType : byte
    {
        /// <summary>
        /// RGBA pixels. Each pixel has four components in this order: red, green, blue, and alpha.
        /// </summary>
        PFD_TYPE_RGBA = 0,

        /// <summary>
        /// Color-index pixels. Each pixel uses a color-index value.
        /// </summary>
        PFD_TYPE_COLORINDEX = 1
    }

    public PIXELFORMATDESCRIPTOR()
    {
    }

    /// <summary>
    /// Specifies the size of this data structure. This value should be set to sizeof(PIXELFORMATDESCRIPTOR).
    /// </summary>
    private readonly short nSize = (short)Marshal.SizeOf<PIXELFORMATDESCRIPTOR>();

    /// <summary>
    /// Specifies the version of this data structure. This value should be set to 1.
    /// </summary>
    private readonly short nVersion = 1;

    /// <summary>
    /// A set of bit flags that specify properties of the pixel buffer. The properties are generally not mutually exclusive.
    /// </summary>
    public Flags dwFlags;

    /// <summary>
    /// Specifies the type of pixel data.
    /// </summary>
    public PixelType iPixelType;

    /// <summary>
    /// Specifies the number of color bitplanes in each color buffer. For RGBA pixel types, it is the size of the color buffer, excluding the alpha bitplanes. For color-index pixels, it is the size of the color-index buffer.
    /// </summary>
    public byte cColorBits;

    /// <summary>
    /// Specifies the number of red bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cRedBits;

    /// <summary>
    /// Specifies the shift count for red bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cRedShift;

    /// <summary>
    /// Specifies the number of green bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cGreenBits;

    /// <summary>
    /// Specifies the shift count for green bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cGreenShift;

    /// <summary>
    /// Specifies the number of blue bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cBlueBits;

    /// <summary>
    /// Specifies the shift count for blue bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cBlueShift;

    /// <summary>
    /// Specifies the number of alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
    /// </summary>
    public byte cAlphaBits;

    /// <summary>
    /// Specifies the shift count for alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
    /// </summary>
    public byte cAlphaShift;

    /// <summary>
    /// Specifies the total number of bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumBits;

    /// <summary>
    /// Specifies the number of red bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumRedBits;

    /// <summary>
    /// Specifies the number of green bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumGreenBits;

    /// <summary>
    /// Specifies the number of blue bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumBlueBits;

    /// <summary>
    /// Specifies the number of alpha bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumAlphaBits;

    /// <summary>
    /// Specifies the depth of the depth (z-axis) buffer.
    /// </summary>
    public byte cDepthBits;

    /// <summary>
    /// Specifies the depth of the stencil buffer.
    /// </summary>
    public byte cStencilBits;

    /// <summary>
    /// Specifies the number of auxiliary buffers. Auxiliary buffers are not supported.
    /// </summary>
    public byte cAuxBuffers;

    /// <summary>
    /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
    /// </summary>
    private readonly byte iLayerType;

    /// <summary>
    /// Specifies the number of overlay and underlay planes. Bits 0 through 3 specify up to 15 overlay planes and bits 4 through 7 specify up to 15 underlay planes.
    /// </summary>
    private readonly byte bReserved;

    /// <summary>
    /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
    /// </summary>
    private readonly int dwLayerMask;

    /// <summary>
    /// Specifies the transparent color or index of an underlay plane. When the pixel type is RGBA, dwVisibleMask is a transparent RGB color value. When the pixel type is color index, it is a transparent index value.
    /// </summary>
    public int dwVisibleMask;

    /// <summary>
    /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
    /// </summary>
    private readonly int dwDamageMask;
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
/// Defines the coordinates of the upper left and lower right corners of a rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct SMALL_RECT
{
    /// <summary>
    /// The x-coordinate of the upper left corner of the rectangle.
    /// </summary>
    public short Left;

    /// <summary>
    /// The y-coordinate of the upper left corner of the rectangle.
    /// </summary>
    public short Top;

    /// <summary>
    /// The x-coordinate of the lower right corner of the rectangle.
    /// </summary>
    public short Right;

    /// <summary>
    /// The y-coordinate of the lower right corner of the rectangle.
    /// </summary>
    public short Bottom;
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
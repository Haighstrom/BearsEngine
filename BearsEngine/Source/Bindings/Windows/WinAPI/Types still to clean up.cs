using System.Runtime.InteropServices;

namespace BearsEngine.WinAPI;



[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class DEVMODE
{
    public DEVMODE()
    {
        Size = (short)Marshal.SizeOf(this);
    }

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string DeviceName;
    public short SpecVersion;
    public short DriverVersion;
    private short Size;
    public short DriverExtra;
    public DeviceModeEnum Fields;

    //internal short Orientation;
    //internal short PaperSize;
    //internal short PaperLength;
    //internal short PaperWidth;
    //internal short Scale;
    //internal short Copies;
    //internal short DefaultSource;
    //internal short PrintQuality;

    public POINT Position;
    public int DisplayOrientation;
    public int DisplayFixedOutput;

    public short Color;
    public short Duplex;
    public short YResolution;
    public short TTOption;
    public short Collate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string FormName;
    public short LogPixels;
    public int BitsPerPel;
    public int PelsWidth;
    public int PelsHeight;
    public int DisplayFlags;
    public int DisplayFrequency;
    public int ICMMethod;
    public int ICMIntent;
    public int MediaType;
    public int DitherType;
    public int Reserved1;
    public int Reserved2;
    public int PanningWidth;
    public int PanningHeight;
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
    internal DisplayDeviceStateFlags StateFlags;
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
internal struct MOUSEMOVEPOINT
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

[StructLayout(LayoutKind.Explicit)]
internal struct RawHID
{
}

internal struct RawInput
{
    internal RawInputHeader Header;
    internal RawInputData Data;
}

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

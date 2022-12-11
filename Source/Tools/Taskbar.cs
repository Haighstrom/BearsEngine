using HaighFramework.Win32API;

namespace BearsEngine;

public enum TaskbarPosition
{
    Unknown = -1,
    Left,
    Top,
    Right,
    Bottom,
}

public static class Taskbar
{
    private const string CLASS_NAME = "Shell_TrayWnd";

    static Taskbar()
    {
        IntPtr taskbarHandle = User32.FindWindow(CLASS_NAME, null);

        APPBARDATA data = new();
        data.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(APPBARDATA));
        data.hWnd = taskbarHandle;
        IntPtr result = Shell32.SHAppBarMessage(APPBARMESSAGE.ABM_GETTASKBARPOS, ref data);
        if (result == IntPtr.Zero)
            throw new InvalidOperationException();

        Position = (TaskbarPosition)data.uEdge;
        R = new Rect(data.rc.left, data.rc.top, data.rc.right - data.rc.left, data.rc.bottom - data.rc.top);

        data.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(APPBARDATA));
        result = Shell32.SHAppBarMessage(APPBARMESSAGE.ABM_GETSTATE, ref data);
        int state = result.ToInt32();
        AlwaysOnTop = (state & (int)ABS.AlwaysOnTop) == (int)ABS.AlwaysOnTop;
        AutoHide = (state & (int)ABS.Autohide) == (int)ABS.Autohide;
    }

    public static Rect R { get; set; }
    public static TaskbarPosition Position { get; set; }
    public static bool AlwaysOnTop { get; set; }
    public static bool AutoHide { get; set; }

    public static Point P => R.P;
    public static int X => (int)R.X;
    public static int Y => (int)R.Y;
    public static int W => (int)R.W;
    public static int H => (int)R.H;
    public static Point Centre => R.Centre;
    public static Point Size => R.Size;
}

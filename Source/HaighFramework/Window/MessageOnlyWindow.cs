using System.Runtime.InteropServices;
using BearsEngine.Win32API;

namespace BearsEngine.Window;

internal class MessageOnlyWindow : IDisposable
{
    private static readonly object _syncRoot = new();
    public static readonly IntPtr HWND_MESSAGE = new(-3);

    private readonly IntPtr _instance = Marshal.GetHINSTANCE(typeof(MessageOnlyWindow).Module);
    private readonly IntPtr _className = Marshal.StringToHGlobalAuto(Guid.NewGuid().ToString());
    bool _classRegistered;
    private MSG _msg;
    private bool _disposed = false;
    //only store this so it doesn't get garbage collected (apparently this happens?)
    private readonly WNDPROC _wndProc;

    internal MessageOnlyWindow(WNDPROC wndProc)
    {
        lock (_syncRoot)
        {
            _wndProc = wndProc;
            Handle = CreateWindow(0, 0, 100, 100);
            Exists = true;
            ProcessEventsOnce();
            User32.SetWindowLongPtr(Handle, GWL.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(wndProc));
            ProcessEventsOnce();
        }
    }

    public IntPtr Handle { get; private set; }

    public bool Exists { get; private set; }

    private IntPtr CreateWindow(int x, int y, int width, int height)
    {
        if (!_classRegistered)
        {
            WNDCLASSEX wc = new();
            wc.cbSize = (uint)Marshal.SizeOf(default(WNDCLASSEX));
            wc.style = WindowClassStyle.CS_OWNDC;
            wc.hInstance = _instance;
            wc.lpfnWndProc = TempWndProc;
            wc.lpszClassName = _className;
            wc.hIcon = IntPtr.Zero;
            wc.hIconSm = IntPtr.Zero;
            wc.hCursor = IntPtr.Zero;

            ushort atom = User32.RegisterClassEx(ref wc);

            if (atom == 0)
                throw new Exception(string.Format("Failed to register window class. Error: {0}", Marshal.GetLastWin32Error()));

            _classRegistered = true;
        }

        IntPtr handle = User32.CreateWindowEx(0, _className, IntPtr.Zero, 0, x, y, width, height, HWND_MESSAGE, IntPtr.Zero, _instance, IntPtr.Zero);

        if (handle == IntPtr.Zero)
            throw new Exception(string.Format("Failed to create window. Error: {0}", Marshal.GetLastWin32Error()));

        return handle;
    }

    private IntPtr TempWndProc(IntPtr handle, WINDOWMESSAGE message, IntPtr wParam, IntPtr lParam)
    {
        return User32.DefWindowProc(handle, message, wParam, lParam);
    }

    public void ProcessEventsOnce()
    {
        while (User32.PeekMessage(ref _msg, Handle, 0, 0, PEEKMESSAGEFLAGS.PM_REMOVE))
        {
            User32.TranslateMessage(ref _msg);
            User32.DispatchMessage(ref _msg);
        }
    }

    public void ProcessEventsUntilDestroyed()
    {
        while (Exists)
        {
            //we use GetMessage so that the thread pauses in between messages
            int ret = User32.GetMessage(ref _msg, Handle, 0, 0);

            if (ret == -1)
                throw new Exception(string.Format("Error getting InputDevice Message: {0}", Marshal.GetLastWin32Error()));

            User32.TranslateMessage(ref _msg);
            User32.DispatchMessage(ref _msg);
        }
    }

    public void Close()
    {
        User32.PostMessage(Handle, WINDOWMESSAGE.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
    }

    private void DestroyWindow()
    {
        if (Exists)
        {
            User32.DestroyWindow(Handle);
            Exists = false;
        }
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool manual)
    {
        if (!_disposed)
        {
            if (manual)
            {
                DestroyWindow();
            }
            else
            {
                //todo: create warnings
            }
            _disposed = true;
        }
    }

    ~MessageOnlyWindow()
    {
        //todo: create warnings
        Dispose(false);
    }
}
using BearsEngine.WinAPI;
using System.Runtime.InteropServices;

namespace BearsEngine.Input.Windows;

internal class InputProcessingWindow
{
    private static readonly object _syncRoot = new();
    public static readonly IntPtr HWND_MESSAGE = new(-3);

    private readonly IntPtr _instance = Marshal.GetHINSTANCE(typeof(InputProcessingWindow).Module);
    private readonly IntPtr _className = Marshal.StringToHGlobalAuto(Guid.NewGuid().ToString());
    bool _classRegistered;
    private bool _exists = true;
    private MSG _msg;

    public InputProcessingWindow(WNDPROC wndProc)
    {
        lock (_syncRoot)
        {
            Handle = CreateWindow();
            ProcessEventsOnce(); //Allow setup code to run before inserting new wndProc
            User32.SetWindowLongPtr(Handle, GWL.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(wndProc));
        }
    }

    public IntPtr Handle { get; }

    private IntPtr CreateWindow()
    {
        if (!_classRegistered)
        {
            WNDCLASSEX wc = new()
            {
                cbSize = (uint)Marshal.SizeOf(default(WNDCLASSEX)),
                style = CLASSSTLYE.CS_OWNDC,
                hInstance = _instance,
                lpfnWndProc = TempWndProc,
                lpszClassName = _className,
                hIcon = IntPtr.Zero,
                hIconSm = IntPtr.Zero,
                hCursor = IntPtr.Zero
            };

            ushort atom = User32.RegisterClassEx(ref wc);

            if (atom == 0)
                throw new Exception(string.Format("Failed to register window class. Error: {0}", Marshal.GetLastWin32Error()));

            _classRegistered = true;
        }

        IntPtr handle = User32.CreateWindowEx(0, _className, IntPtr.Zero, 0, 0, 0, 0, 0, HWND_MESSAGE, IntPtr.Zero, _instance, IntPtr.Zero);

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
        while (_exists)
        {
            //we use GetMessage so that the thread pauses in between messages
            int ret = User32.GetMessage(ref _msg, Handle, 0, 0);

            if (ret == -1)
                throw new Exception(string.Format("Error getting InputDevice Message: {0}", Marshal.GetLastWin32Error()));

            User32.TranslateMessage(ref _msg);
            User32.DispatchMessage(ref _msg);
        }
    }

    public void Destroy()
    {
        if (_exists)
        {
            _exists = false;
            User32.DestroyWindow(Handle);
        }
    }
}

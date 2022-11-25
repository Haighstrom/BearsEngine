using System.Runtime.InteropServices;
using BearsEngine.Input;
using BearsEngine.Win32API;

namespace BearsEngine.Window;

public class Win32Window : IWindow
{
    private const WindowClassStyle DEFAULT_CLASS_STYLE = 0;
    private const WINDOWSTYLE WS_PARENT_SIZING_BORDER = WINDOWSTYLE.WS_OVERLAPPED | WINDOWSTYLE.WS_CAPTION | WINDOWSTYLE.WS_SYSMENU | WINDOWSTYLE.WS_THICKFRAME | WINDOWSTYLE.WS_MAXIMIZEBOX | WINDOWSTYLE.WS_MINIMIZEBOX | WINDOWSTYLE.WS_CLIPCHILDREN;
    private const WINDOWSTYLE WS_PARENT_BORDER = WINDOWSTYLE.WS_OVERLAPPED | WINDOWSTYLE.WS_CAPTION | WINDOWSTYLE.WS_SYSMENU | WINDOWSTYLE.WS_MINIMIZEBOX | WINDOWSTYLE.WS_CLIPCHILDREN;
    private const WINDOWSTYLE WS_PARENT_NO_BORDER = WINDOWSTYLE.WS_POPUP | WINDOWSTYLE.WS_CLIPCHILDREN;
    private const WINDOWSTYLEEX EWS_PARENT = WINDOWSTYLEEX.WS_EX_APPWINDOW | WINDOWSTYLEEX.WS_EX_WINDOWEDGE;
    private const WINDOWSTYLE WS_CHILD = WINDOWSTYLE.WS_VISIBLE | WINDOWSTYLE.WS_CHILD | WINDOWSTYLE.WS_CLIPSIBLINGS;
    private const WINDOWSTYLEEX EWS_CHILD = 0;
    private const int HTCLIENT = 1;
    private const int CW_USEDEFAULT = int.MinValue;

    private static RECT GetWindowPosition(IntPtr windowHandle) 
    {
        _ = DWMAPI.DwmGetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out RECT rect, Marshal.SizeOf(default(RECT)));
        return rect;
    }

    /// <summary>
    /// How big the child window should be (based on the parent)
    /// </summary>
    /// <returns></returns>
    private static Point GetClientSize(IntPtr parentWindowHandle)
    {
        User32.GetClientRect(parentWindowHandle, out RECT r);
        return new Point(r.Width, r.Height);
    }

    private static void SetPixelFormat(IntPtr dc)
    {
        PIXELFORMATDESCRIPTOR pfd = new()
        {
            dwFlags = PIXELFORMATDESCRIPTOR.Flags.PFD_SUPPORT_OPENGL | PIXELFORMATDESCRIPTOR.Flags.PFD_DRAW_TO_WINDOW | PIXELFORMATDESCRIPTOR.Flags.PFD_DOUBLEBUFFER,
            iPixelType = PIXELFORMATDESCRIPTOR.PixelType.PFD_TYPE_RGBA,
            cColorBits = 24,
            cAlphaBits = 8,
            cDepthBits = 24,
            cStencilBits = 8,
        };

        int pixelFormat = GDI32.ChoosePixelFormat(dc, pfd);

        if (!GDI32.SetPixelFormat(dc, pixelFormat, ref pfd))
            throw new Exception($"Failed to set pixel format: {Marshal.GetLastWin32Error()}");
    }

    private readonly IntPtr _windowClassName = Marshal.StringToHGlobalAuto(Guid.NewGuid().ToString());
    private readonly IntPtr _instance = Marshal.GetHINSTANCE(typeof(Win32Window).Module);
    private readonly WNDPROC _wndProc; //need to reference this so it doesn't get garbage collected
    private readonly IntPtr _windowHandle, _childWindowHandle;
    private readonly WINDOWSTYLE _parentStyle;
    private readonly WINDOWSTYLEEX _parentExStyle;
    private bool _disposed = false;
    private MSG _lpMsg;
    private Rect _userPosition; //100% DPI visible Window Position
    private RECT _actualPosition; //DPI-adjusted visible Window Position
    private RECT _reportedPosition; //The position SetWindowRect/GetWindowRect uses - includes invisible borders on Win10 (FU Win10)
    private Point _userClientSize; //100% DPI visible Client Size
    private RECT _actualClientPosition; //DPI-adjusted visible Client Position
    private BorderStyle _border = BorderStyle.SizingBorder;
    private BorderStyle _prevBorder; //used when de-fullscreening
    private RECT _prevPosition; //used when restoring a borderless full screen window
    private WindowState _windowState = WindowState.Normal;
    private Cursor _cursor;
    private Icon _icon;
    private bool _cursorVisible = true;
    private bool _cursorLockedToWindow = false;
    private bool _resizingWindow = false;
    private int _leftInvisBorder, _topInvisBorder, _rightInvisBorder, _bottomInvisBorder;
    
    public Win32Window(WindowSettings settings)
    {
        _userPosition = new Rect();//just to shut up the nullable warnings
        _userClientSize = new Point(settings.Width, settings.Height);//just to shut up the nullable warnings

        ExitOnClose = settings.ExitOnClose;
        _cursor = settings.Cursor;
        _prevBorder = _border = BorderStyle.SizingBorder; //do this initially else WM_Resize screws up due to clientrect being empty
        _icon = settings.Icon;
        _wndProc = StandardWindowProcedure;

        //todo: move this code into a static constructor?
        if (Environment.OSVersion.Version >= new Version(10, 0, 15063)) // win 10 creators update added support for per monitor v2
            User32.SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
        else if (Environment.OSVersion.Version >= new Version(6, 3, 0)) // win 8.1 added support for per monitor dpi
            SHCore.SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.Process_Per_Monitor_DPI_Aware);
        else
            User32.SetProcessDPIAware();

        DPI = GDI32.GetDeviceCaps(User32.GetDC(IntPtr.Zero), GETDEVICECAPS_INDEX.LOGPIXELSX) / 96f;

        if (settings.MinClientSize.X < 0 || settings.MinClientSize.Y < 0 || settings.MaxClientSize.X < 0 || settings.MaxClientSize.Y < 0 || settings.Width < 0 || settings.Height < 0)
            throw new Exception($"Settings requested a negative size: ({settings})");

        if (settings.MinClientSize.X > settings.MaxClientSize.X && settings.MaxClientSize.X != 0)
            throw new Exception($"Settings Minimum Width is bigger than Maximum Width: ({settings})");
        if (settings.MinClientSize.Y > settings.MaxClientSize.Y && settings.MaxClientSize.Y != 0)
            throw new Exception($"Settings Minimum Height is bigger than Maximum Height: ({settings})");

        if (settings.Width < settings.MinClientSize.X || (settings.Width > settings.MaxClientSize.X && settings.MaxClientSize.X > 0))
            throw new Exception($"Settings Width is outside bounds of Min/Max ClientSize: ({settings})");
        if (settings.Height < settings.MinClientSize.Y || (settings.Height > settings.MaxClientSize.Y && settings.MaxClientSize.Y > 0))
            throw new Exception($"Settings Height is outside bounds of Min/Max ClientSize: ({settings})");

        MinClientSize = settings.MinClientSize;
        MaxClientSize = settings.MaxClientSize;

        var wcx = new WNDCLASSEX
        {
            cbSize = (uint)Marshal.SizeOf<WNDCLASSEX>(),
            style = DEFAULT_CLASS_STYLE,
            lpfnWndProc = _wndProc,
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = _instance,
            hIcon = _icon.HIcon,
            hCursor = _cursor.HCursor,
            hbrBackground = IntPtr.Zero,
            lpszMenuName = IntPtr.Zero,
            lpszClassName = _windowClassName,
            hIconSm = _icon.HIcon,
        };

        if (Marshal.GetLastWin32Error() != 0)
            throw new Exception(string.Format("Error: {0}", Marshal.GetLastWin32Error()));

        if (User32.RegisterClassEx(ref wcx) == 0)
            throw new Exception(string.Format("Failed to register window class. Error: {0}", Marshal.GetLastWin32Error()));

        _parentStyle = WS_PARENT_SIZING_BORDER;

        if (Visible)
            _parentStyle |= WINDOWSTYLE.WS_VISIBLE;

        _parentExStyle = EWS_PARENT;

        RECT rect = new() { right = (int)(settings.Width * DPI), bottom = (int)(settings.Height * DPI) };

        User32.AdjustWindowRectEx(ref rect, _parentStyle, false, _parentExStyle); //Note on Win10 this gives the wrong answer due to invis border...

        //shift so x,y are correct but w/h preserved
        rect.right += (int)(settings.X * DPI) - rect.left;
        rect.left = (int)(settings.X * DPI);
        rect.bottom += (int)(settings.Y * DPI) - rect.top;
        rect.top = (int)(settings.Y * DPI);

        IntPtr windowName = Marshal.StringToHGlobalAuto(settings.Title);

        _windowHandle = User32.CreateWindowEx(_parentExStyle, _windowClassName, windowName, _parentStyle, rect.left, rect.top, rect.Width, rect.Height, hWndParent: IntPtr.Zero, hMenu: IntPtr.Zero, _instance, lpParam: IntPtr.Zero);

        if (_windowHandle == IntPtr.Zero)
            throw new Exception(string.Format("Failed to create window. Error: {0}", Marshal.GetLastWin32Error()));

        WINDOWSTYLE style = WS_CHILD;
        WINDOWSTYLEEX exStyle = EWS_CHILD;

        _childWindowHandle = User32.CreateWindowEx(exStyle, _windowClassName, IntPtr.Zero, style, 0, 0, (int)(settings.Width * DPI), (int)(settings.Height * DPI), hWndParent: _windowHandle, hMenu: IntPtr.Zero, _instance, lpParam: IntPtr.Zero);

        if (_childWindowHandle == IntPtr.Zero)
            throw new Exception(string.Format("Failed to create window. Error: {0}", Marshal.GetLastWin32Error()));

        if (settings.Visible)
            User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_SHOW);
        User32.UpdateWindow(_windowHandle);

        //https://stackoverflow.com/questions/34139450/getwindowrect-returns-a-size-including-invisible-borders
        //hack for invisible windows - consider making window visible off screen temporarily?
        //Win11 ??
        if (!settings.Visible && Environment.OSVersion.Version.Major == 10)
            _leftInvisBorder = _rightInvisBorder = _bottomInvisBorder = (int)(7 * DPI);

        rect.left -= _leftInvisBorder;
        rect.right -= _leftInvisBorder;
        rect.top -= _topInvisBorder;
        rect.bottom -= _topInvisBorder;

        User32.SetWindowPos(_windowHandle, rect, SetWindowPosFlags.NOREDRAW);
        
        Border = settings.Border;

        if (settings.Centre)
            Centre();

        State = settings.State;
        CursorVisible = settings.CursorVisible;
        CursorLockedToWindow = settings.CursorLockedToWindow;

        DeviceContext = User32.GetDC(_childWindowHandle);

        SetPixelFormat(DeviceContext);

        RenderContext = CreateRenderContext(settings.OpenGLVersion.major, settings.OpenGLVersion.minor);

        OpenGL32.wglMakeCurrent(DeviceContext, RenderContext);

        string version = OpenGL32.GetString(GetStringEnum.Version).Remove(9);
        BE.Logging.Information($"Successfully set up OpenGL v:{version}, GLSL: {OpenGL32.GetString(GetStringEnum.ShadingLanguageVersion)}");
        BE.Logging.Information($"Graphics Vendor: {OpenGL32.GetString(GetStringEnum.Vendor)}");
        BE.Logging.Information($"Graphics Card: {OpenGL32.GetString(GetStringEnum.Renderer)}");
        BE.Logging.Information($"Graphics Card: {OpenGL32.GetString(GetStringEnum.Renderer)}");
    }

    internal IntPtr StandardWindowProcedure(IntPtr handle, WINDOWMESSAGE message, IntPtr wParam, IntPtr lParam)
    {
        switch (message)
        {
            case WINDOWMESSAGE.WM_DPICHANGED:
                DPI = wParam.ToHIWORD() / 96f;
                var proposedRect = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT))!;
                User32.SetWindowPos(_windowHandle, proposedRect, SetWindowPosFlags.NOZORDER | SetWindowPosFlags.NOACTIVATE);
                return IntPtr.Zero;

            case WINDOWMESSAGE.WM_MOVE:
                SetWindowPositionValues();
                if (!_resizingWindow && _cursorLockedToWindow)
                    ConfineCursor();
                Moved?.Invoke(this, EventArgs.Empty);
                break;

            case WINDOWMESSAGE.WM_SIZING:
                if (MinClientSize.X == 0 && MinClientSize.Y == 0 && MaxClientSize.X == 0 && MaxClientSize.Y == 0)
                    break;

                var plannedWindowR = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT))!;

                var border = (WM_SIZING_wParam)wParam.ToLong();

                RECT borderSize = new();
                User32.AdjustWindowRectEx(ref borderSize, _parentStyle, false, _parentExStyle);

                Point targetWindowSize = new()
                {
                    X = Maths.Clamp(plannedWindowR.Width - borderSize.Width, (int)Math.Round(MinClientSize.X * DPI), MaxClientSize.X == 0 ? plannedWindowR.Width : (int)Math.Round(MaxClientSize.X * DPI)) + borderSize.Width,
                    Y = Maths.Clamp(plannedWindowR.Height - borderSize.Height, (int)Math.Round(MinClientSize.Y * DPI), MaxClientSize.Y == 0 ? plannedWindowR.Height : (int)Math.Round(MaxClientSize.Y * DPI)) + borderSize.Height,
                };

                if (plannedWindowR.Width == targetWindowSize.X && plannedWindowR.Height == targetWindowSize.Y)
                    break;

                if (plannedWindowR.Width != targetWindowSize.X)
                    if (border == WM_SIZING_wParam.WMSZ_LEFT || border == WM_SIZING_wParam.WMSZ_TOPLEFT || border == WM_SIZING_wParam.WMSZ_BOTTOMLEFT)
                        plannedWindowR.left = plannedWindowR.right - (int)targetWindowSize.X;
                    else if (border == WM_SIZING_wParam.WMSZ_RIGHT || border == WM_SIZING_wParam.WMSZ_TOPRIGHT || border == WM_SIZING_wParam.WMSZ_BOTTOMRIGHT)
                        plannedWindowR.right = plannedWindowR.left + (int)targetWindowSize.X;

                if (plannedWindowR.Height != targetWindowSize.Y)
                    if (border == WM_SIZING_wParam.WMSZ_TOP || border == WM_SIZING_wParam.WMSZ_TOPLEFT || border == WM_SIZING_wParam.WMSZ_TOPRIGHT)
                        plannedWindowR.top = plannedWindowR.bottom - (int)targetWindowSize.Y;
                    else if (border == WM_SIZING_wParam.WMSZ_BOTTOM || border == WM_SIZING_wParam.WMSZ_BOTTOMLEFT || border == WM_SIZING_wParam.WMSZ_BOTTOMRIGHT)
                        plannedWindowR.bottom = plannedWindowR.top + (int)targetWindowSize.Y;

                Marshal.StructureToPtr(plannedWindowR, lParam, true);
                break;

            case WINDOWMESSAGE.WM_SIZE:
                SetWindowPositionValues();

                Point desiredUserClientSize = new()
                {
                    X = Maths.Clamp(_userClientSize.X, MinClientSize.X, MaxClientSize.X == 0 ? _userClientSize.X : MaxClientSize.X),
                    Y = Maths.Clamp(_userClientSize.Y, MinClientSize.Y, MaxClientSize.Y == 0 ? _userClientSize.Y : MaxClientSize.Y)
                };

                if (!_userClientSize.Equals(desiredUserClientSize))
                {
                    ClientSize = desiredUserClientSize;
                    break;
                }

                User32.SetWindowPos(_childWindowHandle, IntPtr.Zero, 0, 0, _actualClientPosition.Width, _actualClientPosition.Height, 
                    SetWindowPosFlags.NOZORDER | SetWindowPosFlags.NOOWNERZORDER | SetWindowPosFlags.NOACTIVATE | SetWindowPosFlags.NOSENDCHANGING);

                if (!_resizingWindow && _cursorLockedToWindow)
                    ConfineCursor();

                Resized?.Invoke(this, new SizeEventArgs(_actualClientPosition.Width, _actualClientPosition.Height));
                break;

            case WINDOWMESSAGE.WM_ENTERMENULOOP:
            case WINDOWMESSAGE.WM_ENTERSIZEMOVE:
                _resizingWindow = true;
                break;

            case WINDOWMESSAGE.WM_EXITMENULOOP:
            case WINDOWMESSAGE.WM_EXITSIZEMOVE:
                _resizingWindow = false;
                if (_cursorLockedToWindow)
                    ConfineCursor();
                break;

            case WINDOWMESSAGE.WM_SETCURSOR:
                if (lParam.ToLOWORD() == HTCLIENT)
                {
                    User32.SetCursor(_cursor.HCursor);
                    return IntPtr.Zero;
                }
            break;

            case WINDOWMESSAGE.WM_CHAR:
                char c;
                if (IntPtr.Size == 4) //Environment.Is64Bit?
                    c = (char)wParam.ToInt32();
                else
                    c = (char)wParam.ToInt64();

                if (!char.IsControl(c))
                    CharEntered?.Invoke(this, new KeyboardCharEventArgs(c));
                break;

            case WINDOWMESSAGE.WM_KEYDOWN:
            case WINDOWMESSAGE.WM_SYSKEYDOWN:
                bool extended = (lParam.ToInt64() & 1 << 24) != 0;
                short scancode = (short)((lParam.ToInt64() >> 16) & 0xff);
                VIRTUALKEYCODE vkey = (VIRTUALKEYCODE)wParam;

                Key key = KeyMap.TranslateKey(scancode, vkey, extended);

                if (key != Key.Unknown)
                    KeyDown?.Invoke(this, new KeyboardKeyEventArgs(key));
                break;

            case WINDOWMESSAGE.WM_KEYUP:
            case WINDOWMESSAGE.WM_SYSKEYUP:
                extended = (lParam.ToInt64() & 1 << 24) != 0;      //TODO - theres something called extended1 as well...
                scancode = (short)((lParam.ToInt64() >> 16) & 0xff);
                vkey = (VIRTUALKEYCODE)wParam;

                key = KeyMap.TranslateKey(scancode, vkey, extended);

                if (key != Key.Unknown)
                    KeyUp?.Invoke(this, new Input.KeyboardKeyEventArgs(key));
                break;
            

            case WINDOWMESSAGE.WM_ACTIVATE:
                bool newFocus = wParam.ToLOWORD() != 0;

                if (newFocus != Focussed)
                {
                    Focussed = newFocus;
                    if (Focussed && _cursorLockedToWindow)
                        ConfineCursor();
                    FocusChanged?.Invoke(this, new FocusChangedEventArgs(newFocus));
                }
                break;
            

            case WINDOWMESSAGE.WM_SETFOCUS:
                if (!Focussed)
                {
                    Focussed = true;
                    FocusChanged?.Invoke(this, new FocusChangedEventArgs(true));
                }
                break;
            

            case WINDOWMESSAGE.WM_KILLFOCUS:
                if (Focussed)
                {
                    Focussed = false;
                    FocusChanged?.Invoke(this, new FocusChangedEventArgs(false));
                }
                break;
            

            //https://docs.microsoft.com/en-us/windows/win32/learnwin32/closing-the-window

            case WINDOWMESSAGE.WM_CLOSE:
                CloseAttempted?.Invoke(this, EventArgs.Empty);
                if (ExitOnClose)
                    break;
                return IntPtr.Zero;
            

            case WINDOWMESSAGE.WM_DESTROY:
                Closed?.Invoke(this, EventArgs.Empty);
                IsOpen = false;
                Dispose();
                break;
                
            
        }

        return User32.DefWindowProc(handle, message, wParam, lParam);
    }
    
    private IntPtr CreateRenderContext(int major, int minor)
    {
        if (major < 1 || minor < 0)
            throw new Exception($"invalid GL version to create: {major}.{minor}.");

        BE.Logging.Information($"Creating GL Context: Requested Version {major}.{minor}");

        //create temp context to be able to call wglGetProcAddress
        IntPtr tempContext = OpenGL32.wglCreateContext(DeviceContext);
        if (tempContext == IntPtr.Zero)
            throw new Exception("tempContext failed to create.");
        if (!OpenGL32.wglMakeCurrent(DeviceContext, tempContext))
            throw new Exception("wglMakeCurrent Failed");

        OpenGL32.LoadWGLExtensions();

        var renderContext = OpenGL32.CreateRenderContext(DeviceContext, (major, minor));

        OpenGL32.LoadOpenGL3Extensions();

        OpenGL32.wglDeleteContext(tempContext);

        return renderContext;
    }
    
    private void SetWindowPositionValues()
    {
        User32.GetWindowRect(_windowHandle, out _reportedPosition);
        _ = DWMAPI.DwmGetWindowAttribute(_windowHandle, DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out _actualPosition, RECT.UnmanagedSize);
        _userPosition = UserParentRect_From_ActualParentRect(_actualPosition);
        User32.GetClientRect(_windowHandle, out _actualClientPosition);
        POINT tl = new() { X = _actualClientPosition.left, Y = _actualClientPosition.top };
        User32.ClientToScreen(_windowHandle, ref tl);
        POINT br = new() { X = _actualClientPosition.right, Y = _actualClientPosition.bottom };
        User32.ClientToScreen(_windowHandle, ref br);
        _actualClientPosition.left = tl.X;
        _actualClientPosition.top = tl.Y;;
        _actualClientPosition.right = br.X;
        _actualClientPosition.bottom = br.Y;
        _userClientSize = UserClientPoint_From_ActualClientRect(_actualClientPosition);

        _leftInvisBorder = _actualPosition.left - _reportedPosition.left;
        _topInvisBorder = _actualPosition.top - _reportedPosition.top;
        _rightInvisBorder = _reportedPosition.right - _actualPosition.right;
        _bottomInvisBorder = _reportedPosition.bottom - _actualPosition.bottom;
    }
    
    /// <summary>
    /// User requested Window to be a certain size. What RECT needs to be used in SetWindowPos to acheive it?
    /// We need to add DPI and invisible borders.
    /// _DPI, _leftInvisBorder, _topInvisBorder, _rightInvisBorder, _bottomInvisBorder must have all been set
    /// </summary>
    private RECT ReportedParentRect_From_UserParentRect(Rect requested)
    {
        RECT answer = new() { left = (int)requested.Left, top = (int)requested.Top, right = (int)requested.Right, bottom = (int)requested.Bottom };

        answer.left = (int)Math.Round(answer.left * DPI) - _leftInvisBorder;
        answer.top = (int)Math.Round(answer.top * DPI) - _topInvisBorder;
        answer.right = (int)Math.Round(answer.right * DPI) + _rightInvisBorder;
        answer.bottom = (int)Math.Round(answer.bottom * DPI) + _bottomInvisBorder;

        return answer;
    }    

    /// <summary>
    /// Transform the parent rect down to the user coordinations by scaling down by DPI
    /// </summary>
    private Rect UserParentRect_From_ActualParentRect(RECT rect)
    {
        return new Rect(
            (int)Math.Round(rect.left / DPI),
            (int)Math.Round(rect.top / DPI),
            (int)Math.Round(rect.Width / DPI),
            (int)Math.Round(rect.Height / DPI));
    }    

    /// <summary>
    /// Transform the parent rect down to the user coordinations by scaling down by DPI
    /// </summary>
    private Point UserClientPoint_From_ActualClientRect(RECT actualClientRect)
    {
        return new Point(
            (int)Math.Round(actualClientRect.Width / DPI),
            (int)Math.Round(actualClientRect.Height / DPI));
    }    

    private RECT ActualClientRect_From_UserClientPoint(Point userClientPoint)
    {
        return new()
        {
            right = (int)Math.Round(userClientPoint.X * DPI),
            bottom = (int)Math.Round(userClientPoint.Y * DPI),
        };
    }    

    private void ConfineCursor()
    {
        User32.ClipCursor(ref _actualPosition);
    }    

    private void UnconfineCursor()
    {
        User32.ClipCursor(IntPtr.Zero);
    }

    public bool IsOpen { get; private set; } = true;

    public bool Visible
    {
        get => User32.IsWindowVisible(_windowHandle);
        set
        {
            if (value)
            {
                User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_SHOW);
                User32.BringWindowToTop(_windowHandle);
                User32.SetForegroundWindow(_windowHandle);
            }
            else
                User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_HIDE);
        }
    }

    public void Close()
    {
        if (IsOpen)
            User32.PostMessage(_windowHandle, WINDOWMESSAGE.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        else
            BE.Logging.Error($"Called Close on a {typeof(Win32Window).Name} window that was already destroyed.");
    }

    public void Exit()
    {
        if (IsOpen)
            User32.DestroyWindow(_windowHandle);
        else
            BE.Logging.Error($"Called Exit on a {typeof(Win32Window).Name} window that was already destroyed.");
    }

    public void ProcessEvents()
    {
        while (User32.PeekMessage(ref _lpMsg, _windowHandle, 0, 0, PEEKMESSAGEFLAG.PM_REMOVE))
        {
            User32.TranslateMessage(ref _lpMsg);
            User32.DispatchMessage(ref _lpMsg);
        }
    }

    public void SwapBuffers() => GDI32.SwapBuffers(DeviceContext);

    public bool ExitOnClose { get; set; }

    public event EventHandler? CloseAttempted;

    public event EventHandler? Closed;

    public string Title
    {
        get
        {
            int length = User32.GetWindowTextLength(_windowHandle);
            System.Text.StringBuilder sb = new(length + 1);
            int resultLength = User32.GetWindowText(_windowHandle, sb, sb.Capacity);

            if (resultLength == 0)
                BE.Logging.Warning($"Window title is empty, error code: {Marshal.GetLastWin32Error()}");

            return sb.ToString();
        }
        set
        {
            if (Title != value)
            {
                if (!User32.SetWindowText(_windowHandle, value))
                    BE.Logging.Error($"Failed to change window title, requested: {value}, error code: {Marshal.GetLastWin32Error()}");
            }
        }
    }
    
    //https://stackoverflow.com/questions/706921/problems-with-setting-application-icon
    public Icon Icon
    {
        get => _icon;
        set
        {
            if (value == _icon)
                return;

            User32.SendMessage(_windowHandle, WINDOWMESSAGE.WM_SETICON, (IntPtr)0, _icon.HIcon);
            User32.SendMessage(_windowHandle, WINDOWMESSAGE.WM_SETICON, (IntPtr)1, _icon.HIcon);

            _icon.Dispose();
            _icon = value;
        }
    }

    public BorderStyle Border
    {
        get => _border;
        set
        {
            if (_border == value)
                return;

            WINDOWSTYLE newStyle = value switch
            {
                BorderStyle.SizingBorder => WINDOWSTYLE.WS_OVERLAPPED | WINDOWSTYLE.WS_CAPTION | WINDOWSTYLE.WS_SYSMENU | WINDOWSTYLE.WS_THICKFRAME | WINDOWSTYLE.WS_MAXIMIZEBOX | WINDOWSTYLE.WS_MINIMIZEBOX,
                BorderStyle.Border => WINDOWSTYLE.WS_OVERLAPPED | WINDOWSTYLE.WS_CAPTION | WINDOWSTYLE.WS_SYSMENU | WINDOWSTYLE.WS_MINIMIZEBOX,
                BorderStyle.NoBorder => WINDOWSTYLE.WS_POPUP,
                _ => throw new NotImplementedException(),
            };
            if (Visible)
                newStyle |= WINDOWSTYLE.WS_VISIBLE;

            User32.SetWindowLongPtr(_windowHandle, GWL.GWL_STYLE, new IntPtr((int)newStyle));

            //preserve client size
            RECT newWinRect = new() { right = _actualClientPosition.Width, bottom = _actualClientPosition.Height };
            User32.AdjustWindowRectEx(ref newWinRect, newStyle, false, _parentExStyle);
            User32.SetWindowPos(_windowHandle, IntPtr.Zero, 0, 0, newWinRect.Width, newWinRect.Height, SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOZORDER | SetWindowPosFlags.FRAMECHANGED);

            _border = value;
        }
    }
    
    public WindowState State
    {
        get => _windowState;
        set
        {
            if (_windowState == value)
                return;

            //coming out of fullscreen - reset the border
            if (_windowState == WindowState.Fullscreen)
                Border = _prevBorder;

            switch (value)
            {
                case WindowState.Normal:
                    if (_border != BorderStyle.SizingBorder && (State == WindowState.Maximized || State == WindowState.Fullscreen))
                        User32.SetWindowPos(_windowHandle, _prevPosition, 0);
                    else
                        User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_RESTORE);
                    break;
                case WindowState.Minimized:
                    User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_MINIMIZE);
                    break;
                case WindowState.Maximized:
                    if (Border == BorderStyle.SizingBorder)
                        User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_MAXIMIZE);
                    else //avoid putting it on top of the taskbar (that's what Fullscreen is for)
                    {
                        _prevPosition = _reportedPosition;

                        IntPtr monitor = User32.MonitorFromWindow(_windowHandle, MonitorFrom.Nearest);
                        var mInfo = new MonitorInfo() { Size = MonitorInfo.UnmanagedSize };
                        User32.GetMonitorInfo(monitor, ref mInfo);
                        mInfo.Work.left -= _leftInvisBorder;
                        mInfo.Work.top -= _topInvisBorder;
                        mInfo.Work.right += _rightInvisBorder;
                        mInfo.Work.bottom += _bottomInvisBorder;

                        User32.SetWindowPos(_windowHandle, mInfo.Work, 0);
                    }
                    break;
                case WindowState.Fullscreen:
                    _prevBorder = _border;
                    _prevPosition = _reportedPosition;
                    Border = BorderStyle.NoBorder;
                    User32.ShowWindow(_windowHandle, SHOWWINDOWCOMMAND.SW_MAXIMIZE);
                    break;
            }

            _windowState = value;
        }
    }

    public float DPI { get; private set; }

    public Rect Position
    {
        get => _userPosition;
        set
        {
            RECT r = ReportedParentRect_From_UserParentRect(value);

            User32.SetWindowPos(_windowHandle, new IntPtr(0), r.left, r.top, r.Width, r.Height, SetWindowPosFlags.NOREDRAW);
        }
    }

    public int X
    {
        get => (int)Position.X;
        set => Position = new Rect(value, Y, Width, Height);
    }

    public int Y
    {
        get => (int)Position.Y;
        set => Position = new Rect(X, value, Width, Height);
    }

    public int Width
    {
        get => (int)Position.W;
        set => Position = new Rect(X, Y, value, Height);
    }
    
    public int Height
    {
        get => (int)Position.H;
        set => Position = new Rect(X, Y, Width, value);
    }
    
    public Point ClientSize
    {
        get => _userClientSize;
        set
        {
            RECT r = ActualClientRect_From_UserClientPoint(value);
            User32.AdjustWindowRectEx(ref r, _parentStyle, false, _parentExStyle);
            User32.SetWindowPos(_windowHandle, new IntPtr(0), X, Y, r.Width, r.Height, SetWindowPosFlags.NOREDRAW);
        }
    }

    public Rect Viewport => new Rect(0, 0, _actualClientPosition.Width, _actualClientPosition.Height);

    public Point MinClientSize { get; set; }

    public Point MaxClientSize { get; set; }

    public void Centre()
    {
        IntPtr monitor = User32.MonitorFromWindow(_windowHandle, MonitorFrom.Nearest);

        var mInfo = new MonitorInfo() { Size = MonitorInfo.UnmanagedSize };

        User32.GetMonitorInfo(monitor, ref mInfo);

        int x = Math.Max(0, (int)(mInfo.Work.Centre.X - 0.5f * _actualPosition.Width)) - _leftInvisBorder;
        int y = Math.Max(0, (int)(mInfo.Work.Centre.Y - 0.5f * _actualPosition.Height)) - _topInvisBorder;

        RECT rect = new()
        {
            left = x,
            right = x + _reportedPosition.Width,
            top = y,
            bottom = y + _reportedPosition.Height
        };

        User32.SetWindowPos(_windowHandle, rect, SetWindowPosFlags.NOREDRAW);
    }

    public Point ScreenToClient(Point screenPosition) => new((int)((screenPosition.X - _actualClientPosition.left) / DPI), (int)((screenPosition.Y - _actualClientPosition.top) / DPI));

    public event EventHandler? Moved;

    public event EventHandler? Resized;

    public bool Focussed { get; private set; }

    public void MakeFocussed()
    {
        if (!Focussed)
        {
            User32.SetFocus(_windowHandle);
            User32.BringWindowToTop(_windowHandle);
            User32.SetForegroundWindow(_windowHandle);
        }
        else
            BE.Logging.Warning("Tried to focus the window when it was already focussed.");
    }

    public event EventHandler<FocusChangedEventArgs>? FocusChanged;

    //http://www.cplusplus.com/forum/windows/97017/
    //https://docs.microsoft.com/en-us/windows/win32/learnwin32/setting-the-cursor-image
    public Cursor Cursor
    {
        get => _cursor;
        set
        {
            if (value == _cursor)
                return;

            User32.SetCursor(value.HCursor);

            _cursor.Dispose();
            _cursor = value;
        }
    }
    
    public bool CursorVisible
    {
        get => _cursorVisible;
        set
        {
            if (value == _cursorVisible)
                return;

            _ = User32.ShowCursor(value);
            _cursorVisible = value;
        }
    }
    
    public bool CursorLockedToWindow
    {
        get => _cursorLockedToWindow;
        set
        {
            if (_cursorLockedToWindow == value)
                return;

            if (value)
                ConfineCursor();
            else
                UnconfineCursor();

            _cursorLockedToWindow = value;
        }
    }

    /// <summary>
    /// Called whenever a character, text number or symbol, is input by the keyboard. Will not record modifier keys like shift and alt.
    /// This reflects the actual character input, ie takes into account caps lock, shift keys, numlock etc etc and will catch rapid-fire inputs from a key held down for an extended time. 
    /// Use for eg text box input, rather than for controlling a game character (Use Input.GetKeyboardState)
    /// </summary>
    public event EventHandler<KeyboardCharEventArgs>? CharEntered;

    /// <summary>
    /// Called whenever a keyboard key is pressed
    /// </summary>
    public event EventHandler<KeyboardKeyEventArgs>? KeyDown;

    /// <summary>
    /// Called whenever a keyboard key is released
    /// </summary>
    public event EventHandler<KeyboardKeyEventArgs>? KeyUp;

    public IntPtr DeviceContext { get; }

    public IntPtr RenderContext { get; }

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                // TODO: dispose managed state (managed objects)
            }
            else
                BE.Logging.Warning("Window was disposed by the finaliser.");

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            //User32.UnregisterClass(_windowClassName, _instance); //Not required? https://stackoverflow.com/questions/29654139/how-to-call-and-use-unregisterclass

            _disposed = true;
        }
    }

    // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~Win32Window()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }

#if DEBUG
    private void LogWinRects()
    {
        BE.Logging.Debug($"_userPosition: {_userPosition}");
        BE.Logging.Debug($"_actualPosition: {_actualPosition}");
        BE.Logging.Debug($"_reportedPosition: {_reportedPosition}");
        BE.Logging.Debug($"_userClientSize: {_userClientSize}");
        BE.Logging.Debug($"_actualClientPosition: {_actualClientPosition}");
        BE.Logging.Debug($"Window Position: {GetWindowPosition(_windowHandle)}");
        BE.Logging.Debug($"Window Client: {GetClientSize(_windowHandle)}");
        BE.Logging.Debug($"Child Position: {GetWindowPosition(_childWindowHandle)}");
        BE.Logging.Debug($"Child Client: {GetClientSize(_childWindowHandle)}");
    }
    
    private static void LogIntPtr(IntPtr pointer) => BE.Logging.Debug(pointer.ToLong().ToString());
#endif
}
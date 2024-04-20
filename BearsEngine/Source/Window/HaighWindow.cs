using System.Runtime.InteropServices;

namespace BearsEngine.Window;

public class HaighWindow : IWindow
{
    private readonly IWindow _api;

    public HaighWindow(WindowSettings settings)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            _api = new Windows.WinAPIWindow(settings);
        else
            throw new NotImplementedException();
    }

    public bool IsOpen => _api.IsOpen;

    public bool Visible { get => _api.Visible; set => _api.Visible = value; }

    public bool ExitOnClose { get => _api.ExitOnClose; set => _api.ExitOnClose = value; }
    
    public string Title { get => _api.Title; set => _api.Title = value; }
    
    public Icon Icon { get => _api.Icon; set => _api.Icon = value; }
    
    public BorderStyle Border { get => _api.Border; set => _api.Border = value; }
    
    public WindowState State { get => _api.State; set => _api.State = value; }

    public float DPI => _api.DPI;

    public Rect Position { get => _api.Position; set => _api.Position = value; }
    
    public int X { get => _api.X; set => _api.X = value; }
    
    public int Y { get => _api.Y; set => _api.Y = value; }
    
    public int WindowWidth { get => _api.WindowWidth; set => _api.WindowWidth = value; }
    
    public int WindowHeight { get => _api.WindowHeight; set => _api.WindowHeight = value; }
    
    public Point ClientSize { get => _api.ClientSize; set => _api.ClientSize = value; }

    public Rect Viewport => _api.Viewport;

    public Point MinClientSize { get => _api.MinClientSize; set => _api.MinClientSize = value; }
    
    public Point MaxClientSize { get => _api.MaxClientSize; set => _api.MaxClientSize = value; }

    public bool Focussed => _api.Focussed;

    public Cursor Cursor { get => _api.Cursor; set => _api.Cursor = value; }
    
    public bool CursorVisible { get => _api.CursorVisible; set => _api.CursorVisible = value; }
    
    public bool CursorLockedToWindow { get => _api.CursorLockedToWindow; set => _api.CursorLockedToWindow = value; }

    public IntPtr DeviceContextHandle => _api.DeviceContextHandle;

    public IntPtr RenderContextHandle => _api.RenderContextHandle;

    /// <summary>
    /// Specifies whether VSync should be used. Vsync delays render frames until the monitor refreshes. It will avoid screen tearing, but will cause render frames to delay.
    /// </summary>
    public bool VSync
    {
        get => _api.VSync;
        set => _api.VSync = value;
    }
    public int ClientWidth { get => _api.ClientWidth; set => _api.ClientWidth = value; }
    public int ClientHeight { get => _api.ClientHeight; set => _api.ClientHeight = value; }

    public void Centre() => _api.Centre();

    public void Close() => _api.Close();

    public void Dispose() => _api.Dispose();

    public void Exit() => _api.Exit();

    public void MakeFocussed() => _api.MakeFocussed();

    public void ProcessEvents() => _api.ProcessEvents();

    public Point ScreenToClient(Point screenPosition) => _api.ScreenToClient(screenPosition);

    public void SwapBuffers() => _api.SwapBuffers();

    public event EventHandler? CloseAttempted
    {
        add => _api.CloseAttempted += value;

        remove => _api.CloseAttempted -= value;
    }

    public event EventHandler? Closed
    {
        add => _api.Closed += value;

        remove => _api.Closed -= value;
    }

    public event EventHandler? Moved
    {
        add => _api.Moved += value;

        remove => _api.Moved -= value;
    }

    public event EventHandler? Resized
    {
        add => _api.Resized += value;

        remove => _api.Resized -= value;
    }

    public event EventHandler<FocusChangedEventArgs>? FocusChanged
    {
        add => _api.FocusChanged += value;

        remove => _api.FocusChanged -= value;
    }

    public event EventHandler<KeyboardCharEventArgs>? CharEntered
    {
        add => _api.CharEntered += value;

        remove => _api.CharEntered -= value;
    }

    public event EventHandler<KeyboardKeyEventArgs>? KeyDown
    {
        add => _api.KeyDown += value;

        remove => _api.KeyDown -= value;
    }

    public event EventHandler<KeyboardKeyEventArgs>? KeyUp
    {
        add => _api.KeyUp += value;

        remove => _api.KeyUp -= value;
    }
}

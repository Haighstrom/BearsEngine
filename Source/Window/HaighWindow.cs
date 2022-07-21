using BearsEngine.Input;

namespace BearsEngine.Window;

public class HaighWindow : IWindow
{
    private IWindow _implementation;

    public HaighWindow(WindowSettings settings)
    {
        _implementation = new Win32Window(settings);
    }

    #region Basic
    public bool IsOpen => _implementation.IsOpen;
    public bool Visible { get => _implementation.Visible; set => _implementation.Visible = value; }
    public void Close() => _implementation.Close();
    public void Exit() => _implementation.Exit();
    public void ProcessEvents() => _implementation.ProcessEvents();
    public void SwapBuffers() => _implementation.SwapBuffers();
    public bool ExitOnClose
    {
        get => _implementation.ExitOnClose;
        set => _implementation.ExitOnClose = value;
    }
    public event EventHandler CloseAttempted
    {
        add => _implementation.CloseAttempted += value;
        remove => _implementation.CloseAttempted -= value;
    }
    public event EventHandler Closed
    {
        add => _implementation.Closed += value;
        remove => _implementation.Closed -= value;
    }
    #endregion

    #region Window Frame
    public string Title
    {
        get => _implementation.Title;
        set => _implementation.Title = value;
    }
    public Icon Icon
    {
        get => _implementation.Icon;
        set => _implementation.Icon = value;
    }
    public BorderStyle Border
    {
        get => _implementation.Border;
        set => _implementation.Border = value;
    }
    public WindowState State
    {
        get => _implementation.State;
        set => _implementation.State = value;
    }
    #endregion

    #region Position
    public float DPI { get => _implementation.DPI; }
    public IRect Position
    {
        get => _implementation.Position;
        set => _implementation.Position = value;
    }
    public int X
    {
        get => _implementation.X;
        set => _implementation.X = value;
    }
    public int Y
    {
        get => _implementation.Y;
        set => _implementation.Y = value;
    }
    public int Width
    {
        get => _implementation.Width;
        set => _implementation.Width = value;
    }
    public int Height
    {
        get => _implementation.Height;
        set => _implementation.Height = value;
    }
    public Point ClientSize
    {
        get => _implementation.ClientSize;
        set => _implementation.ClientSize = value;
    }
    public IRect Viewport => _implementation.Viewport;
    public Point MinClientSize
    {
        get => _implementation.MinClientSize;
        set => _implementation.MinClientSize = value;
    }
    public Point MaxClientSize
    {
        get => _implementation.MaxClientSize;
        set => _implementation.MaxClientSize = value;
    }
    public void Centre() => _implementation.Centre();
    public Point ScreenToClient(Point screenPosition) => _implementation.ScreenToClient(screenPosition);
    public event EventHandler Moved
    {
        add => _implementation.Moved += value;
        remove => _implementation.Moved -= value;
    }
    public event EventHandler Resized
    {
        add => _implementation.Resized += value;
        remove => _implementation.Resized -= value;
    }
    #endregion

    #region Focus
    public bool IsFocussed => _implementation.IsFocussed;
    public void MakeFocussed() => _implementation.MakeFocussed();

    public event EventHandler<BoolEventArgs> FocusChanged
    {
        add => _implementation.FocusChanged += value;
        remove => _implementation.FocusChanged -= value;
    }
    #endregion

    #region Cursor
    public Cursor Cursor
    {
        get => _implementation.Cursor;
        set => _implementation.Cursor = value;
    }
    public bool CursorVisible
    {
        get => _implementation.CursorVisible;
        set => _implementation.CursorVisible = value;
    }
    public bool CursorLockedToWindow
    {
        get => _implementation.CursorLockedToWindow;
        set => _implementation.CursorLockedToWindow = value;
    }
    #endregion

    #region Keyboard Input
    public event EventHandler<KeyboardCharEventArgs>? CharEntered
    {
        add => _implementation.CharEntered += value;
        remove => _implementation.CharEntered -= value;
    }
    public event EventHandler<KeyboardKeyEventArgs>? KeyDown
    {
        add => _implementation.KeyDown += value;
        remove => _implementation.KeyDown -= value;
    }
    public event EventHandler<KeyboardKeyEventArgs>? KeyUp
    {
        add => _implementation.KeyUp += value;
        remove => _implementation.KeyUp -= value;
    }
    #endregion

    #region OpenGL
    public IntPtr DeviceContext => _implementation.DeviceContext;
    public IntPtr RenderContext => _implementation.RenderContext;
    #endregion

    #region IDisposable
    public void Dispose() => _implementation.Dispose();
    #endregion
}

using BearsEngine.Window;

namespace BearsEngine;

public class WindowSettings
{
    /// <summary>
    /// The Default settings.
    /// </summary>
    public static WindowSettings Default => new();

    /// <summary>
    /// Type of border for the window. Defaults to a sizing border.
    /// </summary>
    public BorderStyle Border { get; set; } = BorderStyle.Resizable;

    /// <summary>
    /// Whether the window should be created centred on the screen on creation - ignores X,Y
    /// </summary>
    public bool Centre { get; set; } = false;

    /// <summary>
    /// The cursor that should be used while the mouse is over the window
    /// </summary>
    public Cursor Cursor { get; set; } = Cursor.Default;

    /// <summary>
    /// Whether the mouse cursor is locked within the window. Defaults to false.
    /// </summary>
    public bool CursorLockedToWindow { get; set; } = false;

    /// <summary>
    /// Whether the mouse is initially visible. Defaults to true.
    /// </summary>
    public bool CursorVisible { get; set; } = true;

    /// <summary>
    /// Whether when the window starts to close (e.g. X pressed, Close() called) the window will be destroyed. True by default.
    /// Change this to false to allow custom behaviour when closing the window (e.g. popup saying there are unsaved changes).
    /// Use Window.Exit to finally destroy the window, and hook onto the Window.Closing event to replace the default Close() functionality.
    /// </summary>
    public bool ExitOnClose { get; set; } = true;

    /// <summary>
    /// The desired Height of the window client
    /// </summary>
    public int Height { get; set; } = 400;

    /// <summary>
    /// The Window Icon (on the window and taskbar)
    /// </summary>
    public Icon Icon { get; set; } = Icon.Default;

    /// <summary>
    /// Restricts the maximum size of the Window
    /// </summary>
    public Point MaxClientSize { get; set; } = new();
    /// <summary>
    /// Restricts the minimum size of the Window
    /// </summary>
    public Point MinClientSize { get; set; } = new();

    /// <summary>
    /// The OpenGL version to be used for graphics.
    /// </summary>
    public (int major, int minor) OpenGLVersion = (4, 5);

    /// <summary>
    /// Specifies whether VSync should be used. Vsync delays render frames until the monitor refreshes. It will avoid screen tearing, but will cause render frames to delay.
    /// </summary>
    public bool VSync = false;

    /// <summary>
    /// The desired position of the window (X,Y are the top left of the window, W, H are the size of the client)
    /// </summary>
    public Rect Position
    {
        get => new(X, Y, Width, Height);
        set
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Width = (int)value.W;
            Height = (int)value.H;
        }
    }

    /// <summary>
    /// State of the window.
    /// </summary>
    public WindowState State { get; set; } = WindowState.Normal;

    /// <summary>
    /// The title of the window
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// The desired Width of the window client
    /// </summary>
    public int Width { get; set; } = 400;

    /// <summary>
    /// The desired X (left) position of the window
    /// </summary>
    public int X { get; set; } = 100;

    /// <summary>
    /// The desired Y (top) position of the window
    /// </summary>
    public int Y { get; set; } = 100;
}

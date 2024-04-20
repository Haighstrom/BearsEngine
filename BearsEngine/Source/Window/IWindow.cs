namespace BearsEngine.Window;

/// <summary>
/// A type to represent the application window.
/// </summary>
public interface IWindow : IDisposable
{
    /// <summary>
    /// Type of border for the window
    /// </summary>
    BorderStyle Border { get; set; }

    /// <summary>
    /// Gets or sets the size of the drawing area (excluding borders and title bar)
    /// </summary>
    Point ClientSize { get; set; }

    /// <summary>
    /// Gets or sets the width of the drawing area (excluding borders and title bar)
    /// </summary>
    int ClientWidth { get; set; }

    /// <summary>
    /// Gets or sets the height of the drawing area (excluding borders and title bar)
    /// </summary>
    int ClientHeight { get; set; }

    /// <summary>
    /// Gets or sets the current mouse cursor
    /// </summary>
    Cursor Cursor { get; set; }

    /// <summary>
    /// Gets or sets whether the mouse cursor is locked within the window. Defaults to false.
    /// </summary>
    bool CursorLockedToWindow { get; set; }

    /// <summary>
    /// Gets or sets whether the mouse is currently visible. Defaults to true.
    /// </summary>
    bool CursorVisible { get; set; }

    IntPtr DeviceContextHandle { get; }

    /// <summary>
    /// The DPI scaling currently applicable to the window
    /// </summary>
    float DPI { get; }

    /// <summary>
    /// Gets or sets whether when Close events happen (e.g. X pressed, Close() called) the window will be destroyed. True by default.
    /// Change this to false to allow custom behaviour when closing the window (e.g. popup saying there are unsaved changes).
    /// </summary>
    bool ExitOnClose { get; set; }

    /// <summary>
    /// Returns whether the window is the currently active, focussed window (receiving input)
    /// </summary>
    bool Focussed { get; }

    /// <summary>
    /// Gets or sets the height of the window (including borders and title bar)
    /// </summary>
    int WindowHeight { get; set; }

    /// <summary>
    /// Gets or sets the Window Icon (on the window and taskbar)
    /// </summary>
    Icon Icon { get; set; }

    /// <summary>
    /// Restricts the minimum size of the Window
    /// </summary>
    Point MinClientSize { get; set; }

    /// <summary>
    /// Restricts the maximum size of the Window
    /// </summary>
    Point MaxClientSize { get; set; }

    /// <summary>
    /// Gets whether the window has been created and has not been destroyed.
    /// False once Exit() is called.
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// Gets or sets the position of the overall window (including borders and title bar)
    /// </summary>
    Rect Position { get; set; }

    IntPtr RenderContextHandle { get; }

    /// <summary>
    /// Gets or sets the state of the window
    /// </summary>
    WindowState State { get; set; }

    /// <summary>
    /// Gets or sets the title of the window
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// The position of the client after DPI has been considered - needed for OpenGL.Viewport
    /// </summary>
    Rect Viewport { get; }

    /// <summary>
    /// Gets or sets whether the window can currently be seen. True by default.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Specifies whether VSync should be used. Vsync delays render frames until the monitor refreshes. It will avoid screen tearing, but will cause render frames to delay.
    /// </summary>
    bool VSync { get; set; }

    /// <summary>
    /// Gets or sets the width of the window (including borders and title bar)
    /// </summary>
    int WindowWidth { get; set; }

    /// <summary>
    /// Gets or sets the X position of the top left point of the window (including borders and title bar)
    /// </summary>
    int X { get; set; }

    /// <summary>
    /// Gets or sets the Y position of the top left point of the window (including borders and title bar)
    /// </summary>
    int Y { get; set; }

    /// <summary>
    /// Centres the window to the screen it's centre is currently on
    /// </summary>
    void Centre();

    /// <summary>
    /// Requests to close the Window. Triggers the Closing event and will destroy the window if ExitOnClose is true.
    /// </summary>
    void Close();

    /// <summary>
    /// Destroys the window.
    /// </summary>
    void Exit();

    /// <summary>
    /// Makes this window focussed (brings to top / starts receiving input)
    /// </summary>
    void MakeFocussed();

    /// <summary>
    /// Processes all pending OS messages/events.
    /// </summary>
    void ProcessEvents();

    /// <summary>
    /// Returns the DPI-ignored client position for a given DPI-inclusive screen position.
    /// </summary>
    Point ScreenToClient(Point screenPosition);

    /// <summary>
    /// Swaps buffers to render current scene.
    /// </summary>
    void SwapBuffers();

    /// <summary>
    /// Called whenever a character, text number or symbol, is input by the keyboard. Will not record modifier keys like shift and alt.
    /// This reflects the actual character input, ie takes into account caps lock, shift keys, numlock etc etc and will catch rapid-fire inputs from a key held down for an extended time. 
    /// Use for eg text box input, rather than for controlling a game character (Use Input.GetKeyboardState)
    /// </summary>
    public event EventHandler<KeyboardCharEventArgs>? CharEntered;

    /// <summary>
    /// Triggers whenever the Window X is pressed, Close is called, Alt-F4 pressed, etc.
    /// </summary>
    event EventHandler? CloseAttempted;

    /// <summary>
    /// Triggers after the window has been closed/destroyed.
    /// </summary>
    event EventHandler? Closed;

    /// <summary>
    /// Triggers when the window's focus changes
    /// </summary>
    event EventHandler<FocusChangedEventArgs>? FocusChanged;

    /// <summary>
    /// Called whenever a keyboard key is pressed
    /// </summary>
    event EventHandler<KeyboardKeyEventArgs>? KeyDown;

    /// <summary>
    /// Called whenever a keyboard key is released
    /// </summary>
    event EventHandler<KeyboardKeyEventArgs>? KeyUp;

    /// <summary>
    /// Triggers whenever the window moves
    /// </summary>
    event EventHandler? Moved;
    
    /// <summary>
    /// Triggers whenever the window changes size
    /// </summary>
    event EventHandler? Resized;
}

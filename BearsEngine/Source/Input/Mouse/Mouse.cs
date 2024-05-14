using BearsEngine.Window;

namespace BearsEngine.Input;

/// <summary>
/// Provides high level access to the mouse
/// </summary>
internal class Mouse : IMouseInternal
{
    private static Mouse? s_instance;
    internal static Mouse Instance => s_instance ?? throw new InvalidOperationException($"{nameof(Mouse)}.{nameof(Instance)} was accessed before being set");

    private readonly IWindow _window;

    private MouseState _previousState = new();
    private MouseState _currentState = new();

    public Mouse(IWindow window)
    {
        s_instance = this;

        _window = window;
    }

    void IMouseInternal.Update(MouseState newState)
    {
        _previousState = _currentState;
        _currentState = newState;

        if (_previousState.ScreenX != _currentState.ScreenX || _previousState.ScreenY != _currentState.ScreenY)
        {
            MouseMoved?.Invoke(this, new MouseMovedEvent(ScreenPosition, ClientPosition, XDelta, YDelta));
        }

        foreach (var button in _currentState.ButtonStates.Keys)
        {
            if (!_previousState.IsDown(button) && _currentState.IsDown(button))
            {
                MouseButtonPressed?.Invoke(this, new MouseButtonPressedEventArgs(button));

                if (button == MouseButton.Left)
                {
                    MouseLeftPressed?.Invoke(this, EventArgs.Empty);
                }
                else if (button == MouseButton.Right) 
                {
                    MouseRightPressed?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (_previousState.IsDown(button) && !_currentState.IsDown(button))
            {

                MouseButtonReleased?.Invoke(this, new MouseButtonReleasedEventArgs(button));

                if (button == MouseButton.Left)
                {
                    MouseLeftReleased?.Invoke(this, EventArgs.Empty);
                }
                else if (button == MouseButton.Right)
                {
                    MouseRightReleased?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) is currently pressed.
    /// </summary>
    public bool LeftDown => Down(MouseButton.Left);

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) is currently up.
    /// </summary>
    public bool LeftUp => Up(MouseButton.Left);

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) has just been pressed.
    /// </summary>
    public bool LeftPressed => Pressed(MouseButton.Left);

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) has just been released.
    /// </summary>
    public bool LeftReleased => Released(MouseButton.Left);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) is currently down.
    /// </summary>
    public bool RightDown => Down(MouseButton.Right);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) has just been pressed.
    /// </summary>
    public bool RightPressed => Pressed(MouseButton.Right);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) has just been released.
    /// </summary>
    public bool RightReleased => Released(MouseButton.Right);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) is currently up.
    /// </summary>
    public bool RightUp => Up(MouseButton.Right);

    /// <summary>
    /// The mouse cursor's position relative to the top left of the main display, in pixels.
    /// </summary>
    public Point ScreenPosition => new(ScreenX, ScreenY);

    /// <summary>
    /// The mouse cursor's X position relative to the top left of the main display, in pixels.
    /// </summary>
    public int ScreenX => _currentState.ScreenX;

    /// <summary>
    /// The mouse cursor's Y position relative to the top left of the main display, in pixels.
    /// </summary>
    public int ScreenY => _currentState.ScreenY;

    /// <summary>
    /// The mouse cursor's position relative to the top left of the window, in pixels.
    /// </summary>
    public Point ClientPosition => new(ClientX, ClientY);

    /// <summary>
    /// The mouse cursor's X position relative to the top left of the window, in pixels.
    /// </summary>
    public float ClientX => _window.ScreenToClient(ScreenPosition).X;

    /// <summary>
    /// The mouse cursor's Y position relative to the top left of the window, in pixels.
    /// </summary>
    public float ClientY => _window.ScreenToClient(ScreenPosition).Y;

    /// <summary>
    /// The change of the mouse wheel scroll since the last frame. A positive value indicates the wheel was rolled forward, and a negative one, back.
    /// </summary>
    public float WheelDelta => _currentState.WheelY - _previousState.WheelY;

    /// <summary>
    /// The change in the mouse cursor's X position since the last frame, in pixels
    /// </summary>
    public int XDelta => _currentState.ScreenX - _previousState.ScreenX;

    /// <summary>
    /// The change in the mouse cursor's Y position since the last frame, in pixels
    /// </summary>
    public int YDelta => _currentState.ScreenY - _previousState.ScreenY;

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) was just double clicked
    /// </summary>
    public bool LeftDoubleClicked => throw new NotImplementedException();

    public event EventHandler? MouseLeftPressed;
    public event EventHandler? MouseLeftReleased;
    public event EventHandler? MouseLeftDoubleClicked;
    public event EventHandler? MouseRightPressed;
    public event EventHandler? MouseRightReleased;
    public event EventHandler<MouseButtonPressedEventArgs>? MouseButtonPressed;
    public event EventHandler<MouseButtonReleasedEventArgs>? MouseButtonReleased;
    public event EventHandler<MouseMovedEvent>? MouseMoved;

    /// <summary>
    /// Checks if a specified mouse button is currently up (not pressed).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the mouse button is currently up, false if it is pressed down.</returns>
    public bool Up(MouseButton button) => !Down(button);

    /// <summary>
    /// Checks if a specified mouse button is currently pressed down.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the mouse button is currently pressed down, false if it is up.</returns>
    public bool Down(MouseButton button)
    {
        return _currentState.IsDown(button);
    }

    /// <summary>
    /// Checks if a specified mouse button was pressed since the last frame (i.e. last frame it was up, and now it is down).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the button was pressed this frame, false if it was not.</returns>
    public bool Pressed(MouseButton button)
    {
        return !_previousState.IsDown(button) && _currentState.IsDown(button);
    }

    /// <summary>
    /// Checks if a specified mouse button was released since the last frame (i.e. last frame it was down, and now it is up).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the button was released this frame, false if it was not.</returns>
    public bool Released(MouseButton button)
    {
        return _previousState.IsDown(button) && !_currentState.IsDown(button);
    }

    public override string ToString()
    {
        return _currentState.ToString();
    }
}

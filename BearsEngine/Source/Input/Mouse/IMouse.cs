namespace BearsEngine.Input;

/// <summary>
/// Provides high level access to the mouse
/// </summary>
public interface IMouse
{
    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) is currently pressed.
    /// </summary>
    bool LeftDown { get; }

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) is currently up.
    /// </summary>
    bool LeftUp { get; }

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) has just been pressed.
    /// </summary>
    bool LeftPressed { get; }

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) has just been released.
    /// </summary>
    bool LeftReleased { get; }

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) was just double clicked
    /// </summary>
    bool LeftDoubleClicked { get; }

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) is currently down.
    /// </summary>
    bool RightDown { get; }

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) has just been pressed.
    /// </summary>
    bool RightPressed { get; }

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) has just been released.
    /// </summary>
    bool RightReleased { get; }

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) is currently up.
    /// </summary>
    bool RightUp { get; }

    /// <summary>
    /// The mouse cursor's position relative to the top left of the main display, in pixels.
    /// </summary>
    Point ScreenPosition { get; }

    /// <summary>
    /// The mouse cursor's X position relative to the top left of the main display, in pixels.
    /// </summary>
    int ScreenX { get; }

    /// <summary>
    /// The mouse cursor's Y position relative to the top left of the main display, in pixels.
    /// </summary>
    int ScreenY { get; }

    /// <summary>
    /// The mouse cursor's position relative to the top left of the window, in pixels.
    /// </summary>
    Point ClientPosition { get; }

    /// <summary>
    /// The mouse cursor's X position relative to the top left of the window, in pixels.
    /// </summary>
    float ClientX { get; }

    /// <summary>
    /// The mouse cursor's Y position relative to the top left of the window, in pixels.
    /// </summary>
    float ClientY { get; }

    /// <summary>
    /// The change of the mouse wheel scroll since the last frame. A positive value indicates the wheel was rolled forward, and a negative one, back.
    /// </summary>
    float WheelDelta { get; }

    /// <summary>
    /// The change in the mouse cursor's X position since the last frame, in pixels
    /// </summary>
    int XDelta { get; }

    /// <summary>
    /// The change in the mouse cursor's Y position since the last frame, in pixels
    /// </summary>
    int YDelta { get; }

    event EventHandler? MouseLeftPressed, MouseLeftReleased, MouseRightPressed, MouseRightReleased, MouseLeftDoubleClicked;

    event EventHandler<MouseButtonPressedEventArgs>? MouseButtonPressed;

    event EventHandler<MouseButtonReleasedEventArgs>? MouseButtonReleased;

    event EventHandler<MouseMovedEvent>? MouseMoved;

    /// <summary>
    /// Checks if a specified mouse button is currently pressed down.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the mouse button is currently pressed down, false if it is up.</returns>
    bool Down(MouseButton button);

    /// <summary>
    /// Checks if a specified mouse button is currently up (not pressed).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the mouse button is currently up, false if it is pressed down.</returns>
    bool Up(MouseButton button);

    /// <summary>
    /// Checks if a specified mouse button was pressed since the last frame (i.e. last frame it was up, and now it is down).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the button was pressed this frame, false if it was not.</returns>
    bool Pressed(MouseButton button);

    /// <summary>
    /// Checks if a specified mouse button was released since the last frame (i.e. last frame it was down, and now it is up).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the button was released this frame, false if it was not.</returns>
    bool Released(MouseButton button);
}

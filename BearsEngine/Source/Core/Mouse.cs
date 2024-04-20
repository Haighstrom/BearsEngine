using BearsEngine.Input;

namespace BearsEngine;

/// <summary>
/// Provides access to Mouse information
/// </summary>
public static class Mouse
{
    private static MouseState _previousState = new();

    internal static void Update(MouseState currentState)
    {
        _previousState = CurrentState;
        CurrentState = currentState;
    }

    /// <summary>
    /// Get the current state of the mouse.
    /// </summary>
    public static MouseState CurrentState { get; private set; } = new();

    /// <summary>
    /// Returns whether a double click event (as defined by Windows) was called this frame
    /// </summary>
    public static bool LeftDoubleClicked
    {
        get
        {
            Log.Error("left double clicked code does not exist");
            return false;
        }
    }

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) is currently down.
    /// </summary>
    public static bool LeftDown => Down(MouseButton.Left);

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) has just been pressed.
    /// </summary>
    public static bool LeftPressed => Pressed(MouseButton.Left);

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) has just been released.
    /// </summary>
    public static bool LeftReleased => Released(MouseButton.Left);

    /// <summary>
    /// Returns true if the left mouse button (Mouse 1) is currently up.
    /// </summary>
    public static bool LeftUp => Up(MouseButton.Left);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) is currently down.
    /// </summary>
    public static bool RightDown => Down(MouseButton.Right);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) has just been pressed.
    /// </summary>
    public static bool RightPressed => Pressed(MouseButton.Right);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) has just been released.
    /// </summary>
    public static bool RightReleased => Released(MouseButton.Right);

    /// <summary>
    /// Returns true if the right mouse button (Mouse 2) is currently up.
    /// </summary>
    public static bool RightUp => Up(MouseButton.Right);

    /// <summary>
    /// The mouse cursor's position relative to the top left of the main display, in pixels.
    /// </summary>
    public static Point ScreenP => new(ScreenX, ScreenY);

    /// <summary>
    /// The mouse cursor's X position relative to the top left of the main display, in pixels.
    /// </summary>
    public static int ScreenX => CurrentState.ScreenX;

    /// <summary>
    /// The mouse cursor's Y position relative to the top left of the main display, in pixels.
    /// </summary>
    public static int ScreenY => CurrentState.ScreenY;

    /// <summary>
    /// The change of the mouse wheel scroll since the last frame. A positive value indicates the wheel was rolled forward, and a negative one, back.
    /// </summary>
    public static float WheelDelta => CurrentState.WheelY - _previousState.WheelY;

    /// <summary>
    /// The mouse cursor's position relative to the top left of the window, in pixels.
    /// </summary>
    public static Point ClientP => new(ClientX, ClientY);

    /// <summary>
    /// The mouse cursor's X position relative to the top left of the window, in pixels.
    /// </summary>
    public static float ClientX => AppWindow.ScreenToClient(ScreenP).X;

    /// <summary>
    /// The mouse cursor's Y position relative to the top left of the window, in pixels.
    /// </summary>
    public static float ClientY => AppWindow.ScreenToClient(ScreenP).Y;

    /// <summary>
    /// The change in the mouse cursor's X position since the last frame, in pixels
    /// </summary>
    public static int XDelta => CurrentState.ScreenX - _previousState.ScreenX;

    /// <summary>
    /// The change in the mouse cursor's Y position since the last frame, in pixels
    /// </summary>
    public static int YDelta => CurrentState.ScreenY - _previousState.ScreenY;

    /// <summary>
    /// Checks if a specified mouse button is currently pressed down.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the mouse button is currently pressed down, false if it is up.</returns>
    public static bool Down(MouseButton button) => CurrentState.IsDown(button);

    /// <summary>
    /// Checks if a specified mouse button is currently up (not pressed).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the mouse button is currently up, false if it is pressed down.</returns>
    public static bool Up(MouseButton button) => !CurrentState.IsDown(button);

    /// <summary>
    /// Checks if a specified mouse button was pressed since the last frame (i.e. last frame it was up, and now it is down).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the button was pressed this frame, false if it was not.</returns>
    public static bool Pressed(MouseButton button)
    {
        return !_previousState.IsDown(button) && CurrentState.IsDown(button);
    }

    /// <summary>
    /// Checks if a specified mouse button was released since the last frame (i.e. last frame it was down, and now it is up).
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>Returns true if the button was released this frame, false if it was not.</returns>
    public static bool Released(MouseButton button)
    {
        return _previousState.IsDown(button) && !CurrentState.IsDown(button);
    }
}

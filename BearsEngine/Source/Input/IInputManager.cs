namespace BearsEngine.Input;

/// <summary>
/// Type for getting information about input devices and input received.
/// </summary>
public interface IInputManager : IDisposable
{
    /// <summary>
    /// The state of the mouse.
    /// </summary>
    /// <remarks>If multiple mice are attached their states will be aggregated.</remarks>
    MouseState MouseState { get; }

    /// <summary>
    /// The default time for double clicking based on the OS settings.
    /// </summary>
    int DoubleClickTime { get; }

    /// <summary>
    /// The state of the keyboard.
    /// </summary>
    /// <remarks>If multiple keyboards are attached their states will be aggregated.</remarks>
    KeyboardState KeyboardState { get; }
}

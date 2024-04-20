using BearsEngine.Input;

namespace BearsEngine.Window;

/// <summary>
/// An event for when a keyboard key press is received by a window. This will represent the raw key pressed, e.g. if 'Shift + a' was pressed, two events will be raised, one to receive <see cref="Key.LeftShift"/> and one for <see cref="Key.A"/>.
/// </summary>
public class KeyboardKeyEventArgs : EventArgs
{
    internal KeyboardKeyEventArgs(Key key)
    {
        Key = key;
    }      
    
    /// <summary>
    /// The Key received.
    /// </summary>
    public Key Key { get; internal set; }

    public override string ToString()
    {
        return string.Format("KeyboardEventArgs: Key = {0}", Key);
    }
}

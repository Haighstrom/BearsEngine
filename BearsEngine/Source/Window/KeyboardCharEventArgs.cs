namespace BearsEngine.Window;

/// <summary>
/// An event for when a keyboard character is received by a window. This will represent the character after modifies are applied, for example if 'Shift + a' was pressed, this event will receive 'A'.
/// </summary>
public class KeyboardCharEventArgs : EventArgs
{
    internal KeyboardCharEventArgs(char key)
    {
        Char = key;
    }

    /// <summary>
    /// The character received.
    /// </summary>
    public char Char { get; internal set; }
}

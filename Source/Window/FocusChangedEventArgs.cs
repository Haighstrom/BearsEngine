namespace BearsEngine;

public class FocusChangedEventArgs : EventArgs
{
    public FocusChangedEventArgs(bool focussed)
    {
        Focussed = focussed;
    }

    public bool Focussed { get; }
}
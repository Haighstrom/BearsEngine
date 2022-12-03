namespace BearsEngine;

public class PositionChangedEventArgs : EventArgs
{
    public Rect NewR;

    public PositionChangedEventArgs(Rect newR)
    {
        NewR = newR;
    }
}
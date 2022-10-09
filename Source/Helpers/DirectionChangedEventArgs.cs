namespace BearsEngine;

public class DirectionChangedEventArgs : EventArgs
{
    public DirectionChangedEventArgs(Direction oldDirection, Direction newDirection)
    {
        OldDirection = oldDirection;
        NewDirection = newDirection;
    }

    public Direction NewDirection { get; }
    public Direction OldDirection { get; }
}

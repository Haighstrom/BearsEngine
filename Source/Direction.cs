namespace BearsEngine
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }

    public enum EightWayDirection
    {
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7,
    }

    public enum RotateDirection
    {
        Clockwise = 1,
        C = Clockwise,
        CounterClockwise = -1,
        CC = CounterClockwise,
    }
}
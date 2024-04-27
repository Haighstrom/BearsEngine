namespace BearsEngine.Input;

public class MouseMovedEvent : EventArgs
{
    public MouseMovedEvent(Point screenPosition, Point clientPosition, int xDelta, int yDelta)
    {
        ScreenPosition = screenPosition;
        ClientPosition = clientPosition;
        XDelta = xDelta;
        YDelta = yDelta;
    }

    public Point ScreenPosition { get; }
    public Point ClientPosition { get; }
    public int XDelta { get; }
    public int YDelta { get; }
}

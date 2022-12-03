namespace BearsEngine.Source.Core;

public class ResizeEventArgs : EventArgs
{
    public Point OldSize, NewSize;

    public ResizeEventArgs(Point oldSize, Point newSize)
    {
        OldSize = oldSize;
        NewSize = newSize;
    }
}
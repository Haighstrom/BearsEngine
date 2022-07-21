namespace BearsEngine;

public class SizeEventArgs : EventArgs
{
    public float Width;
    public float Height;

    public SizeEventArgs(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public Point Size => new(Width, Height);

    public override string ToString() => string.Format("(W:{0},H:{1})", Width, Height);

}

public class ResizeEventArgs:EventArgs
{
    public Point OldSize, NewSize;

    public ResizeEventArgs(Point oldSize, Point newSize)
    {
        OldSize = oldSize;
        NewSize = newSize;
    }
}
public class BoolEventArgs:EventArgs
{
    public bool Value;
    public BoolEventArgs(bool b)
    {
        Value = b;
    }
}

public class BorderChangeEventArgs : EventArgs
{
    public Window.BorderStyle NewBorderStyle;
    public Window.BorderStyle OldBorderStyle;

    public BorderChangeEventArgs(Window.BorderStyle newBorderStyle, Window.BorderStyle oldBorderStyle)
    {
        NewBorderStyle = newBorderStyle;
        OldBorderStyle = oldBorderStyle;
    }
}

public class WindowStateChangeEventArgs : EventArgs
{
    public Window.WindowState NewWindowState;
    public Window.WindowState OldWindowState;

    public WindowStateChangeEventArgs(Window.WindowState newWindowState, Window.WindowState oldWindowState)
    {
        NewWindowState = newWindowState;
        OldWindowState = oldWindowState;
    }
}

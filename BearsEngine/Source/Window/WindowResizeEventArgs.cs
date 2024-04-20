namespace BearsEngine;

public class WindowResizeEventArgs : EventArgs
{
    public WindowResizeEventArgs(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public float Width { get; }

    public float Height { get; }

    public Point Size => new(Width, Height);
}

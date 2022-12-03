namespace BearsEngine;

public class SizeEventArgs : EventArgs
{
    public SizeEventArgs(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public float Width { get; }

    public float Height { get; }

    public Point Size => new(Width, Height);
}
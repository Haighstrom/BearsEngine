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
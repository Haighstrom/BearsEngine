namespace BearsEngine.Graphics;

public class SpriteMapIndexChangedEventArgs : EventArgs
{
    public SpriteMapIndexChangedEventArgs(int indexX, int indexY, int value)
        : base()
    {
        IndexX = indexX;
        IndexY = indexY;
        Value = value;
    }

    public int IndexX { get; }
    public int IndexY { get; }
    public int Value { get; }
}
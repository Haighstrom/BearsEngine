namespace BearsEngine.Displays;

public record class DisplaySettings(int X, int Y, int Width, int Height, int DisplayFrequency)
{
    public Rect BoundingRect => new(X, Y, Width, Height);

    public override string ToString() => $"Bounds: {BoundingRect}, Refresh Rate: {DisplayFrequency} hz";
}
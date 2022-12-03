namespace BearsEngine.DisplayDevices;

public record DisplaySettings(int X, int Y, int Width, int Height, int DisplayFrequency)
{
    public Rect Position => new(X, Y, Width, Height);
}
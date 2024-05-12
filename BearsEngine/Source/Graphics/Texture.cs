namespace BearsEngine.Graphics;

public readonly struct Texture
{
    public Texture(int id, int width, int height)
    {
        ID = id;
        Width = width;
        Height = height;
    }

    public int ID { get; init; }

    public int Width { get; init; }

    public int Height { get; init; }
}

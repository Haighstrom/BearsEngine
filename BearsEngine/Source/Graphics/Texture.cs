using System.Diagnostics.CodeAnalysis;

namespace BearsEngine.Graphics;

public class Texture : ITexture
{
    public Texture()
    {
        
    }

    [SetsRequiredMembers]
    public Texture(int id, int width, int height)
    {
        ID = id;
        Width = width;
        Height = height;
    }

    public required int ID { get; init; }

    public required int Width { get; init; }

    public required int Height { get; init; }
}

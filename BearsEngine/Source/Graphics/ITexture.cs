namespace BearsEngine.Graphics;

public interface ITexture
{
    int ID { get; }

    /// <summary>
    /// The original image's width, in px
    /// </summary>
    int Width { get; }

    /// <summary>
    /// The original image's height, in px
    /// </summary>
    int Height { get; }
}

namespace BearsEngine.Graphics;

public interface IGraphic : IAddable, IRenderableOnLayer
{
    byte Alpha { get; set; }

    Colour Colour { get; set; }
}
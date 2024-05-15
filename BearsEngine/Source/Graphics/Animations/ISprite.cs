namespace BearsEngine.Graphics;

public interface ISprite : IGraphic
{
    int Frame { get; set; }

    int Frames { get; }
}

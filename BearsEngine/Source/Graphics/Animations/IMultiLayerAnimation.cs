namespace BearsEngine.Graphics;

public interface IMultiLayerAnimation : IAnimation
{
    void AddTexture(ISpriteTexture texture, int layer);

    void RemoveTexture(ISpriteTexture texture);
}

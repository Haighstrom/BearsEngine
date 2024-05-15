namespace BearsEngine.Graphics;

public interface IMultiLayerAnimation : IAnimation
{
    void AddTexture(ISpriteTexture texture, float layer);

    void RemoveTexture(ISpriteTexture texture);
}

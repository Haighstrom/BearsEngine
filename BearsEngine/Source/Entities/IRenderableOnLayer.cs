namespace BearsEngine;

public interface IRenderableOnLayer : IRenderable
{
    float Layer { get; }
    event EventHandler<LayerChangedEventArgs>? LayerChanged;
}
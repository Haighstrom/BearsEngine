namespace BearsEngine;

public interface IRenderableOnLayer : IRenderable
{
    float Layer { get; set; }
    event EventHandler<LayerChangedEventArgs>? LayerChanged;
}
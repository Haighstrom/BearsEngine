namespace BearsEngine
{
    public interface IRenderableOnLayer : IRenderable
    {
        int Layer { get; set; }
        event EventHandler<LayerChangedArgs>? LayerChanged;
    }
}
namespace BearsEngine.Worlds.Cameras;

public interface ICamera : IRectangular, IAddable, IUpdatable, IRenderableOnLayer, IEntityContainer
{
    Rect View { get; }

    bool MouseIntersecting { get; }

    /// <summary>
    /// returns whether the mouse is within maxDistance of the specified edge of the client window
    /// </summary>
    bool MouseIsNearEdge(Direction edgeDirection, int maxDistance);

    void Resize(Point newSize);//todo: remove and replace with W/H overrides?
    void Resize(float newW, float newH);//todo: remove and replace with W/H overrides?

    event EventHandler? ViewChanged;

    float MaxX { get; set; }

    float MaxY { get; set; }

    float MinX { get; set; }

    float MinY { get; set; }

    public float TileHeight { get; }

    public float TileWidth { get; }
}
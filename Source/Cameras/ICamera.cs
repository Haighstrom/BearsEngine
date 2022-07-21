using BearsEngine.Win32API;

namespace BearsEngine.Worlds.Cameras;

public interface ICamera : IRect, IAddable, IUpdatable, IRenderableOnLayer, IContainer
{
    IRect View { get; }

    Colour BackgroundColour { get; set; }

    bool MouseIntersecting { get; }

    /// <summary>
    /// returns whether the mouse is within maxDistance of the specified edge of the client window
    /// </summary>
    bool MouseIsNearEdge(Direction edgeDirection, int maxDistance);

    MSAA_Samples MSAASamples { get; set; }

    void Resize(Point newSize);
    void Resize(float newW, float newH);

    bool IsInBounds(Point p);
    bool IsInBounds(float x, float y);
    bool IsOnEdge(Point p);
    bool IsOnEdge(float x, float y);

    event EventHandler ViewChanged;
}

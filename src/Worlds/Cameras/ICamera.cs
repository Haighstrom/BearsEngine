using HaighFramework;

namespace BearsEngine.Worlds
{
    public interface ICamera : IRect<float>, IAddable, IUpdatable, IRenderableOnLayer, IContainer
    {
        IRect<float> View { get; }

        Colour BackgroundColour { get; set; }

        bool MouseIntersecting { get; }

        /// <summary>
        /// returns whether the mouse is within maxDistance of the specified edge of the client window
        /// </summary>
        bool MouseIsNearEdge(Direction edgeDirection, int maxDistance);

        HaighFramework.OpenGL4.MSAA_Samples MSAASamples { get; set; }

        void Resize(IPoint<float> newSize);
        void Resize(float newW, float newH);

        bool IsInBounds(Point p);
        bool IsInBounds(float x, float y);
        bool IsOnEdge(Point p);
        bool IsOnEdge(float x, float y);

        event EventHandler ViewChanged;
    }
}
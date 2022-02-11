using HaighFramework;

namespace BearsEngine.Worlds
{
    public interface IGraphic : IAddable, IRenderableOnLayer
    {
        Colour Colour { get; set; }
        byte Alpha { get; set; }
        bool ResizeWithParent { get; set; }
        void Resize(float xScale, float yScale);
        bool IsOnScreen { get; }
    }
}
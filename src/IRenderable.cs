using HaighFramework;

namespace BearsEngine
{
    public interface IRenderable
    {
        bool Visible { get; set; }
        void Render(ref Matrix4 projection, ref Matrix4 modelView);
    }
}
using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine.Graphics
{
    public interface IShader
    {
        void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType);
    }
}

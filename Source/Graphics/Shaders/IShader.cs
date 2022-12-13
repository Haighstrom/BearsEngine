using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics.Shaders;

public interface IShader
{
    void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PRIMITIVEMODE drawType);
}

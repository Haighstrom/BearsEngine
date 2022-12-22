using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics.Shaders;

public interface IShader
{
    void Render(ref Matrix3 projection, ref Matrix3 modelView, int verticesLength, PRIMITIVE_TYPE drawType);
}

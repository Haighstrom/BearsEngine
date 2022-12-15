using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics;

public struct Texture
{
    public uint ID;
    public int Width, Height;
    public Texture(uint id, int width, int height)
    {
        ID = id;
        Width = width;
        Height = height;
    }

    public void Bind()
    {
        if (OpenGL.LastBoundTexture != ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, ID);
            OpenGL.LastBoundTexture = ID;
        }
    }
}

using HaighFramework.OpenGL;

namespace BearsEngine.Graphics;

public struct Texture
{
    public int ID;
    public int Width, Height;
    public Texture(int id, int width, int height)
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

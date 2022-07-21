using BearsEngine.Win32API;

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
        if (HV.LastBoundTexture != ID)
        {
            OpenGL32.glBindTexture(TextureTarget.Texture2D, ID);
            HV.LastBoundTexture = ID;
        }
    }
}

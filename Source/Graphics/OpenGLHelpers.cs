using System.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine;

internal static class OpenGL
{
    internal static uint LastBoundFrameBuffer { get; set; }

    internal static uint LastBoundShader { get; set; }

    internal static uint LastBoundTexture { get; set; }

    internal static uint LastBoundVertexBuffer { get; set; }

    internal static Matrix4 OrthoMatrix { get; set; }

    internal static Dictionary<string, Texture> TextureDictionary { get; set; } = new();

    public static uint GenTexture()
    {
        uint[] textures = new uint[1];

        OpenGL32.glGenTextures(1, textures);

        return textures[0];
    }

    public static Rect GetViewport()
    {
        int[] ints = new int[4];
        OpenGL32.glGetIntegerv((int)GLGET.Viewport, ints);
        return new Rect(ints[0], ints[1], ints[2], ints[3]);
    }

    public static string GetString(GETSTRING_NAME name)
    {
        unsafe
        {
            return new string(OpenGL32.glGetString(name));
        }
    }
}
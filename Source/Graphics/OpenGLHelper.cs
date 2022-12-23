using System.Runtime.InteropServices;
using System.Text;
using HaighFramework.OpenGL;

namespace BearsEngine.Graphics;

/// <summary>
/// Valid Samples values for <see cref="OpenGL32.glTexImage2DMultisample"/>
/// </summary>
public enum MSAA_SAMPLES
{
    Disabled = 0,
    X2 = 2,
    X4 = 4,
    X8 = 8,
    X16 = 16
}

internal static class OpenGL
{
    internal static int LastBoundFrameBuffer { get; set; }

    internal static int LastBoundShader { get; set; }

    internal static int LastBoundTexture { get; set; }

    internal static int LastBoundVertexBuffer { get; set; }

    internal static Matrix3 OrthoMatrix { get; set; }

    internal static Dictionary<string, Texture> TextureDictionary { get; set; } = new();

    /// <summary>
    /// Check for any OpenGL errors thrown since last check was called
    /// </summary>
    public static bool CheckOpenGLError(string callerID)
    {
        var err = OpenGL32.glGetError();
        if (err != GL_ERROR.GL_NO_ERROR)
        {
            Log.Warning($"OpenGL error: {err} - thrown in {callerID}.");
            return true;
        }

        return false;
    }

    public static int GenTexture()
    {
        int[] textures = new int[1];

        OpenGL32.glGenTextures(1, textures);

        return textures[0];
    }

    public static Rect GetViewport()
    {
        int[] ints = new int[4];
        OpenGL32.glGetIntegerv(GLCAP.GL_VIEWPORT, ints);
        return new Rect(ints[0], ints[1], ints[2], ints[3]);
    }

    public static string GetString(GETSTRING_NAME name)
    {
        unsafe
        {
            return new string(OpenGL32.glGetString(name));
        }
    }

    public static void BufferData<T>(BUFFER_TARGET target, int size, T[] data, USAGE_PATTERN usage) where T : struct
    {
        GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        OpenGL32.glBufferData(target, (IntPtr)size, pointer, usage);

        pinnedArray.Free();
    }

    public static void DeleteBuffer(int buffer)
    {
        int[] buffers = { buffer };

        OpenGL32.glDeleteBuffers(1, buffers);
    }

    public static int GenBuffer()
    {
        int[] buffers = new int[1];

        OpenGL32.glGenBuffers(1, buffers);

        return buffers[0];
    }

    public static int GenFramebuffer()
    {
        int[] buffers = new int[1];

        OpenGL32.glGenFramebuffers(1, buffers);

        return buffers[0];
    }

    public static string GetProgramInfoLog(int program)
    {
        StringBuilder stringPtr = new(255);

        OpenGL32.glGetProgramInfoLog(program, 255, out _, stringPtr);

        return stringPtr.ToString();
    }

    public static string GetShaderInfoLog(int shader)
    {
        StringBuilder stringPtr = new(255);
        int count;
        OpenGL32.glGetShaderInfoLog(shader, 255, out count, stringPtr);

        return stringPtr.ToString();
    }

    public static void ShaderSource(int shader, string source)
    {
        string[] strings = new string[1] { source };

        OpenGL32.glShaderSource(shader, 1, strings, null);
    }

    public static void UniformMatrix3(int location, Matrix3 matrix)
    {
        unsafe
        {
            fixed (float* valuePointer = matrix.Values)
            {
                OpenGL32.glUniformMatrix3fv(location, 1, false, valuePointer);
            }
        }
    }

    public static void UniformMatrix4(int location, Matrix4 matrix)
    {
        unsafe
        {
            fixed (float* valuePointer = matrix.Values)
            {
                OpenGL32.glUniformMatrix4fv(location, 1, false, valuePointer);
            }
        }
    }

    public static void Viewport(Rect newViewport)
    {
        OpenGL32.glViewport((int)newViewport.X, (int)newViewport.Y, (int)newViewport.W, (int)newViewport.H);
    }
}
using BearsEngine.OpenGL;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BearsEngine.Window.Windows;

internal class WinAPIOpenGLRenderContext
{
    private static IntPtr CreateRenderContext(IntPtr deviceContext, (int major, int minor) openGLversion)
    {
        int[] attribs = {
            (int)ARBCREATECONTEXT_ATTRIBUTE.WGL_CONTEXT_MAJOR_VERSION_ARB, openGLversion.major,
            (int)ARBCREATECONTEXT_ATTRIBUTE.WGL_CONTEXT_MINOR_VERSION_ARB, openGLversion.minor,
            (int)ARBCREATECONTEXT_ATTRIBUTE.WGL_CONTEXT_PROFILE_MASK_ARB, (int)ARBCREATECONTEXT_ATTRIBUTE_VALUES.WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB,
            0 };

        var rC = OpenGL32.wglCreateContextAttribsARB(deviceContext, sharedContext: IntPtr.Zero, attribs);

        if (rC == IntPtr.Zero)
            throw new Win32Exception($"Something went wrong with wglCreateContextAttribsARB: {Marshal.GetLastWin32Error()}");

        return rC;
    }

    public WinAPIOpenGLRenderContext(IntPtr deviceContext, int glVersionMajor, int glVersionMinor)
    {
        if (glVersionMajor < 1 || glVersionMinor < 0)
            throw new Exception($"invalid GL version to create: {glVersionMajor}.{glVersionMinor}.");

        Log.Information($"Creating GL Context: Requested Version {glVersionMajor}.{glVersionMinor}");

        //create temp context to be able to call wglGetProcAddress
        IntPtr tempContext = OpenGL32.wglCreateContext(deviceContext);
        if (tempContext == IntPtr.Zero)
            throw new Exception("tempContext failed to create.");
        if (!OpenGL32.wglMakeCurrent(deviceContext, tempContext))
            throw new Exception("wglMakeCurrent Failed");
        OpenGL32.LoadWGLExtensions();

        Handle = CreateRenderContext(deviceContext, (glVersionMajor, glVersionMinor));

        OpenGL32.LoadOpenGL3Extensions();

        OpenGL32.wglDeleteContext(tempContext);
    }

    public IntPtr Handle { get; }
}

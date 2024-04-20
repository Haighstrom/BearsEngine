using System.Runtime.InteropServices;

namespace BearsEngine.OpenGL;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "OpenGL functions have dumb naming conventions but I'm keeping these APIs pure.")]
public static partial class OpenGL32
{
    #region Delegates
    private delegate bool Delegate_wglChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, [Out] int[] piFormats, out int nNumFormats);
    private delegate IntPtr Delegate_wglCreateContextAttribsARB(IntPtr hDc, IntPtr sharedContext, int[] attribList);
    private delegate string Delegate_wglGetExtensionsStringARB(IntPtr hDc);
    private delegate bool Delegate_wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, [Out] int[] piValues);
    private delegate int Delegate_wglGetSwapIntervalEXT();
    private delegate bool Delegate_wglSwapIntervalEXT(int value);
    #endregion

    #region Entry Points
    private static Delegate_wglChoosePixelFormatARB? _wglChoosePixelFormatARB;
    private static Delegate_wglCreateContextAttribsARB? _wglCreateContextAttribsARB;
    private static Delegate_wglGetExtensionsStringARB? _wglGetExtensionsStringARB;
    private static Delegate_wglGetPixelFormatAttribivARB? _wglGetPixelFormatAttribivARB;
    private static Delegate_wglGetSwapIntervalEXT? _wglGetSwapIntervalEXT;
    private static Delegate_wglSwapIntervalEXT? _wglSwapIntervalEXT;
    #endregion

    internal static void LoadWGLExtensions()
    {
        GL.GetProcAddress("wglChoosePixelFormatARB", out _wglChoosePixelFormatARB);
        GL.GetProcAddress("wglCreateContextAttribsARB", out _wglCreateContextAttribsARB);
        GL.GetProcAddress("wglGetExtensionsStringARB", out _wglGetExtensionsStringARB);
        GL.GetProcAddress("wglGetPixelFormatAttribivARB", out _wglGetPixelFormatAttribivARB);
        GL.GetProcAddress("wglGetSwapIntervalEXT", out _wglGetSwapIntervalEXT);
        GL.GetProcAddress("wglSwapIntervalEXT", out _wglSwapIntervalEXT);
    }

    /// <summary>
    /// This extension adds functions to query pixel format attributes and to choose from the list of supported pixel formats.
    /// </summary>
    public static bool wglChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, [Out] int[] piFormats, out int nNumFormats)
    {
        if (_wglChoosePixelFormatARB is null)
            throw new EntryPointNotFoundException($"{nameof(wglGetExtensionsStringARB)} was called but the entrypoint was not loaded.");

        return _wglChoosePixelFormatARB(hdc, piAttribIList, pfAttribFList, nMaxFormats, piFormats, out nNumFormats);
    }

    /// <summary>
    /// With the advent of new versions of OpenGL which deprecate features and/or break backward compatibility with older versions, there is a need and desire to indicate at context creation which interface will be used.These extensions add a new context creation routine with attributes specifying the GL version and context properties requested for the context, and additionally add an attribute specifying the GL profile requested for a context of OpenGL 3.2 or later.
    /// </summary>
    public static IntPtr wglCreateContextAttribsARB(IntPtr hDc, IntPtr sharedContext, int[] attribList)
    {
        if (_wglCreateContextAttribsARB is null)
            throw new EntryPointNotFoundException($"{nameof(wglGetExtensionsStringARB)} was called but the entrypoint was not loaded.");

        return _wglCreateContextAttribsARB(hDc, sharedContext, attribList);
    }

    /// <summary>
    /// This extension provides a way for applications to determine which WGL extensions are supported by a device. This is the foundation upon which other WGL extensions are built.
    /// </summary>
    public static string wglGetExtensionsStringARB(IntPtr hDc)
    {
        if (_wglGetExtensionsStringARB is null)
            throw new EntryPointNotFoundException($"{nameof(wglGetExtensionsStringARB)} was called but the entrypoint was not loaded.");

        return _wglGetExtensionsStringARB(hDc);
    }

    /// <summary>
    /// This extension adds functions to query pixel format attributes and to choose from the list of supported pixel formats. 
    /// </summary>
    public static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, [Out] int[] piValues)
    {
        if (_wglGetPixelFormatAttribivARB is null)
            throw new EntryPointNotFoundException($"{nameof(wglGetExtensionsStringARB)} was called but the entrypoint was not loaded.");

        return _wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, piValues);
    }

    /// <summary>
    /// This extension allows an application to specify a minimum periodicity of color buffer swaps, measured in video frame periods.
    /// </summary>
    public static int wglGetSwapIntervalEXT()
    {
        if (_wglGetSwapIntervalEXT is null)
            throw new EntryPointNotFoundException($"{nameof(wglGetExtensionsStringARB)} was called but the entrypoint was not loaded.");

        return _wglGetSwapIntervalEXT();
    }

    /// <summary>
    /// This extension allows an application to specify a minimum periodicity of color buffer swaps, measured in video frame periods.
    /// </summary>
    public static bool wglSwapIntervalEXT(int value)
    {
        if (_wglSwapIntervalEXT is null)
            throw new EntryPointNotFoundException($"{nameof(wglGetExtensionsStringARB)} was called but the entrypoint was not loaded.");

        return _wglSwapIntervalEXT(value);
    }
}

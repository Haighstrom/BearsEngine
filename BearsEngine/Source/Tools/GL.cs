using BearsEngine.OpenGL;
using BearsEngine.WinAPI;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BearsEngine;

internal static class GL
{
    public static List<string> GetAvailableExtensions()
    {
        string s = OpenGL32.wglGetExtensionsStringARB(User32.GetDC(IntPtr.Zero));

        return s == null ? new List<string>() : s.Split(' ').ToList();
    }

    public static void GetProcAddress<T>(string functionName, out T functionPointer)
    {
        IntPtr procAddress = OpenGL32.wglGetProcAddress(functionName);

        if (procAddress == IntPtr.Zero)
            throw new Win32Exception($"Failed to load entrypoint for {functionName}.");

        functionPointer = (T)(object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T));
    }
}

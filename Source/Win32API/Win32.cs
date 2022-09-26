using BearsEngine.Win32API;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BearsEngine;

public static class Win32
{
    /// <summary>
    /// User32.GetDoubleClickTime: Returns the windows-system-defined time between clicks that registers as a double click.
    /// </summary>
    public static int GetDoubleClickTime() => User32.GetDoubleClickTime();

    public static long ToLong(this IntPtr ptr)
    {
        if (Environment.Is64BitOperatingSystem)
            return ptr.ToInt64();
        else
            return ptr.ToInt32();
    }

    public static ushort ToLOWORD(this IntPtr ptr) => (ushort)(ptr.ToLong() & 0xFFFF);
    public static ushort ToHIWORD(this IntPtr ptr) => (ushort)((ptr.ToLong() & 0XFFFF0000) >> 16);

    [System.Security.SecurityCritical]
    internal static bool DoesWin32MethodExist(string moduleName, string methodName)
    {
        IntPtr hModule = Kernal32.LoadLibrary(moduleName);

        if (hModule == IntPtr.Zero)
        {
            Debug.Assert(hModule != IntPtr.Zero, "LoadLibrary failed. API must not be available");
            return false;
        }

        IntPtr functionPointer = Kernal32.GetProcAddress(hModule, methodName);

        Kernal32.FreeLibrary(hModule);
        return (functionPointer != IntPtr.Zero);
    }
}

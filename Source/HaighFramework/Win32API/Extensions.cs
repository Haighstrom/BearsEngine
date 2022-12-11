namespace HaighFramework.Win32API;

internal static class Extensions
{
    public static long ToLong(this IntPtr ptr)
    {
        if (Environment.Is64BitOperatingSystem)
            return ptr.ToInt64();
        else
            return ptr.ToInt32();
    }

    public static ushort ToLOWORD(this IntPtr ptr) => (ushort)(ptr.ToLong() & 0xFFFF);
    public static ushort ToHIWORD(this IntPtr ptr) => (ushort)((ptr.ToLong() & 0XFFFF0000) >> 16);
}
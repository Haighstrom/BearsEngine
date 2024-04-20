namespace BearsEngine;

/// <summary>
/// Extension methods relating to Pointers
/// </summary>
internal static class Pointers
{
    /// <summary>
    /// Replacates the Windows.h HIWORD macro, retrieving the high-order word of the specified value.
    /// </summary>
    public static short ToHIWORD(this IntPtr ptr)
    {
        return unchecked((short)((uint)ptr >> 16));
    }

    /// <summary>
    /// Replacates the Windows.h LOWORD macro, retrieving the low-order word of the specified value.
    /// </summary>
    public static short ToLOWORD(this IntPtr ptr)
    {
        return unchecked((short)(uint)ptr);
    }

    /// <summary>
    /// Converts the value of this instance to a 32-bit unsigned integer.
    /// </summary>
    /// <returns>A 32-bit unsigned integer equal to the value of this instance.</returns>
    public static uint ToUInt32(this IntPtr ptr)
    {
        return unchecked(IntPtr.Size == 8 ? (uint)ptr.ToInt64() : (uint)ptr.ToInt32());
    }
}

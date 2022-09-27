using System.Runtime.InteropServices;

namespace BearsEngine;

public static class Types
{
    #region GetUnmanagedSize
    /// <summary>
    /// Returns the unmanaged size in bytes of an object.
    /// </summary>
    /// <param name="o">The object whose size is to be returned.</param>
    public static int GetUnmanagedSize(object o) => Marshal.SizeOf(o);
    /// <summary>
    /// Returns the unmanaged size, in bytes, of a type.
    /// </summary>
    /// <param name="t">The type whose size is to be returned.</param>
    public static int GetUnmanagedSize(Type t) => Marshal.SizeOf(t);
    /// <summary>
    /// Returns the unmanaged size in bytes of a type.
    /// </summary>
    /// <typeparam name="T">The type whose size is to be returned.</typeparam>
    public static int GetUnmanagedSize<T>() => Marshal.SizeOf<T>();
    /// <summary>
    /// Returns the unmanaged size, in bytes, of a type, cast to a specified type.
    /// </summary>
    /// <typeparam name="T1">The return type of the size.</typeparam>
    /// <typeparam name="T2">The type whose size is to be returned.</typeparam>
    public static T1 GetUnmanagedSize<T1, T2>()
        where T1 : IConvertible
    {
        int size = GetUnmanagedSize<T2>();

        var t1 = (T1)Convert.ChangeType(size, typeof(T1));

        if ((int)Convert.ChangeType(t1, typeof(int)) < size)
            throw new ArithmeticException($"Size of {nameof(T2)} is bigger than the maximum value of {nameof(T1)}");

        return t1;
    }
    #endregion
}

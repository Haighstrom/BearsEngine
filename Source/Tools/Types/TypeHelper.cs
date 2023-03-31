using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;

namespace BearsEngine;

public static class TypeHelper
{
    public static int GetSizeOf(Type t)
    {
        return Marshal.SizeOf(t);
    }

    public static IEnumerable<Enum> GetUniqueFlags(Enum flags)
    {
        ulong flag = 1;
        foreach (var value in Enum.GetValues(flags.GetType()).Cast<Enum>())
        {
            ulong bits = Convert.ToUInt64(value);

            while (flag < bits)
            {
                flag <<= 1;
            }

            if (flag == bits && flags.HasFlag(value))
            {
                yield return value;
            }
        }
    }

    /// <summary>
    /// returns the unique Type with the specified name in any of the loaded assemblies
    /// </summary>
    public static Type? FindType(string typeName, bool errorIfNotFound = false, bool errorIfDuplicate = false)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().Select(a => FindType(a.FullName, typeName));

        if (!types.Any())
            if (errorIfNotFound)
                throw new Exception($"Type {typeName} not found in the current domain.");
            else
                return null;
        else if (types.Count() > 1 && errorIfDuplicate)
            throw new Exception("Type {typeName} was not unique in the current domain.");
        else
            return types.First();
    }

    /// <summary>
    /// returns the unique Type with the specified name in the specified assembly
    /// </summary>
    public static Type? FindType(string assembly, string typeName, bool errorIfUnfound = false, bool errorIfDuplicate = false)
    {
        var types = Assembly.Load(assembly).GetTypes().Where(t => t.Name == typeName);

        if (!types.Any())
            if (errorIfUnfound)
                throw new Exception($"Type {typeName} not found in the current domain.");
            else
                return null;
        else if (types.Count() > 1 && errorIfDuplicate)
            throw new Exception($"Type {typeName} was not unique in the current domain.");
        else
            return types.First();
    }

    /// <summary>
    /// Returns the unmanaged size in bytes of an object.
    /// </summary>
    /// <param name="o">The object whose size is to be returned.</param>
    public static int GetUnmanagedSize(object o)
    {
        return Marshal.SizeOf(o);
    }

    /// <summary>
    /// Returns the unmanaged size, in bytes, of a type.
    /// </summary>
    /// <param name="t">The type whose size is to be returned.</param>
    public static int GetUnmanagedSize(Type t)
    {
        return Marshal.SizeOf(t);
    }

    /// <summary>
    /// Returns the unmanaged size in bytes of a type.
    /// </summary>
    /// <typeparam name="T">The type whose size is to be returned.</typeparam>
    public static int GetUnmanagedSize<T>()
    {
        return Marshal.SizeOf<T>();
    }

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
}
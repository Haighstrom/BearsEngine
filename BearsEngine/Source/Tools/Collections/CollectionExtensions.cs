namespace BearsEngine;

public static class CollectionExtensions
{
    public static List<T> ToList<T>(this T[,] array)
    {
        List<T> list = new();

        for (int j = 0; j < array.GetLength(1); ++j)
            for (int i = 0; i < array.GetLength(0); ++i)
                list.Add(array[i, j]);

        return list;
    }

    public static T[] GetRow<T>(this T[,] array, int row)
    {
        var rowLength = array.GetLength(0);
        var rowVector = new T[rowLength];

        for (var i = 0; i < rowLength; i++)
            rowVector[i] = array[i, row];

        return rowVector;
    }

    public static T[] Combine<T>(this T[] array1, T[] array2)
    {
        return array1.Concat(array2).ToArray();
    }

    public static T[,] Transpose<T>(this T[,] array)
    {
        int n = array.GetLength(0);
        int m = array.GetLength(1);

        T[,] newArray = new T[m, n];

        for (int i = 0; i < n; ++i)
            for (int j = 0; j < m; ++j)
                newArray[j, i] = array[i, j];

        return newArray;
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? list)
    {
        return list is null || !list.Any();
    }

    public static void Add(this IList<string> list, string s, params object[] args)
    {
        list.Add(string.Format(s, args));
    }

    public static void Add<T>(this IList<T> list, params T[] items)
    {
        foreach (var item in items)
            list.Add(item);
    }

    public static void Add<T>(this IList<T> list, IEnumerable<T> items)
    {
        foreach (var item in items)
            list.Add(item);
    }

    public static bool IsEmpty<T>(this T[] l)
    {
        return l == null || l.Length == 0;
    }

    public static List<T> GetRange<T>(this List<T> l, int first)
    {
        return l.GetRange(first, l.Count - first);
    }
}

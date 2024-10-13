namespace BearsEngine;

public static class EnumExtensions
{
    public static T Next<T>(this T src) where T : Enum
    {
        var arr = (T[])Enum.GetValues(src.GetType());

        int j = Array.IndexOf(arr, src) + 1;

        return (arr.Length == j) ? arr[0] : arr[j];
    }
}

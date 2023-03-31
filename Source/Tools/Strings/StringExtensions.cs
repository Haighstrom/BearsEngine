namespace BearsEngine;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string s)
    {
        return string.IsNullOrEmpty(s);
    }

    public static string ToCommaSeparatedString(this IEnumerable<string> list)
    {
        return string.Join(",", list);
    }
}
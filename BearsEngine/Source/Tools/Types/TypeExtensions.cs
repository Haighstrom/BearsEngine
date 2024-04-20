namespace BearsEngine;

public static class TypeExtensions
{
    public static int ToInt32(this string s)
    {
        if (!int.TryParse(s, out int i))
            throw new ArgumentException($"Tried to convert a string to an int that didn't look like one: {s}");

        return i;
    }

    public static float ToSingle(this string s) => float.Parse(s, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

    public static T ParseTo<T>(this object obj) => (T)Convert.ChangeType(obj, typeof(T));

    public static IEnumerable<Enum> GetUniqueFlags(this Enum flags)
    {
        return TypeHelper.GetUniqueFlags(flags);
    }
}

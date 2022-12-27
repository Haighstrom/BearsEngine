namespace BearsEngine;

public static class EXTENSIONS
{
    public static object ToType(this object o, Type targetType) => Convert.ChangeType(o, targetType);

    public static T ParseTo<T>(this object obj) => (T)Convert.ChangeType(obj, typeof(T));
    

    public static bool IsMultipleOf(this int i, int n) => Maths.IsMultipleOf(i, n);
    

    public static bool IsInt(this string s) => Maths.IsInt(s);

    public static int ToInt32(this string s)
    {
        if (!int.TryParse(s, out int i))
            throw new ArgumentException($"Tried to convert a string to an int that didn't look like one: {s}");

        return i;
    }
    

    public static float ToSingle(this string s) => float.Parse(s, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

    public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);    

    public static string ToCommaSeparatedString(this IEnumerable<string> list) => string.Join(",", list);
    

    public static IEnumerable<T> Union<T>(this IEnumerable<T> source, T item) =>
        source.Union(Enumerable.Repeat(item, 1));
    
    

    public static T[] Combine<T>(this T[] array1, T[] array2) => array1.Concat(array2).ToArray();
    

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
    
    

    public static bool IsEmpty<T>(this IEnumerable<T>? list) => list == null || !list.Any();

    public static void Add(this List<string> list, string s, params object[] args) => list.Add(string.Format(s, args));

    public static void Add<T>(this List<T> list, params T[] items) => list.AddRange(items);

    public static void Add<T>(this List<T> list, IEnumerable<T> list2) => list.AddRange(list2);

    public static bool IsEmpty<T>(this T[] l) => l == null || l.Length == 0;

    public static List<T> GetRange<T>(this List<T> l, int first) => l.GetRange(first, l.Count - first);
    

    public static Rect NonZeroAlphaRegion(this System.Drawing.Bitmap b) => HF.Graphics.NonZeroAlphaRegion(b);

    public static void WriteToFile(this System.Drawing.Bitmap b, string targetPath) => HF.Graphics.WriteBitmapToFile(b, targetPath);
    

    public static Direction ToDirection(this Point p)
    {
        if (Math.Abs(p.X) > Math.Abs(p.Y))
            if (Math.Sign(p.X) == 1)
                return Direction.Right;
            else
                return Direction.Left;
        else
            if (Math.Sign(p.Y) == 1)
            return Direction.Down;
        else
            return Direction.Up;
    }
    

    public static Point Shift(this Point p, Direction d, float distance)
    {
        return new Point(
            p.X + (d == Direction.Right ? distance : d == Direction.Left ? -distance : 0),
            p.Y + (d == Direction.Down ? distance : d == Direction.Up ? -distance : 0));
    }
    

    public static float ToAngleDegrees(this Point p) => 90 + (float)(Math.Atan2(p.Y, p.X) * 180 / Math.PI);
    

    public static Rect Shift(this Rect r, Direction d, float distance) => new(
            r.X + (d == Direction.Right ? distance : d == Direction.Left ? -distance : 0),
            r.Y + (d == Direction.Down ? distance : d == Direction.Up ? -distance : 0),
            r.W,
            r.H);
    

    public static Rect Shift(this Rect r, EightWayDirection d, float distance, bool adjustDiagonalsBySqrt2)
    {
        Rect rect = new(r);
        switch (d)
        {
            case EightWayDirection.Up:
                rect.Y -= distance;
                break;
            case EightWayDirection.UpRight:
                rect.Y -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            case EightWayDirection.Right:
                rect.X += distance;
                break;
            case EightWayDirection.DownRight:
                rect.Y += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            case EightWayDirection.Down:
                rect.Y += distance;
                break;
            case EightWayDirection.DownLeft:
                rect.Y += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            case EightWayDirection.Left:
                rect.X -= distance;
                break;
            case EightWayDirection.UpLeft:
                rect.Y -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            default:
                throw new ArgumentException($"Expected a single direction; {d} was passed", nameof(d));
        }

        return rect;
    }
    
    public static List<Point> ToVertices(this Rect r) => new() { r.TopLeft, r.TopRight, r.BottomRight, r.BottomLeft };

    public static List<Point> ToClosedVertices(this Rect r) => new() { r.TopLeft, r.TopRight, r.BottomRight, r.BottomLeft, r.TopLeft };
    
    public static IEnumerable<Enum> GetUniqueFlags(this Enum flags) => HF.Types.GetUniqueFlags(flags);
    
    public static Direction Opposite(this Direction d) => (Direction)(((int)d + 2) % 4);
    public static Direction Rotate(this Direction d, int rotations) => (Direction)Maths.Mod((int)d + rotations, 4);
}
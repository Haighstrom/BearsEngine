using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaighFramework;

namespace BearsEngine
{
    public static class EXTENSIONS
    {
        #region object
        public static void Log(this object o) => HConsole.Log(o);

        public static object ToType(this object o, Type targetType) => Convert.ChangeType(o, targetType);

        public static T ParseTo<T>(this object obj) => (T)Convert.ChangeType(obj, typeof(T));
        #endregion

        #region int
        public static bool IsMultipleOf(this int i, int n) => HF.Maths.IsMultipleOf(i, n);
        #endregion

        #region string
        public static bool IsInt(this string s) => HF.Maths.IsInt(s);

        #region string.ToInt32
        public static int ToInt32(this string s)
        {
            int i;

            if (!Int32.TryParse(s, out i))
                throw new HException("Tried to convert a string to an int that didn't look like one: {0}", s);

            return i;
        }
        #endregion

        public static float ToSingle(this string s) => float.Parse(s, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);
        #endregion

        #region IEnumerable<T>
        #region IEnumerable<T>.Each
        /// <summary>
        /// applies the specified action on each element of the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie)
                action(e, i++);
        }
        #endregion

        #region IEnumerable<T>.MinBy
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
            => source.MinBy(selector, null);

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) 
                throw new ArgumentNullException("source");
            if (selector == null) 
                throw new ArgumentNullException("selector");

            if (source.IsEmpty())
                return default(TSource);

            comparer = comparer ?? Comparer<TKey>.Default;

            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }
        #endregion

        #region IEnumerable<T>.MaxBy
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
            => source.MaxBy(selector, null);

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;

            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) > 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }
        #endregion

        #region IEnumerable<string>.ToCommaSeparatedString
        public static string ToCommaSeparatedString(this IEnumerable<string> list) => string.Join(",", list);
        #endregion

        #region IEnumerable<T> Union<T>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> source, T item) =>
            source.Union(Enumerable.Repeat(item, 1));
        #endregion
        #endregion

        #region T[]
        public static T[] Combine<T>(this T[] array1, T[] array2) => array1.Concat(array2).ToArray();
        #endregion

        #region T[,]
        #region T[,].Transpose
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
        #endregion

        #region T[,].ToList
        public static List<T> ToList<T>(this T[,] array)
        {
            List<T> list = new List<T>();

            for (int j = 0; j < array.GetLength(1); ++j)
                for (int i = 0; i < array.GetLength(0); ++i)
                    list.Add(array[i, j]);

            return list;
        }
        #endregion

        #region T[,].GetRow
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            var rowLength = array.GetLength(0);
            var rowVector = new T[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = array[i, row];

            return rowVector;
        }
        #endregion
        #endregion

        public static bool IsEmpty<T>(this IEnumerable<T> list) => list == null || list.Count() == 0;

        #region List<T>
        public static void Add(this List<string> list, string s, params object[] args) => list.Add(string.Format(s, args));

        public static void Add<T>(this List<T> list, params T[] items) => list.AddRange(items);

        public static void Add<T>(this List<T> list, List<T> list2) => list.AddRange(list2);

        public static bool IsEmpty<T>(this T[] l) => l == null || l.Length == 0;

        public static List<T> GetRange<T>(this List<T> l, int first) => l.GetRange(first, l.Count - first);
        #endregion

        #region Bitmap
        public static IRect<float> NonZeroAlphaRegion(this System.Drawing.Bitmap b) => HF.Graphics.NonZeroAlphaRegion(b);

        public static void WriteToFile(this System.Drawing.Bitmap b, string targetPath) => HF.Graphics.WriteBitmapToFile(b, targetPath);
        #endregion

        #region Point
        #region Point.ToDirection
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
        #endregion

        #region Point.Shift
        public static Point Shift(this Point p, Direction d, float distance)
        {
            return new Point(
                p.X + (d == Direction.Right ? distance : d == Direction.Left ? -distance : 0),
                p.Y + (d == Direction.Down ? distance : d == Direction.Up ? -distance : 0));
        }
        #endregion

        public static List<IPoint<float>> ToVertices(this IRect<float> r) => new List<IPoint<float>>() { r.TopLeft, r.TopRight, r.BottomRight, r.BottomLeft };

        public static List<IPoint<float>> ToClosedVertices(this IRect<float> r) => new List<IPoint<float>>() { r.TopLeft, r.TopRight, r.BottomRight, r.BottomLeft, r.TopLeft };
        #endregion

        #region IRect
        #region IRect.Shift
        public static IRect<float> Shift(this IRect<float> r, Direction d, float distance)
        {
            return new Rect(
                r.X + (d == Direction.Right ? distance : d == Direction.Left ? -distance : 0),
                r.Y + (d == Direction.Down ? distance : d == Direction.Up ? -distance : 0),
                r.W,
                r.H
                );
        }
        #endregion
        #endregion

        #region Enum
        public static IEnumerable<Enum> GetUniqueFlags(this Enum flags) => HF.Types.GetUniqueFlags(flags);
        #endregion

        #region Direction
        public static Direction Opposite(this Direction d)
        {
            if (d == Direction.None)
                return Direction.None;
            else
                return (Direction)(((int)d + 2) % 4);
        }
        #endregion

        #region Shift
        public static Direction Shift(this Direction d, int add)
        {
            if (d == Direction.None)
                throw new Exception("Can't apply mod to Direction.None");

            return (Direction)HF.Maths.Mod((int)d + add, 4);
        }
        #endregion
    }
}
namespace BearsEngine;

public static class Maths
{
    public static readonly double Sqrt2 = Math.Sqrt(2);
    public static readonly double Sqrt2Reciprocal = 1 / Math.Sqrt(2);

    public static float LengthSquared(float x1, float y1, float x2, float y2) => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);

    public static float Clamp(float value, float min, float max)
    {
        if (min > max)
            throw new ArgumentOutOfRangeException(nameof(min), $"min value ({min}) was greater than max ({max})");

        return value < min ? min : value > max ? max : value;
    }

    public static int Clamp(int value, int min, int max)
    {
        if (min > max)
            throw new ArgumentOutOfRangeException(nameof(min), $"min value ({min}) was greater than max ({max})");

        return value < min ? min : value > max ? max : value;
    }

    public static float Max(float first, float second) => first > second ? first : second;

    public static float Max(params float[] numbers)
    {
        if (numbers.Length == 0)
            throw new ArgumentException("Min requires at least 1 number passing to it");

        var ret = numbers[0];

        for (int i = 0; i < numbers.Length; ++i)
            if (numbers[i] > ret)
                ret = numbers[i];

        return ret;
    }

    public static int Max(int first, int second) => first > second ? first : second;

    public static int Max(params int[] numbers)
    {
        if (numbers.Length == 0)
            throw new ArgumentException("Min requires at least 1 number passing to it");

        var ret = numbers[0];

        for (int i = 0; i < numbers.Length; ++i)
            if (numbers[i] > ret)
                ret = numbers[i];

        return ret;
    }

    public static float Min(float first, float second) => first < second ? first : second;

    public static float Min(params float[] numbers)
    {
        if (numbers.Length == 0)
            throw new ArgumentException("Min requires at least 1 number passing to it");

        var ret = numbers[0];

        for (int i = 0; i < numbers.Length; ++i)
            if (numbers[i] < ret)
                ret = numbers[i];

        return ret;
    }

    public static int Min(int first, int second) => first < second ? first : second;

    public static int Min(params int[] numbers)
    {
        if (numbers.Length == 0)
            throw new ArgumentException("Min requires at least 1 number passing to it");

        var ret = numbers[0];

        for (int i = 0; i < numbers.Length; ++i)
            if (numbers[i] < ret)
                ret = numbers[i];

        return ret;
    }

    public static int Round(float value) => (int)Math.Round(value);



    /// <summary>
    /// Returns angle in degrees clockwise starting UP from P1
    /// </summary>
    public static double Angle(Point p1, Point p2)
    {
        var x = Math.Atan2(p2.X - p1.X, p1.Y - p2.Y);

        x = x / Math.PI * 180;

        if (x < 0)
            x += 360;

        return x;
    }


    public static float Dist(Point p1, Point p2)
    {
        return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
    public static float DistSquared(Point p1, Point p2)
    {
        return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
    }
    public static float DistGrid(IPosition p1, IPosition p2) => Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);


    /// <summary>
    /// Gets the nearest direction in moving between two points
    /// </summary>
    public static Direction GetDirection(Point start, Point end)
    {
        return (end - start).ToDirection();
    }


    public static bool IsInt(string s)
    {
        return int.TryParse(s, out _);
    }


    public static bool IsMultipleOf(int n, int mult)
    {
        while (n < 0)
            n += mult;
        return n % mult == 0;
    }






    public static int Mod(int x, int m)
    {
        while (x < 0)
            x += m;

        return x % m;
    }

    public static float Mod(float x, float m)
    {
        while (x < 0)
            x += m;

        return x % m;
    }

    public static double Mod(double x, double m)
    {
        while (x < 0)
            x += m;

        return x % m;
    }


    public static bool RectanglesIntersect(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2, bool touchingCounts = false)
    {
        if (touchingCounts)
        {
            if (x1 > x2 + w2) return false;
            if (x1 + w1 < x2) return false;
            if (y1 > y2 + h2) return false;
            if (y1 + h1 < y2) return false;
            return true;
        }
        else
        {
            if (x1 >= x2 + w2) return false;
            if (x1 + w1 <= x2) return false;
            if (y1 >= y2 + h2) return false;
            if (y1 + h1 <= y2) return false;
            return true;
        }
    }
}
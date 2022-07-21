namespace BearsEngine;

public static class Maths
{
    public static readonly double Sqrt2 = Math.Sqrt(2);
    public static readonly double Sqrt2Reciprocal = 1 / Math.Sqrt(2);

    public static float Min(float first, float second) => first < second ? first : second;
    public static float Max(float first, float second) => first > second ? first : second;
    public static float Clamp(float value, float min, float max)
    {
        if (min > max)
            throw new ArgumentOutOfRangeException(nameof(min), $"min value ({min}) was greater than max ({max})");

        return value < min ? min : value > max ? max : value;
    }
    public static int Min(int first, int second) => first < second ? first : second;
    public static int Max(int first, int second) => first > second ? first : second;
    public static int Clamp(int value, int min, int max)
    {
        if (min > max)
            throw new ArgumentOutOfRangeException(nameof(min), $"min value ({min}) was greater than max ({max})");

        return value < min ? min : value > max ? max : value;
    }
}
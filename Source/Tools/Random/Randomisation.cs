using System.Reflection;
using System.Text;

namespace BearsEngine;

public static class Randomisation
{
    private static readonly Random _random = new();

    /// <summary>
    /// returns true with probability (chanceOutOfAHundred)%
    /// </summary>
    public static bool Chance(float chanceOutOfAHundred) => Rand(0, 100) < chanceOutOfAHundred;


    /// <summary>
    /// Returns one of a list of things, at random
    /// </summary>
    public static T Choose<T>(params T[] things) => things[Rand(things.Length)];
    /// <summary>
    /// Returns one of a list of things, at random
    /// </summary>
    public static T Choose<T>(List<T> things) => things[Rand(things.Count)];


    /// <summary>
    /// Returns a double [0,max)
    /// </summary>
    public static double RandD(double max) => _random.NextDouble() * max;

    /// <summary>
    /// returns an int [min,max)
    /// </summary>
    public static int Rand(int min, int max)
    {
        if (max < min)
            throw new Exception("cannot have max less than min");

        return min + _random.Next(max - min);
    }
    /// <summary>
    /// returns an int [0,max)
    /// </summary>
    public static int Rand(int max)
    {
        if (max <= 0)
            return 0;

        return _random.Next(max);
    }

    public static T RandEnum<T>()
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            throw new InvalidOperationException($"Generic parameter T must be an enum. Provided was {typeof(T).Name}.");

        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(_random.Next(values.Length));
    }


    public static Colour RandColour() => new((byte)Rand(255), (byte)Rand(255), (byte)Rand(255), 255);

    public static Point RandPoint() => new(RandF(2) - 1, RandF(2) - 1);

    public static Direction RandDirection()
    {
        return Choose(Direction.Up, Direction.Right, Direction.Down, Direction.Left);
    }


    public static float RandF(int max) => (float)(_random.NextDouble() * max);

    public static float RandF(float max) => (float)(_random.NextDouble() * max);

    /// <summary>
    /// returns a float in range [min,max]
    /// </summary>
    public static float RandF(int min, int max)
    {
        if (max <= min)
            return max;
        return
            min + (float)_random.NextDouble() * (max - min);
    }
    /// <summary>
    /// returns a float in range [min,max]
    /// </summary>
    public static float RandF(float min, float max)
    {
        if (max <= min)
            return max;
        return min + (float)_random.NextDouble() * (max - min);
    }


    public static float RandGaussianApprox(float min, float max)
    {
        if (max <= min) return max;

        //guassian = approx (1 + cos x)/2PI [SD = 1] 
        //therefore (x+sinx)/2PI is integral, x from 0 to 2 PI gives 0-1 with normal distribution ish results

        double zeroTo2PI = 2 * Math.PI * _random.NextDouble();
        double zeroToOne = (zeroTo2PI + Math.Sin(zeroTo2PI)) / (2 * Math.PI);
        return min + (float)(zeroToOne * (max - min));
    }


    public static string RandUpperCaseString(int chars)
    {
        string def = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        StringBuilder ret = new();
        for (int i = 0; i < chars; i++)
            ret.Append(def.AsSpan(_random.Next(def.Length), 1));
        return ret.ToString();
    }


    public static Colour RandSystemColour()
    {
        List<Colour> colours = new();
        foreach (PropertyInfo i in typeof(Colour).GetProperties(
            BindingFlags.Static | BindingFlags.Public))
        {
            var v = i.GetValue(null);
            if (v is Colour colour)
                colours.Add(colour);
        }
        return Choose(colours);
    }


    /// <summary>
    /// Shuffle an array containing Ts
    /// </summary>
    public static T[] Shuffle<T>(T[] array)
    {
        for (int i = array.Length; i > 1; i--)
        {
            // pick random element 0 <= j < i
            int j = Rand(i);
            // swap i and j
            (array[i - 1], array[j]) = (array[j], array[i - 1]);
        }
        return array;
    }

    public static List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count; i > 1; i--)
        {
            // pick random element 0 <= j < i
            int j = Rand(i);
            // swap i and j
            (list[i - 1], list[j]) = (list[j], list[i - 1]);
        }
        return list;
    }
    public static T RandomElement<T>(this IList<T> list)
    {
        return list[Rand(list.Count)];
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _random.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public static void Rotate<T>(this IList<T> list, int amountShifted = 1)
    {
        Ensure.ArgumentPositive(amountShifted, nameof(amountShifted));

        int listSize = list.Count;

        if (listSize <= 1)
            return;

        int steps = amountShifted % listSize;

        if (steps == 0)
            return;

        if (steps < 0)
            steps += listSize;

        var buffer = new T[steps];

        for (int i = 0; i < steps; i++)
            buffer[i] = list[i];

        for (int i = steps; i < listSize; i++)
            list[i - steps] = list[i];

        for (int i = 0; i < steps; i++)
            list[listSize - steps + i] = buffer[i];
    }
}
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.IO;

using Encoding = System.Text.Encoding;
using HaighFramework.OpenGL;

namespace BearsEngine;
public static class HF
{    
    public static class Geom
    {
        public static List<Vertex> QuadToTris(Vertex topLeft, Vertex topRight, Vertex bottomLeft, Vertex bottomRight)
        {
            return new List<Vertex>()
            {
                bottomLeft, topRight, topLeft,
                bottomLeft, topRight, bottomRight
            };
        }

        /// <summary>
        /// returns the unit Point that points in the angle requested clockwise from up
        /// </summary>
        /// <param name="angleInDegrees"></param>
        /// <returns></returns>
        public static Point AngleToPoint(float angleInDegrees) => new((float)Math.Sin(angleInDegrees * Math.PI / 180), (float)Math.Cos(angleInDegrees * Math.PI / 180));
    }
    
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
        
    }

    public static class Types
    {
        public static int GetSizeOf(Type t) => Marshal.SizeOf(t);

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
    }
    
    public static void Repeat(Action function, int times)
    {
        for (int i = 0; i < times; ++i)
            function();
    }
}

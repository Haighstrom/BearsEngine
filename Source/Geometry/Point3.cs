using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BearsEngine;

[StructLayout(LayoutKind.Sequential)]
public struct Point3 : IEquatable<Point3>
{
    #region Static
    public static readonly Point3 Zero = new();

    #region DotProduct
    /// <summary>
    /// Scalar or dot product
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static float DotProduct(Point3 p1, Point3 p2)
    {
        return p1.DotProduct(p2);
    }
    #endregion

    #region CrossProduct
    /// <summary>
    /// Returns Vector or Cross Product between two Point3s, a x b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Point3 CrossProduct(Point3 a, Point3 b)
    {
        return new Point3
            (
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
    }
    #endregion
    #endregion

    #region Fields
    private float _x, _y, _z;
    #endregion

    #region Constructors
    public Point3(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    #endregion

    #region Properties
    public float x { get => _x; set => _x = value; }
    public float y { get => _y; set => _y = value; }
    public float z { get => _z; set => _z = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int X { get => (int)x; set => x = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int Y { get => (int)y; set => y = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int Z { get => (int)z; set => z = value; }

    public float Length => (float)Math.Sqrt(x * x + y * y + z * z);
    public float LengthSquared => x * x + y * y + z * z;

    #region Normal
    /// <summary>
    /// Returns a copy this Point, but with magnitude 1. Does not modify this Point.
    /// </summary>
    public Point3 Normal
    {
        get
        {
            if (x == 0 && y == 0 && z == 0)
                return new Point3();

            float l = Length;

            return new Point3(x / l, y / l, z / l);
        }
    }
    #endregion
    #endregion

    #region Methods
    #region Normalize
    /// <summary>
    /// Normalize this Point3 - set it to have magnitude 1. If it's length is zero, it will be set to (0,0,0,0).
    /// </summary>
    public void Normalize()
    {
        var l = Length;
        if (l == 0)
        {
            x = 0;
            y = 0;
            z = 0;
            return;
        }
        x /= l;
        y /= l;
        z /= l;
    }
    #endregion

    #region Clamp
    /// <summary>
    /// Preserves direction of the Point3 but clamps its magnitude to below maxLength
    /// </summary>
    /// <param name="maxLength"></param>
    public Point3 Clamp(float maxLength)
    {
        float l = Length;

        if (l > maxLength)
        {
            x = x * maxLength / l;
            y = y * maxLength / l;
            z = z * maxLength / l;
        }

        return this;
    }
    #endregion

    #region DotProduct
    /// <summary>
    /// Returns dot product (scalar product) with another point
    /// </summary>
    public float DotProduct(Point3 other)
    {
        return x * other.x + y * other.y + z * other.z;
    }
    #endregion

    #region CrossProduct       
    /// <summary>
    /// Returns vector product or cross product of this x b 
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Point3 CrossProduct(Point3 b)
    {
        return CrossProduct(this, b);
    }
    #endregion

    #region ToPoint
    public Point ToPoint()
    {
        return new Point(x, y);
    }
    #endregion

    #region ToPoint4
    public Point4 ToPoint4()
    {
        return new Point4(x, y, z, 1);
    }
    #endregion

    #region ToArray
    public float[] ToArray()
    {
        return new[] { x, y, z };
    }
    #endregion

    #region Equals
    public bool Equals(Point3 other)
    {
        return x == other.x && y == other.y && z == other.z;
    }
    #endregion
    #endregion

    #region Overloads / Overrides
    public static Point3 operator +(Point3 p1, Point3 p2) { return new Point3(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z); }
    public static Point3 operator -(Point3 p1, Point3 p2) { return new Point3(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z); }
    public static Point3 operator *(float f, Point3 p) { return new Point3(p.x * f, p.y * f, p.z * f); }
    public static Point3 operator *(Point3 p, float f) { return new Point3(p.x * f, p.y * f, p.z * f); }
    public static Point3 operator /(Point3 p, float f) { return new Point3(p.x / f, p.y / f, p.z / f); }

    public static bool operator ==(Point3 p1, Point3 p2) { return (p1.x == p2.x && p1.y == p2.y && p1.z == p2.z); }
    public static bool operator !=(Point3 p1, Point3 p2) { return (p1.x != p2.x || p1.y != p2.y || p1.z != p2.z); }

    #region Equals
    public override bool Equals(object o)
    {
        if (!(o is Point3))
            return false;

        return Equals((Point3)o);
    }
    #endregion

    #region GetHashCode
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion

    #region ToString
    public override string ToString()
    {
        return "(X : " + x + " Y : " + y + " Z : " + z + ")";
    }
    #endregion
    #endregion
}

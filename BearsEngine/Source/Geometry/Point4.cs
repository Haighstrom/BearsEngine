using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BearsEngine;

[StructLayout(LayoutKind.Sequential)]
public struct Point4 : IEquatable<Point4>
{
    public static readonly Point4 Zero = new();

    /// <summary>
    /// Scalar or dot product
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static float DotProduct(Point4 p1, Point4 p2)
    {
        return p1.DotProduct(p2);
    }
    
    

    private float _x, _y, _z, _w;
    

    public Point4(float x, float y, float z, float w)
    {
        _x = x;
        _y = y;
        _z = z;
        _w = w;
    }
    

    public float x { get => _x; set => _x = value; }
    public float y { get => _y; set => _y = value; }
    public float z { get => _z; set => _z = value; }
    public float w { get => _w; set => _w = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int X { get => (int)x; set => x = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int Y { get => (int)y; set => y = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int Z { get => (int)z; set => z = value; }

    [JsonIgnore]
    [XmlIgnore]
    public int W { get => (int)w; set => w = value; }

    public float Length => (float)Math.Sqrt(x * x + y * y + z * z + w * w);
    public float LengthSquared => x * x + y * y + z * z + w * w;

    /// <summary>
    /// Returns a copy this Point, but with magnitude 1. Does not modify this Point.
    /// </summary>
    public Point4 Normal
    {
        get
        {
            if (x == 0 && y == 0 && z == 0 && w == 0)
                return new Point4();

            float l = Length;

            return new Point4(x / l, y / l, z / l, w / l);
        }
    }
    
    

    /// <summary>
    /// Normalize this point4 - set it to have magnitude 1. If it's length is zero, it will be set to (0,0,0,0).
    /// </summary>
    public void Normalize()
    {
        var l = Length;
        if (l == 0)
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
            return;
        }
        x /= l;
        y /= l;
        z /= l;
        w /= l;
    }
    

    /// <summary>
    /// Preserves direction of the point4 but clamps its magnitude to below maxLength
    /// </summary>
    /// <param name="maxLength"></param>
    public Point4 Clamp(float maxLength)
    {
        float l = Length;

        if (l > maxLength)
        {
            x = x * maxLength / l;
            y = y * maxLength / l;
            z = z * maxLength / l;
            w = w * maxLength / l;
        }

        return this;
    }
    

    /// <summary>
    /// Returns dot product (scalar product) with another point
    /// </summary>
    public float DotProduct(Point4 other)
    {
        return x * other.x + y * other.y + z * other.z + w * other.w;
    }
    

    /// <summary>
    /// Apply Matrix4 transformMatrix to this Point4 to result in a new Point4. This Point4 instance is not modified - a new one is returned.
    /// </summary>
    /// <param name="transformMatrix"></param>
    /// <returns></returns>
    public Point4 Transform(Matrix4 transformMatrix)
    {
        return Matrix4.Multiply(ref transformMatrix, this);
    }

    /// <summary>
    /// Apply Matrix4 transformMatrix to this Point4 to result in a new Point4. This Point4 instance is not modified - a new one is returned.
    /// </summary>
    /// <param name="transformMatrix"></param>
    /// <returns></returns>
    public Point4 Transform(ref Matrix4 transformMatrix)
    {
        return Matrix4.Multiply(ref transformMatrix, this);
    }
      

    public Point ToPoint()
    {
        return new Point(x, y);
    }
    

    public Point3 ToPoint3()
    {
        return new Point3(x, y, z);
    }
    

    public float[] ToArray()
    {
        return new[] { x, y, z, w };
    }
    

    public bool Equals(Point4 other)
    {
        return x == other.x && y == other.y && z == other.z && w == other.w;
    }
    
    

    public static Point4 operator +(Point4 p1, Point4 p2) { return new Point4(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z, p1.w + p2.w); }
    public static Point4 operator -(Point4 p1, Point4 p2) { return new Point4(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z, p1.w - p2.w); }
    public static Point4 operator *(float f, Point4 p) { return new Point4(p.x * f, p.y * f, p.z * f, p.w * f); }
    public static Point4 operator *(Point4 p, float f) { return new Point4(p.x * f, p.y * f, p.z * f, p.w * f); }
    public static Point4 operator /(Point4 p, float f) { return new Point4(p.x / f, p.y / f, p.z / f, p.w / f); }

    public static bool operator ==(Point4 p1, Point4 p2) { return p1.x == p2.x && p1.y == p2.y && p1.z == p2.z && p1.w == p2.w; }
    public static bool operator !=(Point4 p1, Point4 p2) { return p1.x != p2.x || p1.y != p2.y || p1.z != p2.z || p1.w != p2.w; }

    public override bool Equals(object o)
    {
        if (!(o is Point4))
            return false;

        return Equals((Point4)o);
    }
    

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    

    public override string ToString()
    {
        return "(X : " + x + " Y : " + y + " Z : " + z + " W : " + w + ")";
    }
    
    
}

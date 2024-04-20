using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BearsEngine;

/// <summary>
/// A standard Point or 2-dimensional Vector.
/// </summary>
/// <param name="X">The X-coordinate or component.</param>
/// <param name="Y">The Y-coordinate or component.</param>
[StructLayout(LayoutKind.Sequential)]
public record struct Point(float X, float Y) : IPosition
{
    private const double RadianConversion = Math.PI / 180;

    /// <summary>
    /// An empty point or vector, with X and Y equal to zero.
    /// </summary>
    public static Point Zero => new();

    /// <summary>
    /// The length of this point as a vector.
    /// </summary>
    [XmlIgnore, JsonIgnore]
    public float Length => (float)Math.Sqrt(LengthSquared);

    /// <summary>
    /// The squared length of this point as a vector.
    /// </summary>
    [XmlIgnore, JsonIgnore]
    public float LengthSquared => X * X + Y * Y;

    /// <summary>
    /// Returns a new point representing the unit normal of this point as a vector.
    /// </summary>
    [XmlIgnore, JsonIgnore]
    public Point Normal => (X == 0 && Y == 0) ? new Point() : new Point(X / Length, Y / Length);

    /// <summary>
    /// Returns a new point representing a vector of equal magnitude to this one at a right angle to this one.
    /// </summary>
    [XmlIgnore, JsonIgnore]
    public Point Perpendicular => new(-Y, X);
    
    /// <summary>
    /// Returns a new point which has the same direction of this point but clamps its magnitude between the values specified (inclusive)
    /// </summary>
    public Point Clamp(float minLength, float maxLength)
    {
        Point point = new(X, Y);

        float scale = Math.Min(Math.Max(Length, minLength), maxLength) / Length;

        point.X *= scale;
        point.Y *= scale;

        return point;
    }

    /// <summary>
    /// Returns the dot (scalar) product with another point.
    /// </summary>
    public float DotProduct(Point other)
    {
        return X * other.X + Y * other.Y;
    }

    /// <summary>
    /// Returns a new point representing this one rotated around (0,0).
    /// </summary>
    /// <param name="angle">Rotation Angle in Degrees.</param>
    /// <param name="rotationAngleInDegrees"></param>
    public Point Rotate(float angle) => Rotate(angle, Zero);

    /// <summary>
    /// Returns a new point representing this one rotated around another point.
    /// </summary>
    /// <param name="angle">Rotation Angle in Degrees.</param>
    /// <param name="centre">The rotation centre.</param>
    public Point Rotate(float angle, Point centre)
    {
        var angleRadians = angle * RadianConversion;

        float rotatedX = (float)(Math.Cos(angleRadians) * (X - centre.X) - Math.Sin(angleRadians) * (Y - centre.Y) + centre.X);
        float rotatedY = (float)(Math.Sin(angleRadians) * (X - centre.X) + Math.Cos(angleRadians) * (Y - centre.Y) + centre.Y);

        return new Point(rotatedX, rotatedY);
    }

    /// <summary>
    /// Returns a new point representing this one scaled by the specified amounts.
    /// </summary>
    /// <param name="xScale">The amount to scale the x-component by.</param>
    /// <param name="yScale">The amount to scale the x-component by.</param>
    public Point Scale(float xScale, float yScale) => new(X * xScale, Y * yScale);

    /// <summary>
    /// Returns a new point representing this one scaled by the specified amount.
    /// </summary>
    /// <param name="scale">The amount to scale the x- and y- components by.</param>
    public Point Scale(float scale) => Scale(scale, scale);

    /// <summary>
    /// Returns a new point representing this one moved by specified amounts.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Point Shift(float x, float y) => new(X + x, Y + y);

    /// <summary>
    /// Return a Rect with Width and Height based on this Point's X and Y.
    /// </summary>
    public Rect ToRect() => new(X, Y);

    public bool Equals(IPosition? other)
    {
        if (other is null)
            return false;
        else
            return X == other.X && Y == other.Y;
    }

    public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);

    public static Point operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static Point operator *(float f, Point p) => new(p.X * f, p.Y * f);

    public static Point operator *(Point p, float f) => new(p.X * f, p.Y * f);

    public static Point operator /(Point p, float f) => new(p.X / f, p.Y / f);

    public static Point operator -(Point p) => new(-p.X, -p.Y);

    public override string ToString() => $"(X:{X},Y:{Y})";
}

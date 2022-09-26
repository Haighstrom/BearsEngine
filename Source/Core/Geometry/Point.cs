using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BearsEngine;

[StructLayout(LayoutKind.Sequential)]
public struct Point : IPosition, IEquatable<Point>
{
    #region Static
    public static readonly Point Zero = new();
    #endregion

    #region Fields
    [JsonIgnore]
    [XmlIgnore]
    private float _x, _y;
    #endregion

    #region Constructors
    public Point(System.Drawing.Point point)
        : this(point.X, point.Y)
    {
    }

    public Point(float x, float y)
    {
        _x = x;
        _y = y;
    }
    #endregion

    #region Properties
    #region X
    public float X
    {
        get => _x;
        set => _x = value;
    }
    #endregion

    #region Y
    public float Y
    {
        get => _y;
        set => _y = value;
    }
    #endregion

    #region Length
    [JsonIgnore]
    [XmlIgnore]
    public float Length => (float)Math.Sqrt(LengthSquared);
    #endregion

    #region LengthSquared
    [JsonIgnore]
    [XmlIgnore]
    public float LengthSquared => X * X + Y * Y;
    #endregion

    #region Normal
    [JsonIgnore]
    [XmlIgnore]
    public Point Normal => (X == 0 && Y == 0) ? new Point() : new Point(X / Length, Y / Length);
    #endregion

    #region Perpendicular
    [JsonIgnore]
    [XmlIgnore]
    /// <summary>
    /// Returns a vector of equal magnitude at right angle to this point
    /// </summary>
    public Point Perpendicular => new(-Y, X);
    #endregion
    #endregion

    #region Methods
    #region Clamp
    /// <summary>
    /// Preserves direction of the point but clamps its magnitude between the values specified (inclusive)
    /// </summary>
    public Point Clamp(float minLength, float maxLength)
    {
        Point point = new(X, Y);

        float scale = Math.Min(Math.Max(Length, minLength), maxLength) / Length;

        point.X *= scale;
        point.Y *= scale;

        return point;
    }
    #endregion

    #region DotProduct
    /// <summary>
    /// Returns dot (scalar) product with another point
    /// </summary>
    public float DotProduct(Point other) => X * other.X + Y * other.Y;
    #endregion

    #region Rotate
    /// <summary>
    /// Rotate this Point around 0,0
    /// </summary>
    /// <param name="rotationAngleInDegrees"></param>
    public Point Rotate(float rotationAngleInDegrees) => Rotate(rotationAngleInDegrees, Zero);
    /// <summary>
    /// Rotate this Point around a rotation axis given by Point RotationCentre
    /// </summary>
    /// <param name="rotationAngleInDegrees"></param>
    /// <param name="rotationCentre"></param>
    public Point Rotate(float rotationAngleInDegrees, Point rotationCentre)
    {
        Point answer = new(X - rotationCentre.X, Y - rotationCentre.Y);
        answer = Matrix2.CreateRotation(-rotationAngleInDegrees) * answer;
        answer.X += rotationCentre.X;
        answer.Y += rotationCentre.Y;
        return answer;
    }
    #endregion

    public Point Scale(float xScale, float yScale) => new(X * xScale, Y * yScale);

    public Point Shift(float x, float y = 0) => new(X + x, Y + y);

    public bool Equals(Point other) => X == other.X && Y == other.Y;

    public bool Equals(IPosition? other) => X == other?.X && Y == other?.Y;
    #endregion

    #region Overloads / Overrides
    public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);

    public static Point operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static Point operator *(float f, Point p) => new(p.X * f, p.Y * f);
    public static Point operator *(double f, Point p) => new((float)(p.X * f), (float)(p.Y * f));

    public static Point operator *(Point p, float f) => new(p.X * f, p.Y * f);
    public static Point operator *(Point p, double f) => new((float)(p.X * f), (float)(p.Y * f));

    public static Point operator /(Point p, float f) => new(p.X / f, p.Y / f);
    public static Point operator /(Point p, double f) => new((float)(p.X / f), (float)(p.Y / f));

    public static bool operator ==(Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;

    public static bool operator !=(Point p1, Point p2) => p1.X != p2.X || p1.Y != p2.Y;

    public static Point operator -(Point p) => new(-p.X, -p.Y);

    #region Equals
    public override bool Equals(object? o)
    {
        if (o is not Point)
            return false;

        return Equals((Point)o);
    }
    #endregion

    public override int GetHashCode() => (int)(X + 17 * Y);

    public override string ToString() => $"(X:{X},Y:{Y})";

    public Rect ToRect() => new(X, Y);
    #endregion
}

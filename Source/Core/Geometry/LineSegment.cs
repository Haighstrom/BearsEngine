using System.Runtime.InteropServices;

namespace BearsEngine;

/// <summary>
/// Struct to represent a line segment between two points, and exposes geometry functions
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct LineSegment : IEquatable<LineSegment>
{
    #region Fields/Autoproperties
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    #endregion

    #region Constructors
    public LineSegment(float startX, float startY, float endX, float endY)
        : this(new Point(startX, startY), new Point(endX, endY))
    {
    }

    public LineSegment(Point startPoint, Point endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }
    #endregion

    #region Properties
    public Point Direction => (EndPoint - StartPoint).Normal;

    /// <summary>
    /// Returns a unit vector perpindicular to this line segment
    /// </summary>
    public Point Normal => Direction.Perpendicular;

    public float Length => (EndPoint - StartPoint).Length;

    public float LengthSquared => (EndPoint - StartPoint).LengthSquared;
    #endregion

    #region Methods
    #region Contains
    /// <summary>
    /// Find if a Point lies on this line segment.
    /// https://stackoverflow.com/questions/328107/how-can-you-determine-a-point-is-between-two-other-points-on-a-line-segment
    /// </summary>
    public bool Contains(Point p)
    {
        return Math.Abs((p.Y - StartPoint.Y) * (EndPoint.X - StartPoint.X) - (p.X - StartPoint.X) * (EndPoint.Y - StartPoint.Y)) <= float.Epsilon
            && Math.Min(StartPoint.X, EndPoint.X) <= p.X
            && p.X <= Math.Max(StartPoint.X, EndPoint.X)
            && Math.Min(StartPoint.Y, EndPoint.Y) <= p.Y
            && p.Y <= Math.Max(StartPoint.Y, EndPoint.Y);
    }

    public bool Contains(LineSegment other) => Contains(other.StartPoint) && Contains(other.EndPoint);
    #endregion

    #region Intersects
    public bool Intersects(LineSegment other) => LineSegmentsIntersect(this, other);

    public bool Intersects(Point lineSegmentStartPoint, Point lineSegmentEndPoint) => LineSegmentsIntersect(StartPoint, EndPoint, lineSegmentStartPoint, lineSegmentEndPoint);

    /// <summary>
    /// Find if a line segment intersects a rectangle, including if it is fully contained within the rect.
    /// https://stackoverflow.com/questions/16203760/how-to-check-if-line-segment-intersects-a-rectangle
    /// </summary>
    public bool Intersects(Rect r)
    {
        if (Intersects(r.TopLeft, r.TopRight)) return true;
        if (Intersects(r.TopRight, r.BottomRight)) return true;
        if (Intersects(r.BottomRight, r.BottomLeft)) return true;
        if (Intersects(r.BottomLeft, r.TopLeft)) return true;

        //Final check for if the line segment is entirely within the rect
        return r.Contains(StartPoint) && r.Contains(EndPoint);
    }

    public static bool Intersects(LineSegment l, Rect r) => Intersects(l.StartPoint, l.EndPoint, r);

    public static bool Intersects(Point startPoint, Point endPoint, Rect r)
    {
        if (LineSegmentsIntersect(startPoint, endPoint, r.TopLeft, r.TopRight)) return true;
        if (LineSegmentsIntersect(startPoint, endPoint, r.TopRight, r.BottomRight)) return true;
        if (LineSegmentsIntersect(startPoint, endPoint, r.BottomRight, r.BottomLeft)) return true;
        if (LineSegmentsIntersect(startPoint, endPoint, r.BottomLeft, r.TopLeft)) return true;

        //Final check for if the line segment is entirely within the rect
        return r.Contains(startPoint) && r.Contains(endPoint);
    }

    #endregion

    #region FindLineIntersection

    /// <summary>
    /// Returns the point at which two line segments intersect. Will return null if no interception found.
    /// </summary>
    public Point? FindLineIntersection(Point start2, Point end2)
    {
        return FindLineIntersection(StartPoint, EndPoint, start2, end2);
    }

    /// <summary>
    /// Returns the point at which two line segments intersect. Will return null if no interception found.
    /// </summary>
    public Point? FindLineIntersection(LineSegment other)
    {
        return FindLineIntersection(StartPoint, EndPoint, other.StartPoint, other.EndPoint);
    }

    /// <summary>
    /// Returns the point at which two line segments intersect. Will return null if no interception found.
    /// </summary>
    public static Point? FindLineIntersection(LineSegment l1, LineSegment l2)
    {
        return FindLineIntersection(l1.StartPoint, l1.EndPoint, l2.StartPoint, l2.EndPoint);
    }

    /// <summary>
    /// Returns the point at which two line segments intersect. Will return null if no interception found.
    /// </summary>
    /// <param name="start1">Point at start of line 1</param>
    /// <param name="end1">Point at end of line 1</param>
    /// <param name="start2">Point at end of line 2</param>
    /// <param name="end2">Point at end of line 2</param>
    /// <returns></returns>
    public static Point? FindLineIntersection(Point start1, Point end1, Point start2, Point end2)
    {
        float denom = ((end1.X - start1.X) * (end2.Y - start2.Y)) - ((end1.Y - start1.Y) * (end2.X - start2.X));

        //  AB & CD are parallel 
        if (denom == 0)
            return null;

        float numer = ((start1.Y - start2.Y) * (end2.X - start2.X)) - ((start1.X - start2.X) * (end2.Y - start2.Y));

        float r = numer / denom;

        float numer2 = ((start1.Y - start2.Y) * (end1.X - start1.Y)) - ((start1.X - start2.X) * (end1.Y - start1.Y));

        float s = numer2 / denom;

        if (r < 0 || r > 1 || s < 0 || s > 1)
            return null;

        // Find intersection point
        return new Point(start1.X + (r * (end1.X - start1.X)), start1.Y + (r * (end1.Y - start1.Y)));
    }
    #endregion

    #region LineSegmentsIntersect
    public static bool LineSegmentsIntersect(LineSegment a, LineSegment b) => LineSegmentsIntersect(a.StartPoint, a.EndPoint, b.StartPoint, b.EndPoint);

    public static bool LineSegmentsIntersect(Point start1, Point end1, Point start2, Point end2)
    {
        float denominator = ((end1.X - start1.X) * (end2.Y - start2.Y)) - ((end1.Y - start1.Y) * (end2.X - start2.X));
        float numerator1 = ((start1.Y - start2.Y) * (end2.X - start2.X)) - ((start1.X - start2.X) * (end2.Y - start2.Y));
        float numerator2 = ((start1.Y - start2.Y) * (end1.X - start1.X)) - ((start1.X - start2.X) * (end1.Y - start1.Y));

        // Detect coincident lines (has a problem, read below) https://gamedev.stackexchange.com/questions/26004/how-to-detect-2d-line-on-line-collision
        if (denominator == 0)
            return numerator1 == 0 && numerator2 == 0;

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return r >= 0 && r <= 1 && s >= 0 && s <= 1;
    }
    #endregion
    #endregion

    #region Overloads / Overrides
    public override bool Equals(object? o)
    {
        if (o is not LineSegment)
            return false;

        return Equals((LineSegment)o);
    }

    public bool Equals(LineSegment other)
    {
        return StartPoint == other.StartPoint && EndPoint == other.EndPoint;
    }

    public override int GetHashCode() => throw new NotImplementedException();

    //Note these mean that a linesegment with the start and end point switched would not be equal
    public static bool operator ==(LineSegment l1, LineSegment l2) => l1.StartPoint == l2.StartPoint && l1.EndPoint == l2.EndPoint;

    public static bool operator !=(LineSegment l1, LineSegment l2) => l1.StartPoint != l2.StartPoint || l1.EndPoint != l2.EndPoint;

    public override string ToString() => $"LineSegment, StartPoint : {StartPoint} EndPoint : {EndPoint}";
    #endregion
}
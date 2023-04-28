using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine;

public static class GeometryExtensions
{
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


    public static Point Shift(this Point p, Direction direction, float distance) => direction switch
    {
        Direction.Up => new(p.X, p.Y - distance),
        Direction.Right => new(p.X + distance, p.Y),
        Direction.Down => new(p.X, p.Y + distance),
        Direction.Left => new(p.X - distance, p.Y),
        _ => throw new NotImplementedException(),
    };

    public static float ToAngleDegrees(this Point p)
    {
        return 90 + (float)(Math.Atan2(p.Y, p.X) * 180 / Math.PI);
    }

    public static Rect Shift(this Rect r, Direction d, float distance)
    {
        return new(
            r.X + (d == Direction.Right ? distance : d == Direction.Left ? -distance : 0),
            r.Y + (d == Direction.Down ? distance : d == Direction.Up ? -distance : 0),
            r.W,
            r.H);
    }

    public static Rect Shift(this Rect r, EightWayDirection d, float distance, bool adjustDiagonalsBySqrt2)
    {
        Rect rect = new(r);
        switch (d)
        {
            case EightWayDirection.Up:
                rect.Y -= distance;
                break;
            case EightWayDirection.UpRight:
                rect.Y -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            case EightWayDirection.Right:
                rect.X += distance;
                break;
            case EightWayDirection.DownRight:
                rect.Y += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            case EightWayDirection.Down:
                rect.Y += distance;
                break;
            case EightWayDirection.DownLeft:
                rect.Y += adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            case EightWayDirection.Left:
                rect.X -= distance;
                break;
            case EightWayDirection.UpLeft:
                rect.Y -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                rect.X -= adjustDiagonalsBySqrt2 ? (float)(distance * Maths.Sqrt2Reciprocal) : distance;
                break;
            default:
                throw new ArgumentException($"Expected a single direction; {d} was passed", nameof(d));
        }

        return rect;
    }

    public static List<Point> ToVertices(this Rect r)
    {
        return new() { r.TopLeft, r.TopRight, r.BottomRight, r.BottomLeft };
    }

    public static List<Point> ToClosedVertices(this Rect r)
    {
        return new() { r.TopLeft, r.TopRight, r.BottomRight, r.BottomLeft, r.TopLeft };
    }

    public static Direction Opposite(this Direction d)
    {
        return (Direction)(((int)d + 2) % 4);
    }

    public static Direction Rotate(this Direction d, int rotations)
    {
        return (Direction)Maths.Mod((int)d + rotations, 4);
    }
}
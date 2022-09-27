using System.Text.Json.Serialization;

namespace BearsEngine;

public interface IRect : IEquatable<IRect>
{
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }

    [JsonIgnore]
    public Point P
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }
    [JsonIgnore]
    public float Left => X;
    [JsonIgnore]
    public float Right => X + W;
    [JsonIgnore]
    public float Top => Y;
    [JsonIgnore]
    public float Bottom => Y + H;
    [JsonIgnore]
    public float Area => W * H;
    [JsonIgnore]
    public Point Size => new(W, H);
    [JsonIgnore]
    public float SmallestSide => Math.Min(W, H);
    [JsonIgnore]
    public float BiggestSide => Math.Max(W, H);
    [JsonIgnore]
    public Point TopLeft => P;
    [JsonIgnore]
    public Point TopCentre => new(X + W * 0.5f, Y);
    [JsonIgnore]
    public Point TopRight => new(X + W, Y);
    [JsonIgnore]
    public Point CentreLeft => new(X, Y + H * 0.5f);
    [JsonIgnore]
    public Point Centre => new(X + W * 0.5f, Y + H * 0.5f);
    [JsonIgnore]
    public Point CentreRight => new(X + W, Y + H * 0.5f);
    [JsonIgnore]
    public Point BottomLeft => new(X, Y + H);
    [JsonIgnore]
    public Point BottomCentre => new(X + W * 0.5f, Y + H);
    [JsonIgnore]
    public Point BottomRight => new(X + W, Y + H);
    [JsonIgnore]
    public IRect Zeroed => new Rect(0, 0, W, H);
    public IRect Shift(Point direction, float distance) => Shift(direction.Normal * distance);
    public IRect Shift(Point direction) => Shift(direction.X, direction.Y);
    public IRect Shift(float x, float y = 0, float w = 0, float h = 0) => new Rect(X + x, Y + y, W + w, H + h);
    public IRect Scale(float scaleX, float scaleY) => new Rect(X, Y, W * scaleX, H * scaleY);
    public IRect ScaleAroundCentre(float scale) => ScaleAroundCentre(scale, scale);
    public IRect ScaleAroundCentre(float scaleX, float scaleY) => ScaleAround(scaleX, scaleY, Centre.X, Centre.Y);
    public IRect ScaleAround(float scaleX, float scaleY, float originX, float originY) => ResizeAround(W * scaleX, H * scaleY, originX, originY);
    public virtual IRect Resize(float newW, float newH) => new Rect(X, Y, newW, newH);

    public IRect ResizeAround(float newW, float newH, Point origin) => ResizeAround(newW, newH, origin.X, origin.Y);
    public IRect ResizeAround(float newW, float newH, float originX, float originY)
        => new Rect(
            originX - newW * (originX - X) / W,
            originY - newH * (originY - Y) / H,
            newW, newH);

    public void SetPosition(float newX, float newY, float newW, float newH)
    {
        X = newX; 
        Y = newY;
        W = newW;
        H = newH;
    }
    public void SetPosition(IRect newPosition)
    {
        X = newPosition.X;
        Y = newPosition.Y;
        W = newPosition.W;
        H = newPosition.H;
    }

    public IRect Grow(float margin) => Grow(margin, margin, margin, margin);
    public IRect Grow(float left, float up, float right, float down) => new Rect(X - left, Y - up, W + left + right, H + up + down);

    public bool Intersects(IRect r, bool touchingCounts = false)
    {
        if (touchingCounts)
            return
                Left <= r.Right &&
                Right >= r.Left &&
                Top <= r.Bottom &&
                Bottom >= r.Top;
        else
            return
                Left < r.Right &&
                Right > r.Left &&
                Top < r.Bottom &&
                Bottom > r.Top;
    }

    public IRect Intersection(IRect r)
    {
        Rect answer = new();

        if (!Intersects(r))
            return answer;

        answer.X = Math.Max(Left, r.Left);
        answer.Y = Math.Max(Top, r.Top);
        answer.W = Math.Min(Right, r.Right) - answer.X;
        answer.H = Math.Min(Bottom, r.Bottom) - answer.Y;

        return answer;
    }
    public bool Contains(IRect r) => X <= r.X && Y <= r.Y && Right >= r.Right && Bottom >= r.Bottom;

    public bool Contains(float x, float y, float w, float h) => X <= x && Y <= y && X + H >= x + w && Y + H >= y + h;

    public bool Contains(int x, int y, int w, int h) => X <= x && Y <= y && X + W >= x + w && Y + H >= y + h;

    public bool Contains(Point p, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false)
    {
        if (onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
            return X <= p.X && Y <= p.Y && X + W >= p.X && Y + H >= p.Y;
        else if (onLeftAndTopEdgesCount && !onRightAndBottomEdgesCount)
            return X <= p.X && Y <= p.Y && X + W > p.X && Y + H > p.Y;
        else if (!onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
            return X < p.X && Y < p.Y && X + W >= p.X && Y + H >= p.Y;
        else
            return X < p.X && Y < p.Y && X + W > p.X && Y + H > p.Y;
    }

    public bool Contains(float x, float y, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false)
    {
        if (onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
            return X <= x && Y <= y && X + W >= x && Y + H >= y;
        else if (onLeftAndTopEdgesCount && !onRightAndBottomEdgesCount)
            return X <= x && Y <= y && X + W > x && Y + H > y;
        else if (!onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
            return X < x && Y < y && X + W >= x && Y + H >= y;
        else
            return X < x && Y < y && X + W > x && Y + H > y;
    }

    public bool IsContainedBy(IRect r) => X >= r.X && Y >= r.Y && X + W <= r.X + r.W && Y + H <= r.Y + r.H;

    public bool IsContainedBy(float x, float y, float w, float h) => X >= x && Y >= y && X + H <= x + h && Y + H <= y + h;

    public List<Point> ToVertices() => new() { TopLeft, TopRight, BottomRight, BottomLeft };
}
using BearsEngine.Win32API;
using System.Text.Json.Serialization;

namespace BearsEngine;

public class Rect
{
    public static readonly Rect EmptyRect = new(0, 0, 0, 0);
    public static readonly Rect UnitRect = new(0, 0, 1, 1);

    internal Rect(RECT rect)
        : this(rect.left, rect.top, rect.Width, rect.Height)
    {
    }

    public Rect()
        : this(0, 0, 0, 0)
    {
    }

    public Rect(float x, float y, float w, float h)
    {
        X = x;
        Y = y;
        W = w;
        H = h;
    }

    public Rect(System.Drawing.Rectangle rect)
        : this(rect.X, rect.Y, rect.Width, rect.Height)
    {
    }

    public Rect(float w, float h)
        : this(0, 0, w, h)
    {
    }

    public Rect(Rect rect)
        : this(rect.X, rect.Y, rect.W, rect.H)
    {
    }

    public Rect(IRectangular rect)
        : this(rect.X, rect.Y, rect.W, rect.H)
    {
    }

    public Rect(Point position, float w, float h)
        : this(position.X, position.Y, w, h)
    {
    }

    public Rect(Point size)
        : this(0, 0, size.X, size.Y)
    {
    }

    public Rect(System.Drawing.Size size)
        : this(0, 0, size.Width, size.Height)
    {
    }

    public Rect(float x, float y, Point size)
        : this(x, y, size.X, size.Y)
    {
    }

    public Rect(Point position, Point size)
        : this(position.X, position.Y, size.X, size.Y)
    {
    }

    /// <summary>
    /// Generate a rect from the ToString() of another ie "X:" + x + ",Y:" + y + ",W:" + w + ",H:" + h. Useful for saving rects to file.
    /// </summary>
    /// <param name="rectString"></param>
    public Rect(string rectString)
    {
        X = Y = W = H = 0;
        
        string[] substrings = rectString.Split(',');

        if (substrings.Length != 4)
            throw new Exception("Error trying to decode rect string");

        for (int i = 0; i < 4; i++)
        {
            substrings[i] = substrings[i][2..]; //Remove "X:" etc
            switch (i)
            {
                case 0:
                    X = float.Parse(substrings[i]);
                    break;
                case 1:
                    Y = float.Parse(substrings[i]);
                    break;
                case 2:
                    W = float.Parse(substrings[i]);
                    break;
                case 3:
                    H = float.Parse(substrings[i]);
                    break;
            }
        }
    }

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
    public Rect Zeroed => new Rect(0, 0, W, H);
    public Rect Shift(Point direction, float distance) => Shift(direction.Normal * distance);
    public Rect Shift(Point direction) => Shift(direction.X, direction.Y);
    public Rect Shift(float x, float y = 0, float w = 0, float h = 0) => new Rect(X + x, Y + y, W + w, H + h);
    public Rect Scale(float scaleX, float scaleY) => new Rect(X, Y, W * scaleX, H * scaleY);
    public Rect ScaleAroundCentre(float scale) => ScaleAroundCentre(scale, scale);
    public Rect ScaleAroundCentre(float scaleX, float scaleY) => ScaleAround(scaleX, scaleY, Centre.X, Centre.Y);
    public Rect ScaleAround(float scaleX, float scaleY, float originX, float originY) => ResizeAround(W * scaleX, H * scaleY, originX, originY);
    public virtual Rect Resize(float newW, float newH) => new Rect(X, Y, newW, newH);

    public Rect ResizeAround(float newW, float newH, Point origin) => ResizeAround(newW, newH, origin.X, origin.Y);
    public Rect ResizeAround(float newW, float newH, float originX, float originY)
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
    public void SetPosition(Rect newPosition)
    {
        X = newPosition.X;
        Y = newPosition.Y;
        W = newPosition.W;
        H = newPosition.H;
    }

    public Rect Grow(float margin) => Grow(margin, margin, margin, margin);
    public Rect Grow(float left, float up, float right, float down) => new Rect(X - left, Y - up, W + left + right, H + up + down);

    public bool Intersects(Rect r, bool touchingCounts = false)
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

    public Rect Intersection(Rect r)
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
    public bool Contains(Rect r) => X <= r.X && Y <= r.Y && Right >= r.Right && Bottom >= r.Bottom;

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

    public bool IsContainedBy(Rect r) => X >= r.X && Y >= r.Y && X + W <= r.X + r.W && Y + H <= r.Y + r.H;

    public bool IsContainedBy(float x, float y, float w, float h) => X >= x && Y >= y && X + H <= x + h && Y + H <= y + h;

    public List<Point> ToVertices() => new() { TopLeft, TopRight, BottomRight, BottomLeft };

    public bool Equals(Rect? other) => X == other?.X && Y == other?.Y && W == other?.W && H == other?.H;

    public override bool Equals(object? o) => o switch
    {
        null => false,
        Rect r => r == this,
        _ => false,
    };

    public override int GetHashCode()
    {
        float hash = X;
        hash *= 37;
        hash += Y;
        hash *= 37;
        hash += W;
        hash *= 37;
        hash += H;
        hash *= 37;
        return (int)hash;
    }

    public override string ToString() => $"(X:{X},Y:{Y},W:{W},H:{H})";

    public static bool operator ==(Rect r1, Rect r2) => r1.X == r2.X && r1.Y == r2.Y && r1.W == r2.W && r1.H == r2.H;

    public static bool operator !=(Rect r1, Rect r2) => r1.X != r2.X || r1.Y != r2.Y || r1.W != r2.W || r1.H != r2.H;

    public static Rect operator +(Rect r, Point p) => new(r.X + p.X, r.Y + p.Y, r.W, r.H);

    public static Rect operator +(Rect left, Rect right) => new(left.X + right.X, left.Y + right.Y, left.W + right.W, left.H + right.H);

    public static Rect operator -(Rect left, Rect right) => new(left.X - right.X, left.Y - right.Y, left.W - right.W, left.H - right.H);
}
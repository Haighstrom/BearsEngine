using BearsEngine.Win32API;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BearsEngine;

public struct Rect : IRect
{
    public static readonly IRect EmptyRect = new Rect(0, 0, 0, 0);
    public static readonly IRect UnitRect = new Rect(0, 0, 1, 1);

    internal Rect(RECT rect)
        : this(rect.left, rect.top, rect.Width, rect.Height)
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

    public Rect(IRect rect)
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

    public bool Equals(IRect? other) => X == other?.X && Y == other?.Y && W == other?.W && H == other?.H;

    public static bool operator ==(Rect r1, Rect r2) => r1.X == r2.X && r1.Y == r2.Y && r1.W == r2.W && r1.H == r2.H;

    public static bool operator !=(Rect r1, Rect r2) => r1.X != r2.X || r1.Y != r2.Y || r1.W != r2.W || r1.H != r2.H;

    public static Rect operator +(Rect r, Point p) => new(r.X + p.X, r.Y + p.Y, r.W, r.H);

    public static Rect operator +(Rect left, Rect right) => new(left.X + right.X, left.Y + right.Y, left.W + right.W, left.H + right.H);

    public static Rect operator -(Rect left, Rect right) => new(left.X - right.X, left.Y - right.Y, left.W - right.W, left.H - right.H);

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
}
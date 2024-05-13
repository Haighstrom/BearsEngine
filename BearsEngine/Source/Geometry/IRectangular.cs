namespace BearsEngine;

public interface IRectangular
{
    float X { get; set; }

    float Y { get; set; }

    float W { get; set; }

    float H { get; set; }

    public Point P
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public Point Size
    {
        get => new(W, H);
        set
        {
            W = value.X;
            H = value.Y;
        }
    }

    public Rect R
    {
        get => new(X, Y, W, H);
        set
        {
            X = value.X;
            Y = value.Y;
            W = value.W;
            H = value.H;
        }
    }

    public Point Centre => new(X + W / 2, Y + H / 2);
}

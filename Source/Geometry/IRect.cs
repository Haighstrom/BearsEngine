namespace BearsEngine;

public interface IRect : IEquatable<IRect>
{
    #region IRect
    #region X/Y/W/H/P/R
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
    public Point P { get; set; }
    public IRect R { get; set; }
    #endregion

    #region Left/../Bottom/Area/Size/SmallestSide/TopLeft/../BottomRight
    public float Left => X;
    public float Right => X + W;
    public float Top => Y;
    public float Bottom => Y + H;
    public float Area { get; }
    public Point Size { get; }
    public float SmallestSide { get; }
    public float BiggestSide { get; }
    public Point TopLeft { get; }
    public Point TopCentre { get; }
    public Point TopRight { get; }
    public Point CentreLeft { get; }
    public Point Centre { get; }
    public Point CentreRight { get; }
    public Point BottomLeft { get; }
    public Point BottomCentre { get; }
    public Point BottomRight { get; }
    #endregion

    #region Zeroed/Shift/Scale/Resize/Grow
    public IRect Zeroed { get; }
    public IRect Shift(Point direction, float distance);
    public IRect Shift(Point direction);
    public IRect Shift(float x, float y = 0, float w = 0, float h = 0);
    public IRect Scale(float scaleX, float scaleY);
    public IRect ScaleAround(float scaleX, float scaleY, float originX, float originY);
    public IRect ScaleAroundCentre(float scaleX, float scaleY);
    public IRect ScaleAroundCentre(float scale);
    public IRect Resize(float newW, float newH);
    public IRect ResizeAround(float newW, float newH, Point origin);
    public IRect ResizeAround(float newW, float newH, float originX, float originY);
    public IRect Grow(float margin);
    public IRect Grow(float left, float up, float right, float down);
    #endregion

    #region Intersects/Intersection/Contains/IsContainedBy
    public bool Intersects(IRect r, bool touchingCounts = false);
    public IRect Intersection(IRect r);
    public bool Contains(IRect r);
    public bool Contains(float x, float y, float w, float h);
    public bool Contains(int x, int y, int w, int h);
    public bool Contains(Point p, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false);
    public bool Contains(float x, float y, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false);
    public bool IsContainedBy(IRect r);
    public bool IsContainedBy(float x, float y, float w, float h);
    #endregion
    #endregion

    public List<Point> ToVertices();
}

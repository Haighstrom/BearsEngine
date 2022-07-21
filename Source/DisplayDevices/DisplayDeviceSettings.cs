namespace BearsEngine.DisplayDevices;

public sealed class DisplayDeviceSettings
{
    #region Fields
    private Rect _boundingRect;
    private int _colourDepth;
    private float _refreshRate;
    #endregion

    #region Constructors
    internal DisplayDeviceSettings() { }

    internal DisplayDeviceSettings(int x, int y, int width, int height, int colourDepth, float refreshRate)
    {
        if (width <= 0) throw new ArgumentOutOfRangeException("width", "must be greater than zero");
        if (height <= 0) throw new ArgumentOutOfRangeException("height", "must be greater than zero");
        if (colourDepth <= 0) throw new ArgumentOutOfRangeException("colourDepth", "must be greater than zero");
        if (refreshRate <= 0) throw new ArgumentOutOfRangeException("refreshRate", "must be greater than zero");

        _boundingRect = new Rect(x, y, width, height);
        _colourDepth = colourDepth;
        _refreshRate = refreshRate;
    }
    #endregion

    #region Public Properties
    public Rect BoundingRect { get { return _boundingRect; } }
    public int X { get { return (int)_boundingRect.X; } }
    public int Y { get { return (int)_boundingRect.Y; } }
    public int Width { get { return (int)_boundingRect.W; } }
    public int Height { get { return (int)_boundingRect.H; } }
    public int ColourDepth { get { return _colourDepth; } }
    public float RefreshRate { get { return _refreshRate; } }
    #endregion

    #region Overrides
    public override string ToString()
    {
        return string.Format("Bounds: {0} \nColour Depth: {1} bits \nRefresh Rate: {2} hz", _boundingRect, _colourDepth, _refreshRate);
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if (this.GetType() != obj.GetType()) return false;

        DisplayDeviceSettings other = (DisplayDeviceSettings)obj;

        return other.Width == this.Width &&
            other.Height == this.Height &&
            other.ColourDepth == this.ColourDepth &&
            other.RefreshRate == this.RefreshRate;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion

    #region Overloads
    public static bool operator ==(DisplayDeviceSettings left, DisplayDeviceSettings right)
    {
        if ((object)left == null && (object)right == null) return true;
        else if (((object)left == null && (object)right != null) || ((object)left != null && (object)right == null)) return false;
        else return left.Equals(right);
    }
    public static bool operator !=(DisplayDeviceSettings left, DisplayDeviceSettings right)
    {
        return !(left == right);
    }
    #endregion
}

namespace BearsEngine.Worlds;

public abstract class LightweightEntity : UpdateableBase, IRenderableOnLayer, IRectAddable, IPosition, IDisposable
{
    private readonly IGraphic _graphic;
    private bool _disposed;

    public LightweightEntity(float layer, float x, float y, float w, float h, IGraphic graphic)
    {
        Layer = layer;
        X = x; 
        Y = y; 
        W = w; 
        H = h;
        _graphic = graphic;
    }

    public LightweightEntity(float layer, Rect r, IGraphic graphic)
        : this(layer, r.X, r.Y, r.W, r.H, graphic)
    {
    }

    public float X { get; set; }

    public float Y { get; set; }

    public float W { get; set; }

    public float H { get; set; }

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

    public float Layer { get; }

    public bool Visible { get; set; } = true;

    public event EventHandler<LayerChangedEventArgs>? LayerChanged;

    public virtual void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (_graphic.Visible)
            _graphic.Render(ref projection, ref modelView);
    }

    public bool Equals(IPosition? other)
    {
        return X == other?.X && Y == other.Y;
    }

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                if (_graphic is IDisposable d)
                    d.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposedCorrectly)' has code to free unmanaged resources
    // ~EntityBase()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }
}

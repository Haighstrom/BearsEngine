using BearsEngine.Win32API;

namespace BearsEngine;

public class Screen : IContainer, IScene
{
    private bool _disposed = false;
    private bool _entitiesNeedSorting = false;
    private readonly List<IAddable> _entities = new();

    public Screen()
    {
    }

    public bool Active { get; set; } = true;

    public Colour BackgroundColour { get; set; } = Colour.CornflowerBlue;

    public ICollection<IAddable> Entities => _entities.ToArray(); //recast to avoid collection modification

    public Point LocalMousePosition => HI.MouseWindowP;

    public bool Visible { get; set; } = true;

    private void OnIRenderableLayerChanged(object? sender, LayerChangedEventArgs args)
    {
        _entitiesNeedSorting = true;
    }

    private void SortEntities()
    {
        static int GetEntityLayer(IAddable a)
        {
            if (a is IRenderableOnLayer r)
                return r.Layer;
            else
                return -1;
        }

        _entities.Sort((a1, a2) => GetEntityLayer(a2).CompareTo(GetEntityLayer(a1))); //sort descending by layer
    }

    public void Add(IAddable e)
    {
        if (e.Parent is not null)
            BE.Logging.Warning($"Added Entity {e} to Container {this} when it was already in Container {e.Parent}.");

        e.Parent = this;

        _entities.Add(e);
        _entitiesNeedSorting = true;

        if (e is IRenderableOnLayer r)
        {
            r.LayerChanged += OnIRenderableLayerChanged;
        } 

        e.OnAdded();
    }

    public void Add(params IAddable[] entities)
    {
        foreach (var e in entities)
            Add(e);
    }

    public virtual void End() { }

    public IList<E> GetEntities<E>(bool considerChildren = true)
    {
        var list = new List<E>();

        foreach (var a in Entities)
        {
            if (a is E e)
                list.Add(e);

            if (considerChildren)
            {
                if (a is IContainer c)
                    list.AddRange(c.GetEntities<E>());
            }
        }

        return list;
    }

    public IList<E> GetEntities<E>(Point p, bool considerChildren = true)
    {
        List<E> list = new();

        foreach (IAddable a in Entities)
        {
            if (a is E e && a is ICollideable col && col.Collideable && col.Collides(p))
                list.Add(e);

            if (considerChildren)
            {
                if (a is IContainer c)
                    list.AddRange(c.GetEntities<E>(p));
            }
        }

        return list;
    }

    public IList<E> GetEntities<E>(Rect r, bool considerChildren = true)
    {
        List<E> list = new();

        foreach (IAddable a in Entities)
        {
            if (a is E e && a is ICollideable col && col.Collideable && col.Collides(r))
                list.Add(e);

            if (considerChildren)
            {
                if (a is IContainer c)
                    list.AddRange(c.GetEntities<E>(r));
            }
        }
        return list;
    }

    public IList<E> GetEntities<E>(ICollideable other, bool considerChildren = true)
    {
        List<E> list = new();

        foreach (IAddable a in Entities)
        {
            if (a is E e && a is ICollideable col && col != other && col.Collideable && col.Collides(other))
                list.Add(e);

            if (considerChildren)
            {
                if (a is IContainer c)
                    list.AddRange(c.GetEntities<E>(other));
            }
        }
        return list;
    }

    public Point GetLocalPosition(Point windowCoords) => windowCoords;

    public Rect GetLocalPosition(Rect windowCoords) => windowCoords;

    public Point GetWindowPosition(Point localCoords) => localCoords;

    public Rect GetWindowPosition(Rect localCoords) => localCoords;

    public void Remove(IAddable e)
    {
        if (e.Parent != this)
            BE.Logging.Warning($"Requested Entity {e} to be removed from Container {this} when its Parent was {e.Parent}.");

        e.Parent = null;

        _entities.Remove(e);

        if (e is IRenderableOnLayer re)
        {
            re.LayerChanged -= OnIRenderableLayerChanged;
        }

        e.OnRemoved();
    }

    public void RemoveAll()
    {
        foreach (IAddable e in Entities)
        {
            Remove(e);
        }
    }

    public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
    {
        //should this be here? i don't like logic in render, but if render is called before update after the list changes...
        if (_entitiesNeedSorting)
        {
            SortEntities();
            _entitiesNeedSorting = false;
        }

        OpenGL32.glClearColour(BackgroundColour);
        OpenGL32.glClear(CLEAR_MASK.GL_COLOR_BUFFER_BIT | CLEAR_MASK.GL_DEPTH_BUFFER_BIT);

        OpenGL32.Enable(GLCAP.Blend);
        OpenGL32.glBlendFunc(BlendScaleFactor.GL_ONE, BlendScaleFactor.GL_ONE_MINUS_SRC_ALPHA);

        foreach (IAddable a in Entities)
        {
            if (a is IRenderable r && r.Visible && a.Parent == this)
                r.Render(ref projection, ref modelView);
        }
    }

    public virtual void Start() { }

    public virtual void Update(float elapsedTime)
    {
        if (_entitiesNeedSorting)
        {
            SortEntities();
            _entitiesNeedSorting = false;
        }

        foreach (IAddable a in Entities)
        {
            if (a is IUpdatable u && u.Active && a.Parent == this)
            {
                u.Update(elapsedTime);
            }
        }

        ClickController.DetermineMouseEventOutcomes();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                foreach (var e in GetEntities<IDisposable>(true))
                {
                    e.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Screen()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
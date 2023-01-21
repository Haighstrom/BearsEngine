namespace BearsEngine.Worlds;

public abstract class EntityBase : AddableRectBase, IUpdatable, IRenderableOnLayer, IContainer, IPosition, IDisposable
{
    private static float GetEntityLayer(IAddable a)
    {
        if (a is IRenderableOnLayer r)
            return r.Layer;
        else
            return float.MaxValue;
    }

    private readonly List<IAddable> _entities = new();
    private readonly List<IAddable> _entitiesToBeRemoved = new();
    private float _layer;
    private bool _disposed;

    public EntityBase(float layer, float x, float y, float w, float h)
        : base(x, y, w, h)
    {
        Layer = layer;
    }

    public EntityBase(float layer, Rect r)
        : base(r)
    {
        Layer = layer;
    }

    public virtual bool Active { get; set; } = true;

    public ICollection<IAddable> Entities => _entities.ToArray(); //recast to avoid collection modification

    public float Layer
    {
        get => _layer;
        set
        {
            if (_layer != value)
            {
                float oldvalue = _layer;
                _layer = value;

                LayerChanged?.Invoke(this, new LayerChangedEventArgs(oldvalue, _layer));
            }
        }
    }

    public abstract Point LocalMousePosition { get; }

    public bool Visible { get; set; } = true;

    public event EventHandler<LayerChangedEventArgs>? LayerChanged;

    private void OnIRenderableLayerChanged(object? sender, LayerChangedEventArgs args)
    {
        var entity = (IAddable)sender!;

        _entities.Remove(entity);

        InsertEntityAtLayerSortedLocation(entity, args.NewLayer);
    }

    private void InsertEntityAtLayerSortedLocation(IAddable entityToAdd, float layer)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            if (layer > GetEntityLayer(_entities[i])) //sorted descending by layer, with new entities on top of others of the same layer
            {
                _entities.Insert(i, entityToAdd);
                return;
            }
        }

        _entities.Add(entityToAdd);
    }

    private void FinaliseEntityRemoval()
    {
        foreach (var e in _entitiesToBeRemoved)
        {
            e.Parent = null;

            _entities.Remove(e);

            if (e is IRenderableOnLayer re)
            {
                re.LayerChanged -= OnIRenderableLayerChanged;
            }

            e.OnRemoved();
        }

        _entitiesToBeRemoved.Clear();
    }

    public void Add(IAddable e)
    {
        if (e.Parent is not null)
            Log.Warning($"Added Entity {e} to Container {this} when it was already in Container {e.Parent}.");

        e.Parent = this;

        InsertEntityAtLayerSortedLocation(e, GetEntityLayer(e));

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

    public abstract Point GetLocalPosition(Point windowCoords);

    public abstract Rect GetLocalPosition(Rect windowCoords);

    public abstract Point GetWindowPosition(Point localCoords);

    public abstract Rect GetWindowPosition(Rect localCoords);

    public void Remove(IAddable e)
    {
        if (e.Parent != this)
            Log.Warning($"Requested Entity {e} to be removed from Container {this} when its Parent was {e.Parent}.");

        _entitiesToBeRemoved.Add(e);
    }

    public void RemoveAll()
    {
        foreach (IAddable e in Entities.ToList())
        {
            Remove(e);
        }
    }

    public virtual void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        foreach (IAddable a in Entities)
        {
            if (a is IRenderable r && r.Visible && a.Parent == this)
                r.Render(ref projection, ref modelView);
        }
    }

    public virtual void Update(float elapsed)
    {
        foreach (IAddable a in Entities)
        {
            if (a is IUpdatable u && u.Active && a.Parent == this)
            {
                u.Update(elapsed);
            }
        }

        FinaliseEntityRemoval();
    }

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                //is this good? if direct children are IDisposables they will also try and cascade down dispose calls and they will be repeated?
                foreach (var child in GetEntities<IDisposable>())
                {
                    child.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
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
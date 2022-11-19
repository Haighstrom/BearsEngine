
namespace BearsEngine.Worlds;

public abstract class EntityBase : AddableRectBase, IUpdatable, IRenderableOnLayer, IContainer
{
    private readonly List<IAddable> _entities = new();
    private int _layer;

    public EntityBase(int layer, float x, float y, float w, float h)
        : base(x, y, w, h)
    {
        Layer = layer;
    }

    public EntityBase(int layer, Rect r)
        : base(r)
    {
        Layer = layer;
    }

    public virtual bool Active { get; set; } = true;

    public ICollection<IAddable> Entities => _entities.ToArray(); //recast to avoid collection modification

    public int Layer
    {
        get => _layer;
        set
        {
            if (_layer == value)
                return;

            int oldvalue = _layer;
            _layer = value;

            LayerChanged?.Invoke(this, new LayerChangedArgs(oldvalue, _layer));
        }
    }

    public abstract Point LocalMousePosition { get; }

    public bool Visible { get; set; } = true;

    public event EventHandler<LayerChangedArgs>? LayerChanged;

    private void OnIRenderableLayerChanged(object? sender, LayerChangedArgs args)
    {
        SortEntities();
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
            HConsole.Warning($"Added Entity {e} to Container {this} when it was already in Container {e.Parent}.");

        e.Parent = this;

        _entities.Add(e);
        SortEntities();

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
            HConsole.Warning($"Requested Entity {e} to be removed from Container {this} when its Parent was {e.Parent}.");

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
        foreach (IAddable e in Entities.ToList())
        {
            Remove(e);
        }
    }

    public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
    {
        foreach (IAddable a in Entities)
        {
            if (a is IRenderable r && r.Visible && a.Parent == this)
                r.Render(ref projection, ref modelView);
        }
    }

    public virtual void Update(float elapsedTime)
    {
        foreach (IAddable a in Entities)
        {
            if (a is IUpdatable u && u.Active && a.Parent == this)
            {
                u.Update(elapsedTime);
            }
        }
    }
}

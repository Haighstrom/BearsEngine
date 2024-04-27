using BearsEngine.Input;
using BearsEngine.OpenGL;
using BearsEngine.Source.Core;

namespace BearsEngine;

public class Screen : IScreen
{
    private static float GetEntityLayer(IAddable a)
    {
        if (a is IRenderableOnLayer r)
            return r.Layer;
        else
            return float.MaxValue;
    }

    private bool _disposed = false;
    private readonly List<IAddable> _entities = new();
    private readonly IMouse _mouse;

    public Screen(IMouse mouse)
    {
        _mouse = mouse;
    }

    public Screen(IMouse mouse, IList<IAddable> entities)
        : this(mouse)
    {
        Add(entities);
    }

    public Screen(IMouse mouse, params IAddable[] entities)
        : this(mouse)
    {
        Add(entities);
    }

    public bool Active { get; set; } = true;

    public Colour BackgroundColour { get; set; } = Colour.CornflowerBlue;

    public ICollection<IAddable> Entities => _entities.ToArray(); //recast to avoid collection modification

    public Point LocalMousePosition => _mouse.ClientPosition;

    public bool Visible { get; set; } = true;

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
        {
            Add(e);
        }
    }

    public void Add(IList<IAddable> entities)
    {
        foreach (var entity in entities)
        {
            Add(entity);
        }
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
            Log.Warning($"Requested Entity {e} to be removed from Container {this} when its Parent was {e.Parent}.");

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

    public virtual void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        OpenGL32.glClearColor(BackgroundColour.R / 255f, BackgroundColour.G / 255f, BackgroundColour.B / 255f, BackgroundColour.A / 255f);
        OpenGL32.glClear(BUFFER_MASK.GL_COLOR_BUFFER_BIT | BUFFER_MASK.GL_DEPTH_BUFFER_BIT);

        OpenGL32.glEnable(GLCAP.GL_BLEND);
        OpenGL32.glBlendFunc(BLEND_SCALE_FACTOR.GL_ONE, BLEND_SCALE_FACTOR.GL_ONE_MINUS_SRC_ALPHA);

        foreach (IAddable a in Entities)
        {
            if (a is IRenderable r && r.Visible && a.Parent == this)
            {
                r.Render(ref projection, ref modelView);
            }
        }
    }

    public virtual void Start() { }

    public virtual void Update(float elapsed)
    {
        foreach (IAddable a in Entities)
        {
            if (a is IUpdatable u && u.Active && a.Parent == this)
            {
                u.Update(elapsed);
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

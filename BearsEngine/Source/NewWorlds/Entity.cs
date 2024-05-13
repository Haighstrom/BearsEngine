namespace BearsEngine.Source.NewWorlds;

public class Entity : IEntity
{
    private float _x, _y, _w, _h;
    private float _layer;
    private readonly IContainer _container = new Container();

    public Entity(float layer, Rect r)
        : this(layer, r.X, r.Y, r.W, r.H)
    {
    }

    public Entity(float layer, float x, float y, float w, float h)
    {
        _layer = layer;
        _x = x;
        _y = y;
        _w = w;
        _h = h;
    }

    public bool Active { get; set; } = true;

    public bool Visible { get; set; } = true;

    public float Layer
    {
        get => _layer;
        set
        {
            var oldLayer = _layer;

            _layer = value;

            LayerChanged?.Invoke(this, new LayerChangedEventArgs(oldLayer, _layer));
        }
    }

    public IEnumerable<IUpdatable> UpdatableEntities => _container.UpdatableEntities;

    public IEnumerable<IRenderable> RenderableEntities => _container.RenderableEntities;

    public float X { get => _x; set => _x = value; }

    public float Y { get => _y; set => _y = value; }

    public float W { get => _w; set => _w = value; }

    public float H { get => _h; set => _h = value; }

    public event EventHandler<LayerChangedEventArgs>? LayerChanged;

    public virtual void Update(float elapsed)
    {

    }

    public virtual void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        Matrix3 mv = modelView;

        mv = Matrix3.Translate(ref mv, _x, _y);

        foreach (var entity in RenderableEntities)
        {
            if (entity.Visible)
            {
                entity.Render(ref projection, ref mv);
            }
        }
    }

    public void Add(object entity) => _container.Add(entity);

    public void Remove(object entity) => _container.Remove(entity);

    public void RemoveAll() => _container.RemoveAll();
}

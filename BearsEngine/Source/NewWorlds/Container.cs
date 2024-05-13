
namespace BearsEngine.Source.NewWorlds;

internal class Container : IContainer
{
    private static float GetEntityLayer(object entity)
    {
        return entity is IRenderableOnLayer r ? r.Layer : float.MaxValue;
    }

    private readonly List<object> _entities = new();

    public Container()
    {

    }

    public IEnumerable<IUpdatable> UpdatableEntities => _entities.OfType<IUpdatable>();

    public IEnumerable<IRenderable> RenderableEntities => _entities.OfType<IRenderable>();

    private void InsertEntityAtLayerSortedLocation(object entityToAdd)
    {
        var layer = GetEntityLayer(entityToAdd);

        for (int i = 0; i < _entities.Count; i++)
        {
            //entities are sorted biggest layer first, so they draw bottom to top
            //therefore if this entity has a higher layer than the next guy (and smaller than any we passed), it should be inserted now

            if (layer > GetEntityLayer(_entities[i])) 
            {
                _entities.Insert(i, entityToAdd);

                return;
            }
        }

        //otherwise it has a higher layer than anything existing, bang it at the end so it's drawn last (on top)

        _entities.Add(entityToAdd);
    }

    public void Add(object entity)
    {
        Ensure.That(!_entities.Contains(entity));

        InsertEntityAtLayerSortedLocation(entity);

        if (entity is IRenderableOnLayer rolEntity)
        {
            rolEntity.LayerChanged += Entity_LayerChanged;
        }
    }

    public void Remove(object entity)
    {
        Ensure.That(_entities.Contains(entity));

        _entities.Remove(entity);

        if (entity is IRenderableOnLayer rolEntity)
        {
            rolEntity.LayerChanged -= Entity_LayerChanged;
        }
    }

    private void Entity_LayerChanged(object? sender, LayerChangedEventArgs e)
    {
        var entity = (IRenderable)sender!;

        _entities.Remove(entity);

        InsertEntityAtLayerSortedLocation(entity);
    }

    public void RemoveAll()
    {
        foreach (var entity in _entities)
        {
            Remove(entity);
        }
    }
}

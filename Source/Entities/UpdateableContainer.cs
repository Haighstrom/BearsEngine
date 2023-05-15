namespace BearsEngine.Worlds;

public class UpdateableContainer : IUpdateableContainer
{
    private readonly List<IAddable> _entities = new();
    private bool _disposed;

    public IReadOnlyCollection<IAddable> Entities => _entities.AsReadOnly(); //recast to avoid collection modification

    public void Add(IAddable e)
    {
        Ensure.ArgumentNotNull(e, nameof(e));

        if (e.Parent is not null)
            Log.Warning($"Added Entity {e} to Container {this} when it was already in Container {e.Parent}.");

        e.Parent = this;

        e.OnAdded();
    }

    public void Add(params IAddable[] entities)
    {
        Ensure.ArgumentNotNull(entities, nameof(entities));

        foreach (var e in entities)
            Add(e);
    }

    public void Remove(IAddable e)
    {
        if (e.Parent != this)
            throw new InvalidOperationException($"Requested Entity {e} to be removed from Container {this} when its Parent was {e.Parent}.");

        _entities.Remove(e);
        e.Parent = null;
        e.OnRemoved();
    }

    public void RemoveAll()
    {
        foreach (IAddable e in Entities)
        {
            Remove(e);
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
    }

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                foreach (var entity in Entities)
                {
                    if (entity is IDisposable disposableEntity)
                    {
                        disposableEntity.Dispose();
                    }
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
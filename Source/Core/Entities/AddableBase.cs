namespace BearsEngine;

public class AddableBase : IAddable
{
    public IContainer? Parent { get; set; }

    public virtual void Remove()
    {
        if (Parent is null)
            throw new InvalidOperationException($"Tried to remove an entity {this} from its parent when it has no parent.");

        Parent.Remove(this);
    }

    public virtual void OnAdded() => Added?.Invoke(this, EventArgs.Empty);

    public virtual void OnRemoved() => Removed?.Invoke(this, EventArgs.Empty);

    public event EventHandler? Added;

    public event EventHandler? Removed;
}
namespace BearsEngine;

public abstract class UpdateableBase : AddableBase, IUpdatable
{
    public bool Active { get; set; } = true;

    public abstract void Update(float elapsed);
}
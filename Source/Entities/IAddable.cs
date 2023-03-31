namespace BearsEngine;

public interface IAddable
{
    IContainer? Parent { get; set; }

    void OnAdded();

    void OnRemoved();

    void Remove();

    event EventHandler Added;

    event EventHandler Removed;
}
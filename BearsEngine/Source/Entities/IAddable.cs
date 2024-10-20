namespace BearsEngine;

public interface IAddable
{
    IContainer? Parent { get; set; }
    bool Exists { get; }

    void OnAdded();

    void OnRemoved();

    void Remove();

    event EventHandler Added;

    event EventHandler Removed;
}

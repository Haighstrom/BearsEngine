namespace BearsEngine;

public interface IContainer
{
    IReadOnlyCollection<IAddable> Entities { get; }

    void Add(IAddable e);

    void Add(params IAddable[] entities);

    void Remove(IAddable e);

    void RemoveAll();
}
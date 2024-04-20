namespace BearsEngine;

public interface IContainer
{
    ICollection<IAddable> Entities { get; }

    Point LocalMousePosition { get; }

    void Add(IAddable e);

    void Add(params IAddable[] entities);

    void Remove(IAddable e);

    void RemoveAll();

    IList<E> GetEntities<E>(bool considerChildren = true);

    IList<E> GetEntities<E>(Point p, bool considerChildren = true);

    IList<E> GetEntities<E>(Rect r, bool considerChildren = true);

    IList<E> GetEntities<E>(ICollideable i, bool considerChildren = true);

    Point GetLocalPosition(Point windowCoords);

    Rect GetLocalPosition(Rect windowCoords);

    Point GetWindowPosition(Point localCoords);

    Rect GetWindowPosition(Rect localCoords);
}
namespace BearsEngine;

public interface IEntityContainer : IContainer
{
    Point LocalMousePosition { get; }

    IList<E> GetEntities<E>(bool considerChildren = true);

    IList<E> GetEntities<E>(Point p, bool considerChildren = true);

    IList<E> GetEntities<E>(Rect r, bool considerChildren = true);

    IList<E> GetEntities<E>(ICollideable i, bool considerChildren = true);

    Point GetLocalPosition(Point windowCoords);

    Rect GetLocalPosition(Rect windowCoords);

    Point GetWindowPosition(Point localCoords);

    Rect GetWindowPosition(Rect localCoords);
}
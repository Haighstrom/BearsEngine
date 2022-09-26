namespace BearsEngine.Worlds
{
    public interface IContainer : IUpdatable, IRenderable
    {
        IList<IAddable> Entities { get; }

        int EntityCount { get; }

        void Add(IAddable e);

        void Add(params IAddable[] entities);

        void Remove(IAddable e);

        void RemoveAll(bool cascadeToChildren = true);

        void RemoveAll<T>(bool cascadeToChildren = true)
            where T : IAddable;

        void RemoveAllExcept<T>(bool cascadeToChildren = true)
            where T : IAddable;

        IList<E> GetEntities<E>(bool considerChildren = true);

        E Collide<E>(Point p, bool considerChildren = true)
            where E : ICollideable;

        E Collide<E>(IRect r, bool considerChildren = true)
            where E : ICollideable;

        E Collide<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable;

        IList<E> CollideAll<E>(Point p, bool considerChildren = true)
            where E : ICollideable;

        IList<E> CollideAll<E>(IRect r, bool considerChildren = true)
            where E : ICollideable;

        IList<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable;

        Point GetWindowPosition(Point localCoords);

        IRect GetWindowPosition(IRect localCoords);

        Point GetLocalPosition(Point windowCoords);

        IRect GetLocalPosition(IRect windowCoords);

        Point LocalMousePosition { get; }
    }
}
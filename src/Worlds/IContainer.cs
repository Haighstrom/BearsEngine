namespace BearsEngine.Worlds
{
    public interface IContainer : IUpdatable, IRenderable
    {
        List<IAddable> Entities { get; }

        int EntityCount { get; }

        E Add<E>(E e)
            where E : IAddable;

        void Add<E>(params E[] entities)
            where E : IAddable;

        E Remove<E>(E e)
            where E : IAddable;

        void RemoveAll(bool cascadeToChildren = true);
        void RemoveAll<T>(bool cascadeToChildren = true)
            where T : IAddable;
        void RemoveAll<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable;

        void RemoveAllExcept<T>(bool cascadeToChildren = true)
            where T : IAddable;
        void RemoveAllExcept<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable;

        List<E> GetEntities<E>(bool considerChildren = true);

        E Collide<E>(Point p, bool considerChildren = true)
            where E : ICollideable;
        E Collide<E>(IRect r, bool considerChildren = true)
            where E : ICollideable;
        E Collide<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable;

        List<E> CollideAll<E>(Point p, bool considerChildren = true)
            where E : ICollideable;
        List<E> CollideAll<E>(IRect r, bool considerChildren = true)
            where E : ICollideable;
        List<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable;

        Point GetWindowPosition(Point localCoords);
        IRect GetWindowPosition(IRect localCoords);
        Point GetLocalPosition(Point windowCoords);
        IRect GetLocalPosition(IRect windowCoords);
        Point LocalMousePosition { get; }
    }
}
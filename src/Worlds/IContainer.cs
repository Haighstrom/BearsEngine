using HaighFramework;

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

        E Collide<E>(IPoint<float> p, bool considerChildren = true)
            where E : ICollideable;
        E Collide<E>(IRect<float> r, bool considerChildren = true)
            where E : ICollideable;
        E Collide<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable;

        List<E> CollideAll<E>(IPoint<float> p, bool considerChildren = true)
            where E : ICollideable;
        List<E> CollideAll<E>(IRect<float> r, bool considerChildren = true)
            where E : ICollideable;
        List<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable;

        IPoint<float> GetWindowPosition(IPoint<float> localCoords);
        IRect<float> GetWindowPosition(IRect<float> localCoords);
        IPoint<float> GetLocalPosition(IPoint<float> windowCoords);
        IRect<float> GetLocalPosition(IRect<float> windowCoords);
        IPoint<float> LocalMousePosition { get; }
    }
}
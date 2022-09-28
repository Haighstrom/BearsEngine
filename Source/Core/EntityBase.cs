namespace BearsEngine.Worlds
{
    public abstract class EntityBase : AddableRectBase, IUpdatable, IRenderableOnLayer, IContainer
    {
        private int _layer;

        public EntityBase(int layer, Rect r)
            : base(r)
        {
            Container = new Container(this);
            Layer = layer;
        }

        protected IContainer Container { get; }

        public virtual bool Active { get; set; } = true;

        public IList<IAddable> Entities => Container.Entities;

        public int EntityCount => Container.EntityCount;

        public int Layer
        {
            get => _layer;
            set
            {
                if (_layer == value)
                    return;

                int oldvalue = _layer;
                _layer = value;

                LayerChanged?.Invoke(this, new LayerChangedArgs(oldvalue, _layer));
            }
        }

        public abstract Point LocalMousePosition { get; }

        public bool Visible { get; set; } = true;

        public event EventHandler<LayerChangedArgs>? LayerChanged;

        public void Add(IAddable e) => Container.Add(e);

        public void Add(params IAddable[] entities) => Container.Add(entities);

        public E Collide<E>(Point p, bool considerChildren = true) where E : ICollideable => Container.Collide<E>(p, considerChildren);

        public E Collide<E>(Rect r, bool considerChildren = true) where E : ICollideable => Container.Collide<E>(r, considerChildren);

        public E Collide<E>(ICollideable i, bool considerChildren = true) where E : ICollideable => Container.Collide<E>(i, considerChildren);

        public IList<E> CollideAll<E>(Point p, bool considerChildren = true) where E : ICollideable => Container.CollideAll<E>(p, considerChildren);

        public IList<E> CollideAll<E>(Rect r, bool considerChildren = true) where E : ICollideable => Container.CollideAll<E>(r, considerChildren);

        public IList<E> CollideAll<E>(ICollideable i, bool considerChildren = true) where E : ICollideable => Container.CollideAll<E>(i, considerChildren);

        public IList<E> GetEntities<E>(bool considerChildren = true) => Container.GetEntities<E>(considerChildren);

        public abstract Point GetLocalPosition(Point windowCoords);

        public abstract Rect GetLocalPosition(Rect windowCoords);

        public abstract Point GetWindowPosition(Point localCoords);

        public abstract Rect GetWindowPosition(Rect localCoords);

        public void Remove(IAddable e) => Container.Remove(e);

        public void RemoveAll(bool cascadeToChildren = true) => Container.RemoveAll(cascadeToChildren);

        public void RemoveAll<T>(bool cascadeToChildren = true) where T : IAddable => Container.RemoveAll<T>(cascadeToChildren);

        public void RemoveAllExcept<T>(bool cascadeToChildren = true) where T : IAddable => Container.RemoveAllExcept<T>(cascadeToChildren);

        public abstract void Render(ref Matrix4 projection, ref Matrix4 modelView);

        public virtual void Update(double elapsedTime)
        {
            Container.Update(elapsedTime);
        }
    }
}
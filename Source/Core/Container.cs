namespace BearsEngine.Worlds
{
    public class Container : IContainer
    {
        #region Fields
        private readonly List<IUpdatable> _entitiesToUpdate = new();
        private readonly List<int> _layers = new();
        private bool _layersNeedSorting = false;
        private readonly Dictionary<int, List<IRenderableOnLayer>> _entitiesToRender = new();
        #endregion

        #region Constructors
        public Container(IContainer parent)
        {
            Parent = parent;
        }
        #endregion

        #region Properties
        public bool Active { get; set; } = true;
        public int EntityCount => Entities.Count;
        public IList<IAddable> Entities { get; private set; } = new List<IAddable>();
        public Point LocalMousePosition => GetLocalPosition(HI.MouseWindowP);
        public IContainer Parent { get; private set; }
        public bool Visible { get; set; } = true;
        #endregion

        #region Methods
        #region Add
        public void Add(params IAddable[] entities)
        {
            foreach (var e in entities)
                Add(e);
        }

        public void Add(IAddable e)
        {
            if (e.Parent != null)
                HConsole.Warning("Added Entity {0} to Container {1} when it was already in Container {2}.", e, this, e.Parent);

            e.Parent = this;

            Entities.Add(e);

            if (e is IUpdatable ue)
                _entitiesToUpdate.Add(ue);

            if (e is IRenderableOnLayer re)
            {
                AddRender(re);
                re.LayerChanged += OnIRenderableLayerChanged;
            }

            e.OnAdded();
        }
        #endregion

        #region AddRender
        private void AddRender(IRenderableOnLayer e) => AddRender(e, e.Layer);

        private void AddRender(IRenderableOnLayer e, int layer)
        {
            if (_entitiesToRender.ContainsKey(layer))
                _entitiesToRender[layer].Add(e);
            else
            {
                _layers.Add(layer);
                _entitiesToRender.Add(layer, new List<IRenderableOnLayer>());
                _entitiesToRender[layer].Add(e);

                _layersNeedSorting = true;
            }
        }
        #endregion

        #region Collide
        public E Collide<E>(Point p, bool considerChildren = true)
            where E : ICollideable
        {
            foreach (IAddable a in Entities)
            {
                if (a is E e && e.Collideable && e.Collides(p))
                    return e;

                if (considerChildren)
                {
                    if (a is IContainer c)
                    {
                        E found = c.Collide<E>(p);
                        if (found != null)
                            return found;
                    }
                }
            }
            return default;
        }

        public E Collide<E>(IRect r, bool considerChildren = true)
            where E : ICollideable
        {
            foreach (IAddable a in Entities)
            {
                if (a is E e && e.Collideable && e.Collides(r))
                    return e;

                if (considerChildren)
                {
                    if (a is IContainer c)
                    {
                        E found = c.Collide<E>(r);
                        if (found != null)
                            return found;
                    }
                }
            }
            return default;
        }

        public E Collide<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable
        {
            foreach (IAddable a in Entities)
            {
                if (a is E e && (ICollideable)e != i && e.Collideable && e.Collides(i))
                    return e;

                if (considerChildren)
                {
                    if (a is IContainer c)
                    {
                        E found = c.Collide<E>(i);
                        if (found != null)
                            return found;
                    }
                }
            }
            return default;
        }
        #endregion

        #region CollideAll
        public IList<E> CollideAll<E>(Point p, bool considerChildren = true)
            where E : ICollideable
        {
            List<E> list = new();

            foreach (IAddable a in Entities)
            {
                if (a is E e && e.Collideable && e.Collides(p))
                    list.Add(e);

                if (considerChildren)
                {
                    if (a is IContainer c)
                        list.AddRange(c.CollideAll<E>(p));
                }
            }

            return list;
        }

        public IList<E> CollideAll<E>(IRect r, bool considerChildren = true)
            where E : ICollideable
        {
            List<E> list = new();

            foreach (IAddable a in Entities)
            {
                if (a is E e)
                {
                    if (e.Collideable && e.Collides(r))
                        list.Add(e);
                }

                if (considerChildren)
                {
                    if (a is IContainer c)
                        list.AddRange(c.CollideAll<E>(r));
                }
            }
            return list;
        }

        public IList<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable
        {
            List<E> list = new();

            foreach (IAddable a in Entities)
            {
                if (a is E e && (ICollideable)e != i && e.Collideable && e.Collides(i))
                    list.Add(e);

                if (considerChildren)
                {
                    if (a is IContainer c)
                        list.AddRange(c.CollideAll<E>(i));
                }
            }
            return list;
        }
        #endregion

        #region GetEntities
        public IList<E> GetEntities<E>(bool considerChildren = true)
        {
            var list = new List<E>();

            foreach (var a in Entities)
            {
                if (a is E e)
                    list.Add(e);

                if (considerChildren)
                {
                    if (a is IContainer c)
                        list.AddRange(c.GetEntities<E>());
                }
            }

            return list;
        }
        #endregion

        #region GetLocalPosition
        public Point GetLocalPosition(Point windowCoords) => Parent.GetLocalPosition(windowCoords);

        public IRect GetLocalPosition(IRect windowCoords) => Parent.GetLocalPosition(windowCoords);
        #endregion

        #region GetWindowPosition
        public Point GetWindowPosition(Point localCoords) => Parent.GetWindowPosition(localCoords);

        public IRect GetWindowPosition(IRect localCoords) => Parent.GetWindowPosition(localCoords);
        #endregion

        #region OnIRenderableLayerChanged
        private void OnIRenderableLayerChanged(object sender, LayerChangedArgs args)
        {
            IRenderableOnLayer e = (IRenderableOnLayer)sender;

            RemoveRender(e, args.OldLayer);

            AddRender(e, args.NewLayer);
        }
        #endregion

        #region RemoveRender
        private void RemoveRender(IRenderableOnLayer e, int layer)
        {
            _entitiesToRender[layer].Remove(e);

            if (_entitiesToRender[layer].Count == 0)
            {
                _entitiesToRender.Remove(layer);
                _layers.Remove(layer);
                //nb no need to sort after a remove
            }
        }
        #endregion

        #region Remove
        public void Remove(IAddable e)
        {
            if (e.Parent != this)
                HConsole.Warning("Requested Entity {0} to be removed from Container {1} when its Parent was {2}.", e, this, e.Parent);

            e.Parent = null;

            Entities.Remove(e);

            if (e is IUpdatable ue)
                _entitiesToUpdate.Remove(ue);

            if (e is IRenderableOnLayer re)
            {
                RemoveRender(re, re.Layer);
                re.LayerChanged -= OnIRenderableLayerChanged;
            }

            e.OnRemoved();
        }
        #endregion

        #region RemoveAll
        public void RemoveAll(bool cascadeToChildren = true)
        {
            foreach (IAddable e in Entities.ToList())
            {
                if (cascadeToChildren)
                {
                    if (e is IContainer c)
                        c.RemoveAll(true);
                }
                Remove(e);
            }
        }

        public void RemoveAll<E>(bool cascadeToChildren = true)
            where E : IAddable
        {
            foreach (E e in Entities.OfType<E>())
            {
                if (cascadeToChildren)
                {
                    if (e is IContainer c)
                        c.RemoveAll(true);
                }
                Remove(e);
            }
        }
        public void RemoveAll<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable
        {
            foreach (IAddable e in Entities.ToList())
                if (e is T1 || e is T2)
                {
                    if (cascadeToChildren)
                    {
                        if (e is IContainer c)
                            c.RemoveAll(true);
                    }
                    Remove(e);
                }
        }
        #endregion

        #region RemoveAllExcept
        public void RemoveAllExcept<T>(bool cascadeToChildren = true)
            where T : IAddable
        {
            foreach (IAddable e in Entities)
                if (e is not T)
                {
                    if (cascadeToChildren)
                    {
                        if (e is IContainer c)
                            c.RemoveAll(true);
                    }
                    Remove(e);
                }
        }

        public void RemoveAllExcept<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable
        {
            foreach (IAddable e in Entities)
                if (e is not T1 && e is not T2)
                {
                    if (cascadeToChildren)
                    {
                        if (e is IContainer c)
                            c.RemoveAll(true);
                    }
                    Remove(e);
                }
        }
        #endregion

        #region Render
        public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (_layersNeedSorting)
            {
                _layers.Sort();
                _layersNeedSorting = false;
            }

            for (int i = _layers.Count - 1; i >= 0; --i)
                foreach (IRenderable e in _entitiesToRender[_layers[i]].ToList())
                    if (e.Visible && ((IAddable)e).Parent == this)
                    {
                        if (e is IGraphic g && !g.IsOnScreen)
                            continue;

                        e.Render(ref projection, ref modelView);
                    }
        }
        #endregion

        #region Update
        public virtual void Update(double elapsed)
        {
            foreach (IUpdatable e in _entitiesToUpdate.ToList()) //fix list at this point in time in case of modifications during loop
            {
                if (e.Active && ((IAddable)e).Parent == this)
                    e.Update(elapsed);
            }
        }
        #endregion
        #endregion
    }
}
namespace BearsEngine.Worlds
{
    public class Container : IContainer
    {
        #region Fields
        private List<IUpdatable> _entitiesToUpdate = new();
        private List<int> _layers = new();
        private bool _layersNeedSorting = false;
        private Dictionary<int, List<IRenderableOnLayer>> _entitiesToRender = new();
        #endregion

        #region Constructors
        public Container(IContainer parent)
        {
            Parent = parent;
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

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

        #region IRenderable
        public bool Visible { get; set; } = true;

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
        #endregion

        #region IContainer
        public List<IAddable> Entities { get; private set; } = new List<IAddable>();

        public int EntityCount => Entities.Count;

        #region Add
        public void Add<E>(params E[] entities)
            where E : IAddable
        {
            foreach (E e in entities)
                Add(e);
        }

        public E Add<E>(E e)
            where E : IAddable
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

            return e;
        }
        #endregion

        #region Remove
        public E Remove<E>(E e)
            where E : IAddable
        {
            if (e.Parent != this)
                HConsole.Warning("Requested Entity {0} to be removed from Container {1} when its Parent was {2}.", e, this, e.Parent);

            e.Parent = null;

            Entities.Remove(e);

            if (e is IUpdatable ue)
                _entitiesToUpdate.Remove(ue);

            if (e is IRenderableOnLayer re)
            {
                RemoveRender(re);
                re.LayerChanged -= OnIRenderableLayerChanged;
            }

            e.OnRemoved();

            return e;
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
                if (!(e is T))
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
                if (!(e is T1) && !(e is T2))
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

        #region GetEntities
        public List<E> GetEntities<E>(bool considerChildren = true)
        {
            var list = new List<E>();

            foreach (var a in Entities)
            {
                if (a is E)
                    list.Add((E)a);

                if (considerChildren)
                {
                    if (a is IContainer c)
                        list.AddRange(c.GetEntities<E>());
                }
            }

            return list;
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
        public List<E> CollideAll<E>(Point p, bool considerChildren = true)
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

        public List<E> CollideAll<E>(IRect r, bool considerChildren = true)
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

        public List<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
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

        #region GetWindowPosition
        public Point GetWindowPosition(Point localCoords) => Parent.GetWindowPosition(localCoords);

        public IRect GetWindowPosition(IRect localCoords) => Parent.GetWindowPosition(localCoords);
        #endregion

        #region GetLocalPosition
        public Point GetLocalPosition(Point windowCoords) => Parent.GetLocalPosition(windowCoords);

        public IRect GetLocalPosition(IRect windowCoords) => Parent.GetLocalPosition(windowCoords);
        #endregion

        public Point LocalMousePosition => GetLocalPosition(HI.MouseWindowP);
        #endregion

        public IContainer Parent { get; private set; }

        #region Methods
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

        #region RemoveRender
        private void RemoveRender(IRenderableOnLayer e)
        {
            _entitiesToRender[e.Layer].Remove(e);

            if (_entitiesToRender[e.Layer].Count == 0)
            {
                _entitiesToRender.Remove(e.Layer);
                _layers.Remove(e.Layer);
                //nb no need to sort after a remove
            }
        }
        #endregion

        #region OnIRenderableLayerChanged
        private void OnIRenderableLayerChanged(object sender, LayerChangedArgs args)
        {
            IRenderableOnLayer e = (IRenderableOnLayer)sender;

            RemoveRender(e);

            AddRender(e, args.NewLayer);
        }
        #endregion
        #endregion
    }
}
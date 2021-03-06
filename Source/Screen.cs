using BearsEngine.Win32API;

namespace BearsEngine.Worlds
{
    public class Screen : IContainer, IScene
    {
        #region Fields
        private readonly IContainer _container;
        private bool _losingFocusPauses = false;
        #endregion

        #region Constructors
        public Screen()
        {
            _container = new Container(this);
        }
        #endregion

        public bool Active { get; set; } = true;
        public bool Visible { get; set; } = true;

        public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            OpenGL32.ClearColour(HV.ScreenColour);
            OpenGL32.Clear(ClearBufferMask.ColourBufferBit | ClearBufferMask.DepthBufferBit);

            OpenGL32.Enable(EnableCap.Blend);
            OpenGL32.glBlendFunc(BlendScaleFactor.GL_ONE, BlendScaleFactor.GL_ONE_MINUS_SRC_ALPHA);

            if (_container.Visible)
                _container.Render(ref projection, ref modelView);
        }
        public virtual void Update(double elapsed) => _container.Update(elapsed);



        #region IContainer
        public List<IAddable> Entities => _container.Entities;

        public int EntityCount => _container.EntityCount;

        public Point GetWindowPosition(Point localCoords) => localCoords;

        public IRect GetWindowPosition(IRect localCoords) => localCoords;

        public Point GetLocalPosition(Point windowCoords) => windowCoords;

        public IRect GetLocalPosition(IRect windowCoords) => windowCoords;

        public Point LocalMousePosition => HI.MouseWindowP;

        public void Add(IAddable e) => _container.Add(e);

        public void Add(params IAddable[] entities) => _container.Add(entities);

        public void Remove(IAddable e) => _container.Remove(e);

        public void RemoveAll(bool cascadeToChildren = true) => _container.RemoveAll(cascadeToChildren);

        public void RemoveAll<T>(bool cascadeToChildren = true)
            where T : IAddable
            => _container.RemoveAll<T>(cascadeToChildren);

        public void RemoveAll<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable
            => _container.RemoveAll<T1, T2>(cascadeToChildren);

        public void RemoveAllExcept<T>(bool cascadeToChildren = true)
            where T : IAddable
            => _container.RemoveAllExcept<T>(cascadeToChildren);

        public void RemoveAllExcept<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable
            => _container.RemoveAllExcept<T1, T2>(cascadeToChildren);

        public List<E> GetEntities<E>(bool considerChildren = true) => _container.GetEntities<E>(considerChildren);

        public E Collide<E>(Point p, bool considerChildren = true)
            where E : ICollideable
            => _container.Collide<E>(p, considerChildren);

        public E Collide<E>(IRect r, bool considerChildren = true)
            where E : ICollideable
            => _container.Collide<E>(r, considerChildren);

        public E Collide<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable
            => _container.Collide<E>(i, considerChildren);

        public List<E> CollideAll<E>(Point p, bool considerChildren = true)
            where E : ICollideable
            => _container.CollideAll<E>(p, considerChildren);

        public List<E> CollideAll<E>(IRect r, bool considerChildren = true)
            where E : ICollideable
            => _container.CollideAll<E>(r, considerChildren);

        public List<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable
            => _container.CollideAll<E>(i, considerChildren);
        #endregion

        public virtual void Start() { }

        public virtual void End() { }

        #region OnResize
        public virtual void OnResize()
        {
            OpenGL32.Viewport(HV.Window.Viewport);
            HV.OrthoMatrix = Matrix4.CreateOrtho(HV.Window.ClientSize.X, HV.Window.ClientSize.Y);
        }
        #endregion

        #region LosingFocusPauses
        public bool LosingFocusPauses
        {
            get => _losingFocusPauses;
            set
            {
                if (value == _losingFocusPauses)
                {
                    HConsole.Warning("Set Screen.LosingFocusPauses to the same value it already was: {0}", value);
                    return;
                }

                if (value)
                    HV.Window.FocusChanged += OnFocusChanged;
                else
                    HV.Window.FocusChanged -= OnFocusChanged;

                _losingFocusPauses = value;
            }
        }
        #endregion

        protected void OnFocusChanged(object? sender, BoolEventArgs e) => Active = e.Value;
    }
}
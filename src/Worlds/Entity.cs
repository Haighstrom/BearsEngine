using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Tweens;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Worlds
{
    public class Entity : AddableRectBase, IEntity
    {
        #region Fields
        private bool _active = true;
        private int _layer;
        private IContainer _container;
        #endregion

        #region Constructors
        public Entity(int layer, IRect<float> pos, string graphicPath)
            : this(layer, pos.X, pos.Y, pos.W, pos.H, new Image(graphicPath, pos.W, pos.H))
        {
        }

        public Entity(int layer, Point pos, float w, float h, string graphicPath)
            : this(layer, pos.X, pos.Y, w, h, new Image(graphicPath, w, h))
        {
        }

        public Entity(int layer, Point size, string graphicPath)
            : this(layer, 0, 0, size.X, size.Y, new Image(graphicPath, size))
        {
        }

        public Entity(int layer, float w, float h, string graphicPath)
            : this(layer, 0, 0, w, h, new Image(graphicPath, w, h))
        {
        }

        public Entity(int layer, float x, float y, float w, float h, string graphicPath)
            : this(layer, x, y, w, h, new Image(graphicPath, w, h))
        {
        }

        public Entity(int layer, IRect<float> pos, Colour colour)
            : this(layer, pos.X, pos.Y, pos.W, pos.H, new Image(colour, pos.Size))
        {
        }

        public Entity(int layer, Point size, Colour colour)
            : this(layer, 0, 0, size.X, size.Y, new Image(colour, size))
        {
        }

        public Entity(int layer, float x, float y, float w, float h, Colour colour)
            : this(layer, x, y, w, h, new Image(colour, w, h))
        {
        }


        public Entity(int layer, IRect<float> pos, params IGraphic[] graphics)
            : this(layer, pos.X, pos.Y, pos.W, pos.H, graphics)
        {
        }

        public Entity(int layer, Point size, params IGraphic[] graphics)
            : this(layer, 0, 0, size.X, size.Y, graphics)
        {
        }

        public Entity(int layer = 0, float x = 0, float y = 0, float w = 0, float h = 0, params IGraphic[] graphics)
            : base(x, y, w, h)
        {
            _container = new Container(this);

            Layer = layer;

            Add(graphics);
            Add(new ClickController(this));
        }
        #endregion

        #region IUpdateable
        #region Active
        public virtual bool Active
        {
            get => _active;
            set
            {
                if (_active == value)
                    return;

                _active = value;

                if (_active)
                    OnActivated();
                else
                    OnDeactivated();
            }
        }
        #endregion

        #region Update
        public virtual void Update(double elapsed)
        {
            if (Parent != null)
                _container.Update(elapsed);
        }
        #endregion
        #endregion

        #region IRenderable
        public virtual bool Visible { get; set; } = true;

        #region Render
        public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            Matrix4 mv = modelView;

            if (Angle != 0)
                mv = Matrix4.RotateAroundPoint(ref mv, Angle, Centre.X, Centre.Y);

            mv = Matrix4.Translate(ref mv, X, Y, 0);

            if (_container.Visible)
                _container.Render(ref projection, ref mv);
        }
        #endregion
        #endregion

        #region IRenderableOnLayer
        #region Layer
        public int Layer
        {
            get => _layer;
            set
            {
                if (_layer == value)
                    return;

                LayerChanged(this, new LayerChangedArgs(_layer, value));

                _layer = value;
            }
        }
        #endregion

        public event EventHandler<LayerChangedArgs> LayerChanged = delegate { };
        #endregion

        #region IContainer
        public List<IAddable> Entities => _container.Entities;

        public int EntityCount => _container.EntityCount;

        public IPoint<float> GetWindowPosition(IPoint<float> localCoords) => Parent != null ? Parent.GetWindowPosition(P.Add(localCoords)) : new Point();

        public IRect<float> GetWindowPosition(IRect<float> localCoords)
        {
            IPoint<float> tl = GetWindowPosition(localCoords.TopLeft);
            IPoint<float> br = GetWindowPosition(localCoords.BottomRight);
            return new Rect<float>(tl, br.X - tl.X, br.Y - tl.Y);
        }

        public IPoint<float> GetLocalPosition(IPoint<float> windowCoords) => Parent.GetLocalPosition(windowCoords.Subtract(P));

        public IRect<float> GetLocalPosition(IRect<float> windowCoords)
        {
            IPoint<float> tl = GetLocalPosition(windowCoords.TopLeft);
            IPoint<float> br = GetLocalPosition(windowCoords.BottomRight);
            return new Rect<float>(tl, br.X - tl.X, br.Y - tl.Y);
        }
        public IPoint<float> LocalMousePosition => GetLocalPosition(HI.MouseWindowP);

        public virtual E Add<E>(E e) where E : IAddable => _container.Add(e);

        public void Add<E>(params E[] entities) where E : IAddable => _container.Add(entities);

        public E Remove<E>(E e) where E : IAddable => _container.Remove(e);

        public void RemoveAll(bool cascadeToChildren = true) => _container.RemoveAll(cascadeToChildren);

        public void RemoveAll<T>(bool cascadeToChildren = true) where T : IAddable => _container.RemoveAll<T>(cascadeToChildren);

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

        public E Collide<E>(IRect<float> r, bool considerChildren = true)
            where E : ICollideable
            => _container.Collide<E>(r, considerChildren);

        public E Collide<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable
            => _container.Collide<E>(i, considerChildren);

        public List<E> CollideAll<E>(Point p, bool considerChildren = true)
            where E : ICollideable
            => _container.CollideAll<E>(p, considerChildren);

        public List<E> CollideAll<E>(IRect<float> r, bool considerChildren = true)
            where E : ICollideable
            => _container.CollideAll<E>(r, considerChildren);

        public List<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
            where E : ICollideable
            => _container.CollideAll<E>(i, considerChildren);
        #endregion

        #region ICollideable
        public IRect<float> WindowPosition => Parent == null ? Rect.Empty : Parent.GetWindowPosition(this);

        public bool Collideable { get; set; } = true;

        #region Collides
        public virtual bool Collides(Point p) => WindowPosition.Contains(p);

        public virtual bool Collides(IRect<float> r) => WindowPosition.Intersects(r);

        public virtual bool Collides(ICollideable i) => WindowPosition.Intersects(i.WindowPosition);
        #endregion
        #endregion

        #region IClickable
        public bool Clickable { get; set; } = true;

        public bool MouseIntersecting => WindowPosition.Contains(HI.MouseWindowP);

        public virtual void OnMouseEnter()
        {
            MouseEntered?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnMouseExit()
        {
            MouseExited?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnLeftDown() { }

        public virtual void OnLeftPressed() => LeftPressed?.Invoke(this, EventArgs.Empty);

        public virtual void OnLeftReleased() => LeftReleased?.Invoke(this, EventArgs.Empty);

        public virtual void OnLeftClicked() => LeftClicked?.Invoke(this, EventArgs.Empty);

        public virtual void OnLeftDoubleClicked() => LeftDoubleClicked?.Invoke(this, EventArgs.Empty);

        public virtual void OnRightDown() { }

        public virtual void OnRightPressed() => RightPressed?.Invoke(this, EventArgs.Empty);

        public virtual void OnRightReleased() => RightReleased?.Invoke(this, EventArgs.Empty);

        public virtual void OnRightClicked() => RightClicked?.Invoke(this, EventArgs.Empty);

        public virtual void OnHover() { }

        public virtual void OnNoMouseEvent() { }
        #endregion

        #region Properties
        public bool IsOnScreen => HV.Window.ClientZeroed.Intersects(WindowPosition);

        #region Angle
        /// <summary>
        /// Angle in Degrees
        /// </summary>
        public float Angle { get; set; }
        #endregion

        public List<IGraphic> Graphics => GetEntities<IGraphic>(false);
        #endregion

        #region Methods
        #region AddToolTip
        public void AddToolTip(UITheme theme, string text, Direction directionFromEntity, int shift)
        {
            var stt = new SimpleToolTip(theme, text);
            MouseEntered += (s, a) => stt.CountTimerDownThenAppear();
            MouseExited += (s, a) => stt.Disappear();
            switch (directionFromEntity)
            {
                case Direction.Up:
                    stt.P = TopCentre.Shift(-stt.W / 2, -shift);
                    break;
                case Direction.Right:
                    stt.P = CentreRight.Shift(shift, -stt.H / 2);
                    break;
                case Direction.Down:
                    stt.P = BottomCentre.Shift(-stt.W / 2, shift);
                    break;
                case Direction.Left:
                    stt.P = CentreLeft.Shift(-shift, -stt.H / 2);
                    break;
                case Direction.None:
                    stt.P = Centre.Shift(-stt.W / 2 + shift, -stt.H / 2 + shift);
                    break;
                default:
                    throw new HException("directionFromEntity case not handled in Entity.AddToolTip");
            }
            HV.Screen.Add(stt);
        }
        #endregion

        #region ClampWithinWindow
        /// <summary>
        /// Moves the entity to be within the window if it isn't already
        /// </summary>
        protected void ClampWithinWindow()
        {
            if (WindowPosition.Left < 0)
                X = Parent.GetLocalPosition(new Point()).X;
            if (WindowPosition.Right > HV.Window.ClientZeroed.Right)
                X = Parent.GetLocalPosition(new Point(HV.Window.ClientZeroed.Right, 0)).X - W;
            if (WindowPosition.Top < 0)
                Y = Parent.GetLocalPosition(new Point()).Y;
            if (WindowPosition.Bottom > HV.Window.ClientZeroed.Bottom)
                Y = Parent.GetLocalPosition(new Point(0, HV.Window.ClientZeroed.Bottom)).Y - H;
        }
        #endregion

        #region ClearGraphics
        public void ClearGraphics()
        {
            foreach (IGraphic g in Graphics)
                g.Remove();
        }
        #endregion

        #region MoveTowards
        /// <summary>
        /// returns how much overshoot there was (i.e. how much asked to be moved minus what did move), will be zero if didn't reach target yet
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual float MoveTowards(Point target, float amount) => MoveTowards(target.X, target.Y, amount);

        /// <summary>
        /// returns how much overshoot there was (i.e. how much asked to be moved minus what did move), will be zero if didn't reach target yet
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual float MoveTowards(float x, float y, float amount)
        {
            float overshoot = 0;

            IPoint<float> p = new Point(x - X, y - Y);
            float distanceFromTarget = p.Length;

            if (distanceFromTarget > amount)
            {
                p = p.Normal;
                X += p.X * amount;
                Y += p.Y * amount;
            }
            else
            {
                overshoot = amount - distanceFromTarget;
                X = x;
                Y = y;
            }
            return overshoot;
        }
        #endregion
        
        protected virtual void OnActivated() { }

        protected virtual void OnDeactivated() { }

        #region OnSizeChanged
        protected override void OnSizeChanged(ResizeEventArgs args)
        {
            foreach (IGraphic g in Graphics)
                if (g.ResizeWithParent)
                    g.Resize(args.NewSize.X / args.OldSize.X, args.NewSize.Y / args.OldSize.Y);
            base.OnSizeChanged(args);
        }
        #endregion
        #endregion

        #region Events
        public event EventHandler MouseEntered;
        public event EventHandler MouseExited;

        public event EventHandler LeftClicked;
        public event EventHandler LeftPressed;
        public event EventHandler LeftReleased;
        public event EventHandler LeftDoubleClicked;

        public event EventHandler RightClicked;
        public event EventHandler RightPressed;
        public event EventHandler RightReleased;
        #endregion

        #region Overloads / Overrides
        public override string ToString() => GetType().Name;
        #endregion
    }
}
using BearsEngine.UI;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Worlds;

public class Entity : AddableRectBase, IContainer, IUpdatable, IRenderableOnLayer, IClickable, ICollideable
{
    #region Fields
    private bool _active = true;
    private int _layer;
    private readonly IContainer _container;
    #endregion

    #region Constructors
    public Entity(int layer, IRect pos, string graphicPath)
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

    public Entity(int layer, IRect pos, Colour colour)
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


    public Entity(int layer, Rect pos, params IGraphic[] graphics)
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

    #region Properties
    public virtual bool Active
    {
        get => _active;
        set
        {
            if (_active == value)
                return;

            _active = value;

            if (Exists)
            {
                if (_active)
                    OnActivated();
                else
                    OnDeactivated();
            }
        }
    }

    /// <summary>
    /// Angle in Degrees
    /// </summary>
    public float Angle { get; set; }
    #endregion

    public bool Clickable { get; set; } = true;
    public bool Collideable { get; set; } = true;

    public bool Exists => Parent is not null;
    public IList<IGraphic> Graphics => GetEntities<IGraphic>(false);

    public bool IsOnScreen => Exists;//todo: HEngine2.Window.ClientSize.ToRect().Intersects(WindowPosition);

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
    
    public bool MouseIntersecting => Exists && WindowPosition.Contains(HI.MouseWindowP);
    
    public virtual Point RotationCentre => Centre;

    public IRect WindowPosition => Exists ? Parent!.GetWindowPosition(this) : Rect.Empty;

    public virtual bool Visible { get; set; } = true;

    public virtual bool Collides(Point p) => WindowPosition.Contains(p);

    public virtual bool Collides(IRect r) => WindowPosition.Intersects(r);

    public virtual bool Collides(ICollideable i) => WindowPosition.Intersects(i.WindowPosition);

    public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
    {
        Matrix4 mv = modelView;

        if (Angle != 0)
            mv = Matrix4.RotateAroundPoint(ref mv, Angle, RotationCentre);

        mv = Matrix4.Translate(ref mv, X, Y, 0);

        if (_container.Visible)
            _container.Render(ref projection, ref mv);
    }

    public virtual void Update(double elapsed)
    {
        if (Exists)
            _container.Update(elapsed);
    }

    public event EventHandler<LayerChangedArgs> LayerChanged;

    public IList<IAddable> Entities => _container.Entities;

    public int EntityCount => _container.EntityCount;

    public Point GetWindowPosition(Point localCoords) => Parent != null ? Parent.GetWindowPosition(P + localCoords) : new Point();

    public IRect GetWindowPosition(IRect localCoords)
    {
        Point tl = GetWindowPosition(localCoords.TopLeft);
        Point br = GetWindowPosition(localCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }

    public Point GetLocalPosition(Point windowCoords) => Parent.GetLocalPosition(windowCoords - P);

    public IRect GetLocalPosition(IRect windowCoords)
    {
        Point tl = GetLocalPosition(windowCoords.TopLeft);
        Point br = GetLocalPosition(windowCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }
    public Point LocalMousePosition => GetLocalPosition(HI.MouseWindowP);

    public void Add(IAddable e) => _container.Add(e);

    public void Add(params IAddable[] entities) => _container.Add(entities);

    public void Remove(IAddable e) => _container.Remove(e);

    public void RemoveAll(bool cascadeToChildren = true) => _container.RemoveAll(cascadeToChildren);

    public void RemoveAll<T>(bool cascadeToChildren = true) where T : IAddable => _container.RemoveAll<T>(cascadeToChildren);

    public void RemoveAllExcept<T>(bool cascadeToChildren = true)
        where T : IAddable
        => _container.RemoveAllExcept<T>(cascadeToChildren);

    public IList<E> GetEntities<E>(bool considerChildren = true) => _container.GetEntities<E>(considerChildren);

    public E Collide<E>(Point p, bool considerChildren = true)
        where E : ICollideable
        => _container.Collide<E>(p, considerChildren);

    public E Collide<E>(IRect r, bool considerChildren = true)
        where E : ICollideable
        => _container.Collide<E>(r, considerChildren);

    public E Collide<E>(ICollideable i, bool considerChildren = true)
        where E : ICollideable
        => _container.Collide<E>(i, considerChildren);

    public IList<E> CollideAll<E>(Point p, bool considerChildren = true)
        where E : ICollideable
        => _container.CollideAll<E>(p, considerChildren);

    public IList<E> CollideAll<E>(IRect r, bool considerChildren = true)
        where E : ICollideable
        => _container.CollideAll<E>(r, considerChildren);

    public IList<E> CollideAll<E>(ICollideable i, bool considerChildren = true)
        where E : ICollideable
        => _container.CollideAll<E>(i, considerChildren);

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

    public void AddToolTip(UITheme theme, string text, Direction directionFromEntity, int shift)
    {
        var stt = new SimpleToolTip(theme, text);
        MouseEntered += (s, a) => stt.CountTimerDownThenAppear();
        MouseExited += (s, a) => stt.Disappear();
        stt.P = directionFromEntity switch
        {
            Direction.Up => TopCentre.Shift(-stt.W / 2, -shift),
            Direction.Right => CentreRight.Shift(shift, -stt.H / 2),
            Direction.Down => BottomCentre.Shift(-stt.W / 2, shift),
            Direction.Left => CentreLeft.Shift(-shift, -stt.H / 2),
            _ => throw new HException("directionFromEntity case not handled in Entity.AddToolTip"),
        };
        //todo:fix HV.Screen.Add(stt);
    }

    /// <summary>
    /// Moves the entity to be within the window if it isn't already
    /// </summary>
    protected void ClampWithinWindow()
    {
        throw new NotImplementedException();
        //if (WindowPosition.Left < 0)
        //    X = Parent.GetLocalPosition(new Point()).X;
        //if (WindowPosition.Right > HV.Window.ClientZeroed.Right)
        //    X = Parent.GetLocalPosition(new Point(HV.Window.ClientZeroed.Right, 0)).X - W;
        //if (WindowPosition.Top < 0)
        //    Y = Parent.GetLocalPosition(new Point()).Y;
        //if (WindowPosition.Bottom > HV.Window.ClientZeroed.Bottom)
        //    Y = Parent.GetLocalPosition(new Point(0, HV.Window.ClientZeroed.Bottom)).Y - H;
    }

    public void ClearGraphics()
    {
        foreach (IGraphic g in Graphics)
            g.Remove();
    }

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

        Point p = new(x - X, y - Y);
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

    protected virtual void OnActivated() { }

    protected virtual void OnDeactivated() { }

    protected override void OnSizeChanged(ResizeEventArgs args)
    {
        foreach (IGraphic g in Graphics)
            if (g.ResizeWithParent)
                g.Resize(args.NewSize.X / args.OldSize.X, args.NewSize.Y / args.OldSize.Y);
        base.OnSizeChanged(args);
    }

    public event EventHandler MouseEntered;
    public event EventHandler MouseExited;

    public event EventHandler LeftClicked;
    public event EventHandler LeftPressed;
    public event EventHandler LeftReleased;
    public event EventHandler LeftDoubleClicked;

    public event EventHandler RightClicked;
    public event EventHandler RightPressed;
    public event EventHandler RightReleased;

    public override string ToString() => GetType().Name;
}

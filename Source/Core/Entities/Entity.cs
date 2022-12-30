namespace BearsEngine.Worlds;

public class Entity : EntityBase, IClickable, ICollideable
{
    private const float DefaultHoverTime = 2f;

    public Entity(float layer, Rect pos, string graphicPath)
        : this(layer, pos.X, pos.Y, pos.W, pos.H, new Image(graphicPath, pos.W, pos.H))
    {
    }

    public Entity(float layer, Point pos, float w, float h, string graphicPath)
        : this(layer, pos.X, pos.Y, w, h, new Image(graphicPath, w, h))
    {
    }

    public Entity(float layer, Point size, string graphicPath)
        : this(layer, 0, 0, size.X, size.Y, new Image(graphicPath, size))
    {
    }

    public Entity(float layer, float w, float h, string graphicPath)
        : this(layer, 0, 0, w, h, new Image(graphicPath, w, h))
    {
    }

    public Entity(float layer, float x, float y, float w, float h, string graphicPath)
        : this(layer, x, y, w, h, new Image(graphicPath, w, h))
    {
    }

    public Entity(float layer, float x, float y, Point size, string graphicPath)
    : this(layer, x, y, size.X, size.Y, new Image(graphicPath, size.X, size.Y))
    {
    }

    public Entity(float layer, Rect pos, Colour colour)
        : this(layer, pos.X, pos.Y, pos.W, pos.H, new Image(colour, pos.Size))
    {
    }

    public Entity(float layer, Point size, Colour colour)
        : this(layer, 0, 0, size.X, size.Y, new Image(colour, size))
    {
    }

    public Entity(float layer, float x, float y, float w, float h, Colour colour)
        : this(layer, x, y, w, h, new Image(colour, w, h))
    {
    }

    public Entity(float layer, float x, float y, Point size, params IGraphic[] graphics)
        : this(layer, x, y, size.X, size.Y, graphics)
    {
    }

    public Entity(float layer, Rect pos, params IGraphic[] graphics)
        : this(layer, pos.X, pos.Y, pos.W, pos.H, graphics)
    {
    }

    public Entity(float layer, Point size, params IGraphic[] graphics)
        : this(layer, 0, 0, size.X, size.Y, graphics)
    {
    }

    public Entity(float layer = 0, float x = 0, float y = 0, float w = 0, float h = 0, params IGraphic[] graphics)
        : base(layer, x, y, w, h)
    {
        Add(graphics);
        Add(new ClickController(this));
    }

    public virtual bool Clickable { get; set; } = true;

    public bool Collideable { get; set; } = true;

    public bool Exists => Parent is not null;

    public IList<IGraphic> Graphics => GetEntities<IGraphic>(false);

    public bool IsOnScreen => Exists;//todo: HEngine2.Window.ClientSize.ToRect().Intersects(WindowPosition);

    public override Point LocalMousePosition => GetLocalPosition(Mouse.ClientP);

    public virtual bool MouseIntersecting => WindowPosition.Contains(Mouse.ClientP);
    
    public virtual Point RotationCentre => R.Centre;

    public float TimeToHover { get; set; } = DefaultHoverTime;

    public virtual Rect WindowPosition => Parent.GetWindowPosition(R);

    void IClickable.OnLeftClicked()
    {
        OnLeftClicked();
        LeftClicked?.Invoke(this, EventArgs.Empty);
    }

    void IClickable.OnLeftPressed()
    {
        OnLeftPressed();
        LeftPressed?.Invoke(this, EventArgs.Empty);
    }

    void IClickable.OnLeftReleased()
    {
        OnLeftReleased();
        LeftReleased?.Invoke(this, EventArgs.Empty);
    }

    void IClickable.OnMouseEntered()
    {
        OnMouseEntered();
        MouseEntered?.Invoke(this, EventArgs.Empty);
    }

    void IClickable.OnMouseExited()
    {
        OnMouseExited();
        MouseExited?.Invoke(this, EventArgs.Empty);
    }

    void IClickable.OnMouseHovered()
    {
        OnMouseHovered();
        MouseHovered?.Invoke(this, EventArgs.Empty);
    }

    void IClickable.OnNoMouseEvent()
    {
        OnNoMouseEvent();
        NoMouseEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnLeftClicked() { }

    protected virtual void OnLeftPressed() { }

    protected virtual void OnLeftReleased() { }

    protected virtual void OnMouseEntered() { }

    protected virtual void OnMouseExited() { }

    protected virtual void OnMouseHovered() { }

    protected virtual void OnNoMouseEvent() { }

    public virtual bool Collides(Point p) => WindowPosition.Contains(p);

    public virtual bool Collides(Rect r) => WindowPosition.Intersects(r);

    public virtual bool Collides(ICollideable i) => WindowPosition.Intersects(i.WindowPosition);

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        Matrix3 mv = modelView;

        mv = Matrix3.Translate(ref mv, X, Y);

        base.Render(ref projection, ref mv);
    }

    public override Point GetWindowPosition(Point localCoords) => Parent.GetWindowPosition(new Point(X, Y) + localCoords);

    public override Rect GetWindowPosition(Rect localCoords)
    {
        Point tl = GetWindowPosition(localCoords.TopLeft);
        Point br = GetWindowPosition(localCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }

    public override Point GetLocalPosition(Point windowCoords) => Parent.GetLocalPosition(windowCoords - new Point(X, Y));

    public override Rect GetLocalPosition(Rect windowCoords)
    {
        Point tl = GetLocalPosition(windowCoords.TopLeft);
        Point br = GetLocalPosition(windowCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }

    /// <summary>
    /// Returns how much overshoot there was, i.e. how much asked to be moved minus what did move. Will be zero if didn't reach target yet.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public virtual float MoveTowards(Point target, float amount) => MoveTowards(target.X, target.Y, amount);

    /// <summary>
    /// Returns how much overshoot there was, i.e. how much asked to be moved minus what did move. Will be zero if didn't reach target yet.
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

    public event EventHandler? LeftClicked;

    public event EventHandler? LeftPressed;

    public event EventHandler? LeftReleased;

    public event EventHandler? MouseEntered;

    public event EventHandler? MouseExited;

    public event EventHandler? MouseHovered;

    public event EventHandler? NoMouseEvent;
}

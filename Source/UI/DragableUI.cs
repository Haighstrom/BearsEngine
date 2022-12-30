namespace BearsEngine.UI;

public class DragableUI : Entity
{
    private int _dragStartX, _dragStartY;

    public DragableUI(float layer, Rect pos, Colour colour)
        : base(layer, pos, colour)
    {
    }
    public DragableUI(float layer, Rect pos, string gfx)
        : base(layer, pos, gfx)
    {
    }
    public DragableUI(float layer, Rect pos, IGraphic gfx)
        : base(layer, pos, gfx)
    {
    }

    public bool Dragable { get; set; } = true;

    public bool Dragging { get; private set; } = false;

    protected virtual Rect DragGrabArea => R;

    public override Rect WindowPosition => Parent!.GetWindowPosition(DragGrabArea);

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        if (!Visible)
            return;

        if (Dragable && Mouse.LeftPressed && MouseIntersecting)
        {
            Dragging = true;
            OnStartedDragging();
            _dragStartX = (int)(Mouse.ClientX - X);
            _dragStartY = (int)(Mouse.ClientY - Y);
        }

        if (Dragging && (Mouse.LeftUp || !Dragable))
        {
            Dragging = false;
            OnStoppedDragging();
        }

        if (Dragging)
        {
            X = Mouse.ClientX - _dragStartX;
            Y = Mouse.ClientY - _dragStartY;
        }
    }
    

    protected virtual void OnStartedDragging()
    {
        StartedDragging(this, EventArgs.Empty);
    }
    public event EventHandler StartedDragging = delegate { };
    protected virtual void OnStoppedDragging()
    {
        StoppedDragging(this, EventArgs.Empty);
    }
    public event EventHandler StoppedDragging = delegate { };
    
}

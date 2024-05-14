using BearsEngine.Input;

namespace BearsEngine.UI;

public class DragableUI : Entity
{
    private int _dragStartX, _dragStartY;
    private readonly IMouse _mouse;

    public DragableUI(float layer, Rect pos, Colour colour)
        : base(layer, pos, colour)
    {
        _mouse = Mouse.Instance;
    }
    public DragableUI(float layer, Rect pos, string gfx)
        : base(layer, pos, gfx)
    {
        _mouse = Mouse.Instance;
    }
    public DragableUI(float layer, Rect pos, IGraphic gfx)
        : base(layer, pos, gfx)
    {
        _mouse = Mouse.Instance;
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

        if (Dragable && _mouse.LeftPressed && MouseIntersecting)
        {
            Dragging = true;
            OnStartedDragging();
            _dragStartX = (int)(_mouse.ClientX - X);
            _dragStartY = (int)(_mouse.ClientY - Y);
        }

        if (Dragging && (_mouse.LeftUp || !Dragable))
        {
            Dragging = false;
            OnStoppedDragging();
        }

        if (Dragging)
        {
            X = _mouse.ClientX - _dragStartX;
            Y = _mouse.ClientY - _dragStartY;
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

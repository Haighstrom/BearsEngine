﻿namespace BearsEngine.UI;

public class DragableUI : Entity
{
    private int _dragStartX, _dragStartY;

    public DragableUI(int layer, Rect pos, Colour colour)
        : base(layer, pos, colour)
    {
    }
    public DragableUI(int layer, Rect pos, string gfx)
        : base(layer, pos, gfx)
    {
    }
    public DragableUI(int layer, Rect pos, IGraphic gfx)
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

        if (Dragable && HI.MouseLeftPressed && MouseIntersecting)
        {
            Dragging = true;
            OnStartedDragging();
            _dragStartX = (int)(HI.MouseWindowX - X);
            _dragStartY = (int)(HI.MouseWindowY - Y);
        }

        if (Dragging && (HI.MouseLeftUp || !Dragable))
        {
            Dragging = false;
            OnStoppedDragging();
        }

        if (Dragging)
        {
            X = HI.MouseWindowX - _dragStartX;
            Y = HI.MouseWindowY - _dragStartY;
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

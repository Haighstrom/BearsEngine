namespace BearsEngine.UI;

public class Checkbox : Button
{
    private bool _disabled;
    private readonly IGraphic _boxGraphic;
    private readonly IGraphic _disabledGraphic;
    private readonly IGraphic _tickGraphic;

    public Checkbox(float layer, Rect r, string boxGraphic, string disabledGraphic, string tickGraphic, UITheme theme, bool startTicked)
        : this(layer, r, new Image(boxGraphic, r.Size), new Image(disabledGraphic, r.Size), new Image(tickGraphic, r.Size), theme, startTicked)
    {
        DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
    }

    public Checkbox(float layer, Rect r, IRectGraphic boxGraphic, IGraphic disabledGraphic, IGraphic tickGraphic, UITheme theme, bool startTicked, bool startDisabled = false)
        : this(layer, r, boxGraphic, disabledGraphic, tickGraphic, startTicked, startDisabled)
    {
        DefaultColour = theme.Button.DefaultColour;
        UnclickableColour = theme.Button.UnclickableColour;
        HoverColour = theme.Button.HoverColour;
        PressedColour = theme.Button.PressedColour;
    }
    

    public Checkbox(float layer, Rect r, string boxGraphic, string disabledGraphic, string tickGraphic, bool startTicked, bool startDisabled = false)
        : this(layer, r, new Image(1, boxGraphic, r.Size), new Image(1, disabledGraphic, r.Size), new Image(0, tickGraphic, r.Size), startTicked, startDisabled)
    {
    }

    public Checkbox(float layer, Rect r, Colour boxGraphic, Colour disabledGraphic, string tickGraphic, bool startTicked, bool startDisabled = false)
        : this(layer, r, new Image(boxGraphic, r.Size), new Image(disabledGraphic, r.Size), new Image(tickGraphic, r.Size), startTicked, startDisabled)
    {
        DefaultColour = boxGraphic; //ignore theme colour if colour is set
    }

    public Checkbox(float layer, Rect r, IRectGraphic boxGraphic, IGraphic disabledGraphic, IGraphic tickGraphic, bool startTicked, bool startDisabled = false)
        : base(layer, r.X, r.Y, r.W, r.H)
    {
        _boxGraphic = boxGraphic;
        _disabledGraphic = disabledGraphic;
        _tickGraphic = tickGraphic;
        _disabled = startDisabled;

        BackgroundGraphic = startDisabled ? _disabledGraphic : _boxGraphic;
        Add(BackgroundGraphic);

        _tickGraphic.Visible = startTicked;
        Add(_tickGraphic);
    }

    protected virtual void OnChecked()
    {
        Checked?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnUnchecked()
    {
        Unchecked?.Invoke(this, EventArgs.Empty);
    }

    public bool IsChecked
    {
        get => _tickGraphic.Visible;
        set
        {
            if (_tickGraphic.Visible != value)
            {
                _tickGraphic.Visible = value;

                if (value)
                {
                    OnChecked();
                }
                else
                {
                    OnUnchecked();
                }
            }
        }
    }

    public bool IsDisabled
    {
        get => _disabled;

        set
        {
            if (_disabled != value)
            {
                _disabled = value;

                BackgroundGraphic!.Remove();
                BackgroundGraphic = _disabled ? _disabledGraphic : _boxGraphic;
                Add(BackgroundGraphic);
            }
        }
    }

    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();

        if (!_disabled)
        {
            IsChecked = !IsChecked;
        }
    }
    
    public event EventHandler<EventArgs>? Checked, Unchecked;
}

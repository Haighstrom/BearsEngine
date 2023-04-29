using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public class Button : Entity, IClickable
{
    private readonly TextGraphic _text;

    public Button(float layer, Rect position, string graphic, UITheme theme, string text, Action? actionOnClicked = null)
        : this(layer, position, new Image(graphic, position.Size), theme, text, actionOnClicked)
    {
        DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
    }

    public Button(float layer, Rect position, Colour graphic, UITheme theme, string text, Action? actionOnClicked = null)
        : this(layer, position, new Image(graphic, position.Size), theme, text, actionOnClicked)
    {
        DefaultColour = graphic; //ignore theme colour if colour is set
    }

    public Button(float layer, Rect position, string graphic, UITheme theme, Action? actionOnClicked = null)
        : this(layer, position, new Image(graphic, position.Size), theme, actionOnClicked)
    {
        DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
    }

    public Button(float layer, Rect position, Colour graphic, UITheme theme, Action? actionOnClicked = null)
        : this(layer, position, new Image(graphic, position.Size), theme, actionOnClicked)
    {
        DefaultColour = graphic; //ignore theme colour if colour is set
    }

    public Button(float layer, Rect position, UITheme theme, Action? actionOnClicked = null)
        : this(layer, position, new Image(theme.Button.DefaultColour, position.Size), theme, actionOnClicked)
    {
    }

    public Button(float layer, IRectGraphic g, UITheme theme, Action? actionOnClicked = null)
        : this(layer, g.R, g, theme, actionOnClicked)
    {
    }

    public Button(float layer, Rect position, IGraphic graphic, UITheme theme, Action? actionOnClicked = null)
        : this(layer, position.X, position.Y, position.W, position.H, graphic, null, null, "", actionOnClicked)
    {
        DefaultColour = theme.Button.DefaultColour;
        UnclickableColour = theme.Button.UnclickableColour;
        HoverColour = theme.Button.HoverColour;
        PressedColour = theme.Button.PressedColour;
    }

    public Button(float layer, Rect position, IGraphic graphic, UITheme theme, string text, Action? actionOnClicked = null)
        : this(layer, position.X, position.Y, position.W, position.H, graphic, theme.Button.Text.Font, theme.Button.Text.FontColour, text, actionOnClicked)
    {
        DefaultColour = theme.Button.DefaultColour;
        UnclickableColour = theme.Button.UnclickableColour;
        HoverColour = theme.Button.HoverColour;
        PressedColour = theme.Button.PressedColour;
    }

    public Button(float layer, Rect r, string graphic, Action? actionOnClicked = null)
        : this(layer, r, new Image(graphic, r.Size), actionOnClicked)
    {
    }

    public Button(float layer, Rect r, Colour graphic, Action? actionOnClicked = null)
        : this(layer, r, new Image(graphic, r.Size), actionOnClicked)
    {
        DefaultColour = graphic; //ignore theme colour if colour is set
    }

    public Button(float layer, Rect r, IGraphic? graphic = null, Action? actionOnClicked = null)
        : this(layer, r.X, r.Y, r.W, r.H, graphic, null, null, "", actionOnClicked)
    {
    }

    public Button(float layer, Rect r, Colour graphic, HFont font, Colour fontColour, string text = "", Action? actionOnClicked = null)
        : this(layer, r.X, r.Y, r.W, r.H, new Image(graphic, r.Size), font, fontColour, text, actionOnClicked)
    {
        DefaultColour = graphic; //ignore theme colour if colour is set
    }

    public Button(float layer, Rect r, string graphic, HFont font, Colour fontColour, string text = "", Action? actionOnClicked = null)
        : this(layer, r.X, r.Y, r.W, r.H, new Image(graphic, r.Size), font, fontColour, text, actionOnClicked)
    {
    }

    public Button(float layer = 0, float x = 0, float y = 0, float w = 0, float h = 0, IGraphic? graphic = null, HFont? font = null, Colour? fontColour = null, string text = "", Action? actionOnClicked = null)
        : base(layer, x, y, w, h)
    {
        if (graphic != null)
            Add(BackgroundGraphic = graphic);

        Add(_text = new TextGraphic(font ?? UITheme.Default.Text.Font, fontColour ?? UITheme.Default.Text.FontColour, new Point(w, h), text)
        {
            HAlignment = HAlignment.Centred,
            VAlignment = VAlignment.Centred
        });

        ActionOnClicked = actionOnClicked;
    }

    protected IGraphic? BackgroundGraphic { get; set; }
    
    public Action? ActionOnClicked { get; set; }

    public override bool Clickable
    {
        get => base.Clickable;
        set
        {
            if (value != Clickable)
            {
                base.Clickable = value;

                if (Clickable)
                    BackgroundGraphic.Colour = DefaultColour;
                else
                    BackgroundGraphic.Colour = UnclickableColour;
            }
        }
    }

    public Colour DefaultColour { get; set; } = Colour.White;

    public Colour UnclickableColour { get; set; } = Colour.White;

    public Colour HoverColour { get; set; } = Colour.White;

    public Colour PressedColour { get; set; } = Colour.White;

    public string Text
    {
        get => _text.Text;
        set => _text.Text = value;
    }

    public void SetDefaultAutoShadingColours()
    {
        DefaultColour = Colour.White;
        UnclickableColour = Colour.DarkGray;
        HoverColour = new Colour(255, 255, 125, 255);
        PressedColour = Colour.Yellow;
    }
    
    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();
        ActionOnClicked?.Invoke();
        if (Clickable)
            BackgroundGraphic.Colour = HoverColour;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();
        if (Clickable)
            BackgroundGraphic.Colour = PressedColour;
    }
    
    protected override void OnMouseEntered()
    {
        base.OnMouseEntered();
        if (Clickable)
            BackgroundGraphic.Colour = HoverColour;
    }

    protected override void OnNoMouseEvent()
    {
        base.OnNoMouseEvent();
        if (Clickable)
            BackgroundGraphic.Colour = DefaultColour;
    }

    public void Press() => OnLeftClicked();
}

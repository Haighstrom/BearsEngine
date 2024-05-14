using BearsEngine.Input;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public class SimpleToolTip : Entity
{
    private Image _bg;
    private TextGraphic _text;
    private readonly float _borderThickness, _textToEdgeGap;
    private readonly Colour _borderColour, _backgroundColour;
    private bool _waitingToAppear = false;
    private float _timer;
    

    public SimpleToolTip(UITheme theme, string initialText)
        : this(theme, 0, 0, initialText)
    {
    }

    public SimpleToolTip(UITheme theme, int x = 0, int y = 0, string initialText = "")
        : this(x, y, theme.Label.Text.Font, theme.Label.Text.FontColour, theme.Label.Panel.Border.Thickness, theme.Label.Panel.Border.Colour, theme.Label.Panel.BackgroundColour, theme.Label.EdgeToTextSpace, initialText)
    {
    }
    

    public SimpleToolTip(HFont font, Colour textColour, Colour borderColour, Colour backgroundColour, float borderThickness, float edgeToTextSpace)
        : this(0, 0, font, textColour, borderThickness, borderColour, backgroundColour, edgeToTextSpace, "", false)
    {
    }

    public SimpleToolTip(HFont font, Colour textColour, string initialText, Colour borderColour, Colour backgroundColour, float borderThickness, float edgeToTextSpace)
        : this(0, 0, font, textColour, borderThickness, borderColour, backgroundColour, edgeToTextSpace, initialText, false)
    {
    }

    public SimpleToolTip(Point p, HFont font, string initialText, Colour borderColour, Colour backgroundColour, Colour textColour, float borderThickness, float edgeToTextSpace, bool initiallyVisible = false)
        : this(p.X, p.Y, font, textColour, borderThickness, borderColour, backgroundColour, edgeToTextSpace, initialText, initiallyVisible)
    {
    }

    public SimpleToolTip(float x, float y, HFont font, Colour textColour, float borderThickness, Colour borderColour, Colour backgroundColour, float edgeToTextSpace, string initialText = "", bool initiallyVisible = false)
        : base(0, x, y)
    {
        _borderThickness = borderThickness;
        _textToEdgeGap = edgeToTextSpace;
        _borderColour = borderColour;
        _backgroundColour = backgroundColour;

        SetText(font, textColour, initialText);
        Visible = initiallyVisible;
    }
    
    

    public string Text
    {
        get => _text.Text;
        set => SetText(_text.Font, _text.Colour, value);
    }
    

    public float TimeToAppear { get; set; } = 0.4f;

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        //ClampWithinWindow();

        if (_waitingToAppear)
        {
            if ((_timer -= (float)elapsed) < 0)
            {
                Appear();
                _waitingToAppear = false;
            }
        }
    }
    

    public void SetText(HFont font, Colour textColour, string text)
    {
        int textW = (int)font.MeasureString(text).X;
        int textH = font.HighestChar;

        _bg?.Remove();
        _text?.Remove();

        _bg = new Image(OpenGLHelper.GenRectangle((int)(textW + 2 * _textToEdgeGap), (int)(textH + 2 * _textToEdgeGap), (int)_borderThickness, _borderColour, _backgroundColour));
        W = _bg.W;
        H = _bg.H;

        _text = new TextGraphic(font, textColour, new Rect(_textToEdgeGap, _textToEdgeGap, textW, textH), text)
        {
            HAlignment = HAlignment.Centred,
            VAlignment = VAlignment.Centred
        };

        Add(_bg, _text);
    }
    

    public void CountTimerDownThenAppear()
    {
        if (!_waitingToAppear)
        {
            _waitingToAppear = true;
            _timer = TimeToAppear;
        }
    }
    

    public void Appear() => Visible = true;

    public void Disappear()
    {
        Visible = false;
        _waitingToAppear = false;
    }
    
    
}

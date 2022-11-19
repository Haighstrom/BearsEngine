using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public class TextLabel : Entity
{
    private readonly HText _hText;
    

    public TextLabel(int layer, Rect position, Colour labelColour, HFont font, string text, Colour textColour)
        : base(layer, position, labelColour)
    {
        Add(_hText = new HText(font, position.Zeroed, text)
        {
            Colour = textColour,
            HAlignment = HAlignment.Centred,
            VAlignment = VAlignment.Centred,
        });
    }
    public TextLabel(int layer, Rect position, string labelGraphic, HFont font, string text, Colour textColour)
        : base(layer, position, labelGraphic)
    {
        Add(_hText = new HText(font, position.Zeroed, text)
        {
            Colour = textColour,
            HAlignment = HAlignment.Centred,
            VAlignment = VAlignment.Centred,
        });
    }
    

    public string Text
    {
        get => _hText.Text;
        set => _hText.Text = value;
    }
    public Colour TextColour { get { return _hText.Colour; } set { _hText.Colour = value; } }
    
}

using BearsEngine.Source.Core;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public struct TextTheme
{
    public static TextTheme Default => new()
    {
        Font = HFont.Default,
        FontColour = Colour.Black,
        HAlignment = HAlignment.Left,
        VAlignment = VAlignment.Top,
        FontScale = 1,
    };
    

    public TextTheme(TextTheme theme)
    {
        Font = theme.Font;
        FontColour = theme.FontColour;
        HAlignment = theme.HAlignment;
        VAlignment = theme.VAlignment;
        FontScale = theme.FontScale;
    }

    public HFont Font;
    public Colour FontColour;
    public float FontScale;
    public HAlignment HAlignment;
    public VAlignment VAlignment;
}

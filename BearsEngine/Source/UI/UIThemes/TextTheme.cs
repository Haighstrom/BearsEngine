using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public class TextTheme
{
    public static TextTheme Default => new();
    
    public TextTheme()
    {
    }

    public HFont Font { get; init; } = HFont.Default;

    public Colour FontColour { get; init; } = Colour.Black;

    public HAlignment HAlignment { get; init; } = HAlignment.Centred;

    public VAlignment VAlignment { get; init; } = VAlignment.Centred;

    public float FontScale { get; init; } = 1;
}

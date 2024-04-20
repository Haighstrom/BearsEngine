namespace BearsEngine.UI;

public class ButtonTheme
{
    public static ButtonTheme Default => new();

    public ButtonTheme()
    {
    }

    public Colour DefaultColour { get; init; } = Colour.White;

    public Colour HoverColour { get; init; } = Colour.Yellow;

    public Colour PressedColour { get; init; } = Colour.Gray;

    public Colour UnclickableColour { get; init; } = Colour.DarkGray;

    public TextTheme Text { get; init; } = TextTheme.Default;
}
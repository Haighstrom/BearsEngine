namespace BearsEngine.UI;

public class ScrollbarTheme
{
    private static ButtonTheme DefaultButtonTheme
    {
        get
        {
            return new()
            {
                DefaultColour = Colour.DarkGray,
                HoverColour = Colour.Gray,
                PressedColour = Colour.Black,
                UnclickableColour = Colour.Gray,
            };
        }
    }

    public static ScrollbarTheme Default => new();

    public ScrollbarTheme()
    {
    }

    public Colour BarBackgroundColour { get; init; } = Colour.LightGray;

    public Colour ButtonBackgroundColour { get; init; } = Colour.LightGray;

    public int EdgeToBarSpace { get; init; } = 2;

    public int EdgeToArrowSpace { get; init; } = 4;

    public ButtonTheme BarButton { get; init; } = DefaultButtonTheme;

    public ButtonTheme ArrowButton { get; init; } = DefaultButtonTheme;
}
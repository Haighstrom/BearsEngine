namespace BearsEngine.UI;

public class BorderTheme
{
    public static BorderTheme Default => new();

    public BorderTheme()
    {
    }

    public Colour Colour { get; init; } = Colour.Black;

    public float Thickness { get; init; } = 2;
}
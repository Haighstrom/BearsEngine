namespace BearsEngine.UI;

public class PanelTheme
{
    public static PanelTheme Default => new();

    public PanelTheme()
    {
    }

    public Colour BackgroundColour { get; init; } = Colour.LightBlue;

    public BorderTheme Border { get; init; } = BorderTheme.Default;
}
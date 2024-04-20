namespace BearsEngine.UI;

public class TabbedPanelTheme
{
    public static TabbedPanelTheme Default => new();

    public TabbedPanelTheme()
    {
    }
    
    public PanelTheme Panel { get; init; } = PanelTheme.Default;

    public Colour ActivatedTabColour { get; init; } = Colour.PeachPuff;

    public Colour DeactivatedTabColour { get; init; } = Colour.CadetBlue;

    public TextTheme Text { get; init; } = TextTheme.Default;
}
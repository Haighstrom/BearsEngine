namespace BearsEngine.UI;

public class ScrollingListPanelTheme
{
    public static ScrollingListPanelTheme Default => new();
    
    public ScrollingListPanelTheme()
    {
    }

    public Colour PanelColour { get; init; } = Colour.White;
}
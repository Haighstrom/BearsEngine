namespace BearsEngine.UI;

public class UITheme
{
    public static UITheme Default => new();

    public UITheme()
    {
    }

    public TextTheme Text { get; set; } = TextTheme.Default;
    public ButtonTheme Button { get; set; } = ButtonTheme.Default;
    public InputBoxTheme InputBox { get; set; } = InputBoxTheme.Default;
    public LabelTheme Label { get; set; } = LabelTheme.Default;
    public PanelTheme Panel { get; set; } = PanelTheme.Default;
    public TabbedPanelTheme TabbedPanel { get; set; } = TabbedPanelTheme.Default;
    public ScrollbarTheme Scrollbar { get; set; } = ScrollbarTheme.Default;
    public ScrollingListPanelTheme ScrollingListPanel { get; set; } = ScrollingListPanelTheme.Default;
}

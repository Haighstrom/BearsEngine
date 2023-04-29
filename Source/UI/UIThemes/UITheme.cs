namespace BearsEngine.UI;

public class UITheme
{
    public static UITheme Default => new();

    public UITheme()
    {
    }

    public ButtonTheme Button { get; init; } = ButtonTheme.Default;

    public InputBoxTheme InputBox { get; init; } = InputBoxTheme.Default;

    public LabelTheme Label { get; init; } = LabelTheme.Default;

    public PanelTheme Panel { get; init; } = PanelTheme.Default;

    public TextTheme Text { get; init; } = TextTheme.Default;

    public ScrollbarTheme Scrollbar { get; init; } = ScrollbarTheme.Default;

    public ScrollingListPanelTheme ScrollingListPanel { get; init; } = ScrollingListPanelTheme.Default;

    public TabbedPanelTheme TabbedPanel { get; init; } = TabbedPanelTheme.Default;
}

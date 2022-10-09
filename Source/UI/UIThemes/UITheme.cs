namespace BearsEngine.UI;

public struct UITheme
{
    public static UITheme Default => new()
    {
        Text = TextTheme.Default,
        Button = ButtonTheme.Default,
        InputBox = InputBoxTheme.Default,
        Label = LabelTheme.Default,
        Panel = PanelTheme.Default,
        TabbedPanel = TabbedPanelTheme.Default,
        Scrollbar = ScrollbarTheme.Default,
        ScrollingListPanel = ScrollingListPanelTheme.Default,
    };
    

    public TextTheme Text;
    public ButtonTheme Button;
    public InputBoxTheme InputBox;
    public LabelTheme Label;
    public PanelTheme Panel;
    public TabbedPanelTheme TabbedPanel;
    public ScrollbarTheme Scrollbar;
    public ScrollingListPanelTheme ScrollingListPanel;
}

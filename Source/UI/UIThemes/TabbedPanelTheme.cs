namespace BearsEngine.UI
{
    public struct TabbedPanelTheme
    {
        public static TabbedPanelTheme Default => new()
        {
            Panel = PanelTheme.Default,
        };
        

        public PanelTheme Panel;
    }
}
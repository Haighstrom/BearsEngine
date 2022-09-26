namespace BearsEngine.UI
{
    public struct TabbedPanelTheme
    {
        #region Default
        public static TabbedPanelTheme Default => new()
        {
            Panel = PanelTheme.Default,
        };
        #endregion

        public PanelTheme Panel;
    }
}
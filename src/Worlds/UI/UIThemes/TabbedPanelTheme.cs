namespace BearsEngine.Worlds
{
    public struct TabbedPanelTheme
    {
        #region Default
        public static TabbedPanelTheme Default => new TabbedPanelTheme
        {
            Panel = PanelTheme.Default,
        };
        #endregion

        public PanelTheme Panel;
    }
}
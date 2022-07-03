namespace BearsEngine.Worlds.UI.UIThemes
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
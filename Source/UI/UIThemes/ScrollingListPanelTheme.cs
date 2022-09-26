namespace BearsEngine.UI
{
    public struct ScrollingListPanelTheme
    {
        #region Default
        public static ScrollingListPanelTheme Default => new()
        {
            PanelColour = Colour.White,
        };
        #endregion

        public Colour PanelColour;
    }
}
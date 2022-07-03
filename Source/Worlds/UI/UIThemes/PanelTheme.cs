namespace BearsEngine.Worlds.UI.UIThemes
{
    public struct PanelTheme
    {
        #region Default
        public static PanelTheme Default => new()
        {
            BackgroundColour = Colour.LightBlue,
            Border = BorderTheme.Default,
        };
        #endregion

        public Colour BackgroundColour;
        public BorderTheme Border;
    }
}
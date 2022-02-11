using HaighFramework;

namespace BearsEngine.Worlds
{
    public struct PanelTheme
    {
        #region Default
        public static PanelTheme Default => new PanelTheme
        {
            BackgroundColour = Colour.LightBlue,
            Border = BorderTheme.Default,
        };
        #endregion

        public Colour BackgroundColour;
        public BorderTheme Border;
    }
}
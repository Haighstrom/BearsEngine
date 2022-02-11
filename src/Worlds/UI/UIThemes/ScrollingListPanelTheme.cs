using HaighFramework;

namespace BearsEngine.Worlds
{
    public struct ScrollingListPanelTheme
    {
        #region Default
        public static ScrollingListPanelTheme Default => new ScrollingListPanelTheme
        {
            PanelColour = Colour.White,
        };
        #endregion

        public Colour PanelColour;
    }
}
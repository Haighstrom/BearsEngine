using BearsEngine.Source.Core;

namespace BearsEngine.UI
{
    public struct ScrollingListPanelTheme
    {
        public static ScrollingListPanelTheme Default => new()
        {
            PanelColour = Colour.White,
        };
        

        public Colour PanelColour;
    }
}
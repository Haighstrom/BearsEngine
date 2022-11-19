using BearsEngine.Source.Core;

namespace BearsEngine.UI
{
    public struct TabbedPanelTheme
    {
        public static TabbedPanelTheme Default => new()
        {
            Panel = PanelTheme.Default,
            ActivatedTabColour = Colour.PeachPuff,
            DeactivatedTabColour = Colour.CadetBlue,
        };
        

        public PanelTheme Panel;
        public Colour ActivatedTabColour, DeactivatedTabColour;
    }
}
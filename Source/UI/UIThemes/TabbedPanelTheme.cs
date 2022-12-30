using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI
{
    public struct TabbedPanelTheme
    {
        public static TabbedPanelTheme Default => new()
        {
            Panel = PanelTheme.Default,
            ActivatedTabColour = Colour.PeachPuff,
            DeactivatedTabColour = Colour.CadetBlue,
            TabTextHAlignment = HAlignment.Centred,
            TabTextVAlignment = VAlignment.Centred,
        };
        

        public PanelTheme Panel;
        public Colour ActivatedTabColour, DeactivatedTabColour;
        public HAlignment TabTextHAlignment;
        public VAlignment TabTextVAlignment;
    }
}
namespace BearsEngine.UI
{
    public struct PanelTheme
    {
        public static PanelTheme Default => new()
        {
            BackgroundColour = Colour.LightBlue,
            Border = BorderTheme.Default,
        };
        

        public Colour BackgroundColour;
        public BorderTheme Border;
    }
}
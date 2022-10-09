namespace BearsEngine.UI
{
    public struct LabelTheme
    {
        public static LabelTheme Default => new()
        {
            Panel = PanelTheme.Default,
            Text = TextTheme.Default,
            EdgeToTextSpace = 5,
        };
        

        public PanelTheme Panel;
        public TextTheme Text;
        public float EdgeToTextSpace;
    }
}
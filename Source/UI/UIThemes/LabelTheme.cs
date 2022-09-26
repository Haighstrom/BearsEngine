namespace BearsEngine.UI
{
    public struct LabelTheme
    {
        #region Default
        public static LabelTheme Default => new()
        {
            Panel = PanelTheme.Default,
            Text = TextTheme.Default,
            EdgeToTextSpace = 5,
        };
        #endregion

        public PanelTheme Panel;
        public TextTheme Text;
        public float EdgeToTextSpace;
    }
}
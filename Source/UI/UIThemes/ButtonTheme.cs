namespace BearsEngine.UI
{
    public struct ButtonTheme
    {
        public static ButtonTheme Default => new()
        {
            DefaultColour = Colour.White,
            HoverColour = Colour.Yellow,
            PressedColour = Colour.Gray,
            UnclickableColour = Colour.DarkGray,
            Text = TextTheme.Default,
        };
        

        public Colour DefaultColour, HoverColour, PressedColour, UnclickableColour;
        public TextTheme Text;
    }
}
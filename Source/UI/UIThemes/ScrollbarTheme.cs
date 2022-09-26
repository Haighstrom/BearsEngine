namespace BearsEngine.UI
{
    public struct ScrollbarTheme
    {
        #region Default
        public static ScrollbarTheme Default => new()
        {
            BarBackgroundColour = Colour.LightGray,
            ButtonBackgroundColour = Colour.LightGray,
            EdgeToBarSpace = 2,
            EdgeToArrowSpace = 4,
            Bar = new ButtonTheme
            {
                DefaultColour = Colour.DarkGray,
                HoverColour = Colour.Gray,
                PressedColour = Colour.Black,
                UnclickableColour = Colour.Gray,
            },
            Arrow = new ButtonTheme
            {
                DefaultColour = Colour.DarkGray,
                HoverColour = Colour.Gray,
                PressedColour = Colour.Black,
                UnclickableColour = Colour.Gray,
            },
        };
        #endregion

        public Colour BarBackgroundColour, ButtonBackgroundColour;
        public int EdgeToBarSpace, EdgeToArrowSpace;
        public ButtonTheme Bar, Arrow;
    }
}
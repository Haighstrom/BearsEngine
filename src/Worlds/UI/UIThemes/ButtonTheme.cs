using HaighFramework;

namespace BearsEngine.Worlds
{
    public struct ButtonTheme
    {
        #region Default
        public static ButtonTheme Default => new ButtonTheme
        {
            DefaultColour = Colour.White,
            HoverColour = Colour.Yellow,
            PressedColour = Colour.Gray,
            UnclickableColour = Colour.DarkGray,
            Text = TextTheme.Default,
        };
        #endregion

        public Colour DefaultColour, HoverColour, PressedColour, UnclickableColour;
        public TextTheme Text;
    }
}
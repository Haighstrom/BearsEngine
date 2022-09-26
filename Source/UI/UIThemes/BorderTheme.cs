namespace BearsEngine.UI
{
    public struct BorderTheme
    {
        #region Default
        public static BorderTheme Default => new()
        {
            Colour = Colour.Black,
            Thickness = 2,
        };
        #endregion

        public Colour Colour;
        public float Thickness;
    }
}
using HaighFramework;

namespace BearsEngine.Worlds
{
    public struct BorderTheme
    {
        #region Default
        public static BorderTheme Default => new BorderTheme
        {
            Colour = Colour.Black,
            Thickness = 2,
        };
        #endregion

        public Colour Colour;
        public float Thickness;
    }
}
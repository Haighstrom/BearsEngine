using BearsEngine.Source.Core;

namespace BearsEngine.UI
{
    public struct BorderTheme
    {
        public static BorderTheme Default => new()
        {
            Colour = Colour.Black,
            Thickness = 2,
        };
        

        public Colour Colour;
        public float Thickness;
    }
}
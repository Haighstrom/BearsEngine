namespace BearsEngine.Worlds.Graphics.Text.Components
{
    internal class LC_Word : ILineComponent
    {
        public LC_Word(string text, HFont font, Colour colour, float extraCharSpacing, float scaleX, float scaleY, bool underline, bool strikethrough)
        {
            if (text.IsNullOrEmpty())
                throw new HException("LCWord/ctr: Tried to create an empty word.");

            Text = text;
            Font = font;
            Colour = colour;
            Length = scaleX * (font.MeasureString(text).X + extraCharSpacing * (text.Length - 1));
            Height = scaleY * font.HighestChar;
            IsUnderlined = underline;
            IsStruckthrough = strikethrough;
        }

        public float Length { get; }
        public float Height { get; }
        public HFont Font { get; }
        public string Text { get; }
        public Colour Colour { get; }
        public bool IsUnderlined { get; }
        public bool IsStruckthrough { get; }
    }
}
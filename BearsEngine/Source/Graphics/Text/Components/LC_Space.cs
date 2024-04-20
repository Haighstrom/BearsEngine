namespace BearsEngine.Worlds.Graphics.Text.Components;

internal class LC_Space : ILineComponent
{
    public LC_Space(HFont font, Colour colour, float extraSpaceWidth, float scaleX, float scaleY, bool underline, bool strikethrough)
    {
        Font = font;
        Length = scaleX * (font.SpaceWidth + extraSpaceWidth);
        Height = scaleY * font.HighestChar;
        IsUnderlined = underline;
        IsStruckthrough = strikethrough;
        Colour = colour;
    }

    public HFont Font { get; }
    public float Length { get; }
    public float Height { get; }
    public bool IsUnderlined { get; }
    public bool IsStruckthrough { get; }
    public Colour Colour { get; }
}
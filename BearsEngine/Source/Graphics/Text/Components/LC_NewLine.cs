namespace BearsEngine.Worlds.Graphics.Text.Components;

internal class LC_NewLine : ILineComponent
{
    public LC_NewLine(HFont font, float scaleY)
    {
        Height = scaleY * font.HighestChar;
    }

    public float Length => 0;
    public float Height { get; }
    public bool IsUnderlined => false;
    public bool IsStruckthrough => false;
}
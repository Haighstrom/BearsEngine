namespace BearsEngine.Worlds.Graphics.Text.Components
{
    internal interface ILineComponent
    {
        float Length { get; }
        float Height { get; }
        bool IsUnderlined { get; }
        bool IsStruckthrough { get; }
    }
}
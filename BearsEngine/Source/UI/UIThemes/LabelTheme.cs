namespace BearsEngine.UI;

public class LabelTheme
{
    public static LabelTheme Default => new();
   
    public LabelTheme()
    {
    }

    public PanelTheme Panel { get; init; } = PanelTheme.Default;

    public TextTheme Text { get; init; } = TextTheme.Default;

    public float EdgeToTextSpace { get; init; } = 5;
}
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.Globals;

internal static class GV
{
    public static HFont MainFont = HFont.Load("Verdana", 10, FontStyle.Regular, true);
    public static UITheme Theme = UITheme.Default;
}
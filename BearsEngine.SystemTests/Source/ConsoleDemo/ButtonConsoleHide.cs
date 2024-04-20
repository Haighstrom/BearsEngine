using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleHide : Button
{
    public ButtonConsoleHide()
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleHide, Colour.Black, HFont.Default, Colour.White, "Hide")
    {
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        AppConsole.HideConsole();
    }
}

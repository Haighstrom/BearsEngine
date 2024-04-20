using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleShow : Button
{
    public ButtonConsoleShow()
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleShow, Colour.Black, HFont.Default, Colour.White, "Show")
    {
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        AppConsole.ShowConsole();
    }
}

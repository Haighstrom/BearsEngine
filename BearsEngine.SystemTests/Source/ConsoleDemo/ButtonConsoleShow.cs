using BearsEngine.Console;
using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleShow : Button
{
    private readonly IConsoleWindow _console;

    public ButtonConsoleShow(IConsoleWindow console)
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleShow, Colour.Black, HFont.Default, Colour.White, "Show")
    {
        _console = console;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        _console.ShowConsole();
    }
}

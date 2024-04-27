using BearsEngine.Console;
using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleHide : Button
{
    private readonly IConsoleWindow _console;

    public ButtonConsoleHide(IMouse mouse, IConsoleWindow console)
        : base(mouse, GL.UI.Button, GP.ConsoleDemo.ButtonConsoleHide, Colour.Black, HFont.Default, Colour.White, "Hide")
    {
        _console = console;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        _console.HideConsole();
    }
}

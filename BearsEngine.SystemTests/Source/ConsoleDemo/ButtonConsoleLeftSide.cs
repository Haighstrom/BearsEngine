using BearsEngine.Console;
using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleLeftSide : Button
{
    private readonly IConsoleWindow _console;

    public ButtonConsoleLeftSide(IConsoleWindow console)
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleLeftSide, Colour.Black, HFont.Default, Colour.White, "Move Left")
    {
        _console = console;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        _console.MoveConsoleTo(0, 0, ConsoleSettings.DefaultWidth, _console.MaxHeight);
    }
}

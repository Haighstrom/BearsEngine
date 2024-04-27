using BearsEngine.Console;
using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleRightSide : Button
{
    private readonly IConsoleWindow _console;

    public ButtonConsoleRightSide(IMouse mouse, IConsoleWindow console)
        : base(mouse, GL.UI.Button, GP.ConsoleDemo.ButtonConsoleRightSide, Colour.Black, HFont.Default, Colour.White, "Move Right")
    {
        _console = console;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        _console.MoveConsoleTo(_console.MaxWidth - ConsoleSettings.DefaultWidth, 0, ConsoleSettings.DefaultWidth, _console.MaxHeight);
    }
}

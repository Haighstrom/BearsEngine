using BearsEngine.Console;
using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleMaximise : Button
{
    private readonly IConsoleWindow _console;

    public ButtonConsoleMaximise(IConsoleWindow console)
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleMaximise, Colour.Black, HFont.Default, Colour.White, "Maximise")
    {
        _console = console;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        _console.MoveConsoleTo(0, 0, _console.MaxWidth, _console.MaxHeight);
    }
}

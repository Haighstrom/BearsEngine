using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleLeftSide : Button
{
    public ButtonConsoleLeftSide()
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleLeftSide, Colour.Black, HFont.Default, Colour.White, "Move Left")
    {
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        Console.MoveConsoleTo(0, 0, ConsoleSettings.DefaultWidth, Console.MaxHeight);
    }
}
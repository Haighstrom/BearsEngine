using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleRightSide : Button
{
    public ButtonConsoleRightSide()
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleRightSide, Colour.Black, HFont.Default, Colour.White, "Move Right")
    {
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        Console.MoveConsoleTo(Console.MaxWidth - Console.DefaultWidth, 0, Console.DefaultWidth, Console.MaxHeight);
    }
}
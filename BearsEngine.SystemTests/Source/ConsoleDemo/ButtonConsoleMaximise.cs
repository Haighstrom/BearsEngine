using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ButtonConsoleMaximise : Button
{
    public ButtonConsoleMaximise()
        : base(GL.UI.Button, GP.ConsoleDemo.ButtonConsoleMaximise, Colour.Black, HFont.Default, Colour.White, "Maximise")
    {
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        Console.MoveConsoleTo(0, 0, Console.MaxWidth, Console.MaxHeight);
    }
}
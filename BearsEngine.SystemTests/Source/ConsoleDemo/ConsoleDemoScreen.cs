namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ConsoleDemoScreen : Screen
{
    public ConsoleDemoScreen()
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton());
        Add(new ButtonConsoleHide());
        Add(new ButtonConsoleShow());
        Add(new ButtonConsoleLeftSide());
        Add(new ButtonConsoleRightSide());
        Add(new ButtonConsoleMaximise());
    }
}

namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ConsoleDemoScreen : Screen
{
    public ConsoleDemoScreen(IGameEngine engine, IScreenFactory screenFactory)
        :base()
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton(engine, screenFactory));
        Add(new ButtonConsoleHide(engine.Console));
        Add(new ButtonConsoleShow(engine.Console));
        Add(new ButtonConsoleLeftSide(engine.Console));
        Add(new ButtonConsoleRightSide(engine.Console));
        Add(new ButtonConsoleMaximise(engine.Console));
    }
}

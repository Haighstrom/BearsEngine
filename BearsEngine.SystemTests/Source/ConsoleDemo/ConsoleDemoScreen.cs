namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ConsoleDemoScreen : Screen
{
    public ConsoleDemoScreen(IGameEngine engine, IScreenFactory screenFactory)
        :base(engine.Mouse)
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton(engine, screenFactory));
        Add(new ButtonConsoleHide(engine.Mouse, engine.Console));
        Add(new ButtonConsoleShow(engine.Mouse, engine.Console));
        Add(new ButtonConsoleLeftSide(engine.Mouse, engine.Console));
        Add(new ButtonConsoleRightSide(engine.Mouse, engine.Console));
        Add(new ButtonConsoleMaximise(engine.Mouse, engine.Console));
    }
}

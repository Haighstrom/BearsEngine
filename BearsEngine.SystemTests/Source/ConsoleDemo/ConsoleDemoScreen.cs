namespace BearsEngine.SystemTests.Source.ConsoleDemo;

internal class ConsoleDemoScreen : Screen
{
    public ConsoleDemoScreen(IApp app, IScreenFactory screenFactory)
        :base(app.Mouse)
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton(app, screenFactory));
        Add(new ButtonConsoleHide(app.Mouse, app.Console));
        Add(new ButtonConsoleShow(app.Mouse, app.Console));
        Add(new ButtonConsoleLeftSide(app.Mouse, app.Console));
        Add(new ButtonConsoleRightSide(app.Mouse, app.Console));
        Add(new ButtonConsoleMaximise(app.Mouse, app.Console));
    }
}

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class InputDemoScreen : Screen
{
    public InputDemoScreen(IGameEngine app, IScreenFactory screenFactory)
        : base()
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton(app, screenFactory));

        Add(new MouseDisplay(app.Mouse));
        Add(new MouseStateDisplay(app.Mouse));
        Add(new MouseActivityList(app.Mouse));
        Add(new KeyboardActivityList(app.Window));
        Add(new CharActivityList(app.Window));
    }
}

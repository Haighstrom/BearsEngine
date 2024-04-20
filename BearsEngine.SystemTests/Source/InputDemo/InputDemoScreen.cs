namespace BearsEngine.SystemTests.Source.InputDemo;

public class InputDemoScreen : Screen
{
    public InputDemoScreen()
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton());

        Add(new MouseDisplay());
        Add(new MouseStateDisplay());
        Add(new MouseActivityList());
        Add(new KeyboardActivityList());
        Add(new CharActivityList());
    }
}

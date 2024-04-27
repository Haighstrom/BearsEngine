﻿namespace BearsEngine.SystemTests.Source.InputDemo;

internal class InputDemoScreen : Screen
{
    public InputDemoScreen(IApp app, IScreenFactory screenFactory)
        : base(app.Mouse)
    {
        BackgroundColour = Colour.CornflowerBlue;

        Add(new ReturnButton(app, screenFactory));

        Add(new MouseDisplay(app.Mouse));
        Add(new MouseStateDisplay(app.Mouse));
        Add(new MouseActivityList(app.Mouse));
        Add(new KeyboardActivityList(app.Window, app.Mouse));
        Add(new CharActivityList(app.Window, app.Mouse));
    }
}
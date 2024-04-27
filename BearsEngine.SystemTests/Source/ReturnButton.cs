using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source;

internal class ReturnButton : Button
{
    public ReturnButton(IApp app, IScreenFactory screenFactory)
        : base(app.Mouse, GL.UI.Button, GP.ReturnButton, Colour.LightGray, GV.Theme, "Return", () => app.ChangeScene(screenFactory.CreateMainMenuScreen()))
    {
    }
}

using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source;

internal class ReturnButton : Button
{
    public ReturnButton(IGameEngine app, IScreenFactory screenFactory)
        : base(GL.UI.Button, GP.ReturnButton, Colour.LightGray, GV.Theme, "Return", () => app.ChangeScene(screenFactory.CreateMainMenuScreen()))
    {
    }
}

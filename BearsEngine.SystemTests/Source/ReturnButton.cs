using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source;

internal class ReturnButton : Button
{
    public ReturnButton()
        : base(GL.UI.Button, GP.ReturnButton, Colour.LightGray, GV.Theme, "Return", () => Engine.Scene = new MenuScreen())
    {
    }
}
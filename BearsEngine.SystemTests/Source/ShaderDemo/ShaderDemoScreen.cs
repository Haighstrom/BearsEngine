using BearsEngine.Graphics.Shaders;
using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.ShaderDemo;

internal class ShaderDemoScreen : Screen
{
    public ShaderDemoScreen(IGameEngine app, IScreenFactory screenFactory)
        : base(app.Mouse)
    {
        Add(new Button(app.Mouse, 1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, "Return", () => app.ChangeScene(screenFactory.CreateMainMenuScreen())));

        Add(new Image(GA.GFX.Bear, new Rect(50, 100, 60, 80)));

        Add(new Image(GA.GFX.Bear, new Rect(150, 100, 60, 80))
        {
            Shader = new GreyScaleShader()
        });

        Add(new Image(GA.GFX.Bear, new Rect(250, 100, 60, 80))
        {
            Shader = new SolidColourShader()
        });

        LightingShader.AddLight(new LightInfo() { Colour = Colour.White, CutoffRadius = 50, Position = new Point(350, 100), Radius = 50 });

        Add(new Image(GA.GFX.Bear, new Rect(350, 100, 60, 80))
        {
            Shader = new LightingShader()
        });
    }
}

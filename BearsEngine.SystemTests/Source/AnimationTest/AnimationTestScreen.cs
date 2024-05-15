using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.AnimationTest;

internal class AnimationTestScreen : Screen
{
    public AnimationTestScreen(IGameEngine app, IScreenFactory screenFactory)
    {
        Add(new Button(1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, "Return", () => app.ChangeScene(screenFactory.CreateMainMenuScreen())));

        var texture = OpenGLHelper.LoadSpriteTexture("Assets/GFX/SpriteMan.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);
        var sprite = new Sprite(texture, new Rect(100, 100, 80, 100));

        Add(sprite);
    }
}

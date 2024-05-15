using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.AnimationTest;

internal class AnimationTestScreen : Screen
{
    public AnimationTestScreen(IGameEngine app, IScreenFactory screenFactory)
    {
        Add(new Button(1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, "Return", () => app.ChangeScene(screenFactory.CreateMainMenuScreen())));

        var texture = OpenGLHelper.LoadSpriteTexture("Assets/GFX/SpriteMan.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);

        var animation1 = new Animation(texture, new Rect(100, 100, 80, 120));
        animation1.Play(1, 0, 2, 0);
        Add(animation1);

        var animation2 = new Animation(texture, new Rect(200, 100, 80, 120));
        animation2.Play(4, 3, 5, 3);
        Add(animation2);

        var animation3 = new Animation(texture, new Rect(300, 100, 80, 120));
        animation3.Play(7, 6, 8, 6);
        Add(animation3);

        var animation4 = new Animation(texture, new Rect(400, 100, 80, 120));
        animation4.Play(10, 9, 11, 9);
        Add(animation4);
    }
}

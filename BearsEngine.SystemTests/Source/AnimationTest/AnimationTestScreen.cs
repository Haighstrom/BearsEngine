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

        var textureFace = OpenGLHelper.LoadSpriteTexture("Assets/GFX/AnimationTest/Face_01.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);
        var textureHandsFront = OpenGLHelper.LoadSpriteTexture("Assets/GFX/AnimationTest/HandsFront_01.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);
        var textureHandsRear = OpenGLHelper.LoadSpriteTexture("Assets/GFX/AnimationTest/HandsRear_01.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);
        var textureOnesie = OpenGLHelper.LoadSpriteTexture("Assets/GFX/AnimationTest/Onesie_01.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);
        var textureTail = OpenGLHelper.LoadSpriteTexture("Assets/GFX/AnimationTest/Tail_01.png", 4, 3, 2, OpenGL.TEXPARAMETER_VALUE.GL_NEAREST);

        var animation5 = new MultiLayerAnimation(new Rect(500, 100, 96, 96));
        animation5.AddTexture(textureFace, 45);
        animation5.AddTexture(textureHandsFront, 15);
        animation5.AddTexture(textureHandsRear, 50);
        animation5.AddTexture(textureOnesie, 40);
        animation5.AddTexture(textureTail, 30);
        animation5.Play(1, 0, 2, 0, 4, 3, 5, 3, 7, 6, 8, 6, 10, 9, 11, 9);
        Add(animation5);
    }
}

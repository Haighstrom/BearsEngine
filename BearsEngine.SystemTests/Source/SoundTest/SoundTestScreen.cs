using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.SoundTest;

internal class SoundTestScreen : Screen
{
    public SoundTestScreen()
        : base()
    {
    }

    public override void Start()
    {
        base.Start();

        var b = new Button(1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, () => Engine.Scene = new MenuScreen());
        b.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(60, 40), "Return") { HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
        Add(b);

        Add(new PlaySFXButton(new Rect(200, 200, 50, 50), GA.SFX.Powerup8));
    }
}

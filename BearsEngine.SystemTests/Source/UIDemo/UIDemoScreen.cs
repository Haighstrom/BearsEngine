using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Cameras;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.UIDemo;

internal class UIDemoScreen : Screen
{
    private readonly IScreenFactory _screenFactory;
    private readonly ICamera _camera;

    public UIDemoScreen(IApp app, IScreenFactory screenFactory)
        : base(app.Mouse)
    {
        _screenFactory = screenFactory;

        var b = new Button(app.Mouse, 1, new Rect(730, 550, 60, 40), Colour.White, GV.Theme, () => app.ChangeScene(_screenFactory.CreateMainMenuScreen()));
        b.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(60, 40), "Return") { HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
        Add(b);

        _camera = new Camera(app.Mouse, 1, new Rect(25, 25, 300, 300), 50, 50)
        {
            BackgroundColour = Colour.LemonChiffon
        };
        //_camera.MaxX = 10;
        //_camera.MaxY = 25;
        Add(_camera);
        Entity e1 = new(app.Mouse, 1, new Rect(0, 0, 1, 1), Colour.Red);
        e1.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(0, 0, 1, 1), "TNR 12") { Layer = 100, ScaleX = 1f / 50, ScaleY = 1f / 50, HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
        _camera.Add(e1);
        _camera.Add(new Entity(app.Mouse, 1, new Rect(1, 1, 2, 2), Colour.Brown));
        _camera.Add(new Entity(app.Mouse, 1, new Rect(3, 3, 3, 3), Colour.LightBlue));
        //_camera.Add(new Entity(1, new Rect(6, 6, 2, 2), Colour.Blue));
        //_camera.Add(new Entity(1, new Rect(1, 8, 1, 1), Colour.Green));

        //Add(new Scrollbar(1, new Rect(25, 350, 300, 20), ScrollbarDirection.Horizontal, Colour.LightBlue, Colour.White, Colour.LightGray, Colour.DarkGray, 4, 6, 5, _camera));
        //Add(new Scrollbar(1, new Rect(350, 25, 20, 300), ScrollbarDirection.Vertical, Colour.LightBlue, Colour.White, Colour.LightGray, Colour.DarkGray, 4, 6, 5, _camera));

        //var iS = new IntervalScrollbar(1, new Rect(25, 400, 300, 20), ScrollbarDirection.Horizontal, Colour.LightBlue, Colour.White, Colour.LightGray, Colour.DarkGray, 4, 6, 5, 1);
        //iS.CurrentIntervalChanged += (sender, args) => args.Value.Log();
        //Add(iS);

        //Add(new TextInputBox(theme, Colour.Beige, 1, new Rect(500, 50, 100, 30)));
    }
}

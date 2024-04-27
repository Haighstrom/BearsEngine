using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Cameras;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.TestSquare;

internal class TestSquareScreen : Screen
{
    private readonly IApp _app;
    private readonly IScreenFactory _screenFactory;
    private Camera _camera;
    private Image _image;
    private Button _button;

    public TestSquareScreen(IApp app, IScreenFactory screenFactory)
        : base(app.Mouse)
    {
        _app = app;
        _screenFactory = screenFactory;

        BackgroundColour = Colour.CornflowerBlue;

        app.Window.Resized += OnWindowResized;

        _button = new Button(app.Mouse, 1, new Rect(5, 5, 60, 40), Colour.White, GV.Theme, () => app.ChangeScene(_screenFactory.CreateMainMenuScreen()));
        _button.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(60, 40), "Return") { HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
        Add(_button);

        Add(_camera = new(app.Mouse, 100, new Rect(0, 0, 800, 600), 1, 1));
        _camera.Add(_image = new(GA.GFX.TestSquare, GP.DefaultClientSize));
    }

    private void OnWindowResized(object? sender, EventArgs e)
    {
        _camera.Resize(_app.Window.ClientSize);
        _image.W = _app.Window.ClientSize.X;
        _image.H = _app.Window.ClientSize.Y;
    }
}

using BearsEngine.Input;
using BearsEngine.Source.Tools;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Window;
using BearsEngine.Worlds.Cameras;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.BearSpinner;

internal class BearSpinnerScreen : Screen
{
    private readonly IWindow _window;
    private readonly IMouse _mouse;
    private readonly IScreenFactory _screenFactory;

    private readonly TextGraphic _bearCount;
    private readonly Camera _camera;

    public BearSpinnerScreen(IApp app, IWindow window, IMouse mouse, IScreenFactory screenFactory)
        : base(mouse)
    {
        _window = window;
        _mouse = mouse;
        _screenFactory = screenFactory;

        BackgroundColour = Colour.Black;

        window.Resized += OnWindowResize;

        _camera = new Camera(mouse, 1, new Rect(0, 0, window.ClientSize.X, window.ClientSize.Y), 1, 1);
        Add(_camera);
        Repeat.CallMethod(() => _camera.Add(new Bear(mouse, Randomisation.Rand((int)window.ClientSize.X), Randomisation.Rand((int)window.ClientSize.Y))), window.WindowWidth * window.WindowHeight / 3000);

        var b = new Button(mouse, 1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, () => app.ChangeScene(_screenFactory.CreateMainMenuScreen()));
        b.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(60, 40), "Return") { HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
        Add(b);

        _bearCount = new TextGraphic(GV.MainFont, Colour.Black, new Rect(80, 10, 150, 40), $"Bear Count:{_camera.Entities.Count}")
        {
            Layer = 0,
            HAlignment = HAlignment.Centred,
            VAlignment = VAlignment.Centred
        };

        Add(_bearCount);
    }

    private void OnWindowResize(object? sender, EventArgs e)
    {
        _camera.Resize(_window.ClientSize);
        _camera.RemoveAll();
        Repeat.CallMethod(() => _camera.Add(new Bear(_mouse, Randomisation.Rand((int)_window.ClientSize.X), Randomisation.Rand((int)_window.ClientSize.Y))), _window.WindowWidth * _window.WindowHeight / 3000);
        _bearCount.Text = $"Bear Count:{_camera.Entities.Count}";
    }

}

using BearsEngine.Source.Tools;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Cameras;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.BearSpinner;

public class BearSpinnerScreen : Screen
{
    TextGraphic _bearCount;
    Camera _camera;
    public BearSpinnerScreen()
        : base()
    {
    }

    public override void Start()
    {
        base.Start();

        BackgroundColour = Colour.Black;

        Window.Resized += OnWindowResize;

        _camera = new Camera(1, new Rect(0, 0, Window.ClientSize.X, Window.ClientSize.Y), 1, 1);
        Add(_camera);
        Repeat.CallMethod(() => _camera.Add(new Bear(Randomisation.Rand((int)Window.ClientSize.X), Randomisation.Rand((int)Window.ClientSize.Y))), Window.WindowWidth * Window.WindowHeight / 3000);

        var b = new Button(1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, () => Engine.Scene = new MenuScreen());
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
        _camera.Resize(Window.ClientSize);
        _camera.RemoveAll();
        Repeat.CallMethod(() => _camera.Add(new Bear(Randomisation.Rand((int)Window.ClientSize.X), Randomisation.Rand((int)Window.ClientSize.Y))), Window.WindowWidth * Window.WindowHeight / 3000);
        _bearCount.Text = $"Bear Count:{_camera.Entities.Count}";
    }

}

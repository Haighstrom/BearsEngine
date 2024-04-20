using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Cameras;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.TestSquare
{
    public class TestSquareScreen : Screen
    {
        private Camera _camera;
        private Image _image;
        private Button _button;
        public override void Start()
        {
            base.Start();

            BackgroundColour = Colour.CornflowerBlue;

            AppWindow.Resized += OnWindowResized;

            _button = new Button(1, new Rect(5, 5, 60, 40), Colour.White, GV.Theme, () => Engine.Scene = new MenuScreen());
            _button.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(60, 40), "Return") { HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
            Add(_button);

            Add(_camera = new(100, new Rect(0, 0, 800, 600), 1, 1));
            _camera.Add(_image = new(GA.GFX.TestSquare, GP.DefaultClientSize));
        }

        private void OnWindowResized(object? sender, EventArgs e)
        {
            _camera.Resize(AppWindow.ClientSize);
            _image.W = AppWindow.ClientSize.X;
            _image.H = AppWindow.ClientSize.Y;
        }
    }
}

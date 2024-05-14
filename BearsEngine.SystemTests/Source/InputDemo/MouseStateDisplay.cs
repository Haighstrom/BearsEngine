using BearsEngine.Input;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class MouseStateDisplay : Entity
{
    private readonly IMouse _mouse;
    private readonly TextGraphic _stateText;

    public MouseStateDisplay(IMouse mouse)
        : base(10, 90, 230, 220, 65, Colour.White)
    {
        _mouse = mouse;

        Add(_stateText = new TextGraphic(HFont.Load("Helvetica", 8), Colour.Black, Size));
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _stateText.Text = _mouse.ToString()!;
    }
}

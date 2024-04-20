using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class MouseStateDisplay : Entity
{
    private TextGraphic _stateText;

    public MouseStateDisplay()
        : base(10, 90, 230, 220, 65, Colour.White)
    {
        Add(_stateText = new TextGraphic(HFont.Load("Helvetica", 8), Colour.Black, Size));
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _stateText.Text = Mouse.CurrentState.ToString();
    }
}
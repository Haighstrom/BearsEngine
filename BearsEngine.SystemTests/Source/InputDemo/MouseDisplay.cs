using BearsEngine.Controllers;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Input;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class MouseDisplay : Entity
{
    private Image _leftPressed, _rightPressed, _mouseMoved, _wheelPressed, _wheelScrolled;

    public MouseDisplay()
        : base(10, 100, 20, 200, 200, GA.GFX.InputDemo.MouseDisplay)
    {
        Add(_leftPressed = new Image(GA.GFX.InputDemo.MouseDisplay_LeftPressed, 200, 200) { Visible = false });
        Add(_rightPressed = new Image(GA.GFX.InputDemo.MouseDisplay_RightPressed, 200, 200) { Visible = false });
        Add(_mouseMoved = new Image(GA.GFX.InputDemo.MouseDisplay_MouseMoved, 200, 200) { Visible = false });
        Add(_wheelPressed = new Image(GA.GFX.InputDemo.MouseDisplay_WheelPressed, 200, 200) { Visible = false });
        Add(_wheelScrolled = new Image(GA.GFX.InputDemo.MouseDisplay_WheelScrolled, 200, 200) { Visible = false });
        Add(new Alarm(0.5f, true, WriteScreenCursorInfo));
    }

    private void WriteScreenCursorInfo()
    {
        Log.Debug("MouseScreenP: " + Mouse.ScreenP.ToString());
        Log.Debug("MouseWindowP: " + Mouse.ClientP.ToString());
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _leftPressed.Visible = Mouse.LeftDown;

        _rightPressed.Visible = Mouse.RightDown;

        _mouseMoved.Visible = Mouse.XDelta != 0 || Mouse.YDelta != 0;

        _wheelPressed.Visible = Mouse.Down(MouseButton.Middle);

        _wheelScrolled.Visible = Mouse.WheelDelta != 0;

    }
}

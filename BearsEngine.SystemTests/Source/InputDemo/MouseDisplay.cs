using BearsEngine.Controllers;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Input;
using BearsEngine.Source.Core;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class MouseDisplay : Entity
{
    private readonly IMouse _mouse;
    private Image _leftPressed, _rightPressed, _mouseMoved, _wheelPressed, _wheelScrolled;

    public MouseDisplay(IMouse mouse)
        : base(mouse, 10, 100, 20, 200, 200, GA.GFX.InputDemo.MouseDisplay)
    {
        _mouse = mouse;

        Add(_leftPressed = new Image(GA.GFX.InputDemo.MouseDisplay_LeftPressed, 200, 200) { Visible = false });
        Add(_rightPressed = new Image(GA.GFX.InputDemo.MouseDisplay_RightPressed, 200, 200) { Visible = false });
        Add(_mouseMoved = new Image(GA.GFX.InputDemo.MouseDisplay_MouseMoved, 200, 200) { Visible = false });
        Add(_wheelPressed = new Image(GA.GFX.InputDemo.MouseDisplay_WheelPressed, 200, 200) { Visible = false });
        Add(_wheelScrolled = new Image(GA.GFX.InputDemo.MouseDisplay_WheelScrolled, 200, 200) { Visible = false });
        Add(new Alarm(0.5f, true, WriteScreenCursorInfo));
    }

    private void WriteScreenCursorInfo()
    {
        Log.Debug("MouseScreenP: " + _mouse.ScreenPosition.ToString());
        Log.Debug("MouseWindowP: " + _mouse.ClientPosition.ToString());
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _leftPressed.Visible = _mouse.LeftDown;

        _rightPressed.Visible = _mouse.RightDown;

        _mouseMoved.Visible = _mouse.XDelta != 0 || _mouse.YDelta != 0;

        _wheelPressed.Visible = _mouse.Down(MouseButton.Middle);

        _wheelScrolled.Visible = _mouse.WheelDelta != 0;
    }
}

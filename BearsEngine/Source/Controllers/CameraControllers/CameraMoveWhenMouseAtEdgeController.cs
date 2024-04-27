using BearsEngine.Input;
using BearsEngine.Source.Core;
using BearsEngine.Window;
using BearsEngine.Worlds.Cameras;

namespace BearsEngine.Source.Controllers.CameraControllers;


public class CameraMoveWhenMouseAtEdgeController : UpdateableBase
{
    private readonly IWindow _window;
    private readonly IMouse _mouse;
    private readonly ICamera _parent;
    private readonly float _cameraMoveSpeed;
    private readonly int _windowEdgeDistance;

    public CameraMoveWhenMouseAtEdgeController(IWindow window, IMouse mouse, ICamera parent, float cameraMoveSpeed, int windowEdgeDistance)
    {
        _window = window;
        _mouse = mouse;
        _parent = parent;
        _cameraMoveSpeed = cameraMoveSpeed;
        _windowEdgeDistance = windowEdgeDistance;
    }

    public override void Update(float elapsed)
    {
        if (_mouse.ClientY < _windowEdgeDistance)
            _parent.View.Y = Maths.Max(_parent.MinY, _parent.View.Y - _cameraMoveSpeed * (float)elapsed);
        if (_mouse.ClientX > _window.ClientWidth - _windowEdgeDistance)
            _parent.View.X = Maths.Min(_parent.MaxX - _parent.View.W, _parent.View.X + _cameraMoveSpeed * (float)elapsed);
        if (_mouse.ClientY > _window.ClientHeight - _windowEdgeDistance)
            _parent.View.Y = Maths.Min(_parent.MaxY - _parent.View.H, _parent.View.Y + _cameraMoveSpeed * (float)elapsed);
        if (_mouse.ClientX < _windowEdgeDistance)
            _parent.View.X = Maths.Max(_parent.MinX, _parent.View.X - _cameraMoveSpeed * (float)elapsed);
    }
}

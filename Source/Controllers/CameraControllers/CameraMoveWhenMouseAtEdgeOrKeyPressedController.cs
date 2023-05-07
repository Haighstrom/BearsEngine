using BearsEngine.Worlds.Cameras;
using HaighFramework.Input;

namespace BearsEngine.Source.Controllers.CameraControllers;


public class CameraMoveWhenMouseAtEdgeOrKeyPressedController : UpdateableBase
{
    private readonly ICamera _parent;
    private readonly float _cameraMoveSpeed;
    private readonly int _windowEdgeDistance;
    private readonly IList<Key> _upKeys, _downKeys, _leftKeys, _rightKeys;

    public CameraMoveWhenMouseAtEdgeOrKeyPressedController(ICamera parent, float cameraMoveSpeed, int windowEdgeDistance, IList<Key> upKeys, IList<Key> downKeys, IList<Key> leftKeys, IList<Key> rightKeys)
    {
        _parent = parent;
        _cameraMoveSpeed = cameraMoveSpeed;
        _windowEdgeDistance = windowEdgeDistance;
        _upKeys = upKeys;
        _downKeys = downKeys;
        _leftKeys = leftKeys;
        _rightKeys = rightKeys;
    }

    public override void Update(float elapsed)
    {
        if (Mouse.ClientY < _windowEdgeDistance || Keyboard.AnyKeyDown(_upKeys))
            _parent.View.Y = Maths.Max(_parent.MinY, _parent.View.Y - _cameraMoveSpeed * (float)elapsed);
        if (Mouse.ClientX > Window.ClientWidth - _windowEdgeDistance || Keyboard.AnyKeyDown(_rightKeys))
            _parent.View.X = Maths.Min(_parent.MaxX - _parent.View.W, _parent.View.X + _cameraMoveSpeed * (float)elapsed);
        if (Mouse.ClientY > Window.ClientHeight - _windowEdgeDistance || Keyboard.AnyKeyDown(_downKeys))
            _parent.View.Y = Maths.Min(_parent.MaxY - _parent.View.H, _parent.View.Y + _cameraMoveSpeed * (float)elapsed);
        if (Mouse.ClientX < _windowEdgeDistance || Keyboard.AnyKeyDown(_leftKeys))
            _parent.View.X = Maths.Max(_parent.MinX, _parent.View.X - _cameraMoveSpeed * (float)elapsed);
    }
}

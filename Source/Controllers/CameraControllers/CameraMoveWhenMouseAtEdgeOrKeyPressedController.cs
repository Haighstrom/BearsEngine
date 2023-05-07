using BearsEngine.Worlds.Cameras;
using HaighFramework.Input;

namespace BearsEngine.Source.Controllers.CameraControllers;


public class CameraMoveWhenMouseAtEdgeOrKeyPressedController : UpdateableBase
{
    private readonly ICamera _camera;
    private readonly float _cameraMoveSpeed;
    private readonly int _windowEdgeDistance;
    private readonly IList<Key> _upKeys, _downKeys, _leftKeys, _rightKeys;

    public CameraMoveWhenMouseAtEdgeOrKeyPressedController(ICamera parent, float cameraMoveSpeed, int windowEdgeDistance, IList<Key> upKeys, IList<Key> downKeys, IList<Key> leftKeys, IList<Key> rightKeys)
    {
        _camera = parent;
        _cameraMoveSpeed = cameraMoveSpeed;
        _windowEdgeDistance = windowEdgeDistance;
        _upKeys = upKeys;
        _downKeys = downKeys;
        _leftKeys = leftKeys;
        _rightKeys = rightKeys;
    }

    public override void Update(float elapsed)
    {
        //X
        if (_camera.View.W >= _camera.MaxX)
        {
            _camera.View.X = -(_camera.View.W - _camera.MaxX) / 2;
        }
        else
        {
            if (Mouse.ClientX > Window.ClientWidth - _windowEdgeDistance || Keyboard.AnyKeyDown(_rightKeys))
            {
                _camera.View.X = Maths.Min(_camera.MaxX - _camera.View.W, _camera.View.X + _cameraMoveSpeed * (float)elapsed);
            }
            if (Mouse.ClientX < _windowEdgeDistance || Keyboard.AnyKeyDown(_leftKeys))
            {
                _camera.View.X = Maths.Max(_camera.MinX, _camera.View.X - _cameraMoveSpeed * (float)elapsed);
            }
        }

        //Y
        if (_camera.View.H >= _camera.MaxY)
        {
            _camera.View.Y = -(_camera.View.H - _camera.MaxY) / 2;
        }
        else
        {
            if (Mouse.ClientY < _windowEdgeDistance || Keyboard.AnyKeyDown(_upKeys))
                _camera.View.Y = Maths.Max(_camera.MinY, _camera.View.Y - _cameraMoveSpeed * (float)elapsed);
            if (Mouse.ClientY > Window.ClientHeight - _windowEdgeDistance || Keyboard.AnyKeyDown(_downKeys))
                _camera.View.Y = Maths.Min(_camera.MaxY - _camera.View.H, _camera.View.Y + _cameraMoveSpeed * (float)elapsed);
        }
    }
}
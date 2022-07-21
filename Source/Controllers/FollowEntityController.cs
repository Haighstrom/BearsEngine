using BearsEngine.Worlds.Cameras;

namespace BearsEngine.Worlds.Controllers;

[Flags]
public enum CameraFollowMode
{
    Default = 0,
    StopAtEdges = 1 << 0,
    CentreIfWindowBiggerThanMap = 1 << 1,
}

public class CameraFollowController : AddableBase, IUpdatable
{
    #region Fields
    private readonly ICamera _camera;
    private readonly IRect _target;
    private readonly CameraFollowMode _mode;
    private readonly float _cameraMinX, _cameraMaxX, _cameraMinY, _cameraMaxY;
    #endregion

    #region Constructors
    public CameraFollowController(ICamera camera, IRect target, CameraFollowMode mode, float cameraMinX, float cameraMaxX, float cameraMinY, float cameraMaxY)
    {
        _camera = camera;
        _cameraMinX = cameraMinX;
        _cameraMaxX = cameraMaxX;
        _cameraMinY = cameraMinY;
        _cameraMaxY = cameraMaxY;
        _target = target;
        _mode = mode;

        SetCameraPosition();
    }
    #endregion

    private void SetCameraPosition()
    {
        _camera.View.X = _target.Centre.X - _camera.View.W / 2;
        _camera.View.Y = _target.Centre.Y - _camera.View.H / 2;

        if ((_mode & CameraFollowMode.CentreIfWindowBiggerThanMap) > 0)
        {
            if (_camera.View.W >= _cameraMaxX)
                _camera.View.X = -(_camera.View.W - _cameraMaxX) / 2;
            if (_camera.View.H >= _cameraMaxY)
                _camera.View.Y = -(_camera.View.H - _cameraMaxY) / 2;
        }

        if ((_mode & CameraFollowMode.StopAtEdges) > 0)
        {
            if (_camera.View.W < _cameraMaxX - _cameraMinX || _cameraMaxX == 0)
            {
                if (_camera.View.X < _cameraMinX)
                    _camera.View.X = _cameraMinX;
                if (_cameraMaxX > 0 && _camera.View.Right > _cameraMaxX)
                    _camera.View.X = _cameraMaxX - _camera.View.W;
            }
            if (_camera.View.H < _cameraMaxY - _cameraMinY || _cameraMaxY == 0)
            {
                if (_camera.View.Y < _cameraMinY)
                    _camera.View.Y = _cameraMinY;
                if (_cameraMaxY > 0 && _camera.View.Bottom > _cameraMaxY)
                    _camera.View.Y = _cameraMaxY - _camera.View.H;
            }
        }

        _camera.View.X += CameraAdjustX;
        _camera.View.Y += CameraAdjustY;
    }

    #region IUpdateable
    public bool Active { get; set; } = true;

    #region Update
    public virtual void Update(double elapsed)
    {
        SetCameraPosition();
    }
    #endregion
    #endregion

    #region Properties
    public float CameraAdjustX { get; set; }
    public float CameraAdjustY { get; set; }
    #endregion
}

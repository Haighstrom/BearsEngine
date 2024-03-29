﻿using BearsEngine.Worlds.Cameras;

namespace BearsEngine.Controllers;

public class CameraFollowController : AddableBase, IUpdatable
{
    private readonly ICamera _camera;
    private readonly IRectangular _target;
    private readonly CameraFollowMode _mode;
    private readonly float _cameraMinX, _cameraMaxX, _cameraMinY, _cameraMaxY;

    public CameraFollowController(ICamera camera, IRectangular target, CameraFollowMode mode, float cameraMinX, float cameraMaxX, float cameraMinY, float cameraMaxY)
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

    public bool Active { get; set; } = true;

    public float CameraAdjustX { get; set; }

    public float CameraAdjustY { get; set; }

    private void SetCameraPosition()
    {
        _camera.View.X = _target.R.Centre.X - _camera.View.W / 2;
        _camera.View.Y = _target.R.Centre.Y - _camera.View.H / 2;

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

    public virtual void Update(float elapsed)
    {
        SetCameraPosition();
    }
}

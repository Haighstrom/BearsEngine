namespace BearsEngine.Worlds.Controllers
{
    [Flags]
    public enum CameraFollowMode
    {
        Default = 0,
        StopAtEdges = 1 << 0,
        CentreIfWindowBiggerThanMap = 1 << 1,
    }

    public class CameraFollowController : AddableBase//, IUpdatable
    {
        //#region Fields
        //private ICamera _camera;
        //private Rect _target;
        //private CameraFollowMode _mode;
        //#endregion

        //#region Constructors
        //public CameraFollowController(ICamera camera, Rect target, CameraFollowMode mode)
        //{
        //    _camera = camera;
        //    _target = target;
        //    _mode = mode;
        //}
        //#endregion

        //#region IUpdateable
        //public bool Active { get; set; } = true;

        //#region Update
        //public virtual void Update(double elapsed)
        //{
        //    _camera.View.x = _target.Centre.x - _camera.View.w / 2;
        //    _camera.View.y = _target.Centre.y - _camera.View.h / 2;

        //    if ((_mode & CameraFollowMode.CentreIfWindowBiggerThanMap) > 0)
        //    {
        //        if (_camera.View.w >= _camera.MaxX)
        //            _camera.View.x = -(_camera.View.w - _camera.MaxX) / 2;
        //        if (_camera.View.h >= _camera.MaxY)
        //            _camera.View.y = -(_camera.View.h - _camera.MaxY) / 2;
        //    }

        //    if ((_mode & CameraFollowMode.StopAtEdges) > 0)
        //    {
        //        if (_camera.View.w < _camera.MaxX - _camera.MinX || _camera.MaxX == 0)
        //        {
        //            if (_camera.View.x < _camera.MinX)
        //                _camera.View.x = _camera.MinX;
        //            if (_camera.MaxX > 0 && _camera.View.Right > _camera.MaxX)
        //                _camera.View.x = _camera.MaxX - _camera.View.w;
        //        }
        //        if (_camera.View.h < _camera.MaxY - _camera.MinY || _camera.MaxY == 0)
        //        {
        //            if (_camera.View.y < _camera.MinY)
        //                _camera.View.y = _camera.MinY;
        //            if (_camera.MaxY > 0 && _camera.View.Bottom > _camera.MaxY)
        //                _camera.View.y = _camera.MaxY - _camera.View.h;
        //        }
        //    }

        //    _camera.View.x += CameraAdjustX;
        //    _camera.View.y += CameraAdjustY;
        //}
        //#endregion
        //#endregion

        //#region Properties
        //public float CameraAdjustX { get; set; }

        //public float CameraAdjustY { get; set; }
        //#endregion
    }
}
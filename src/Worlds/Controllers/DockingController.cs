using HaighFramework;

namespace BearsEngine.Worlds.Controllers
{
    public class DockingController : AddableBase, IUpdatable
    {
        #region Fields
        private IRect<float> _target;
        private Func<IRect<float>> _dockTo;
        #endregion
        
        #region Constructors
        public DockingController(IRect<float> target, Func<IRect<float>> dockTo, DockPosition dockPosition, Point shift)
            : this(target, dockTo, dockPosition, (int)shift.X, (int)shift.Y)
        {
        }

        public DockingController(IRect<float> target, Func<IRect<float>> dockTo, DockPosition dockPosition, int xShift = 0, int yShift = 0)
        {
            _target = target;
            _dockTo = dockTo;
            DockPosition = dockPosition;
            XShift = xShift;
            YShift = yShift;

            UpdatePosition();
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        public virtual void Update(double elapsed) => UpdatePosition();
        #endregion

        #region Properties
        public DockPosition DockPosition { get; set; }

        public float XShift { get; set; }

        public float YShift { get; set; }
        #endregion

        #region Methods
        #region UpdatePosition
        private void UpdatePosition()
        {
            switch (DockPosition)
            {
                case DockPosition.TopLeft:
                    _target.X = _dockTo().Left + XShift;
                    _target.Y = _dockTo().Top + YShift;
                    break;
                case DockPosition.TopMiddle:
                    _target.X = (int)(_dockTo().Centre.X - _target.W / 2) + XShift;
                    _target.Y = _dockTo().Top + YShift;
                    break;
                case DockPosition.TopRight:
                    _target.X = _dockTo().Right - _target.W - XShift;
                    _target.Y = _dockTo().Top + YShift;
                    break;
                case DockPosition.MiddleLeft:
                    _target.X = _dockTo().Left + XShift;
                    _target.Y = (int)(_dockTo().Centre.Y - _target.H / 2) + YShift;
                    break;
                case DockPosition.Centre:
                    _target.X = (int)(_dockTo().Centre.X - _target.W / 2) + XShift;
                    _target.Y = (int)(_dockTo().Centre.Y - _target.H / 2) + YShift;
                    break;
                case DockPosition.MiddleRight:
                    _target.X = _dockTo().Right - _target.W - XShift;
                    _target.Y = (int)(_dockTo().Centre.Y - _target.H / 2) + YShift;
                    break;
                case DockPosition.BottomLeft:
                    _target.X = _dockTo().Left + XShift;
                    _target.Y = _dockTo().Bottom - _target.H - YShift;
                    break;
                case DockPosition.BottomMiddle:
                    _target.X = (int)(_dockTo().Centre.X - _target.W / 2) + XShift;
                    _target.Y = _dockTo().Bottom - _target.H - YShift;
                    break;
                case DockPosition.BottomRight:
                    _target.X = _dockTo().Right - _target.W - XShift;
                    _target.Y = _dockTo().Bottom - _target.H - YShift;
                    break;
            }
        }
        #endregion
        #endregion
    }
}
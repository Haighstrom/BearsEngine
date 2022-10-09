namespace BearsEngine.Worlds.Controllers
{
    public class DockingController : AddableBase, IUpdatable
    {
        private Rect _target;
        private readonly Func<Rect> _dockTo;
        

        public DockingController(Rect target, Func<Rect> dockTo, QuadrantPosition dockPosition, Point shift)
            : this(target, dockTo, dockPosition, (int)shift.X, (int)shift.Y)
        {
        }

        public DockingController(Rect target, Func<Rect> dockTo, QuadrantPosition dockPosition, int xShift = 0, int yShift = 0)
        {
            _target = target;
            _dockTo = dockTo;
            DockPosition = dockPosition;
            XShift = xShift;
            YShift = yShift;

            Update(0);
        }
        

        public bool Active { get; set; } = true;
        public QuadrantPosition DockPosition { get; set; }
        public float XShift { get; set; }
        public float YShift { get; set; }
        

        public virtual void Update(float elapsedTime)
        { 
            switch (DockPosition)
            {
                case QuadrantPosition.TopLeft:
                    _target.X = _dockTo().Left + XShift;
                    _target.Y = _dockTo().Top + YShift;
                    break;
                case QuadrantPosition.TopMiddle:
                    _target.X = (int)(_dockTo().Centre.X - _target.W / 2) + XShift;
                    _target.Y = _dockTo().Top + YShift;
                    break;
                case QuadrantPosition.TopRight:
                    _target.X = _dockTo().Right - _target.W - XShift;
                    _target.Y = _dockTo().Top + YShift;
                    break;
                case QuadrantPosition.MiddleLeft:
                    _target.X = _dockTo().Left + XShift;
                    _target.Y = (int)(_dockTo().Centre.Y - _target.H / 2) + YShift;
                    break;
                case QuadrantPosition.Centre:
                    _target.X = (int)(_dockTo().Centre.X - _target.W / 2) + XShift;
                    _target.Y = (int)(_dockTo().Centre.Y - _target.H / 2) + YShift;
                    break;
                case QuadrantPosition.MiddleRight:
                    _target.X = _dockTo().Right - _target.W - XShift;
                    _target.Y = (int)(_dockTo().Centre.Y - _target.H / 2) + YShift;
                    break;
                case QuadrantPosition.BottomLeft:
                    _target.X = _dockTo().Left + XShift;
                    _target.Y = _dockTo().Bottom - _target.H - YShift;
                    break;
                case QuadrantPosition.BottomMiddle:
                    _target.X = (int)(_dockTo().Centre.X - _target.W / 2) + XShift;
                    _target.Y = _dockTo().Bottom - _target.H - YShift;
                    break;
                case QuadrantPosition.BottomRight:
                    _target.X = _dockTo().Right - _target.W - XShift;
                    _target.Y = _dockTo().Bottom - _target.H - YShift;
                    break;
            }
        }
        
        
    }
}
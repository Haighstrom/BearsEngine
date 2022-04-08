namespace BearsEngine.Worlds.Controllers
{
    public class WaypointChangedArgs : EventArgs
    {
    }

    public class WaypointController : AddableBase, IUpdatable
    {
        #region Fields
        private IMoveable _target;
        #endregion

        #region Constructors
        public WaypointController(IMoveable target)
            : this(target, null)
        {
        }
        public WaypointController(IMoveable target, List<IPosition> waypoints)
        {
            _target = target;
            Waypoints = waypoints;
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            if (ReachedDestination)
                return;

            float amountToMove = (float)elapsed * _target.Speed;

            while (amountToMove > 0 && !Waypoints.IsEmpty())
            {
                if (Waypoints[0].X == _target.X && Waypoints[0].Y == _target.Y)
                {
                    Waypoints.RemoveAt(0);
                    ReachedWaypoint?.Invoke(this, new WaypointChangedArgs());
                }
                else
                {
                    Point p = new Point(Waypoints[0].X - _target.X, Waypoints[0].Y - _target.Y);
                    float distanceFromTarget = p.Length;

                    if (distanceFromTarget > amountToMove)
                    {
                        p = p.Normal;
                        _target.X += p.X * amountToMove;
                        _target.Y += p.Y * amountToMove;
                        amountToMove = 0;
                    }
                    else
                    {
                        _target.X = Waypoints[0].X;
                        _target.Y = Waypoints[0].Y;
                        amountToMove = distanceFromTarget - amountToMove;
                    }
                }
            }
        }
        #endregion

        public List<IPosition> Waypoints { get; set; }


        public event EventHandler<WaypointChangedArgs> ReachedWaypoint;
        #endregion

        public IPosition CurrentPosition => _target.P;
        public IPosition NextWaypoint => Waypoints.IsEmpty() ? default : Waypoints[0];
        public bool ReachedDestination => Waypoints.IsEmpty();
    }
}
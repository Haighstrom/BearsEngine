namespace BearsEngine.Worlds.Controllers
{
    public class WaypointController : AddableBase, IUpdatable
    {
        #region Fields
        private readonly IMoveable _target;
        private Direction? _lastDirection = null;
        private Direction? _direction = null;
        #endregion

        #region Constructors
        public WaypointController(IMoveable target, List<IPosition> waypoints)
            :this(target)
        {
            Waypoints = waypoints;
        }

        public WaypointController(IMoveable target)
        {
            _target = target;
            Waypoints = new List<IPosition>();
        }
        #endregion

        #region Properties
        public bool Active { get; set; } = true;
        public List<IPosition> Waypoints { get; private set; }
        public IPosition CurrentPosition => _target.P;
        public IPosition NextWaypoint => !Waypoints.IsEmpty() ? Waypoints[0] : throw new Exception($"Requested WaypointController.NextWaypoint when Waypoints of {_target} is empty.");
        public bool ReachedDestination => Waypoints.IsEmpty();
        #endregion

        #region Update
        public virtual void Update(double elapsed)
        {
            if (!ReachedDestination)
            {
                float amountToMove = (float)elapsed * _target.Speed;

                while (amountToMove > 0 && !Waypoints.IsEmpty())
                {
                    if (Waypoints[0].X == _target.X && Waypoints[0].Y == _target.Y)
                    {
                        Waypoints.RemoveAt(0);
                        Arrived?.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        Point p = new(Waypoints[0].X - _target.X, Waypoints[0].Y - _target.Y);
                        _direction = p.ToDirection();
                        
                        float distanceToNextWaypoint = p.Length;

                        if (distanceToNextWaypoint > amountToMove)
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
                            amountToMove -= distanceToNextWaypoint;
                        }
                    }
                }
            }
            else
                _direction = null;

            if (_direction != _lastDirection)
                DirectionChanged?.Invoke(this, new ValueEventArgs<Direction?>(_direction));

            _lastDirection = _direction;
        }
        #endregion

        #region ClearWaypoints
        public void ClearWaypoints()
        {
            if (Waypoints == null)
                throw new Exception($"Tried to clear waypoints for WaypointController of {_target}, but its waypoints were not set.");

            Waypoints.Clear();
        }
        #endregion

        #region SetWaypoints
        public void SetWaypoints(params IPosition[] positions) => Waypoints = positions.ToList();
        public void SetWaypoints(IEnumerable<IPosition> positions) => Waypoints = positions.ToList();
        #endregion

        #region AddWaypoints
        public void AddWaypoints(params IPosition[] positions) => AddWaypoints(positions);
        public void AddWaypoints(IEnumerable<IPosition> positions) => Waypoints.AddRange(positions);
        #endregion

        public event EventHandler<EventArgs>? Arrived;
        public event EventHandler<ValueEventArgs<Direction?>>? DirectionChanged;
    }
}
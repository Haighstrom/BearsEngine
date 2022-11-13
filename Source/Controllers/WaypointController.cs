namespace BearsEngine.Worlds.Controllers
{
    public class WaypointController : AddableBase, IUpdatable
    {
        private readonly IMoveable _target;
        private Direction _lastDirection;
        private Direction _direction;

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
        

        public bool Active { get; set; } = true;
        public List<IPosition> Waypoints { get; private set; }
        public IPosition CurrentPosition => _target.P;
        public IPosition NextWaypoint => !Waypoints.IsEmpty() ? Waypoints[0] : throw new Exception($"Requested WaypointController.NextWaypoint when Waypoints of {_target} is empty.");
        public bool ReachedDestination => Waypoints.IsEmpty();
        

        public virtual void Update(float elapsed)
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
                _direction = Direction.None;

            if (_direction != _lastDirection && _direction != Direction.None) //todo: are we happy with the second condition? without it the unsuspecting consumer ends up making their guy look left, because None = -1 = 3 = left.
                DirectionChanged?.Invoke(this, new DirectionChangedEventArgs(_lastDirection, _direction));

            _lastDirection = _direction;
        }
        

        public void ClearWaypoints()
        {
            if (Waypoints == null)
                throw new Exception($"Tried to clear waypoints for WaypointController of {_target}, but its waypoints were not set.");

            Waypoints.Clear();
        }
        

        public void SetWaypoints(params IPosition[] positions) => Waypoints = positions.ToList();
        public void SetWaypoints(IEnumerable<IPosition> positions) => Waypoints = positions.ToList();
        

        public void AddWaypoints(params IPosition[] positions) => AddWaypoints(positions);
        public void AddWaypoints(IEnumerable<IPosition> positions) => Waypoints.AddRange(positions);
        

        public event EventHandler<EventArgs>? Arrived;
        public event EventHandler<DirectionChangedEventArgs>? DirectionChanged;
    }
}
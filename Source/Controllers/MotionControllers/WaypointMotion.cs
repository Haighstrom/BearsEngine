using BearsEngine.Source.Controllers.MotionControllers;

namespace BearsEngine.Controllers;

public class WaypointMotion : UpdateableBase, IWaypointMotion
{
    private Direction _lastDirection;
    private Direction _direction;
    private readonly float _speed;

    public WaypointMotion(IPosition initialPosition, IList<IPosition> waypoints, float speed)
    {
        CurrentPosition = new Point(initialPosition.X, initialPosition.Y);
        Waypoints = waypoints;
        _speed = speed;
    }

    public WaypointMotion(IPosition initialPosition, float speed)
        : this(initialPosition, new List<IPosition>(), speed)
    {
    }

    public Point CurrentPosition { get; set; }

    public IList<IPosition> Waypoints { get; private set; }

    public bool ReachedDestination
    {
        get
        {
            return Waypoints.IsEmpty();
        }
    }

    public event EventHandler<EventArgs>? Arrived;
    public event EventHandler<DirectionChangedEventArgs>? DirectionChanged;
    public event EventHandler<EventArgs>? ReachedWaypoint;

    public void SetWaypoints(params IPosition[] positions)
    {
        Waypoints = positions.ToList();
    }

    public void SetWaypoints(IEnumerable<IPosition> positions)
    {
        Waypoints = positions.ToList();
    }

    public void AddWaypoints(params IPosition[] positions)
    {
        Waypoints.Add(positions);
    }

    public void AddWaypoints(IEnumerable<IPosition> positions)
    {
        Waypoints.Add(positions);
    }

    public IPosition GetNextWaypoint()
    {
        Ensure.CollectionNotNullOrEmpty(Waypoints);
        return Waypoints[0];
    }

    public void ClearWaypoints()
    {
        Ensure.NotNull(Waypoints);

        Waypoints.Clear();
    }

    public override void Update(float elapsed)
    {
        if (!ReachedDestination)
        {
            float amountToMove = (float)elapsed * _speed;

            while (amountToMove > 0 && !ReachedDestination)
            {
                if (Waypoints[0].X == CurrentPosition.X && Waypoints[0].Y == CurrentPosition.Y)
                {
                    Waypoints.RemoveAt(0);
                    ReachedWaypoint?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Point p = new(Waypoints[0].X - CurrentPosition.X, Waypoints[0].Y - CurrentPosition.Y);
                    _direction = p.ToDirection();

                    float distanceToNextWaypoint = p.Length;

                    if (distanceToNextWaypoint > amountToMove)
                    {
                        p = p.Normal;
                        CurrentPosition = new Point(CurrentPosition.X + p.X * amountToMove, CurrentPosition.Y + p.Y * amountToMove);
                        amountToMove = 0;
                    }
                    else
                    {
                        CurrentPosition = new Point(Waypoints[0].X, Waypoints[0].Y);
                        amountToMove -= distanceToNextWaypoint;
                    }
                }
            }

            if (ReachedDestination)
            {
                Arrived?.Invoke(this, new EventArgs());
            }
        }

        if (_direction != _lastDirection)
        {
            DirectionChanged?.Invoke(this, new DirectionChangedEventArgs(_lastDirection, _direction));
        }

        _lastDirection = _direction;
    }
}
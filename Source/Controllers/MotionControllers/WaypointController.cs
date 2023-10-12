﻿using BearsEngine.Source.Controllers.MotionControllers;

namespace BearsEngine.Controllers;

public class WaypointController : UpdateableBase, IWaypointController
{
    private readonly IMoveable _target;
    private Direction _lastDirection;
    private Direction _direction;
    private bool _movingLastFrame;

    public WaypointController(IMoveable target, List<IPosition> waypoints)
        : this(target)
    {
        Waypoints = waypoints;
    }

    public WaypointController(IMoveable target)
    {
        _target = target;
        Waypoints = new List<IPosition>();
    }

    public IList<IPosition> Waypoints { get; private set; }

    public IPosition CurrentPosition
    {
        get
        {
            return _target.P;
        }
    }

    public bool ReachedDestination
    {
        get
        {
            return Waypoints.IsEmpty();
        }
    }

    public event EventHandler<EventArgs>? StartedMoving;
    public event EventHandler<EventArgs>? ReachedWaypoint;
    public event EventHandler<DirectionChangedEventArgs>? DirectionChanged;
    public event EventHandler<EventArgs>? Arrived;

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

    public override void Update(float elapsed)
    {
        if (!ReachedDestination)
        {
            if (!_movingLastFrame)
            {
                StartedMoving?.Invoke(this, EventArgs.Empty);
                _movingLastFrame = true;
            }

            float amountToMove = (float)elapsed * _target.Speed;

            while (amountToMove > 0 && !Waypoints.IsEmpty())
            {
                if (Waypoints[0].X == _target.X && Waypoints[0].Y == _target.Y)
                {
                    Waypoints.RemoveAt(0);
                    ReachedWaypoint?.Invoke(this, EventArgs.Empty);
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

            if (_direction != _lastDirection)
            {
                DirectionChanged?.Invoke(this, new DirectionChangedEventArgs(_lastDirection, _direction));
            }

            _lastDirection = _direction;

            if (ReachedDestination)
            {
                Arrived?.Invoke(this, new EventArgs());
            }
        }
        else
        {
            _movingLastFrame = false;
        }
    }
}
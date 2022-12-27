namespace BearsEngine.Source.Controllers.MotionControllers;

/// <summary>
/// A controller which will attach an object to the side of another one, and move with it
/// </summary>
public class AttachToSideController : UpdateableBase
{
    private readonly Rect _target;
    private readonly Direction _direction;
    private readonly Rect _hangFrom;

    /// <summary>
    /// Create an AttachToSideController
    /// </summary>
    /// <param name="target">The target to be controlled</param>
    /// <param name="directionToAttachFrom">The direction the target should attach from.</param>
    /// <param name="hangFrom">The object the target should attach to.</param>
    public AttachToSideController(Rect target, Direction directionToAttachFrom, Rect hangFrom)
    {
        _target = target;
        _direction = directionToAttachFrom;
        _hangFrom = hangFrom;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        switch (_direction)
        {
            case Direction.Up:
                _target.Y = _hangFrom.Top - _target.H;
                break;
            case Direction.Right:
                _target.X = _hangFrom.Right;
                break;
            case Direction.Down:
                _target.Y = _hangFrom.Bottom;
                break;
            case Direction.Left:
                _target.X = _hangFrom.Left - _target.W;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Update this object
    /// </summary>
    /// <param name="elapsed">How much time should pass for the object</param>
    public override void Update(float elapsed)
    {
        UpdatePosition();
    }
}

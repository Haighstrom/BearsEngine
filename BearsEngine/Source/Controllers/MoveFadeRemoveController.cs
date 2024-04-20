namespace BearsEngine.Source.Controllers;

public class MoveFadeRemoveController : UpdateableBase
{
    private readonly IRectAddable _target;
    private readonly float _speed;
    private readonly Direction _direction;
    private float _duration;

    public MoveFadeRemoveController(IRectAddable target, float speed, Direction direction, float duration)
    {
        _target = target;
        _speed = speed;
        _direction = direction;
        _duration = duration;
    }

    public override void Update(float elapsed)
    {
        switch (_direction)
        {
            case Direction.Up:
                _target.Y -= _speed * elapsed;
                break;
            case Direction.Right:
                _target.X += _speed * elapsed;
                break;
            case Direction.Down:
                _target.Y += _speed * elapsed;
                break;
            case Direction.Left:
                _target.X -= _speed * elapsed;
                break;
            default:
                throw new NotImplementedException();
        }

        _duration -= elapsed;

        if (_duration < 0)
        {
            _target.Remove();
            if (_target is IDisposable disposable)
                disposable.Dispose();
            Remove();
            return;
        }
    }
}

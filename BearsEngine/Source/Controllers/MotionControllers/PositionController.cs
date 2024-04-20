namespace BearsEngine.Controllers;

public delegate float GetPosition(float time);

public class PositionController : AddableBase, IUpdatable
{
    private Rect _target;
    private readonly float _initialX, _initialY;
    private readonly GetPosition _getX, _getY;
    private float _totalElapsed;


    public PositionController(Rect target, bool positionAbsolute, GetPosition getX, GetPosition getY)
    {
        _target = target;
        if (!positionAbsolute)
        {
            _initialX = target.X;
            _initialY = target.Y;
        }
        _getX = getX;
        _getY = getY;
    }


    public bool Active { get; set; } = true;

    public virtual void Update(float elapsed)
    {
        _totalElapsed += elapsed;
        _target.X = _initialX + _getX(_totalElapsed);
        _target.Y = _initialY + _getY(_totalElapsed);
    }


}
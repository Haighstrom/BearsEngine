namespace BearsEngine;

internal class ClickController : AddableBase, IUpdatable
{
    internal enum ClickState { None, Hovering, LeftPressedDown }

    private readonly IClickable _target;
    private ClickState _state = ClickState.None;

    public ClickController(IClickable target)
    {
        _target = target;
    }

    public bool Active { get; set; } = true;

    private void HandleClickStateNone()
    {
        if (_target.MouseIntersecting)
        {
            _state = ClickState.Hovering;
            _target.OnMouseEntered();
        }
    }

    private void HandleClickStateHovering()
    {
        if (!_target.MouseIntersecting)
        {
            _state = ClickState.None;
            _target.OnMouseExited();
        }
        else if (HI.MouseLeftPressed)
        {
            _state = ClickState.LeftPressedDown;
            _target.OnLeftPressed();
        }
    }

    private void HandleClickStateLeftPressedDown()
    {
        if (HI.MouseLeftReleased)
        {
            _state = ClickState.None;
            _target.OnLeftReleased();
            _target.OnLeftClicked();
        }
    }

    public void Update(float elapsedTime)
    {
        switch (_state)
        {
            case ClickState.None:
                HandleClickStateNone();
                break;
            case ClickState.Hovering:
                HandleClickStateHovering();
                break;
            case ClickState.LeftPressedDown:
                HandleClickStateLeftPressedDown();
                break;
            default:
                throw new InvalidOperationException($"Enum value ({_state}) not handled.");
        }
    }
}

//if (_target.Clickable && _target.Visible)
//{
//    bool _mouseOver = _target.MouseIntersecting;

//    if (_mouseOver && !_mouseWasOverLastFrame)
//        MouseEntered?.Invoke(this, EventArgs.Empty);
//    else if (!_mouseOver && _mouseWasOverLastFrame)
//        MouseExited?.Invoke(this, EventArgs.Empty);

//    _mouseWasOverLastFrame = _mouseOver;
//}

//if (_target.Clickable && _target.Visible && _target.MouseIntersecting)
//{
//    bool leftOrRight = false;

//    if (HI.MouseLeftDoubleClicked)
//    {
//        _target.OnLeftDoubleClicked();
//        leftOrRight = true;
//    }
//    else if (HI.MouseLeftPressed)
//    {
//        _target.OnLeftPressed();
//        _waitingForLeftUp = true;
//        leftOrRight = true;
//    }
//    else if (HI.MouseLeftDown)
//    {
//        _target.OnLeftDown();
//        leftOrRight = true;
//    }
//    else if (HI.MouseLeftReleased)
//    {
//        _target.OnLeftReleased();
//        if (_waitingForLeftUp)
//            _target.OnLeftClicked();
//        _waitingForLeftUp = false;
//        leftOrRight = true;
//    }

//    if (HI.MouseRightPressed)
//    {
//        _target.OnRightPressed();
//        _waitingForRightUp = true;
//        leftOrRight = true;
//    }
//    else if (HI.MouseRightDown)
//    {
//        _target.OnRightDown();
//        leftOrRight = true;
//    }
//    else if (HI.MouseRightReleased)
//    {
//        _target.OnRightReleased();
//        if (_waitingForRightUp)
//            _target.OnRightClicked();
//        _waitingForRightUp = false;
//        leftOrRight = true;
//    }

//    if (!leftOrRight)
//        _target.OnHover();
//}
//else
//{
//    _target.OnNoMouseEvent();
//    _waitingForLeftUp = false;
//    _waitingForRightUp = false;
//}

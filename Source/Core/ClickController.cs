namespace BearsEngine;

internal class ClickController : AddableBase, IUpdatable
{
    private enum ClickState
    {
        None,
        Hovering,
        PushedAndHovered,
        PushedNotHovered
    }

    private static readonly List<IClickable> _leftPressedRequests = new();
    private static readonly List<IClickable> _leftReleasedRequests = new();
    private static readonly List<IClickable> _leftClickedRequests = new();
    private static readonly List<IClickable> _mouseEnteredRequests = new();
    private static readonly List<IClickable> _mouseHoveredRequests = new();

    public static void RequestOnLeftPressed(IClickable c)
    {
        _leftPressedRequests.Add(c);
    }

    public static void RequestOnLeftReleased(IClickable c)
    {
        _leftReleasedRequests.Add(c);
    }

    public static void RequestOnLeftClicked(IClickable c)
    {
        _leftClickedRequests.Add(c);
    }

    public static void RequestOnMouseEntered(IClickable c)
    {
        _mouseEnteredRequests.Add(c);
    }

    public static void RequestOnMouseHovered(IClickable c)
    {
        _mouseHoveredRequests.Add(c);
    }

    public static void DetermineMouseEventOutcomes()
    {
        if (_leftPressedRequests.Any())
        {
            _leftPressedRequests.First().OnLeftPressed();
            _leftPressedRequests.Clear();
        }

        if (_leftReleasedRequests.Any())
        {
            _leftReleasedRequests.First().OnLeftReleased();
            _leftReleasedRequests.Clear();
        }

        if (_leftClickedRequests.Any())
        {
            _leftClickedRequests.First().OnLeftClicked();
            _leftClickedRequests.Clear();
        }

        if (_mouseEnteredRequests.Any())
        {
            _mouseEnteredRequests.First().OnMouseEntered();
            _mouseEnteredRequests.Clear();
        }

        if (_mouseHoveredRequests.Any())
        {
            _mouseHoveredRequests.First().OnMouseHovered();
            _mouseHoveredRequests.Clear();
        }
    }

    private readonly IClickable _target;
    private ClickState _state = ClickState.None;
    private float _timeToTriggerOnHovered = 0;

    public ClickController(IClickable target)
    {
        _target = target;
    }

    public bool Active { get; set; } = true;

    private void HandleClickStateHovering(float elapsed)
    {
        if (!_target.MouseIntersecting)
        {
            _state = ClickState.None;
            _target.OnMouseExited();
            _target.OnNoMouseEvent();
        }
        else if (HI.MouseLeftReleased)
        {
            RequestOnLeftReleased(_target);
        }
        else if (HI.MouseLeftPressed)
        {
            _state = ClickState.PushedAndHovered;
            RequestOnLeftPressed(_target);
        }
        else if (_timeToTriggerOnHovered > 0)
        {
            _timeToTriggerOnHovered -= elapsed;

            if (_timeToTriggerOnHovered <= 0)
            {
                RequestOnMouseHovered(_target);
            }
        }
    }

    private void HandleClickStateNone()
    {
        if (_target.MouseIntersecting)
        {
            _state = ClickState.Hovering;
            _timeToTriggerOnHovered = _target.TimeToHover;
            RequestOnMouseEntered(_target);
        }
    }

    private void HandleClickStatePushedAndHovered(float elapsed)
    {
        if (HI.MouseLeftReleased)
        {
            _state = ClickState.None;
            RequestOnLeftReleased(_target);
            RequestOnLeftClicked(_target);
        }
        else if (!_target.MouseIntersecting)
        {
            _state = ClickState.PushedNotHovered;
        }
        else if (_timeToTriggerOnHovered > 0)
        {
            _timeToTriggerOnHovered -= elapsed;
            if (_timeToTriggerOnHovered <= 0)
            {
                RequestOnMouseHovered(_target);
            }
        }
    }

    private void HandleClickStatePushedNotHovered()
    {
        if (HI.MouseLeftReleased)
        {
            _state = ClickState.None;
            _target.OnNoMouseEvent();
        }
        else if (_target.MouseIntersecting)
        {
            _state = ClickState.PushedAndHovered;
            _timeToTriggerOnHovered = _target.TimeToHover;
        }
    }

    public void Update(float elapsed)
    {
        switch (_state)
        {
            case ClickState.None:
                HandleClickStateNone();
                break;
            case ClickState.Hovering:
                HandleClickStateHovering(elapsed);
                break;
            case ClickState.PushedAndHovered:
                HandleClickStatePushedAndHovered(elapsed);
                break;
            case ClickState.PushedNotHovered:
                HandleClickStatePushedNotHovered();
                break;
            default:
                throw new InvalidOperationException($"State of ({_state}) was not handled.");
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
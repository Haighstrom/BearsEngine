﻿using BearsEngine.Input;

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
            var target = _leftPressedRequests.Last();

            if (target.Clickable)
                target.OnLeftPressed();
            _leftPressedRequests.Clear();
        }

        if (_leftReleasedRequests.Any())
        {
            var target = _leftReleasedRequests.Last();

            if (target.Clickable)
                target.OnLeftReleased();
            _leftReleasedRequests.Clear();
        }

        if (_leftClickedRequests.Any())
        {
            var target = _leftClickedRequests.Last();

            if (target.Clickable)
                target.OnLeftClicked();
            _leftClickedRequests.Clear();
        }

        if (_mouseEnteredRequests.Any())
        {
            var target = _mouseEnteredRequests.Last();

            if (target.Clickable)
                target.OnMouseEntered();
            _mouseEnteredRequests.Clear();
        }

        if (_mouseHoveredRequests.Any())
        {
            var target = _mouseHoveredRequests.Last();

            if (target.Clickable)
                target.OnMouseHovered();
            _mouseHoveredRequests.Clear();
        }
    }

    private readonly IMouse _mouse;
    private readonly IClickable _target;
    private ClickState _state = ClickState.None;
    private float _timeToTriggerOnHovered = 0;

    public ClickController(IMouse mouse, IClickable target)
    {
        _mouse = mouse;
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
        else if (_mouse.LeftReleased)
        {
            RequestOnLeftReleased(_target);
        }
        else if (_mouse.LeftPressed)
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
        if (_mouse.LeftReleased)
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
        if (_mouse.LeftReleased)
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
        //what about if an object becomes non-visible after mouse has been pressed?
        if (!_target.Visible)
            return;

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

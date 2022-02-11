namespace BearsEngine.Worlds.Controllers
{
    public class ClickController : AddableBase, IUpdatable
    {
        #region Fields
        private IClickable _target;
        private bool _mouseWasOverLastFrame;
        private bool _waitingForLeftUp;
        private bool _waitingForRightUp;
        #endregion

        #region Constructors
        public ClickController(IClickable target)
        {
            _target = target;
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            if (_target.Clickable)
            {
                bool _mouseOver = _target.MouseIntersecting;

                if (_mouseOver && !_mouseWasOverLastFrame)
                    _target.OnMouseEnter();
                else if (!_mouseOver && _mouseWasOverLastFrame)
                    _target.OnMouseExit();

                _mouseWasOverLastFrame = _mouseOver;
            }

            if (_target.Clickable && _target.MouseIntersecting)
            {
                bool leftOrRight = false;

                if (HI.MouseLeftDoubleClicked)
                {
                    _target.OnLeftDoubleClicked();
                    leftOrRight = true;
                }
                else if (HI.MouseLeftPressed)
                {
                    _target.OnLeftPressed();
                    _waitingForLeftUp = true;
                    leftOrRight = true;
                }
                else if (HI.MouseLeftDown)
                {
                    _target.OnLeftDown();
                    leftOrRight = true;
                }
                else if (HI.MouseLeftReleased)
                {
                    _target.OnLeftReleased();
                    if (_waitingForLeftUp)
                        _target.OnLeftClicked();
                    _waitingForLeftUp = false;
                    leftOrRight = true;
                }

                if (HI.MouseRightPressed)
                {
                    _target.OnRightPressed();
                    _waitingForRightUp = true;
                    leftOrRight = true;
                }
                else if (HI.MouseRightDown)
                {
                    _target.OnRightDown();
                    leftOrRight = true;
                }
                else if (HI.MouseRightReleased)
                {
                    _target.OnRightReleased();
                    if (_waitingForRightUp)
                        _target.OnRightClicked();
                    _waitingForRightUp = false;
                    leftOrRight = true;
                }

                if (!leftOrRight)
                    _target.OnHover();
            }
            else
            {
                _target.OnNoMouseEvent();
                _waitingForLeftUp = false;
                _waitingForRightUp = false;
            }
        }
        #endregion
        #endregion
    }
}

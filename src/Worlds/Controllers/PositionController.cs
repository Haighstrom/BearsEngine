namespace BearsEngine.Worlds.Controllers
{
    public delegate float GetPosition(float time);

    public class PositionController : AddableBase, IUpdatable
    {
        #region Fields
        private Rect _target;
        private float _initialX, _initialY;
        private GetPosition _getX, _getY;
        private float _totalElapsed;
        #endregion

        #region Constructors
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
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            _totalElapsed += (float)elapsed;
            _target.X = _initialX + _getX(_totalElapsed);
            _target.Y = _initialY + _getY(_totalElapsed);
        }
        #endregion
        #endregion
    }
}
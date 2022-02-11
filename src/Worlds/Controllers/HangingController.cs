using HaighFramework;

namespace BearsEngine.Worlds.Controllers
{
    public class HangingController : AddableBase, IUpdatable
    {
        #region Fields
        private IRect<float> _target;
        private IRect<float> _hangFrom;
        #endregion

        #region Constructors
        public HangingController(IRect<float> target, IRect<float> hangFrom)
        {
            _target = target;
            _hangFrom = hangFrom;

            UpdatePosition();
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        public virtual void Update(double elapsed) => UpdatePosition();
        #endregion

        #region Methods
        #region UpdatePosition
        private void UpdatePosition()
        {
            _target.Y = _hangFrom.Bottom;
        }
        #endregion
        #endregion
    }
}
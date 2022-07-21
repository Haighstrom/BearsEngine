namespace BearsEngine.Worlds.Controllers
{
    public class FollowMouseController : AddableBase, IUpdatable
    {
        #region Fields
        private readonly IRectAddable _target;
        #endregion

        #region Constructors
        public FollowMouseController(IRectAddable target, int xShift, int yShift)
            : this(target, new Point(xShift, yShift))
        {
        }

        public FollowMouseController(IRectAddable target, Point shift)
        {
            _target = target;
            Shift = shift;
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            if (_target.Parent is null)
                throw new NullReferenceException($"The Target ({_target}) of Follow Mouse Controller ({this}) is not added to anything, so its mouse position cannot be resolved.");

            _target.P = _target.Parent.LocalMousePosition + Shift;
        }
        #endregion
        #endregion

        #region Properties
        private Point Shift { get; set; }
        #endregion
    }
}
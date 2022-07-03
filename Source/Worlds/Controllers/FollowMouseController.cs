namespace BearsEngine.Worlds.Controllers
{
    public class FollowMouseController : AddableBase, IUpdatable
    {
        #region Fields
        private readonly IEntity _target;
        #endregion

        #region Constructors
        public FollowMouseController(IEntity target, int xShift, int yShift)
            : this(target, new Point(xShift, yShift))
        {
        }

        public FollowMouseController(IEntity target, Point shift)
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
            _target.P = _target.Parent.LocalMousePosition + Shift;
        }
        #endregion
        #endregion

        #region Properties
        private Point Shift { get; set; }
        #endregion
    }
}
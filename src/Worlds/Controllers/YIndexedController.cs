namespace BearsEngine.Worlds.Controllers
{
    public class YIndexedLayerController : AddableBase, IUpdatable
    {
        #region Fields
        float _baseY;
        private Entity _target;
        #endregion

        #region Constructors
        public YIndexedLayerController(Entity target, float baseY)
        {
            _target = target;
            _baseY = baseY;

            _target.PositionChanged += OnTargetPositionChanged;
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        public void Update(double elapsed)
        {
        }
        #endregion

        private void OnTargetPositionChanged(object sender, PositionChangedArgs e) => _target.Layer = (int)(_baseY - e.NewR.Y);
    }
}

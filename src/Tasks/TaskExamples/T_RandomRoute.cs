using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks.TaskExamples
{
    public class T_RandomRoute<N> : Task
        where N : INode
    {
        private int _maxSteps;
        private IWaypointableAndPathable<N> _entity;

        public T_RandomRoute(IWaypointableAndPathable<N> entity, int maxSteps)
        {
            _entity = entity;
            _maxSteps = maxSteps;

            CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
        }

        protected override void Start()
        {
            base.Start();
            var route = HF.Pathfinding.ChooseRandomRoute(_entity.CurrentNode, _entity.CanPathThrough, _maxSteps);
            _entity.WaypointController.Waypoints = route.Select(n => (IPosition)new Point(n.X, n.Y)).ToList();
        }
    }
}
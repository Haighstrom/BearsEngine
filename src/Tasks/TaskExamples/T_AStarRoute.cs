using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;
using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks
{
    public class T_AStarRoute<N> : Task
        where N : INode
    {
        private IWaypointableAndPathable<N> _entity;
        private N _destination;

        public T_AStarRoute(IWaypointableAndPathable<N> entity, N destination)
        {
            _entity = entity;
            _destination = destination;

            CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
        }

        protected override void Start()
        {
            base.Start();
            var route = HF.Pathfinding.GetAStarRoute<N>(_entity.CurrentNode, _destination, _entity.CanPathThrough);
            _entity.WaypointController.Waypoints = route.Select(n => (IPosition)new Point(n.X, n.Y)).ToList();
        }
    }
}
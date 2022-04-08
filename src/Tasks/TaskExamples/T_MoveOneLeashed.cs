using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks.TaskExamples
{
    public class T_MoveOneLeashed<N> : Task
        where N : INode
    {
        private N _leashNode;
        private float _leashDistance;
        private IWaypointableAndPathable<N> _entity;

        public T_MoveOneLeashed(IWaypointableAndPathable<N> entity, N leashNode, float leashDistance)
        {
            _entity = entity;
            _leashNode = leashNode;
            _leashDistance = leashDistance;

            CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
        }

        protected override void Start()
        {
            base.Start();
            List<N> possibleNodes = new();
            foreach (var possibleNode in _entity.CurrentNode.ConnectedNodes)
            {
                if (Math.Max(Math.Abs(possibleNode.X - _leashNode.X), Math.Abs(possibleNode.Y - _leashNode.Y)) <= _leashDistance)
                    possibleNodes.Add((N)possibleNode);
            }
            _entity.WaypointController.Waypoints = new List<IPosition> { HF.Randomisation.Choose(possibleNodes) };
        }
    }
}
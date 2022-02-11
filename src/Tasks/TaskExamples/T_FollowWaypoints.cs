using HaighFramework;
using BearsEngine.Worlds;

namespace BearsEngine.Tasks
{
    public class T_FlyTo : Task
    {
        private IWaypointable _entity;
        private IPosition _destination;

        public T_FlyTo(IWaypointable entity, IPosition destination)
        {
            _entity = entity;
            _destination = destination;

            CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
        }

        protected override void Start()
        {
            base.Start();
            _entity.WaypointController.Waypoints = new List<IPosition>() { _destination };
        }
    }
}
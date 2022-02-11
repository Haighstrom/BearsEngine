using BearsEngine.Worlds;
using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks
{
    public class TG_Idle<N> : TaskGroup
        where N : INode
    {
        public TG_Idle(IWaypointableAndPathable<N> entity, int maxSteps, float waitTime)
            : base(new T_RandomRoute<N>(entity, maxSteps), new T_Wait(waitTime))
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaighFramework;
using BearsEngine.Worlds;

namespace BearsEngine.Tasks
{
    public class TG_Idle : TaskGroup
    {
        public TG_Idle(IWaypointableAndPathable entity, int maxSteps, float waitTime)
            : base(new T_RandomRoute(entity, maxSteps), new T_Wait(waitTime))
        {
        }
    }
}
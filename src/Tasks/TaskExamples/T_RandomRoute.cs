using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;
using BearsEngine.Tasks;
using BearsEngine.Pathfinding;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Tasks
{
    public class T_RandomRoute : Task
    {
        private int _maxSteps;
        private IWaypointableAndPathable _entity; 

        public T_RandomRoute(IWaypointableAndPathable entity, int maxSteps)
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
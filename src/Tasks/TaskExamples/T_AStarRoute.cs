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
    public class T_AStarRoute : Task
    {
        private IWaypointableAndPathable _entity;
        private INode _destination;

        public T_AStarRoute(IWaypointableAndPathable entity, INode destination)
        {
            _entity = entity;
            _destination = destination;

            CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
        }

        protected override void Start()
        {
            base.Start();
            var route = HF.Pathfinding.GetAStarRoute(_entity.CurrentNode, _destination, _entity.CanPathThrough);
            _entity.WaypointController.Waypoints = route.Select(n => (IPosition)new Point(n.X, n.Y)).ToList();
        }
    }
}
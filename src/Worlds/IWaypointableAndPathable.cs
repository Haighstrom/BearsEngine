using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Pathfinding;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Worlds
{
    public interface IWaypointable : IMoveable
    {
        WaypointController WaypointController { get; }
    }
    public interface IWaypointableAndPathable : IWaypointable, IPathable
    {
    }
}
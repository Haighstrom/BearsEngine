using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Pathfinding;

namespace BearsEngine.Worlds
{
    public interface IMoveable : IRect<float>
    {
        float Speed { get; }
    }
}
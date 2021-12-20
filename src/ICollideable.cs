using System;
using System.Collections.Generic;
using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine
{
    public interface ICollideable : IAddable, IRect<float>
    {
        IRect<float> WindowPosition { get; }
        bool Collideable { get; set; }

        bool Collides(Point p);
        bool Collides(IRect<float> r);
        bool Collides(ICollideable i);
    }
}

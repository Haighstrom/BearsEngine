using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine
{
    public interface IUpdatable
    {
        bool Active { get; set; }
        void Update(double elapsedTime);
    }
}
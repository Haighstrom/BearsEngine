using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine.Tasks
{
    public interface IInterruptableTask : ITask
    {
        void Interrupt();
    }
}
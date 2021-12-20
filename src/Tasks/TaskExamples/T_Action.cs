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
    public class T_Action : Task
    {
        public T_Action(Action action)
        {
            ActionsOnComplete.Add(action);
        }
    }
}
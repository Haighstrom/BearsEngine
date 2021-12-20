using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine.Pathfinding
{
    public interface IPathable
    {
        INode CurrentNode { get; }
        bool CanPathThrough(INode node);
    }
}
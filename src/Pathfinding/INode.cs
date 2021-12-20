using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine.Pathfinding
{
    public interface INode
    {
        int X { get; }
        int Y { get; }

        List<INode> ConnectedNodes { get; }

        INode ParentNode { get; set; }
        double PF { get; set; }
        double PG { get; set; }
        double PH { get; set; }
    }

    /// <summary>
    /// The simplest possible implementation of INode
    /// </summary>
    public class Node : INode
    {
        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public List<INode> ConnectedNodes { get; set; } = new List<INode>();
        public INode ParentNode { get; set; }
        public double PF { get; set; }
        public double PG { get; set; }
        public double PH { get; set; }
    }

    public static class NodeExts
    {
        public static bool Equals<P>(this INode n1, INode n2) => n1.X == n2.X && n1.Y == n2.Y;
    }
}
using System.Xml.Linq;

namespace BearsEngine.Pathfinding;

public interface IPathfinder<TNode> where TNode : IPathfindNode<TNode>
{
    IList<TNode>? FindPath(TNode start, TNode end, Func<TNode, TNode, bool> passableTest);

    IList<TNode>? FindRandomPath(TNode start, Func<TNode, TNode, bool> passableTest, int steps, bool canBacktrack);
}
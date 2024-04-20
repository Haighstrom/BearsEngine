namespace BearsEngine.Pathfinding;

/// <summary>
/// A utility for building paths from node maps
/// </summary>
/// <typeparam name="TNode">The type of Node to be pathed on</typeparam>
public interface IPathfinder<TNode> where TNode : IPathfindNode<TNode>
{
    /// <summary>
    /// A function to determine whether a path can pass from one node to the next
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public delegate bool PassTestDelegate(TNode from, TNode to);

    /// <summary>
    /// Find a path from a start node to an end node
    /// </summary>
    /// <param name="start">The node to path from</param>
    /// <param name="end">The node to path to</param>
    /// <param name="passableTest">Whether a path can pass from one node to the next</param>
    /// <returns>Returns a path if one is found, null if no path could be found</returns>
    IList<TNode>? FindPath(TNode start, TNode end, Func<TNode, TNode, bool> passableTest);

    /// <summary>
    /// Finds a random path from a node
    /// </summary>
    /// <param name="start">The node to start from</param>
    /// <param name="passableTest">Whether a path can pass from one node to the next</param>
    /// <param name="targetSteps">The maximum number of steps in the path</param>
    /// <param name="canBacktrack">Whether the path can double back on itself. False will return a path of unique nodes.</param>
    /// <returns>Returns a random path. If the path is unable to reach the target length (e.g. it travels down a dead end and can't backtrack), it will return the path of smaller size that was found. The path will always contain at least one element of the starting node.</returns>
    IList<TNode> FindRandomPath(TNode start, Func<TNode, TNode, bool> passableTest, int targetSteps, bool canBacktrack);
}
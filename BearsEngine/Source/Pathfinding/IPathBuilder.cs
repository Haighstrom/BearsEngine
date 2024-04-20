namespace BearsEngine.Pathfinding;

/// <summary>
/// Builds paths after the completion of graph search methods
/// </summary>
/// <typeparam name="TNode">Type of Node</typeparam>
public interface IPathbuilder<TNode> where TNode : IPathfindNode<TNode>
{
    /// <summary>
    /// Builds the path determined by an IPathfinder
    /// </summary>
    /// <param name="start">the first node in the path</param>
    /// <param name="end">the last node in the path</param>
    /// <returns></returns>
    public IList<TNode> BuildPath(TNode start, TNode end);
}
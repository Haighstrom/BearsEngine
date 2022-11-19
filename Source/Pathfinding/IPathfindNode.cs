namespace BearsEngine.Pathfinding;

/// <summary>
/// A node which graph search algorithms can be built on
/// </summary>
/// <typeparam name="TNode"></typeparam>
public interface IPathfindNode<TNode> : INode<TNode> where TNode : IPathfindNode<TNode>
{
    /// <summary>
    /// The distance between two connected nodes
    /// </summary>
    public float DistToConnectedNode { get; } //todo - make static in .NET 7

    /// <summary>
    /// The preceding node in a path formed by the algorithm
    /// </summary>
    public TNode? ParentNode { get; set; }

    /// <summary>
    /// Additional data stored on the node by the algorithm
    /// </summary>
    public object? GraphSearchData { get; set; }
}
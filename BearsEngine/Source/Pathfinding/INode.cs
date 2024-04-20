namespace BearsEngine.Pathfinding;

/// <summary>
/// A basic node for building graphs
/// </summary>
/// <typeparam name="TNode"></typeparam>
public interface INode<TNode> : IEquatable<TNode> where TNode : INode<TNode>
{
    IList<TNode> ConnectedNodes { get; }
}
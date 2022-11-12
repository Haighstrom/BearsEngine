namespace BearsEngine.Pathfinding;

public interface IPathable<N>
    where N : INode<N>
{
    N CurrentNode { get; }
    bool CanPathThrough(N fromNode, N intoNode);
}
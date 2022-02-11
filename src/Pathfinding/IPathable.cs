namespace BearsEngine.Pathfinding;

public interface IPathable<N>
    where N : INode
{
    N CurrentNode { get; }
    bool CanPathThrough(N node);
}
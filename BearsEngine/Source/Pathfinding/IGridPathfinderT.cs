namespace BearsEngine.Pathfinding;

public interface IGridPathfinder<TNode> : IPathfinder<TNode> where TNode : IPathfindNode<TNode>, IPosition
{
    public TNode this[int x, int y] { get; }

    public bool IsValidGridPosition(float x, float y);

    public int Height { get; }

    public int Width { get; }

    public TNode GetClosestNode(float nodeX, float nodeY);

    public TNode GetClosestNode(IPosition p);
}
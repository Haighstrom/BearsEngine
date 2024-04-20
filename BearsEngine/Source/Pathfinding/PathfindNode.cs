namespace BearsEngine.Pathfinding;

public class PathfindNode : PathfindNodeBase, IPathfindNode<PathfindNode>, IPosition
{
    public PathfindNode(float x, float y, float distanceBetweenConnectedNodes)
        :base(x,y, distanceBetweenConnectedNodes)
    {
    }

    public IList<PathfindNode> ConnectedNodes { get; } = new List<PathfindNode>();

    public PathfindNode? ParentNode { get; set; }

    public bool Equals(PathfindNode? other) => Equals(other as IPosition);
}
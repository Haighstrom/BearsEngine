namespace BearsEngine.Pathfinding;

public class PathfindNode : IPathfindNode<PathfindNode>, IPosition
{
    public PathfindNode(float x, float y, float distanceBetweenConnectedNodes)
    {
        X = x;
        Y = y;
        DistanceBetweenConnectedNodes = distanceBetweenConnectedNodes;
    }

    public IList<PathfindNode> ConnectedNodes { get; } = new List<PathfindNode>();

    public virtual float DistToConnectedNode { get; } = 1; //todo: make abstract to force consideration?

    public object? GraphSearchData { get; set; }

    public PathfindNode? ParentNode { get; set; }

    public float X { get; }

    public float Y { get; }

    public float DistanceBetweenConnectedNodes { get; }

    public bool Equals(IPosition? other)
    {
        if (other is null)
            return false;

        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? other) => Equals(other as IPosition);

    public bool Equals(PathfindNode? other) => Equals(other as IPosition);
}
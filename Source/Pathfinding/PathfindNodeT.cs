namespace BearsEngine.Pathfinding;

public class PathfindNode<TEnum> : IPathfindNode<PathfindNode<TEnum>>, IPosition where TEnum : Enum
{
    public PathfindNode(float x, float y, TEnum passType)
    {
        X = x;
        Y = y;
        PassType = passType;
    }

    public IList<PathfindNode<TEnum>> ConnectedNodes { get; } = new List<PathfindNode<TEnum>>();

    public virtual float DistToConnectedNode { get; } = 1; //todo: make abstract to force consideration?

    public object? GraphSearchData { get; set; }

    public PathfindNode<TEnum>? ParentNode { get; set; }

    public TEnum PassType { get; set; }

    public float X { get; }

    public float Y { get; }

    public bool Equals(IPosition? other)
    {
        if (other is null)
            return false;

        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? other) => Equals(other as IPosition);

    public bool Equals(PathfindNode<TEnum>? other) => Equals(other as IPosition);

    public override string ToString()
    {
        return $"Node:({X},{Y})";
    }
}
namespace BearsEngine.Pathfinding;

/// <summary>
/// The simplest possible implementation of INode
/// </summary>
public class Node : INode
{
    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public float X { get; set; }

    public float Y { get; set; }

    public IList<INode> ConnectedNodes { get; set; } = new List<INode>();

    public INode? ParentNode { get; set; }

    public float AStarFValue { get; set; }

    public float AStarGValue { get; set; }

    public float AStarHValue { get; set; }

    public bool Equals(IPosition? other)
    {
        if (other is null)
            return false;

        return X == other.X && Y == other.Y;
    }

    public bool Equals(INode? other)
    {
        if (other is null)
            return false;

        return X == other.X && Y == other.Y;
    }
}

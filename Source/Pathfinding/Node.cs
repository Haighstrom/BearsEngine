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
    public List<INode> ConnectedNodes { get; set; } = new List<INode>();
    public INode ParentNode { get; set; }
    public double PF { get; set; }
    public double PG { get; set; }
    public double PH { get; set; }
}

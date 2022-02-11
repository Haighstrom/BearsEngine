namespace BearsEngine.Pathfinding;

public interface INode : IPosition
{
    List<INode> ConnectedNodes { get; }

    INode ParentNode { get; set; }
    double PF { get; set; }
    double PG { get; set; }
    double PH { get; set; }
}

public static class NodeExts
{
    public static bool Equals(this INode n1, INode n2) => n1.X == n2.X && n1.Y == n2.Y;
}
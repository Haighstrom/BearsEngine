namespace BearsEngine.Pathfinding;

public interface INode : IPosition, IEquatable<INode>
{
    IList<INode> ConnectedNodes { get; }

    INode? ParentNode { get; set; }

    float AStarFValue { get; set; }

    float AStarGValue { get; set; }

    float AStarHValue { get; set; }
}
namespace BearsEngine.Pathfinding;

/// <summary>
/// Node with a parameter to indicate the "pass type" - for an Pathable to determine if it can pass thorugh it
/// </summary>
public class Node<TEnum> : Node where TEnum : Enum
{
    public Node(int x, int y, TEnum passType)
        : base(x, y)
    {
        PassType = passType;
    }

    public TEnum PassType { get; set; }
}

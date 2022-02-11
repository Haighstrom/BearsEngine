namespace BearsEngine.Pathfinding
{
    /// <summary>
    /// Node with a parameter to indicate the "pass type" - for an Pathable to determine if it can pass thorugh it
    /// </summary>
    public class Node<P> : Node
    {
        public Node(int x, int y, P passType)
            : base(x, y)
        {
            PassType = passType;
        }

        public P PassType { get; set; }
    }
}
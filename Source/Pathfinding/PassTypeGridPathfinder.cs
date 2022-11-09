namespace BearsEngine.Pathfinding;

public class PassTypeGridPathfinder<TEnum> : IPathfinder<Node<TEnum>> where TEnum : Enum
{
    private static readonly Func<INode, INode, float> DefaultHeuristic = (n1, n2) => Math.Abs(n1.X - n2.X) + Math.Abs(n1.Y - n2.Y);

    private readonly Node<TEnum>[,] _nodegrid;

    public PassTypeGridPathfinder(int width, int height, Func<int,int,TEnum> getPassType)
        : this(width, height)
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                _nodegrid[i, j].PassType = getPassType(i, j);
            }
    }

    public PassTypeGridPathfinder(int width, int height)
    {
        _nodegrid = new Node<TEnum>[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                _nodegrid[i, j] = new Node<TEnum>(i, j, (TEnum)Convert.ChangeType(0, typeof(TEnum)));
            }

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (IsValidNodePosition(i - 1, j))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i - 1, j]);
                if (IsValidNodePosition(i + 1, j))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i + 1, j]);
                if (IsValidNodePosition(i, j - 1))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i, j - 1]);
                if (IsValidNodePosition(i, j + 1))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i, j + 1]);
            }
    }

    public Node<TEnum> this[int x, int y]
    {
        get => _nodegrid[x, y];
    }

    public int Height => _nodegrid.GetLength(1);

    public int Width => _nodegrid.GetLength(0);

    private bool IsValidNodePosition(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;

    public IList<Node<TEnum>>? GetAStarRoute(Node<TEnum> start, Node<TEnum> end, Func<Node<TEnum>, Node<TEnum>, bool> passableTest, Func<Node<TEnum>, Node<TEnum>, float> heuristic)
    {
        throw new NotImplementedException();
    }
    public IList<Node<TEnum>>? GetAStarRoute(Node<TEnum> start, Node<TEnum> end, Func<Node<TEnum>, Node<TEnum>, bool> passableTest)
    {
        if (!IsValidGridPosition(start.X, start.Y))
            throw new ArgumentOutOfRangeException(nameof(start));

        if (!IsValidGridPosition(end.X, end.Y))
            throw new ArgumentOutOfRangeException(nameof(end));

        if (start.Equals(end))
            return new List<Node<TEnum>>() { end };

        throw new NotImplementedException();
    }

    public IList<Node<TEnum>>? GetRandomRoute(Node<TEnum> start, int steps, Func<Node<TEnum>, Node<TEnum>, bool> passableTest)
    {
        throw new NotImplementedException();
    }

    public bool IsValidGridPosition(float x, float y)
    {
        if (((int)x) != x || ((int)y) != y)
            return false;

        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
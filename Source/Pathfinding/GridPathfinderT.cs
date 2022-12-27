namespace BearsEngine.Pathfinding;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TNode">The type of Node used in this grid</typeparam>
public class GridPathfinder<TNode> : Pathfinder<TNode> where TNode : IPathfindNode<TNode>, IPosition
{
    private readonly TNode[,] _nodegrid;

    public GridPathfinder(int width, int height, Func<int, int, TNode> createNode)
        : base()
    {
        _nodegrid = new TNode[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                _nodegrid[i, j] = createNode(i, j);
            }

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (IsValidGridPosition(i - 1, j))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i - 1, j]);
                if (IsValidGridPosition(i + 1, j))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i + 1, j]);
                if (IsValidGridPosition(i, j - 1))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i, j - 1]);
                if (IsValidGridPosition(i, j + 1))
                    _nodegrid[i, j].ConnectedNodes.Add(_nodegrid[i, j + 1]);
            }
    }

    public TNode this[int x, int y] => _nodegrid[x, y];

    public TNode FindClosestNode(IPosition p)
    {
        int x = (int)Maths.Clamp(p.X, 0, Width - 1);
        int y = (int)Maths.Clamp(p.Y, 0, Height - 1);
        return this[x, y];
    }

    public TNode FindClosestNode(float nodeX, float nodeY)
    {
        int x = (int)Maths.Clamp(nodeX, 0, Width - 1);
        int y = (int)Maths.Clamp(nodeY, 0, Height - 1);
        return this[x, y];
    }

    public int Height => _nodegrid.GetLength(1);

    public int Width => _nodegrid.GetLength(0);

    public bool IsValidGridPosition(float x, float y)
    {
        if (((int)x) != x || ((int)y) != y)
            return false;

        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
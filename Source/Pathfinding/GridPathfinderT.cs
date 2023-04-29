namespace BearsEngine.Pathfinding;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TNode">The type of Node used in this grid</typeparam>
public class GridPathfinder<TNode> : Pathfinder<TNode>, IGridPathfinder<TNode> where TNode : IPathfindNode<TNode>, IPosition
{
    public GridPathfinder(int width, int height, Func<int, int, TNode> createNode)
        : base()
    {
        Nodegrid = new TNode[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                Nodegrid[i, j] = createNode(i, j);
            }

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (IsValidGridPosition(i - 1, j))
                    Nodegrid[i, j].ConnectedNodes.Add(Nodegrid[i - 1, j]);
                if (IsValidGridPosition(i + 1, j))
                    Nodegrid[i, j].ConnectedNodes.Add(Nodegrid[i + 1, j]);
                if (IsValidGridPosition(i, j - 1))
                    Nodegrid[i, j].ConnectedNodes.Add(Nodegrid[i, j - 1]);
                if (IsValidGridPosition(i, j + 1))
                    Nodegrid[i, j].ConnectedNodes.Add(Nodegrid[i, j + 1]);
            }
    }

    protected TNode[,] Nodegrid { get; }

    public TNode this[int x, int y] => Nodegrid[x, y];

    public int Width => Nodegrid.GetLength(0);

    public int Height => Nodegrid.GetLength(1);

    public TNode GetClosestNode(float nodeX, float nodeY)
    {
        int x = (int)Maths.Clamp(nodeX, 0, Width - 1);
        int y = (int)Maths.Clamp(nodeY, 0, Height - 1);

        return this[x, y];
    }

    public TNode GetClosestNode(IPosition p) => GetClosestNode(p.X, p.Y);

    public bool IsValidGridPosition(float x, float y)
    {
        //check x,y are integers
        if (x % 1 != 0 || y % 1 != 0)
            return false;

        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
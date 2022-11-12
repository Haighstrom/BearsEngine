using System.Xml.Linq;

namespace BearsEngine.Pathfinding;

public class GridPathfinder<TPassEnum> : IPathfinder<PathfindNode<TPassEnum>> where TPassEnum : Enum
{
    private readonly PathfindNode<TPassEnum>[,] _nodegrid;

    public GridPathfinder(int width, int height, Func<int,int,TPassEnum> getPassType)
        : this(width, height)
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                _nodegrid[i, j].PassType = getPassType(i, j);
            }
    }

    public GridPathfinder(int width, int height)
    {
        _nodegrid = new PathfindNode<TPassEnum>[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                _nodegrid[i, j] = new PathfindNode<TPassEnum>(i, j, (TPassEnum)Convert.ChangeType(0, typeof(TPassEnum)));
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

    public PathfindNode<TPassEnum> this[int x, int y]
    {
        get => _nodegrid[x, y];
    }

    public int Height => _nodegrid.GetLength(1);

    public int Width => _nodegrid.GetLength(0);

    public IList<PathfindNode<TPassEnum>>? FindPath(PathfindNode<TPassEnum> start, PathfindNode<TPassEnum> end, Func<PathfindNode<TPassEnum>, PathfindNode<TPassEnum>, bool> passableTest)
    {
        if (!IsValidGridPosition(start.X, start.Y))
            throw new ArgumentOutOfRangeException(nameof(start));

        if (!IsValidGridPosition(end.X, end.Y))
            throw new ArgumentOutOfRangeException(nameof(end));

        if (start.Equals(end))
            return new List<PathfindNode<TPassEnum>>() { end };

        throw new NotImplementedException();
    }

    public IList<PathfindNode<TPassEnum>>? FindRandomPath(PathfindNode<TPassEnum> start, Func<PathfindNode<TPassEnum>, PathfindNode<TPassEnum>, bool> passableTest, int steps, bool canBacktrack)
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
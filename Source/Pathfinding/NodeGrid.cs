namespace BearsEngine.Pathfinding;

public class NodeGrid<N>
    where N : INode
{
    public NodeGrid(int width, int height)
    {
        Nodes = new N[width, height];
    }
    
    public N this[int x, int y]
    {
        get { return Nodes[x, y]; }
        set { Nodes[x, y] = value; }
    }

    public N this[Point p]
    {
        get { return Nodes[(int)p.X, (int)p.Y]; }
        set { Nodes[(int)p.X, (int)p.Y] = value; }
    }
    
    public int Width => Nodes.GetLength(0);
    public int Height => Nodes.GetLength(1);
    public N[,] Nodes { get; set; }
    
    public bool IsInBounds(Point p) => IsInBounds((int)p.X, (int)p.Y);
    public bool IsInBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    public void ResizeGrid(int w, int h)
    {
        N[,] newNodes = new N[w, h];

        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
            {
                if (i < Width && j < Height) newNodes[i, j] = Nodes[i, j];
                else newNodes[i, j] = default;
            }

        Nodes = newNodes;
    }
    
    
}

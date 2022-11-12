namespace BearsEngine.Pathfinding;

public class Pathfinder<N> : IPathfinder<N> where N : IPathfindNode<N>, IPosition
{
    protected static readonly Func<N, N, float> DefaultHeuristic = (n1, n2) => Math.Abs(n1.X - n2.X) + Math.Abs(n1.Y - n2.Y);

    public Func<N, N, float> Heuristic { get; set; } = DefaultHeuristic;

    public IList<N>? FindPath(N start, N end, Func<N, N, bool> passableTest)
    {
        AStarSolver<N> solver = new(start, end, passableTest, Heuristic);

        var result = solver.TrySolve();

        if (result)
            return solver.Path;
        else
            return null;
    }

    public IList<N> FindRandomPath(N startNode, Func<N,N, bool> passableTest, int maximumSteps, bool canBacktrack)
    {
        int stepsTaken = 0;

        List<N> openNodes = new() { startNode };
        List<N> path = new();

        while (stepsTaken <= maximumSteps)
        {
            N currentNode = openNodes[HF.Randomisation.Rand(openNodes.Count)];
            path.Add(currentNode);

            //replace open nodes with current node's connections, skipping any already in path
            openNodes.Clear();

            foreach (N n in currentNode.ConnectedNodes)
            {
                if (passableTest(currentNode, n) && !path.Contains(n))
                    openNodes.Insert(HF.Randomisation.Rand(0, openNodes.Count), n);
            }

            stepsTaken++;
            if (openNodes.Count == 0)
                break;
        }
        return path;
    }
}
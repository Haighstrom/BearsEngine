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

    public IList<N> FindRandomPath(N startNode, Func<N,N, bool> passableTest, int targetSteps, bool canBacktrack)
    {
        RandomPathSolver<N> solver = new(startNode, passableTest, targetSteps, canBacktrack);

        solver.TrySolve();

        return solver.Path;
    }
}
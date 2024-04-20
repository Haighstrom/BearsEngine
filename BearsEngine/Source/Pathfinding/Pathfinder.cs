namespace BearsEngine.Pathfinding;

public class Pathfinder<TNode> : IPathfinder<TNode> where TNode : IPathfindNode<TNode>, IPosition
{
    protected static readonly Func<TNode, TNode, float> DefaultHeuristic = (n1, n2) => Math.Abs(n1.X - n2.X) + Math.Abs(n1.Y - n2.Y);

    public Func<TNode, TNode, float> Heuristic { get; set; } = DefaultHeuristic;

    public IList<TNode>? FindPath(TNode start, TNode end, Func<TNode, TNode, bool> passableTest)
    {
        AStarSolver<TNode> solver = new(start, end, passableTest, Heuristic);

        var result = solver.TrySolve();

        if (result)
            return solver.Path;
        else
            return null;
    }

    public IList<TNode> FindRandomPath(TNode startNode, Func<TNode,TNode, bool> passableTest, int targetSteps, bool canBacktrack)
    {
        RandomPathSolver<TNode> solver = new(startNode, passableTest, targetSteps, canBacktrack);

        solver.TrySolve();

        return solver.Path;
    }
}
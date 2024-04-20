namespace BearsEngine.Pathfinding;

public interface IPathSolver<TNode> where TNode : IPathfindNode<TNode>
{
    /// <summary>
    /// The current Path determined by the Solver. Will return an empty list if the solver hasn't started, or failed to find a path.
    /// </summary>
    IList<TNode> Path { get; }

    /// <summary>
    /// Returns the nodes that have been evaluated, the nodes next to be evaluated, and the current status of the Solver after this step.
    /// </summary>
    (IList<TNode> ExploredNodes, IList<TNode> NodesToExplore, SolveStatus SolveStatus) State { get; }

    /// <summary>
    /// Take one step in the solving algorithm - used to build an estimate initial path, or for testing/visualising
    /// </summary>
    /// <returns>Returns the state of the solver after the step is taken.</returns>
    SolveStatus Step();

    /// <summary>
    /// Attempt to find a path
    /// </summary>
    /// <returns>Returns whether a path was successfully found</returns>
    bool TrySolve();
}
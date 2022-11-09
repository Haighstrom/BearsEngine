namespace BearsEngine.Pathfinding;

public interface INodePathSolver<N> where N : INode
{
    /// <summary>
    /// Current status of the solver
    /// </summary>
    PathSolveStatus Status { get; }

    /// <summary>
    /// Take one step in the solving algorithm - used to build an estimate initial path, or for testing/visualising
    /// </summary>
    /// <returns>Returns the current status of the Solver after this step.</returns>
    PathSolveStatus NextSolveStep();

    /// <summary>
    /// Attempt to find a path
    /// </summary>
    /// <returns>Returns the status of the Solver after completion - either Success or Failure.</returns>
    PathSolveStatus TrySolve();

    /// <summary>
    /// The current Path determined by the Solver. Will return an empty list if the solver failed to find a path.
    /// </summary>
    IList<N> Path { get; }

    /// <summary>
    /// The list of Nodes which have been evaluated. Mainly for use in testing with <see cref="NextSolveStep"/>.
    /// </summary>
    IList<N> ExploredNodes { get; }

    /// <summary>
    /// If a solve is currently in progress (e.g. through use of <see cref="NextSolveStep"/>, list of Nodes being considered to be next explored. Returns an empty list if the solve is not started or finished.
    /// </summary>
    IList<N> NodesToExplore { get; }
}
namespace BearsEngine.Pathfinding;

public class RandomPathSolver<N> : IPathSolver<N> where N : IPathfindNode<N>
{
    private readonly N _start;
    private readonly Func<N, N, bool> _passableTest;
    private readonly int _targetSteps;
    private readonly bool _canBacktrack;
    private N _currentNode;
    private SolveStatus _state = SolveStatus.NotStarted;
    private List<N> _openNodes = new(); //todo: priority queue
    private readonly Random _random = new();

    public RandomPathSolver(N start, Func<N, N, bool> passableTest, int targetSteps, bool canBacktrack)
    {
        _currentNode = _start = start;
        Path = new List<N>() { start };
        _passableTest = passableTest;
        _targetSteps = targetSteps;
        _canBacktrack = canBacktrack;

        IdentifyOpenNodes();
    }

    /// <summary>
    /// The current state of the solver
    /// </summary>
    public (IList<N> ExploredNodes, IList<N> NodesToExplore, SolveStatus SolveStatus) State => (Path, _openNodes, _state);

    public IList<N> Path { get; private set; }

    private void IdentifyOpenNodes()
    {
        _openNodes = _currentNode.ConnectedNodes.Where(n => _passableTest(_currentNode, n) && (!Path.Contains(n) || _canBacktrack)).ToList();
    }

    /// <summary>
    /// Take one step in the solving algorithm - used to build an estimate initial path, or for testing/visualising
    /// </summary>
    /// <returns>Returns the nodes that have been evaluated, the nodes next to be evaluated, and the current status of the Solver after this step.</returns>
    public SolveStatus Step()
    {
        if (_openNodes.Count == 0)
            return _state = SolveStatus.Failure;

        var nextNode = _openNodes[_random.Next(_openNodes.Count)];
        Path.Add(nextNode);
        _currentNode = nextNode;

        IdentifyOpenNodes();

        if (Path.Count - 1 == _targetSteps)
            return _state = SolveStatus.Success;
        else
            return _state = SolveStatus.InProgress;
    }

    /// <summary>
    /// Attempt to find a path
    /// </summary>
    /// <returns>Returns whether a path was successfully found</returns>
    public bool TrySolve()
    {
        do
            Step();
        while (_state == SolveStatus.InProgress);

        return _state == SolveStatus.Success;
    }
}
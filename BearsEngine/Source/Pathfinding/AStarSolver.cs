namespace BearsEngine.Pathfinding;

public class AStarSolver<N> : IPathSolver<N> where N : IPathfindNode<N>
{
    private record AStarData { public float F; public float G; }

    private readonly N _start;
    private readonly N _end;
    private readonly Func<N, N, bool> _passableTest;
    private readonly Func<N, N, float> _heuristic;
    private N _currentNode;
    private SolveStatus _state = SolveStatus.NotStarted;
    private readonly List<N> _openNodes = new(); //todo: priority queue
    private readonly List<N> _closedNodes = new();
    private readonly IPathbuilder<N> _pathbuilder = new Pathbuilder<N>();

    public AStarSolver(N start, N end, Func<N, N, bool> passableTest, Func<N, N, float> heuristic)
    {
        _currentNode = _start = start;
        _currentNode.GraphSearchData = new AStarData() { F = 0, G = 0 };
        _end = end;
        _passableTest = passableTest;
        _heuristic = heuristic;

        IdentifyOpenNodes();
    }

    /// <summary>
    /// The current state of the solver
    /// </summary>
    public (IList<N> ExploredNodes, IList<N> NodesToExplore, SolveStatus SolveStatus) State => (_closedNodes, _openNodes, _state);

    public IList<N> Path => _pathbuilder.BuildPath(_start, _currentNode);

    private void IdentifyOpenNodes()
    {
        foreach (N testNode in _currentNode.ConnectedNodes)
        {
            if (!_passableTest(_currentNode, testNode))
                continue;

            float g = ((AStarData)_currentNode.GraphSearchData!).G + _currentNode.DistanceBetweenConnectedNodes; //this node is one node further than the last one
            float h = _heuristic(testNode, _end); //and this estimates how much further from the goal
            float f = g + h; //total estimate for how close this node is to the end point

            if ((!_openNodes.Contains(testNode) && !_closedNodes.Contains(testNode)) || f < ((AStarData)testNode.GraphSearchData!).F) //if we found a better route to here, or we never looked at this node before
            {
                testNode.GraphSearchData = new AStarData() { F = f, G = g };
                testNode.ParentNode = _currentNode;
            }

            if (!_openNodes.Contains(testNode) && !_closedNodes.Contains(testNode))
                _openNodes.Add(testNode);
        }
    }

    /// <summary>
    /// Take one step in the solving algorithm - used to build an estimate initial path, or for testing/visualising
    /// </summary>
    /// <returns>Returns the nodes that have been evaluated, the nodes next to be evaluated, and the current status of the Solver after this step.</returns>
    public SolveStatus Step()
    {
        if (_start.Equals(_end))
            return _state = SolveStatus.Success;

        _closedNodes.Add(_currentNode);

        if (_openNodes.Count == 0)
        {
            return _state = SolveStatus.Failure;
        }

        _openNodes.Sort((n1, n2) => ((AStarData)n2.GraphSearchData!).F.CompareTo(((AStarData)n1.GraphSearchData!).F));
        _currentNode = _openNodes[^1];
        _openNodes.RemoveAt(_openNodes.Count - 1);

        IdentifyOpenNodes();

        if (_currentNode.Equals(_end))
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
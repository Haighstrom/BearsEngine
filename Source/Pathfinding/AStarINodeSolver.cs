namespace BearsEngine.Pathfinding;

public class AStarINodeSolver<N> : INodePathSolver<N> where N : INode
{
    private readonly N _start;
    private readonly N _target;
    private readonly Func<N, N, bool> _passableTest;
    private readonly Func<N, N, float> _heuristic;
    private N _currentNode;
    private readonly List<N> _openNodes = new(); //todo: priority queue
    private readonly List<N> _closedNodes = new();

    public AStarINodeSolver(N start, N target, Func<N, N, bool> passableTest, Func<N, N, float> heuristic)
    {
        _currentNode = _start = start;
        _target = target;
        _passableTest = passableTest;
        _heuristic = heuristic;

        ExploreNextNode();
    }

    public IList<N> ExploredNodes => _closedNodes;

    public IList<N> NodesToExplore => _openNodes;

    public IList<N> Path
    {
        get
        {
            N node = _currentNode;
            List<N> path = new() { node };

            while (!node.Equals(_start))
            {
                node = (N)node.ParentNode!;

                path.Insert(0, node);
            }

            return path;
        }
    }

    public PathSolveStatus Status { get; private set; } = PathSolveStatus.Incomplete;

    private void ExploreNextNode()
    {
        foreach (N testNode in _currentNode.ConnectedNodes.Select(v => (N)v))
        {
            if (!_passableTest(_currentNode, testNode))
                continue;

            float g = _currentNode.AStarGValue + 1; //this node is one further than the last one
            float h = _heuristic(testNode, _target); //and this estimates how much further from the goal
            float f = g + h; //total estimate for how close this node is to the end point

            if (f < testNode.AStarFValue || (!_openNodes.Contains(testNode) && !_closedNodes.Contains(testNode))) //if we found a better route to here, or we never looked at this node before
            {
                testNode.AStarGValue = g;
                testNode.AStarHValue = h;
                testNode.AStarFValue = f;
                testNode.ParentNode = _currentNode;
            }

            if (!_openNodes.Contains(testNode) && !_closedNodes.Contains(testNode))
                _openNodes.Add(testNode);
        }
    }

    public PathSolveStatus NextSolveStep()
    {
        _closedNodes.Add(_currentNode);

        if (_openNodes.Count == 0)
            return Status = PathSolveStatus.Failure;

        _openNodes.Sort((n1, n2) => n2.AStarFValue.CompareTo(n1.AStarFValue));
        _currentNode = _openNodes[^1];
        _openNodes.RemoveAt(_openNodes.Count - 1);

        ExploreNextNode();

        if (_currentNode.Equals(_target))
            return Status = PathSolveStatus.Success;
        else
            return PathSolveStatus.Incomplete;
    }

    public PathSolveStatus TrySolve()
    {
        while (Status == PathSolveStatus.Incomplete)
            NextSolveStep();

        return Status;
    }
}
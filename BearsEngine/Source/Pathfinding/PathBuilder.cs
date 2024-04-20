namespace BearsEngine.Pathfinding;

/// <summary>
/// Builds paths after the completion of graph search methods - assumes ParentNodes have been set backwards through the path
/// </summary>
/// <typeparam name="N">Type of Node</typeparam>
internal class Pathbuilder<N> : IPathbuilder<N> where N : IPathfindNode<N>
{
    /// <summary>
    /// Builds the path determined by an IPathfinder
    /// </summary>
    /// <param name="start">the first node in the path</param>
    /// <param name="end">the last node in the path</param>
    /// <returns></returns>
    public IList<N> BuildPath(N start, N finish)
    {
        N currentNode = finish;

        List<N> path = new() { currentNode };

        while (!currentNode.Equals(start))
        {
            currentNode = currentNode.ParentNode ?? throw new InvalidOperationException("Could not complete ");

            path.Insert(0, currentNode);
        }

        return path;
    }
}
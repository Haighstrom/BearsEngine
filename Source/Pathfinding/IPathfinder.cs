namespace BearsEngine.Pathfinding;

public interface IPathfinder<N> where N : IPosition
{
    IList<N>? GetAStarRoute(N start, N end, Func<N, N, bool> passableTest, Func<N, N, float> heuristic);
    IList<N>? GetAStarRoute(N start, N end, Func<N, N, bool> passableTest);
    IList<N>? GetRandomRoute(N start, int steps, Func<N, N, bool> passableTest);
}
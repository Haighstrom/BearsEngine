namespace BearsEngine.Pathfinding;

public class PathfindNodeBase : IPosition
{
    public PathfindNodeBase(float x, float y, float distanceBetweenConnectedNodes)
    {
        X = x;
        Y = y;
        DistanceBetweenConnectedNodes = distanceBetweenConnectedNodes;
    }

    public virtual float DistanceBetweenConnectedNodes { get; } = 1; //todo: make abstract to force consideration?

    public object? GraphSearchData { get; set; }

    public float X { get; }

    public float Y { get; }

    public bool Equals(IPosition? other)
    {
        if (other is null)
            return false;

        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? other) => Equals(other as IPosition);
}
using BearsEngine.Pathfinding;

namespace BearsEngine.Tasks;

public class TaskMoveOneLeashed<N> : Task
    where N : INode<N>, IPosition
{
    private readonly N _leashNode;
    private readonly float _leashDistance;
    private readonly IWaypointableAndPathable<N> _entity;

    public TaskMoveOneLeashed(IWaypointableAndPathable<N> entity, N leashNode, float leashDistance)
    {
        _entity = entity;
        _leashNode = leashNode;
        _leashDistance = leashDistance;

        CompletionConditions.Add(() => entity.WaypointController.ReachedDestination);
    }

    public override void Start()
    {
        base.Start();
        List<N> possibleNodes = new();
        foreach (var possibleNode in _entity.CurrentNode.ConnectedNodes)
        {
            if (Math.Max(Math.Abs(possibleNode.X - _leashNode.X), Math.Abs(possibleNode.Y - _leashNode.Y)) <= _leashDistance)
                possibleNodes.Add(possibleNode);
        }
        //todo: what if possible nodes is empty?
        _entity.WaypointController.SetWaypoints(HF.Randomisation.Choose(possibleNodes));
    }
}

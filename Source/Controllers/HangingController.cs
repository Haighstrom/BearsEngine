namespace BearsEngine.Worlds.Controllers;

public class HangingController : AddableBase, IUpdatable
{
    private Rect _target;
    private Rect _hangFrom;

    public HangingController(Rect target, Rect hangFrom)
    {
        _target = target;
        _hangFrom = hangFrom;

        UpdatePosition();
    }

    public bool Active { get; set; } = true;

    private void UpdatePosition()
    {
        _target.Y = _hangFrom.Bottom;
    }

    public virtual void Update(double elapsed) => UpdatePosition();
}

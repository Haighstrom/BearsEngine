namespace BearsEngine.Worlds.Controllers;

public class HangingController : AddableBase, IUpdatable
{
    private IRect _target;
    private IRect _hangFrom;

    public HangingController(IRect target, IRect hangFrom)
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

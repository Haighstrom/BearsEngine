using BearsEngine.Input;

namespace BearsEngine.Controllers;

public class FollowMouseController : AddableBase, IUpdatable
{
    private readonly IRectAddable _target;
    private readonly IMouse _mouse;

    public FollowMouseController(IMouse mouse, IRectAddable target, Point shift)
    {
        _mouse = mouse;
        _target = target;
        Shift = shift;
    }

    public FollowMouseController(IMouse mouse, IRectAddable target, int xShift, int yShift)
        : this(mouse, target, new Point(xShift, yShift))
    {
    }

    public bool Active { get; set; } = true;

    private Point Shift { get; set; }

    public virtual void Update(float elapsed)
    {
        if (_target.Parent is null)
            throw new NullReferenceException($"The Target ({_target}) of Follow Mouse Controller ({this}) is not added to anything, so its mouse position cannot be resolved.");

        _target.P = _mouse.ClientPosition + Shift;
    }
}

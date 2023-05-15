namespace BearsEngine.Controllers;

public class FollowMouseController : AddableBase, IUpdatable
{
    private readonly IRectAddable _target;

    public FollowMouseController(IRectAddable target, int xShift, int yShift)
        : this(target, new Point(xShift, yShift))
    {
    }

    public FollowMouseController(IRectAddable target, Point shift)
    {
        _target = target;
        Shift = shift;
    }

    public bool Active { get; set; } = true;

    private Point Shift { get; set; }

    public virtual void Update(float elapsed)
    {
        if (_target.Parent is null)
            throw new NullReferenceException($"The Target ({_target}) of Follow Mouse Controller ({this}) is not added to anything, so its mouse position cannot be resolved.");

        _target.P = (_target.Parent as IEntityContainer)!.LocalMousePosition + Shift;
    }
}
namespace BearsEngine.Worlds.Cameras;


public class CameraMoveWhenMouseAtEdgeController : UpdateableBase
{
    private readonly ICamera _parent;
    private readonly float _cameraMoveSpeed;
    private readonly int _windowEdgeDistance;

    public CameraMoveWhenMouseAtEdgeController(ICamera parent, float cameraMoveSpeed, int windowEdgeDistance)
    {
        _parent = parent;
        _cameraMoveSpeed = cameraMoveSpeed;
        _windowEdgeDistance = windowEdgeDistance;
    }

    public override void Update(float elapsed)
    {
        if (Mouse.ClientY < _windowEdgeDistance)
            _parent.View.Y = Maths.Max(_parent.MinY, _parent.View.Y - _cameraMoveSpeed * (float)elapsed);
        if (Mouse.ClientX > Window.ClientWidth - _windowEdgeDistance)
            _parent.View.X = Maths.Min(_parent.MaxX - _parent.View.W, _parent.View.X + _cameraMoveSpeed * (float)elapsed);
        if (Mouse.ClientY > Window.ClientHeight - _windowEdgeDistance)
            _parent.View.Y = Maths.Min(_parent.MaxY - _parent.View.H, _parent.View.Y + _cameraMoveSpeed * (float)elapsed);
        if (Mouse.ClientX < _windowEdgeDistance)
            _parent.View.X = Maths.Max(_parent.MinX, _parent.View.X - _cameraMoveSpeed * (float)elapsed);
    }
}

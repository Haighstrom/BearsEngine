namespace BearsEngine.Controllers;

public class YIndexedLayerController : AddableBase, IUpdatable
{
    private readonly float _baseY;
    private readonly Entity _target;

    public YIndexedLayerController(Entity target, float baseY)
    {
        _target = target;
        _baseY = baseY;

        _target.PositionChanged += OnTargetPositionChanged;
    }

    public bool Active { get; set; } = true;

    private void OnTargetPositionChanged(object? sender, PositionChangedArgs e) => _target.Layer = (int)(_baseY - e.NewR.Y);

    public void Update(float elapsed)
    {
    }
}

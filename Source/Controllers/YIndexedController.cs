namespace BearsEngine.Controllers;

/// <summary>
/// A controller for automatically updating Layer in an entity as its Y position updates
/// </summary>
public class YIndexedLayerController : AddableBase, IUpdatable
{
    private readonly float _baseY;
    private readonly Entity _target;

    /// <summary>
    /// Creates the controller
    /// </summary>
    /// <param name="target">The target to update the layer of</param>
    /// <param name="baseY">The default Layer. the target's Y position will be sutracted from this layer.</param>
    public YIndexedLayerController(Entity target, float baseY)
    {
        _target = target;
        _baseY = baseY;

        _target.PositionChanged += OnTargetPositionChanged;
    }

    /// <summary>
    /// Whether this object should be updated
    /// </summary>
    public bool Active { get; set; } = true;

    private void OnTargetPositionChanged(object? sender, PositionChangedEventArgs e) => _target.Layer = (int)(_baseY - e.NewR.Y);
    
    /// <summary>
    /// Update this object
    /// </summary>
    /// <param name="elapsed">How much time should pass for the object</param>
    public void Update(float elapsed)
    {
    }
}

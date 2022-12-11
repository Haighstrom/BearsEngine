namespace BearsEngine;

/// <summary>
/// An object which can be updated
/// </summary>
public interface IUpdatable
{
    /// <summary>
    /// Whether this object should be updated
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Update this object
    /// </summary>
    /// <param name="elapsed">How much time should pass for the object</param>
    void Update(float elapsed);
}
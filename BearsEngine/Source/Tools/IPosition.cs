namespace BearsEngine;

/// <summary>
/// Type that exposes positional X and Y coordinates
/// </summary>
public interface IPosition : IEquatable<IPosition>
{
    /// <summary>
    /// The x-coordinate.
    /// </summary>
    float X { get; }

    /// <summary>
    /// The y-coordinate.
    /// </summary>
    float Y { get; }
}

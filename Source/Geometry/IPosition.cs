namespace BearsEngine;

/// <summary>
/// Interface for Points and Nodes
/// </summary>
public interface IPosition : IEquatable<IPosition>
{
    float X { get; }
    float Y { get; }
}
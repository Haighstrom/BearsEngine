namespace BearsEngine;

public interface IUpdatable
{
    bool Active { get; set; }
    void Update(float elapsedTime);
}
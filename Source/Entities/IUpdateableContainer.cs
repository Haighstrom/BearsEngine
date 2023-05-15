namespace BearsEngine.Worlds;

public interface IUpdateableContainer : IContainer, IDisposable
{
    void Update(float elapsed);
}
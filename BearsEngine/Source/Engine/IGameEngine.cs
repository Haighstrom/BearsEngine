using BearsEngine.Window;

namespace BearsEngine;

public interface IGameEngine : IDisposable
{
    IScene Scene { get; set; }

    void Run(IScene firstScene);
}

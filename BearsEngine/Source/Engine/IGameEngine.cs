using BearsEngine.Window;

namespace BearsEngine;

internal interface IGameEngine : IDisposable
{
    IScene Scene { get; set; }

    void Run(IWindow window);
}

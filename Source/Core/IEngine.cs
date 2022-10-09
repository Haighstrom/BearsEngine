using BearsEngine.Window;

namespace BearsEngine;

internal interface IEngine : IDisposable
{
    IScene Scene { get; set; }

    IWindow Window { get; }

    void Run();
}
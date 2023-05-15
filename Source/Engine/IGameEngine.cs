namespace BearsEngine;

internal interface IGameEngine : IDisposable
{
    IScene Scene { get; set; }

    IUpdateableContainer UpdateContainer { get; }

    void Run();
}
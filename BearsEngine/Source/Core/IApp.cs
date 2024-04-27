using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.Window;

namespace BearsEngine;
public interface IApp
{
    IConsoleWindow Console { get; }
    IDisplayManager Displays { get; }
    ILogger Logger { get; }
    IMouse Mouse { get; }
    IWindow Window { get; }
    void Run(IScene firstScene);
    void ChangeScene(IScene scene);
}

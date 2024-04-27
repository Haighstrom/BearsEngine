using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.IO;
using BearsEngine.Window;

namespace BearsEngine.Source.Core;

internal interface IAppFactory
{
    IConsoleWindow CreateConsoleWindow(ConsoleSettings settings);

    ILogger CreateLogger(LogSettings settings);

    IIoHelper CreateIOHelper(IoSettings settings);

    IDisplayManager CreateDisplayManager();

    IWindow CreateWindow(WindowSettings settings);

    IGameEngine CreateGameEngine(IWindow window, IMouseInternal mouse, EngineSettings settings, Func<IScene> sceneFactory);
}

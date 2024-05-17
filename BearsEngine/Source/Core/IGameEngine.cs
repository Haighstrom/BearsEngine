using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.Window;

namespace BearsEngine;

public interface IGameEngine : IDisposable
{
    ILogger Logger { get; }

    IDisplayManager Displays { get; }

    IConsoleWindow Console { get; }

    IWindow Window { get; }

    IMouse Mouse { get; }

    IKeyboard Keyboard { get; }

    IScene Scene { get; }

    bool KeyboardUpdatesWhenWindowUnfocussed { get; set; }

    bool RunWhenUnfocussed { get; set; }

    int RenderFramesPerSecond { get; }

    int UpdateFramesPerSecond { get; }

    void Run(IScene firstScene);

    void ChangeScene(IScene scene);
}

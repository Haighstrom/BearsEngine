using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.Window;

namespace BearsEngine;

internal class GameEngineComponentFactory
{
    public ILogger CreateLogger(LogSettings settings)
    {
        return new Logger(settings);
    }

    public IConsoleWindow CreateConsole(ConsoleSettings settings)
    {
        return new ConsoleWindow(settings);
    }

    public IDisplayManager CreateDisplayManager(ILogger logger)
    {
        return new DisplayManager();
    }

    public IWindow CreateWindow(WindowSettings settings)
    {
        return new HaighWindow(settings);
    }

    public IInputReader CreateInputReader()
    {
        return new InputReader();
    }

    public IMouseInternal CreateMouse(IWindow window) 
    {
        return new Mouse(window);
    }

    public IKeyboardInternal CreateKeyboard()
    { 
        return new Keyboard();
    }

    public ISceneManager CreateSceneManager()
    {
        return new SceneManager();
    }
}

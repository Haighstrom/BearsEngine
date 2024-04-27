using BearsEngine.Console;
using BearsEngine.Displays;
using BearsEngine.Input;
using BearsEngine.Window;

namespace BearsEngine;

public interface IBearsApp
{
    IDisplayManager Displays { get; }

    IConsoleWindow Console { get; }

    IWindow Window { get; }

    IMouse Mouse { get; }

    IGameEngine Engine { get; }
}

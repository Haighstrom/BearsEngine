using BearsEngine.Logging;

namespace BearsEngine;

public interface IConsoleManager
{
    bool IsOpen { get; }

    Point SizeInCharacters { get; }

    void HideConsole();
    void ShowConsole();
    void ShowConsole(ConsoleSettings settings);
    void MoveConsoleTo(int x, int y, int w, int h);
}
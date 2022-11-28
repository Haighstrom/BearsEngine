namespace BearsEngine;

public interface IConsoleManager
{
    bool IsOpen { get; }

    int MaxHeight { get; }

    int MaxWidth { get; }

    void HideConsole();

    void MoveConsoleTo(int x, int y, int w, int h);

    void ShowConsole();

    void ShowConsole(int x, int y, int w, int h);
}
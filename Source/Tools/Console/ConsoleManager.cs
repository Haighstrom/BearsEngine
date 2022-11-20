using BearsEngine.Logging;
using BearsEngine.Win32API;

namespace BearsEngine;

public class ConsoleManager : IConsoleManager
{
    public bool IsOpen { get; private set; }

    public Point SizeInCharacters => throw new NotImplementedException();

    public void HideConsole()
    {
        Kernal32.FreeConsole();
        IsOpen = false;
    }

    public void MoveConsoleTo(int x, int y, int w, int h)
    {
        throw new NotImplementedException();
    }

    public void ShowConsole()
    {
        Kernal32.AllocConsole();
        IsOpen = true;
    }

    public void ShowConsole(ConsoleSettings settings)
    {
        ShowConsole();

        //todo: use settings
        HConsole.MoveConsoleTo(-7, 0, 450, HConsole.MaxHeight);
    }
}
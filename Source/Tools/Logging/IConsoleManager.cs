namespace BearsEngine.Logging;

public interface IConsoleManager
{
    bool IsOpen { get; }

    Point SizeInCharacters { get; }

    void Hide();
    void Show();
    void MoveConsoleTo(int x, int y, int w, int h);
}
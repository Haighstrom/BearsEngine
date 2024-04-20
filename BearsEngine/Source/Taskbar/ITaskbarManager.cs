namespace BearsEngine.Taskbar;

public interface ITaskbarManager
{
    bool AlwaysOnTop { get; }

    bool AutoHide { get; }

    int Height { get; }

    int Width { get; }

    TaskbarPosition Position { get; }

    int X { get; }

    int Y { get; }
}
